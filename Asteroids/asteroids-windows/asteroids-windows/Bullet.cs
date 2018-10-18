using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace asteroids.Desktop
{
	public class Bullet
	{
		private Vector2 _position;
		private Vector2 _origin;
		private Vector2 _direction;
		private float _angle;
		private bool _destroy = false;
		private int _speed = 400;
   
		private static Texture2D _texture;
		public static List<Bullet> bullets = new List<Bullet>();

		public Bullet(Vector2 newPos, float newAngle, Texture2D newTex)
		{
			_texture = newTex;
			_position = newPos;
			_angle = newAngle;
			_origin = new Vector2(_texture.Width / 2, _texture.Height);
		}

		public Vector2 Position
		{
			get { return _position; }
		}

        public float Angle
		{
			get { return _angle; }
		}

		public Vector2 Origin
		{
			get { return _origin; }
		}
        
        public bool Destroy
		{
			get { return _destroy; }
			set { _destroy = value; }
		}
        

		public void Update(GameTime gameTime, Rectangle viewPortRect)
		{
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			//move the bullets toward their own angle
			_direction = new Vector2((float)Math.Cos(_angle), (float)Math.Sin(_angle));
            _direction.Normalize();
			_position += _direction * _speed * deltaTime;
            

            //if bullet is not contained withing the vieport rectangle, mark it to be destroyed
			if(_destroy == false)
			{
				if(!viewPortRect.Contains(new Point((int)_position.X, (int)_position.Y)))
				{
					_destroy = true;
				}
				
			}
            

		}

		public static void Draw(SpriteBatch spriteBatch)
		{
			foreach (Bullet b in Bullet.bullets)
            {
				spriteBatch.Draw(_texture, b.Position, null, Color.White, b.Angle, b.Origin, 1, SpriteEffects.None, 0);
            }
		}
    }
}
