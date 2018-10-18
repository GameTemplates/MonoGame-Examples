using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace asteroids.Desktop
{
    public class Particle //all particels fade out slowly and then get deleted
    {
		protected Vector2 _position;
		protected Texture2D _texture;
		protected Color _color;
		protected float _timer;
		protected Vector2 _angle;
		protected Vector2 _movementDirection;
		protected float _particleLife;
		static protected float _deltaTime;
		static protected Random _rnd = new Random();
        

		public static List<Particle> particles = new List<Particle>();

		public Particle(Vector2 position, Texture2D texture, Color color)
        {
			//Random rnd = new Random();
			_position = position;
			_texture = texture;
			_color = color;
              
        }

		public Color Color
		{
			get { return _color; }
		}
        
		public void Update(GameTime gameTime)
		{
			//calculate delta time
			_deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
			//make the particles fade out
			foreach(Particle p in particles)
			{
				p._timer += 1 * _deltaTime;
				if(p._timer > _particleLife)
				{
					p._color *= .9f;
					p._timer = 0;
                     
				}
			}
           
		}
        
		public void Draw(SpriteBatch spriteBatch)
		{
			foreach(Particle p in particles)
			{
				spriteBatch.Draw(_texture, _position, _color);
			}
		}
    }

	public class DebrisParticle : Particle //debris particles are moving in to random direction
	{
		static private int _debrisSpeed = 20; //the speed the debris is moving
		
		public DebrisParticle(Vector2 position, Texture2D texture, Color color) : base(position, texture, color)
        {
			//pick a random angle
			_angle = new Vector2(_rnd.Next(0,350), _rnd.Next(0,350));

			//calculate movement direction using the random angle
            var direction = new Vector2((float)Math.Cos(_angle.X), (float)Math.Sin(_angle.Y));
            direction.Normalize();
            _movementDirection = direction;
			_particleLife = .5f;

        }

		public void Update()
		{
			//move debris toward angle
			_position += _movementDirection * _debrisSpeed * _deltaTime;
		}
        
	}


}

