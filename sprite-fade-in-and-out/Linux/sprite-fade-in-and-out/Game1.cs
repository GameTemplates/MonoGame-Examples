using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace sprite_fade_in_and_out.Desktop
{
  
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		SpriteFont font;
		Texture2D image;
		Color imageColor = Color.White; //we are going to manipulate this color that's why we sotre it in a variable so we have a reference
		bool imageFadeOut = true; //to control if the image is fade in or out
		float fadeTimer; //to control how fast the image is going to fade 

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

			//load the image
			image = Content.Load<Texture2D>("p1_front");

			//load the font
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



			//fade the image in and out by multiplaying the color value
			fadeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (fadeTimer > .1f)
			{
				if (imageFadeOut == true) //fade the image out
				{
					imageColor *= .8f;
					fadeTimer = 0;
					if (imageColor.A <= 2)
					{
						imageFadeOut = false;
					}
				}
				else //fade the image in
				{
					imageColor *= 2f;
					fadeTimer = 0;
					if (imageColor.A >= 255)
					{
						imageFadeOut = true;
					}
				}
			}
            

            base.Update(gameTime);
        }

 
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			//draw the image on the screen, note we are using the imageColor variable
			spriteBatch.Draw(image, new Vector2(350,300), imageColor);

			//draw message on the screen
			string text = "The image should fade in and out";
			spriteBatch.DrawString(font, text, new Vector2(10, 10), Color.Black);

			spriteBatch.End();
			           

            base.Draw(gameTime);
        }
    }
}
