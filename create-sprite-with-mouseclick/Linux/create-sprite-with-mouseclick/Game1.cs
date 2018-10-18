using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace create_sprite_with_mouseclick.Desktop
{
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
		Texture2D coinImage;
		SpriteFont font;

		bool mouseClicked = false; //this is to make sure we can create only 1 coin with each mouse click

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

			//load the coin image
			coinImage = Content.Load<Texture2D>("coinGold");

			//load the font
			font = Content.Load<SpriteFont>("Font");

            
        }

       
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

      
        protected override void Update(GameTime gameTime)
        {
			//exit the game if gamped back button or escape key is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
                
			//check the state of the mouse
            MouseState mState = Mouse.GetState();

            //if mouse button is pressed, create a coin in position of the click
            if (mState.LeftButton == ButtonState.Pressed && mouseClicked == false)
            {
                //calculate center point of the coin as we want to create the coin at the center
                var posX = mState.X - coinImage.Width / 2;
                var posY = mState.Y - coinImage.Height / 2;

                //create the coin and add it to the coins list
				Coin.coins.Add(new Coin(new Vector2(posX, posY), coinImage));

                //set _mouseclicked = true so we need to release the mouse and clikc it again, to create a new coin
                mouseClicked = true;
            }

			//delete coins with right mouse click
			if (mState.RightButton == ButtonState.Pressed && mouseClicked == false)
			{
				//check if mouse overlaping any of the coins
				for (var i = Coin.coins.Count; i --> 0;) //we are looping through the elements backward to delete the top ones first
				{
					//if the pointer is over the coin, set the destroy flag to true
					if(Coin.coins[i].CollisionMask.Contains(new Point(mState.X, mState.Y)))
					{
						Coin.coins[i].Destroy = true;

                        //break from the loop to make sure we flag only 1 coin at the time
						break;
					}
				}

				//delete all coins from the list flagged to be destroyed 
				Coin.coins.RemoveAll(c => c.Destroy == true);

				//set mouse clicked to false so we can destroy only a single coin / click
                mouseClicked = true;
                
			}

            //if mouse button is released, set mouseClikced to false so we can create an other coin if pressed
			if (mState.LeftButton == ButtonState.Released && mState.RightButton == ButtonState.Released)
                mouseClicked = false;

            base.Update(gameTime);
        }

     
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

            //go through all coins in the list, and call the draw method for each to draw them on the screen
			foreach(Coin c in Coin.coins)
			{
				c.Draw(spriteBatch);
			}

			//draw a message
			string text = "create a sprite with left mouse click, delete a sprite with rigth mouse click";
			spriteBatch.DrawString(font, text, new Vector2(10, 10), Color.White);

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
