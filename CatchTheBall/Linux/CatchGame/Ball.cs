using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace CatchGame.Desktop
{
    public class Ball
    {

		private Vector2 _position;
		private Texture2D _texture;
		private Rectangle _collisionMask;
		private int _velocityX = _velocity;
		private int _velocityY = _velocity;
		private GraphicsDeviceManager _graphics;
		private bool _triggerOnce = true;
		private SoundEffect _bounceSound;
		private SoundEffect _glassSound;

		private static float _ballSpeedTimer;
		private static int _score;
		private static bool _gameover = false;
		private static int _velocity = 5;
        private static int _initialLife = 3;
        private static int _count = 1; //number of balls in the game
        private static int _life = _initialLife * _count; //life is multiplied by the number of balls we have in-game

		public Ball(Vector2 newPos, Texture2D newTexture, GraphicsDeviceManager newGraphics, SoundEffect bounce, SoundEffect glass)
        {
			_position = newPos;
			_texture = newTexture;
			_graphics = newGraphics;
			_bounceSound = bounce;
			_glassSound = glass;
        }

        public static int Score
		{
			get { return _score; }
		}

        public static int Life
		{
			get { return _life; }
		}

        public static int Count
		{
			get { return _count; }
		}

        public static bool GameOver
		{
			get { return _gameover; }
		}

		public void Update(Rectangle handMask, GameTime gameTime)
		{
			//update the collision mask to match the position and size of the ball
			_collisionMask = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);

			//move the ball
			_position += new Vector2(_velocityX,_velocityY);

			if(_position.X > _graphics.GraphicsDevice.Viewport.Width - _texture.Width)
			{
				_velocityX *= -1;
			}
			if(_position.X < 0)
			{
				_velocityX *= -1;
			}
			if(_position.Y < 0 && _velocityY < 0)
			{
				_velocityY *= -1;
			}
			if(_position.Y > _graphics.GraphicsDevice.Viewport.Height)
			{
				_glassSound.Play();
				_position.Y = -_texture.Height;

				if (_life > 0)
				{
					_life--;
				}
				else //if life is = 0 then set velocity to 0 and gameover to true
				{
					_velocityY = 0;
					_velocityX = 0;
					_gameover = true;
				}
			}

            //check collision with the hand
			if(_collisionMask.Intersects(handMask) && _triggerOnce == true)
			{
				_velocityY *= -1;
				_bounceSound.Play();
				_score += 10;
				_triggerOnce = false;
			}
			if(!_collisionMask.Intersects(handMask))
			{
				_triggerOnce = true;
			}
            
            //if game over and enter key is pressed, start the game again
			if(_gameover == true)
			{
				KeyboardState kState = Keyboard.GetState();
				if(kState.IsKeyDown(Keys.Enter))
				{
					_life = _initialLife * _count; //reset life
					_velocityX = _velocity; //reset velocity
					_velocityY = _velocity;
					_score = 0; //reset score
					_gameover = false;
				}
			}

			//increase the speed of the ball after some time
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			_ballSpeedTimer += deltaTime;
			if(_ballSpeedTimer > 5)
			{
				if (_velocityX > 0) { _velocityX += 1; }
                if (_velocityX < 0) { _velocityX -= 1; }
                if (_velocityY > 0) { _velocityY += 1; }
                if (_velocityY < 0) { _velocityY -= 1; }
				_ballSpeedTimer = 0;
			}
   
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, Color.White);
		}
    }
}
