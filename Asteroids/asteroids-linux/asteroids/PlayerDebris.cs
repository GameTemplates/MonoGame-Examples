using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace asteroids.Desktop
{
    public class PlayerDebris
    {
		private Vector2 _position;
		private Texture2D _texture;
		private float _angle;
		private Vector2 _origin;
		private bool _destroy = false;
		private int _speed = 300;
		private int _rotationSpeed = 5;
		private Vector2 _movementDirection;
		private static int _angleVariant = 360; //it is to make sure the pieces are going to fly in to different directions
		private static GraphicsDeviceManager _graphics;

		public static List<PlayerDebris> debris = new List<PlayerDebris>();

		public PlayerDebris(Vector2 position, Texture2D texture, float angle)
        {
			_texture = texture;
			_position = position;
			_angle = angle;
            _origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            //calculate movement direction, different for each part using _angleVariant         
            var _direction = new Vector2((float)Math.Cos(_angle - _angleVariant), (float)Math.Sin(_angle - _angleVariant));
            _direction.Normalize();
            _movementDirection = _direction;
            _angleVariant -= 90;

        }

		public static void Initialize(GraphicsDeviceManager graphics)
		{
			_graphics = graphics; //get the graphics device manager
		}

		public static void Update(GameTime gameTime)
		{
			//get delta time
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
			foreach(PlayerDebris p in PlayerDebris.debris)
			{
                //move and rotate the pieces
				p._position += p._movementDirection * p._speed * deltaTime;
				p._angle += p._rotationSpeed * deltaTime;

                //if the pieces are outside the screen, mark them to be deleted
                if (p._position.X < 0 - p._texture.Width)
                {
					p._destroy = true;
                }
                //right to left
                if (p._position.X > _graphics.GraphicsDevice.Viewport.Width + p._texture.Width)
                {
					p._destroy = true;
                }
                //top to bottom
                if (p._position.Y < 0 - p._texture.Height)
                {
					p._destroy = true;
                }
                //bottom to top
                if (p._position.Y > _graphics.GraphicsDevice.Viewport.Height + p._texture.Height)
                {
					p._destroy = true;
                }

			}

			//any pieces are marked to be deleted, delete
			PlayerDebris.debris.RemoveAll(d => d._destroy == true);

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, null, Color.White, _angle, _origin, 1, SpriteEffects.None, 0);
		}
    }
}
