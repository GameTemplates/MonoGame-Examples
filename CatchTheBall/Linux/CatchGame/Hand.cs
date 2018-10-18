using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatchGame.Desktop
{
    public class Hand
    {
		private Vector2 _position;
		private Texture2D _texture;
		private int _velocity = 10;
		private Rectangle _collisionMask;

		public Hand(Vector2 newPos, Texture2D newTex)
        {
			_position = newPos;
			_texture = newTex;
        }

		public Rectangle CollisionMask
		{
			get { return _collisionMask; }
		}

		public void Update(GraphicsDeviceManager graphics)
		{

			//update collision mask position and size
			_collisionMask = new Rectangle((int)_position.X, (int)_position.Y + (_texture.Height/2), _texture.Width, _texture.Height);

            //move the hand
			KeyboardState kState = Keyboard.GetState();
			if(kState.IsKeyDown(Keys.Left))
			{
				if (_position.X > 0)
				{
					_position.X -= _velocity;
				}
			}
			if(kState.IsKeyDown(Keys.Right))
			{
				if (_position.X < graphics.GraphicsDevice.Viewport.Width - _texture.Width)
				{
					_position.X += _velocity;
				}

			}
            
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, Color.White);
		}
    }
}
