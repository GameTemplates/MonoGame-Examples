using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace play_animation_from_image_array.Desktop
{
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		Player player;
		Texture2D[] playerImages;
		SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			//setup window resolution
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

			//load player images in to the array
			playerImages = new Texture2D[10];
			for (var i = 1; i <= 9; i++)
			{
				playerImages[i] = Content.Load<Texture2D>("Player/p1_walk" + i.ToString());
			}

			//load font
			font = Content.Load<SpriteFont>("Font");

			//create a player object
			player = new Player(new Vector2(350,300), playerImages);
        }

    
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

  
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

            //call the draw method for the player object to play the animation
			player.Draw(spriteBatch, gameTime);

			//draw message on the screen
			string text = "sprite animation from image array";
			spriteBatch.DrawString(font, text, new Vector2(10, 10), Color.White);

			spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
