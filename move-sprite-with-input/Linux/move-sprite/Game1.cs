using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace move_sprite.Desktop
{
  
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		Texture2D playerSprite;
		Player player;

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

			//load player image
			playerSprite = Content.Load<Texture2D>("p1_front");

			//create a player object
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
			//listen to the keyboard and gamepad for player one
            KeyboardState kState = Keyboard.GetState();
            GamePadState gState = GamePad.GetState(PlayerIndex.One);
			
			//exit the program if the escape key or the back/select button on the gamepad is pressed
			if (gState.IsButtonDown(Buttons.Back) || kState.IsKeyDown(Keys.Escape))
                Exit();
			
            //call update method of the player to move the player
			player.Update(kState, gState,gameTime);
            

            base.Update(gameTime);
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
			spriteBatch.Begin();

			//call draw method of player to draw player sprite on the screen
			player.Draw(spriteBatch);

			//draw a message on the screen
			string text = "use the arrow keys on the keyboard or the gamepad to move the sprite";
			spriteBatch.DrawString(font, text, new Vector2(10, 10), Color.White);

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
