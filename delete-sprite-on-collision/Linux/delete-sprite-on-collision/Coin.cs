using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace delete_sprite_on_collision.Desktop
{
	public class Coin
	{
		private Vector2 _position;
		private Texture2D _texture;
		private Rectangle _collisionMask;
		private bool _delete = false;
		public static List<Coin> coins = new List<Coin>();

		public Coin(Vector2 newPos, Texture2D newTexture)
		{
			_position = newPos;
			_texture = newTexture;
			//create a collision mask
			_collisionMask = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
		}

        public bool Delete
		{
			get { return _delete; }
		}

		public void Update(Rectangle playerCollisionMask)
		{
			//delete coins on collision with the player
			if(_collisionMask.Intersects(playerCollisionMask))
			{
				//mark coin to be deleted
				_delete = true;
			}

		}

		public void Draw(SpriteBatch spriteBatch)
		{

			spriteBatch.Draw(_texture, _position, Color.White);

		}
	}
}
