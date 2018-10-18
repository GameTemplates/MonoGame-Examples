using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace infinite_scrolling_background.Desktop
{
    public class Background
    {
		private Vector2 _position;
		private Texture2D _texture;
		private int _speed = 100;
		
		public Background(Vector2 newPos, Texture2D newTexture)
        {
			_position = newPos;
			_texture = newTexture;
        }

		public void Move(GameTime gameTime)
		{
			//calculate delta time so we can move the backround independently from frame rate
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			//move move background to the left, floor the value to get a whole number and avoid little gaps between the instances
			_position.X -= (float)Math.Floor(_speed * deltaTime);

            //if the position of the background on the X is less than the width of the image, change it position to create the infinite scrolling effect
			if(_position.X <= -_texture.Width) 
			{
				_position.X = _texture.Width;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, Color.White);
		}
    }
}
