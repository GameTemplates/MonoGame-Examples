using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace rotate_sprite_with_input.Desktop
{
    public class Ship
    {
		private Vector2 _position;
		private Texture2D _texture;
		private float _angle = 0;
		private Vector2 _center;
		
		public Ship(Vector2 newPos, Texture2D newTexture)
        {
			_position = newPos;
			_texture = newTexture;
			//calculate center point of the sprite as we want to rotate around the center point
			_center.X = _texture.Width / 2;
			_center.Y = _texture.Height / 2;
        }

		public void Rotate(GameTime gameTime)
		{
			//get mouse state
			MouseState mState = Mouse.GetState();

            //set angle toward the position of the pointer
			_angle = (float)System.Math.Atan2(mState.Y - _position.Y, mState.X - _position.X);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			//draw the sprite with rotation
			spriteBatch.Draw(_texture, _position, null, Color.White, _angle, _center, 1, SpriteEffects.None, 0);
		}
        
    }
}
