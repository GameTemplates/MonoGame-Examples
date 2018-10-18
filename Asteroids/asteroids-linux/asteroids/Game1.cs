using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media; //needed for song
using Microsoft.Xna.Framework.Audio; //needed for sound effect

namespace asteroids.Desktop
{
   
    public class Game1 : Game
    {
		
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		Random rnd = new Random();

		Color BackgroundColor;
		int WindowWidth;
		int WindowHeight;
		Texture2D SpritePlayer;
		Texture2D SpriteBullet;
		Texture2D particleImage;

		Player player;

		SoundEffect BulletSound;
		SoundEffect ExplosionSound;
		SoundEffect thruster;
		SoundEffect thrusterDown;

		int numberOfAsteroids = 10; //number of asteroids to spawn
		Texture2D[] spriteLargAsteroid = new Texture2D[3];
		Texture2D[] spriteMediumAsteroid = new Texture2D[3];
		Texture2D[] spriteSmallAsteroid = new Texture2D[3];
		Texture2D[] spritePlayerDebris = new Texture2D[3];

		SpriteFont font;

		Rectangle viewPortRect; //this is to know the size of the screen

		string gameState = "menu"; //this is to store the state of the game and draw and update content accordingly
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			IsMouseVisible = true;
			//BackgroundColor = new Color(26, 30, 38);
			BackgroundColor = Color.Black;
            WindowWidth = 1024;
            WindowHeight = 768;
            SoundEffect.MasterVolume = 0.1F;

			graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
        }

        protected override void Initialize()
        {
			//create a rectangle that is the same size as the screen
			viewPortRect = new Rectangle(0,0,graphics.GraphicsDevice.Viewport.Width,graphics.GraphicsDevice.Viewport.Height);

            //initialize asteroids
			Asteroid.Initialize(graphics);

			//initialize player debris
			PlayerDebris.Initialize(graphics);

            base.Initialize();
        }

