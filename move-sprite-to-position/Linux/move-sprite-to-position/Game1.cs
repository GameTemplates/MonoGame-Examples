using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace move_sprite_to_position.Desktop
{
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		Player player;
		Texture2D playerSprite;
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

            base.Initialize();
        }


		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//load player sprite
			playerSprite = Content.Load<Texture2D>("p1_front");

			//create player object
			player = new Player(new Vector2(100, 100), playerSprite);

			//load font
			font = Content.Load<SpriteFont>("Font");
        }

       
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

       
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			player.Update(gameTime);

            base.Update(gameTime);
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			//call the draw method of the player to draw the player on the screen
			player.Draw(spriteBatch);

			//draw text to let you know what to do
			string text = "Click with left mouse button and the sprite should move to the position of the click";
			spriteBatch.DrawString(font, text, new Vector2(10, 10), Color.White);

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
