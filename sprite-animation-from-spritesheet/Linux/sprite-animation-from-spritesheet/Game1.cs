using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace sprite_animation_from_spritesheet.Desktop
{
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		Player player;
		Texture2D spriteSheet;
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

			//load sprite sheet
			spriteSheet = Content.Load<Texture2D>("p1_walk");

			//load font
			font = Content.Load<SpriteFont>("Font");

			//create an instance of the player
			player = new Player(new Vector2(350, 300), spriteSheet);
        }

      
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

    
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			//call PlayAnimation method of the player to play the animation
			player.PlayAnimation(gameTime);

            base.Update(gameTime);
        }

   
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

            //call the Draw method of the player to draw the animation on the screen
			player.Draw(spriteBatch);

			//draw a message on the screen
			string text = "playing animation using a sprite sheet";
			spriteBatch.DrawString(font, text, new Vector2(10, 10), Color.White);

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
