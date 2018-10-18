
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using System;
namespace CatchGame.Desktop
{
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
		Texture2D ballSprite;
		Texture2D handSprite;
		Texture2D lifeSprite;
		SoundEffect bounceSound;
		SoundEffect glassSound;
		SpriteFont font;
        
		Ball[] balls;
		Hand hand;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = 1024;
			graphics.PreferredBackBufferHeight = 768;
			IsMouseVisible = true;
            
        }

    
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

      
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //load images
			handSprite = Content.Load<Texture2D>("hand");
			ballSprite = Content.Load<Texture2D>("basketball");
			lifeSprite = ballSprite;

			//load sounds
			bounceSound = Content.Load<SoundEffect>("swish");
			glassSound = Content.Load<SoundEffect>("crash");

			//create the hand
			hand = new Hand(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height - handSprite.Height), handSprite);
            
			//create balls
			Random rnd = new Random();
			balls = new Ball[Ball.Count];
			for (var i = 0; i < balls.Length;i++)
			{
				balls[i] = new Ball(new Vector2(rnd.Next(100,500),rnd.Next(0,100)), ballSprite, graphics, bounceSound, glassSound);
			}

			//load font
			font = Content.Load<SpriteFont>("font");
            
        }

     
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

      
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //update hand
			hand.Update(graphics);

            //update ball
			foreach(Ball ball in balls)
			{
				ball.Update(hand.CollisionMask, gameTime);
			}
            
            base.Update(gameTime);
        }

     
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
			spriteBatch.Begin();

            //draw the hand
			hand.Draw(spriteBatch);

            //draw the balls
			foreach (Ball ball in balls)
			{
				ball.Draw(spriteBatch);
			}

            //draw the score text at the top left corner
			spriteBatch.DrawString(font, "Score: " + Ball.Score.ToString(), new Vector2(20, 20), Color.Yellow, 0, new Vector2(0,0), 2, SpriteEffects.None,0);

			//draw balls to display number of life at the right edge of the screen
			int x = graphics.GraphicsDevice.Viewport.Width - ((lifeSprite.Width/2) * Ball.Life);
			for (var i = 1; i <= Ball.Life; i++)
            {
				spriteBatch.Draw(lifeSprite, new Vector2(x, 20),null, Color.White, 0, new Vector2(0,0), 0.5f, SpriteEffects.None, 0 );
				x += 35;
            }

            //if gameover, draw a message on the screen to press enter to start again
			if(Ball.GameOver == true)
			{
				string text = "Your Score is: " + Ball.Score.ToString() + " Press ENTER to start again...";
				var posX = graphics.GraphicsDevice.Viewport.Width / 2 - (text.Length * 5);
				var posY = graphics.GraphicsDevice.Viewport.Height / 2;
				spriteBatch.DrawString(font, text, new Vector2(posX, posY), Color.Red, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
			}

			spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
