
/*
    this example is using the MonoGame.Extended package to create a 2D camera
    you can easily install packages using NuGet
*/


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//using MonoGame.Extended for creating the 2D camera
using MonoGame.Extended;

namespace _2d_camera_follow_sprite.Desktop
{
 
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		Camera2D camera;
		Player player;
		Texture2D playerImage;
		SpriteFont font;
		Texture2D backgroundImage;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			//set window size
			graphics.PreferredBackBufferWidth = 800;
			graphics.PreferredBackBufferHeight = 600;

			//set mouse visible
			IsMouseVisible = true;
        }

       
        protected override void Initialize()
        {
			//create a new 2D camera
			camera =  new Camera2D(graphics.GraphicsDevice);

            base.Initialize();
        }


		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//load player image
			playerImage = Content.Load<Texture2D>("p1_front");

			//load font
			font = Content.Load<SpriteFont>("Font");

			//load background image
			backgroundImage = Content.Load<Texture2D>("castle-bg");

			//create an instance of player object
			player = new Player(new Vector2(100, 600), playerImage);
        }

  
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

    
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			//load the move method of player to move it
			player.Move(gameTime, camera);

            base.Update(gameTime);
        }

     
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			//this spritebatch is using the camera we have created to follow the player
            //so everything inside will be effected by the camera/position of player
			spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());

			//draw 3 instance of the background next to each other
			spriteBatch.Draw(backgroundImage, new Vector2(-backgroundImage.Width,0), Color.White);
			spriteBatch.Draw(backgroundImage, new Vector2(0, 0), Color.White);
			spriteBatch.Draw(backgroundImage, new Vector2(backgroundImage.Width, 0), Color.White);

			//call draw method of player to draw it on the screen, the camera will follow the player
			player.Draw(spriteBatch);
            
			spriteBatch.End();

            //start a new spritebatch that is NOT using the camera that follow the player 
			//so we can draw a message on the screen in a position that is not effected by the camera/position of player
			spriteBatch.Begin();
            
			//draw a message
            string text = "use the arrow keys to move the character, the camera will follow";
            spriteBatch.DrawString(font, text, new Vector2(10, 10), Color.Black);

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
