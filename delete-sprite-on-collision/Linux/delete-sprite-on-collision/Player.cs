using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace delete_sprite_on_collision.Desktop
{
    public class Player
    {
		private Vector2 _position;
		private Texture2D _texture;
		private Rectangle _collisionMask;
		private int _speed = 150;

		public Player(Vector2 newPos, Texture2D newTexture)
        {
			_position = newPos;
			_texture = newTexture;
            //create a collision mask for the player
			_collisionMask = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        }

		public Rectangle CollisionMask
		{
			get { return _collisionMask; }
		}

		public void Update(GameTime gameTime)
		{
			//update collision mask to follow the position of the player
			_collisionMask.X = (int)_position.X;
			_collisionMask.Y = (int)_position.Y;

			//get keyboard state
			KeyboardState kState = Keyboard.GetState();

			//get delta time to move the player
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //move the player with the arrow keys
			if(kState.IsKeyDown(Keys.Left))
			{
				_position.X -= _speed * deltaTime;
			}
			if (kState.IsKeyDown(Keys.Right))
            {
                _position.X += _speed * deltaTime;
            }
			if (kState.IsKeyDown(Keys.Up))
            {
                _position.Y -= _speed * deltaTime;
            }
			if (kState.IsKeyDown(Keys.Down))
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
