using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media; //needed for song
using Microsoft.Xna.Framework.Audio; //needed for sound effect

namespace asteroids.Desktop
{
    public class Player
    {
		
		private static Vector2 _position;
		private static float _angle;
		private static Vector2 _direction;
		private static Vector2 _origin;
		private static int _rotationSPeed = 2;
        private static Vector2 _movementDirection;
		private static bool _destroyed = false;
		private static bool _canDestroy = false;
		private static float _destroyTimer;
		private static int _destroyTime = 3; // for 3 seconds the player can not be destroyed

		private SoundEffectInstance _thrusterSound;
		private SoundEffectInstance _thrusterDownSound;
		private Texture2D _thrusterTexture;

		private static Texture2D _texture;
        private static Texture2D _bulletTexture;
		private static SoundEffect _bulletSound;
		private static int _initialSpeed = 50;
		private static int _speed = _initialSpeed;
		private static int _acceleration = _speed * 2;
		private static int _triggerOnce = 1;
		private static GraphicsDeviceManager _graphics;

		public static int life = 3;
        

		public Player(Texture2D newTexture, Vector2 newPos, float newAngle)
        {
			_position = newPos;
			_angle = newAngle;
			_texture = newTexture;
        }

        public bool CanDestroy
		{
			get { return _canDestroy; }
			set { _canDestroy = value; }
		}

        public bool Destroyed
		{
			get { return _destroyed; }
			set { _destroyed = value; }
		}

		public Vector2 Position 
		{ 
			get { return _position; } 
			set { _position = value; } 
		}

        public float Angle
		{
			get { return _angle; }
		}

		public static Vector2 Origin
		{
			get{ return _origin = new Vector2(_texture.Width / 2, _texture.Height / 2); }
		}

		public void Initialize(SoundEffect thruster, SoundEffect thrusterDown, SoundEffect bulletsound, Texture2D bulletTexture, Texture2D thrusterTexture, GraphicsDeviceManager graphics)
		{
			//at the beginning, set a random movement direction for the ship so it start moving in to a random direction
			Random random = new Random();
			_direction = new Vector2((float)Math.Cos(random.Next(0,300)), (float)Math.Sin(random.Next(0,300)));
            _direction.Normalize();
            _movementDirection = _direction;
            
            //initialize sound effects
			_thrusterSound = thruster.CreateInstance();
			_thrusterDownSound = thrusterDown.CreateInstance();
			_bulletSound = bulletsound;

			//get bullet texture to be able to create them in this class
			_bulletTexture = bulletTexture;

			//get the thruster texture to create the thruster particles
			_thrusterTexture = thrusterTexture;

			//get the graphics device manager to be able to get the width and heiht of the screen
			_graphics = graphics;
		}

		public void Update(GameTime gameTime)
		{

			//only if player is not destroyed
			if (_destroyed == false)
			{

                //get delta time
				float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                //set player can destroy after a certain amount of time to make sure the don't destroy the player immediately at the beginning
				if(_canDestroy == false)
				{
					_destroyTimer += deltaTime;
					if(_destroyTimer > _destroyTime)
					{
						_canDestroy = true;
						_destroyTimer = 0;
					}
				}
            
				//get keyboard state
				KeyboardState kState = Keyboard.GetState();

				//controls
				if (kState.IsKeyDown(Keys.D)) //rotate right
				{
					_angle += _rotationSPeed * deltaTime;
				}
				if (kState.IsKeyDown(Keys.A)) //rotate left
				{
					_angle -= _rotationSPeed * deltaTime;
				}
				if (kState.IsKeyDown(Keys.W)) //if up key is pressed set movement direction to be the angle of the ship and move the ship at accelerated speed
				{
					_direction = new Vector2((float)Math.Cos(_angle), (float)Math.Sin(_angle));
					_direction.Normalize();
					_movementDirection = _direction;
					_position += _movementDirection * _acceleration * deltaTime;

					//play the thruster sound
					if (_thrusterSound.State == SoundState.Stopped)
					{
						_thrusterDownSound.Stop();
						_thrusterSound.Play();
					}
                   
				}
				else
				{
					//if the forward key is not pressed, play the thruster down sound only once
					if (_thrusterSound.State == SoundState.Playing && _thrusterDownSound.State == SoundState.Stopped)
					{
						_thrusterSound.Stop();
						_thrusterDownSound.Play();
					}
				}
				if (kState.IsKeyUp(Keys.W)) //constantly move the ship toward the movement direction at normal speed if the up key is not pressed
				{
					_position += _movementDirection * _speed * deltaTime;
				}

				//screen wrap - change ship position if outside the screen to come back on the other side
				//left to right
				if (_position.X < 0 - _texture.Width)
				{
					_position.X = _graphics.GraphicsDevice.Viewport.Width + _texture.Width;
				}
				//right to left
				if (_position.X > _graphics.GraphicsDevice.Viewport.Width + _texture.Width)
				{
					_position.X = 0 - _texture.Width;
				}
				//top to bottom
				if (_position.Y < 0 - _texture.Height)
				{
					_position.Y = _graphics.GraphicsDevice.Viewport.Height + _texture.Height;
				}
				//bottom to top
				if (_position.Y > _graphics.GraphicsDevice.Viewport.Height + _texture.Height)
				{
					_position.Y = 0 - _texture.Height;
				}


				//shoot bullets when space key is pressed only once
				if (kState.IsKeyDown(Keys.Space) && _triggerOnce == 1)
				{
					Bullet.bullets.Add(new Bullet(_position, _angle, _bulletTexture));
					_bulletSound.Play();
					_triggerOnce = 0;

				}

				if (kState.IsKeyUp(Keys.Space))
				{
					_triggerOnce = 1;
				}

			}//end if if statement to check if player is destroyed
			else
			{
				//stop the engine sound if the ship is destroyed
				_thrusterSound.Stop();
			}
            

		}

		public static void Draw(SpriteBatch spriteBatch)
		{
			if(_destroyed == false)
			    spriteBatch.Draw(_texture, _position, null, Color.White, _angle, Origin, 1, SpriteEffects.None, 0);
		}
    }
    
}
