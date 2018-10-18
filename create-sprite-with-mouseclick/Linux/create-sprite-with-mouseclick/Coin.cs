using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace create_sprite_with_mouseclick.Desktop
{
    public class Coin
    {
		private Vector2 _position;
		private Texture2D _texture;
		private Rectangle _collisionMask;
		private bool _destroy = false;
		public static List<Coin> coins = new List<Coin>();

		public Coin(Vector2 newPos, Texture2D coinImage)
        {
			_position = newPos;
			_texture = coinImage;
			//create collision mask using a rectangle
			_collisionMask = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        }

		public Rectangle CollisionMask
		{
			get { return _collisionMask; }
		}

        public bool Destroy
		{
			get { return _destroy; }
			set { _destroy = value; }
		}
              
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, Color.White);
		}
    }
}
