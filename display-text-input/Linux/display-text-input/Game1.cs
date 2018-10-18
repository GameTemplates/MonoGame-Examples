using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace display_text_input.Desktop
{

	public class Game1 : Game
	{
		
        GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Keys pressedKey;
		char character;      
		SpriteFont font;
		string text = "";

        
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
			
            //initialize text input handler
			Window.TextInput += TextInputHandler;

            base.Initialize();
        }

		private void TextInputHandler(object sender, TextInputEventArgs args)
		{
			//capture all charaters and keys from input
			pressedKey = args.Key;
			character = args.Character;

            //if the pressed key is not the Back key, add character to text
			if (pressedKey.ToString() != "Back")
            {
                text += character; 
            }
            else //if the pressed key is the Back key, remove last character from text
            {
                if (text.Length > 0) //only if there is any character to remove
                {
                    text = text.Remove(text.Length - 1);
                }
            }

            //if the pressed key is Enter, start a new line
            if (pressedKey.ToString() == "Enter")
            {
                text = text + "\n";
            }



		}

		protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

			//load font
			font = Content.Load<SpriteFont>("Font");
        }

       
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

     
        protected override void Update(GameTime gameTime)
        {
			//if escape key is pressed, quit the program
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

    
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

			spriteBatch.Begin();
         
			//draw message on the screen
			spriteBatch.DrawString(font, "Start typing:", new Vector2(10, 10), Color.Black);

			//draw text on the screen
			try
			{
				spriteBatch.DrawString(font, text, new Vector2(10, 30), Color.Black);
			}
			catch (ArgumentException)
            {
				//special characters like ÉÁŰ cause the program to crash as the spritefont does not support the characters, you need to edit the spritefont file to add support for the charcaters you need
                //remove the character from text in case we get this error to avoid crash
				text = text.Remove(text.Length - 1);
            }
			
            
			spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
