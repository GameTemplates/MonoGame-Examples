using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace infinite_scrolling_background.Desktop
{
  
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
		Texture2D backgroundImage;
		Background[] background;
		SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			//set window size
			graphics.PreferredBackBufferWidth = 1024;
			graphics.PreferredBackBufferHeight = 800;

			//set mouse visible
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

			//load the font
			font = Content.Load<SpriteFont>("Font");

			//load background image
			backgroundImage = Content.Load<Texture2D>("castle-bg");

			//create two instances of the background they are going to swap position to make the infinite scrolling effect
			background = new Background[2];
			background[0] = new Background(new Vector2(0,0), backgroundImage);
			background[1] = new Background(new Vector2(backgroundImage.Width,0), backgroundImage);
        }

     
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //call the move method for each background
			foreach(Background bg in background)
			{
				bg.Move(gameTime);
			}

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

            //draw each instance of the background on the screen
			foreach(Background bg in background)
			{
				bg.Draw(spriteBatch);
			}

			//draw message on the screen
			string text = "infinite scrolling background";
			spriteBatch.DrawString(font, text, new Vector2(10,10), Color.Black);

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