        protected override void LoadContent()
        {
        

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

			//load font
			font = Content.Load<SpriteFont>("Font");
            
            //load player images
			SpritePlayer = Content.Load<Texture2D>("ship/ship_1");
			SpriteBullet = Content.Load<Texture2D>("ship/ship_bullet");
			for (var i = 0; i < spritePlayerDebris.Length; i++)
			{
				spritePlayerDebris[i] = Content.Load<Texture2D>("ship/ship_part_" + (i+1).ToString());
			}

			//load asteroid images
            //large
			spriteLargAsteroid[0] = Content.Load<Texture2D>("asteroids/larg_asteroid_1");
			spriteLargAsteroid[1] = Content.Load<Texture2D>("asteroids/larg_asteroid_2");
			spriteLargAsteroid[2] = Content.Load<Texture2D>("asteroids/larg_asteroid_3");
            //medium
			spriteMediumAsteroid[0] = Content.Load<Texture2D>("asteroids/medium_asteroid_1");
			spriteMediumAsteroid[1] = Content.Load<Texture2D>("asteroids/medium_asteroid_2");
			spriteMediumAsteroid[2] = Content.Load<Texture2D>("asteroids/medium_asteroid_3");
            //small
			spriteSmallAsteroid[0] = Content.Load<Texture2D>("asteroids/small_asteroid_1");
			spriteSmallAsteroid[1] = Content.Load<Texture2D>("asteroids/small_asteroid_2");
            spriteSmallAsteroid[2] = Content.Load<Texture2D>("asteroids/small_asteroid_3");

			//load particle image
			particleImage = Content.Load<Texture2D>("asteroids/debrish");

			//load sounds
			BulletSound = Content.Load<SoundEffect>("audio/Laser_Shoot");
			ExplosionSound = Content.Load<SoundEffect>("audio/Explosion");
			thruster = Content.Load<SoundEffect>("audio/thruster");
			thrusterDown = Content.Load<SoundEffect>("audio/thruster_down");

            //create player object
			player = new Player(SpritePlayer, new Vector2(WindowWidth / 2, WindowHeight / 2), 30f);
			player.Initialize(thruster, thrusterDown, BulletSound, SpriteBullet, particleImage, graphics);

			//create large asteroids
			CreateLargeAsteroids();
            
        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			if (gameState == "menu" )//update this part only if the game is at the menu
            {
				//if enter key is pressed, start the game
				KeyboardState kState = Keyboard.GetState();
				if(kState.IsKeyDown(Keys.Enter))
				{
					gameState = "game";
				}
            }

			if(gameState == "gameover" || gameState == "gamecomplete") //update this part ony if the game os over or complete
			{
				//if enter key is pressed, start the game
                KeyboardState kState = Keyboard.GetState();
                if (kState.IsKeyDown(Keys.Enter))
                {
                    gameState = "game";
					Player.life = 3; //reset number of player lifes

                    //delete all asteroids remain from previous game
					foreach(Asteroid a in Asteroid.largeAsteroids)
					{
						a.Destroy = true;
					}
					foreach (Asteroid a in Asteroid.mediumAsteroids)
                    {
                        a.Destroy = true;
                    }
					foreach (Asteroid a in Asteroid.smallAsteroids)
                    {
                        a.Destroy = true;
                    }

					//create new asteroids for the new game
					CreateLargeAsteroids();
                }
                
			}

			if(gameState == "game" || gameState == "gameover") //update this part is the game is running or game over
			{
				/******************
                    PLAYER DEBRIS
                *******************/
                PlayerDebris.Update(gameTime);
			}

			if (gameState == "game") //update all this if the game is running
			{
				/******************
                    PLAYER
                *******************/

				//update player
				player.Update(gameTime);


				//check collision between the player and asteroids, only of player is not destroyed
				if (player.Destroyed == false)
				{
					if (player.CanDestroy == true) //if player can be destroyed
					{
						foreach (Asteroid a in Asteroid.largeAsteroids)
						{
							if (Vector2.Distance(a.Position, player.Position) < a.Radius)
							{
								player.Destroyed = true;
								//create player parts in position of the player
								for (var i = 0; i < spritePlayerDebris.Length; i++)
								{
									PlayerDebris.debris.Add(new PlayerDebris(player.Position, spritePlayerDebris[i], player.Angle));
									ExplosionSound.Play();

								}

								player.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
								Player.life--;
								player.CanDestroy = false;

                                //if player life = 0 then set game to game over
								if(Player.life == 0)
								{
									SetGameOver(gameTime);
								}

							}
						}
						foreach (Asteroid a in Asteroid.mediumAsteroids)
                        {
                            if (Vector2.Distance(a.Position, player.Position) < a.Radius)
                            {
                                player.Destroyed = true;
                                //create player parts in position of the player
                                for (var i = 0; i < spritePlayerDebris.Length; i++)
                                {
                                    PlayerDebris.debris.Add(new PlayerDebris(player.Position, spritePlayerDebris[i], player.Angle));
                                    ExplosionSound.Play();

                                }

                                player.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
                                Player.life--;
                                player.CanDestroy = false;

                                //if player life = 0 then set game to game over
                                if (Player.life == 0)
                                {
									SetGameOver(gameTime);
                                }

                            }
                        }
						foreach (Asteroid a in Asteroid.smallAsteroids)
                        {
                            if (Vector2.Distance(a.Position, player.Position) < a.Radius)
                            {
                                player.Destroyed = true;
                                //create player parts in position of the player
                                for (var i = 0; i < spritePlayerDebris.Length; i++)
                                {
                                    PlayerDebris.debris.Add(new PlayerDebris(player.Position, spritePlayerDebris[i], player.Angle));
                                    ExplosionSound.Play();

                                }

                                player.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
                                Player.life--;
                                player.CanDestroy = false;

                                //if player life = 0 then set game to game over
                                if (Player.life == 0)
                                {
									SetGameOver(gameTime);
                                }

                            }
                        }
					}
				}
				else //if player is destroyed, check if player debris is present, if there is no more debris, set destroy to false
				{
					if (PlayerDebris.debris.Count == 0)
					{
						player.Destroyed = false;
					}
				}


				/***************
                    BULLET
                ****************/
				//update bullet
				foreach (Bullet b in Bullet.bullets)
				{
					b.Update(gameTime, viewPortRect);
				}

				//if bullet is in collision with asteroid, and the bullet is not set to be destroyed, play explosion sound and set bullet and asteroid to destroy and create smaller asteroids in position
				foreach (Bullet b in Bullet.bullets)
				{
					// check collision with large asteroid
					foreach (Asteroid a in Asteroid.largeAsteroids)
					{

						if (Vector2.Distance(b.Position, a.Position) < a.Radius && b.Destroy == false)
						{
							b.Destroy = true;
							a.Destroy = true;
							var randomPitch = (rnd.NextDouble() * 1.8) - 1;
							ExplosionSound.Play(1, (float)randomPitch, 1);

							//create 2 medium asteroids in position of the large asteroid
							for (var i = 0; i < 2; i++)
							{
								Asteroid.mediumAsteroids.Add(new Asteroid(a.Position, spriteMediumAsteroid[rnd.Next(0, 2)]));
							}

							//create some debris
							CreateDebris(20, a.Position);

						}

					}

					// check collision with medium asteroid
					foreach (Asteroid a in Asteroid.mediumAsteroids)
					{


						if (Vector2.Distance(b.Position, a.Position) < a.Radius && b.Destroy == false)
						{
							b.Destroy = true;
							a.Destroy = true;
							var randomPitch = (rnd.NextDouble() * 1.8) - 1;
							ExplosionSound.Play(1, (float)randomPitch, 1);

							//create 3 small asteroids in position of the medium asteroid
							for (var i = 0; i < 3; i++)
							{
								Asteroid.smallAsteroids.Add(new Asteroid(a.Position, spriteSmallAsteroid[rnd.Next(0, 2)]));
							}

							//create some debris
							CreateDebris(10, a.Position);
						}
					}

					// check collision with small asteroid
					foreach (Asteroid a in Asteroid.smallAsteroids)
					{
						if (Vector2.Distance(b.Position, a.Position) < a.Radius && b.Destroy == false)
						{
							b.Destroy = true;
							a.Destroy = true;
							var randomPitch = (rnd.NextDouble() * 1.8) - 1;
							ExplosionSound.Play(1, (float)randomPitch, 1);

							//create some debris
							CreateDebris(5, a.Position);
						}

					}

				}

				//remove bullets marked to be destroyed
				Bullet.bullets.RemoveAll(b => b.Destroy == true);

				//remove asteroids marked to be destroyed
				Asteroid.largeAsteroids.RemoveAll(a => a.Destroy == true);
				Asteroid.mediumAsteroids.RemoveAll(a => a.Destroy == true);
				Asteroid.smallAsteroids.RemoveAll(a => a.Destroy == true);

				//delete any particles if faded out
				Particle.particles.RemoveAll(p => p.Color.A == 0);


				/**************
                   PARTICLES
               ***************/
				//update particles (fade all particles out the same way)
				foreach (Particle p in Particle.particles)
				{
					p.Update(gameTime);
				}

				//update debris particles (move only debris particles in to random angle slowly)

				foreach (DebrisParticle d in Particle.particles)
				{
					d.Update();
				}
               


			}//end of if statement check if the game is running
            

            /******************
                ASTEROID
            *******************/
            //update asteroids
			foreach(Asteroid a in Asteroid.largeAsteroids)
			{
				a.Update(gameTime);
			}
			foreach (Asteroid a in Asteroid.mediumAsteroids)
            {
                a.Update(gameTime);
            }
			foreach (Asteroid a in Asteroid.smallAsteroids)
            {
                a.Update(gameTime);
            }

            //if all asterods destroyed, set game to complete
			if(Asteroid.largeAsteroids.Count == 0)
			{
				if(Asteroid.mediumAsteroids.Count == 0)
				{
					if(Asteroid.smallAsteroids.Count == 0)
					{
						gameState = "gamecomplete";
					}
				}
			}
            

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
			GraphicsDevice.Clear(BackgroundColor);

			spriteBatch.Begin();

			//draw the asteroids
            Asteroid.Draw(spriteBatch);

            if(gameState == "menu")
			{
				//draw text to press enter to start the game
				spriteBatch.DrawString(font,"Press ENTER to start the game", new Vector2(400,300), Color.White);

			}

			if(gameState == "game" || gameState == "gameover")
			{
				//draw player debris
                foreach (PlayerDebris p in PlayerDebris.debris)
                {
                    p.Draw(spriteBatch);
                }
			}

			if (gameState == "game")
			{
				//draw all bullets
				Bullet.Draw(spriteBatch);

				//draw the player
				Player.Draw(spriteBatch);

				//draw particles
				foreach (Particle p in Particle.particles)
				{
					p.Draw(spriteBatch);
				}

				//draw life of player on the screen
				spriteBatch.DrawString(font, "Life: " + Player.life.ToString(), new Vector2(10, 10), Color.White);
			}

            if(gameState == "gameover")
			{
				//draw text on the screen to ask the player to press enter to try again
				spriteBatch.DrawString(font, "Press ENTER to try again...", new Vector2(400,300), Color.White);
			}

			if (gameState == "gamecomplete")
            {
                //draw text on the screen to ask the player to press enter to try again
                spriteBatch.DrawString(font, "Well Done! Press ENTER to start again...", new Vector2(400, 300), Color.White);
            }

			spriteBatch.End();
            base.Draw(gameTime);
            
        }

      

        /******************
            OWN FUNCTIONS
        *******************/

		public void CreateDebris(int quantity, Vector2 position)
		{
			for (var i = 0; i < quantity; i++)
            {
                Particle.particles.Add(new DebrisParticle(new Vector2(position.X + rnd.Next(0, 10), position.Y + rnd.Next(0, 10)), particleImage, Color.White));
            }
		}

        public void CreateLargeAsteroids()
		{
			for (var i = 0; i < numberOfAsteroids; i++)
            {
                Asteroid.largeAsteroids.Add(new Asteroid(new Vector2(rnd.Next(100, WindowWidth), rnd.Next(10, WindowHeight)), spriteLargAsteroid[rnd.Next(0, 2)]));
            }
		}


		public void SetGameOver(GameTime gameTime)
		{
			player.Update(gameTime); //update the player to stop the engine sound and then change gamestate
            gameState = "gameover";
		}
    }
}
