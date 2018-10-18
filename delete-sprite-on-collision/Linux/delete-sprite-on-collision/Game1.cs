using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace delete_sprite_on_collision.Desktop
{
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
		Player player;
		Texture2D playerImage;
		Texture2D coinImage;
		SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			//set window size
			graphics.PreferredBackBufferWidth = 800;
			graphics.PreferredBackBufferHeight = 600;

			//make mouse visible
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
			playerImage = Content.Load<Texture2D>("p1_front");
			//load coing image
			coinImage = Content.Load<Texture2D>("coinGold");
			//load font
			font = Content.Load<SpriteFont>("Font");

			//create an instance of the player object
			player = new Player(new Vector2(400, 300), playerImage);

			//create some coins in reandom positions on the screen
			Random rnd = new Random();
			for (var i = 0; i <= 10; i++)
			{
				Coin.coins.Add(new Coin(new Vector2(rnd.Next(10,700), rnd.Next(10,500)), coinImage));
			}
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

			//call the update method of the player to move it
			player.Update(gameTime);

            //call Update method for each coin to check collision
			foreach(Coin c in Coin.coins)
			{
				c.Update(player.CollisionMask);
			}
			//remove all coin from list marked to be deleted
			Coin.coins.RemoveAll(c => c.Delete == true);

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			//call the draw method of the player to display it
			player.Draw(spriteBatch);

			//call draw methid of coins to draw all coind on the screen
			foreach(Coin c in Coin.coins)
			{
				c.Draw(spriteBatch);
			}

			//draw message
			string text = "use the arrow keys to move the character, all coins should be deleted on collision";
			spriteBatch.DrawString(font, text, new Vector2(10, 10), Color.White);

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
