
/*
    this example is using MonoGame.Extended packages to load Tiled maps, you can install packages easily using NuGet
    after the packages installed, you need to add a reference to the MonoGame.Extended.Content.Pipeline assembly 
    in the Content manager in order to compile tiled maps in to xnb
    Click Content in the manager and in the properties select References, click Add then find the assembly in the Packages folder
*/


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//using MonoGame.Extended
using MonoGame.Extended.Tiled; //this is to load the tiled map
using MonoGame.Extended.Tiled.Graphics; //this is to draw the tiled map

namespace load_tiled_map.Desktop
{
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		TiledMap tiledMap; //to store our map data
		TiledMapRenderer tiledMapRenderer; //to store renderer to draw the map on the screen

		Texture2D playerImage;
		Vector2 playerPosition;

		SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			//set window size
			graphics.PreferredBackBufferWidth = 1024;
			graphics.PreferredBackBufferHeight = 768;

			//set mouse visible
			IsMouseVisible = true;
            
        }

     
        protected override void Initialize()
        {
			//create a new tiled map rendered
			tiledMapRenderer = new TiledMapRenderer(graphics.GraphicsDevice);

            base.Initialize();
        }

    
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

			//load font
			font = Content.Load<SpriteFont>("Font");

			//load the tiled map
			tiledMap = Content.Load<TiledMap>("map1");

			//load the player image
			playerImage = Content.Load<Texture2D>("p1_front");

			//our tiled map comes with an object layer and we have the player position selected on the layer
            //get the object layer of the tiled map called "objects" 
            TiledMapObject[] tiledObjects = tiledMap.GetLayer<TiledMapObjectLayer>("objects").Objects;
            //go through each object on the objects layer
			foreach(TiledMapObject objects in tiledObjects)
			{
				//find the object called "player" and get the position, we are going to use this to draw player image in that position
				if(objects.Name == "player" )
				{
					playerPosition = objects.Position;
				}
			}
           
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

            //draw the tiles and plants layer of the tiled map on the screen
			tiledMapRenderer.Draw(tiledMap.GetLayer("tiles"));
			tiledMapRenderer.Draw(tiledMap.GetLayer("plants"));
            
            //draw the player image in position of the player object of the tiled map
			spriteBatch.Draw(playerImage, playerPosition, Color.White);
            
            //draw the trees layer of the tiled map after the player so the player can move behind the trees for example
			tiledMapRenderer.Draw(tiledMap.GetLayer("trees"));

			//draw message
			string text = "a map made in Tiled with object layer to set position of player image";
			spriteBatch.DrawString(font, text, new Vector2(10,10), Color.White);

			spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
