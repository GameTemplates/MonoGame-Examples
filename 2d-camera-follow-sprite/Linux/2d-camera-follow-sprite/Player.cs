using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;

namespace _2d_camera_follow_sprite.Desktop
{
    public class Player
    {
		private Vector2 _position;
		private Texture2D _texture;
		private int _speed = 150;

		public Player(Vector2 newPos, Texture2D newTexture)
        {
			_position = newPos;
			_texture = newTexture;
        }

		public void Move(GameTime gameTime, Camera2D camera)
		{
			//make the camera follow the player
			camera.LookAt(_position);

			//calculate delta to move sprite at frame independent speed
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			//get kayboard state
			KeyboardState kState = Keyboard.GetState();

			//move sprite with the arrow keys
			if(kState.IsKeyDown(Keys.Left))
			{
				_position.X -= _speed * deltaTime;
			}
			if(kState.IsKeyDown(Keys.Right))

            {
                _position.X += _speed * deltaTime;
            }
			if(kState.IsKeyDown(Keys.Up))

            {
                _position.Y -= _speed * deltaTime;
            }
			if(kState.IsKeyDown(Keys.Down))

            {
                _position.Y += _speed * deltaTime;
            }

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, Color.White);
		}
    }
}
