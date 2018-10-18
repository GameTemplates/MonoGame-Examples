using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace rotate_sprite_with_input.Desktop
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		Ship ship;
		Texture2D shipImage;
		SpriteFont font;

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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

     
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

			//load ship image
			shipImage = Content.Load<Texture2D>("orange-ship");

			//load font
			font = Content.Load<SpriteFont>("Font");

			//create a ship object
			ship = new Ship(new Vector2(400,300), shipImage);
        }

      
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

       
        protected override void Update(GameTime gameTime)
        {
			//if escape key is pressed, quit the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			//call the rotate method of the ship
			ship.Rotate(gameTime);

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			//call the draw method of the ship to display it with rotation applied
			ship.Draw(spriteBatch);

			//draw message
			string text = "use the left and right arrow keys to rotate, up key to move forward";
			spriteBatch.DrawString(font, text, new Vector2(10,10), Color.White);

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
