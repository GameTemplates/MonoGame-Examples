using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace asteroids.Desktop
{
	public class Asteroid
	{
		private Vector2 _position;
		private Texture2D _texture;
		private bool _destroy = false;
		private int _speed = 100;
		private float _movementAngle;
		private float _angle = 0;
		private int _rotationSpeed = 2;
		private float _radius;
		private int _rotationDirection;
		private Vector2 _center;
		static private Random _rnd = new Random();
		static private GraphicsDeviceManager _graphics; //this is to get the screen width in the class
        
		static public List<Asteroid> largeAsteroids = new List<Asteroid>();
		static public List<Asteroid> mediumAsteroids = new List<Asteroid>();
		static public List<Asteroid> smallAsteroids = new List<Asteroid>();
		
		public Asteroid(Vector2 position, Texture2D texture)
        {
			_position = position;
			_texture = texture;
			//pick random angle of movement
			_movementAngle = _rnd.Next(0, 350);
			//calculate origin of the sprite as we want to rotate it at the center point
			_center = new Vector2(_texture.Width / 2, _texture.Height/2);
			//pick a random rotation direction
			_rotationDirection = _rnd.Next(0, 3);
			//calculate radius
			_radius = _texture.Width / 2; 
         

        }
        

        public bool Destroy
		{
			get { return _destroy; }
			set { _destroy = value; }
		}

        public float Radius
		{
			get { return _radius; }
		}
        

		public Vector2 Position
		{
			get { return _position; }
		}

		static public void Initialize(GraphicsDeviceManager graphics)
		{
			//get graphics device manager to be access in this class
			_graphics = graphics;
		}

		public void Update(GameTime gameTime)
		{
			
			//calculate delta time 
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            //move asteroid toward it movement angle
			var direction = new Vector2((float)Math.Cos(_movementAngle), (float)Math.Sin(_movementAngle));
            direction.Normalize();
			_position += direction * _speed * deltaTime;


			//rotate asteroid
			if(_rotationDirection == 0)
			{
				_angle -= _rotationSpeed * deltaTime;
			}
			else
			{
				_angle += _rotationSpeed * deltaTime;
			}

			//screen wrap - change asteroid position if outside the screen to come back on the other side
            //left to right
            if (_position.X < 0 - _texture.Width)
            {
                _position.X = _graphics.GraphicsDevice.Viewport.Width + _texture.Width;
            }
            //right to left
            if (_position.X > _graphics.GraphicsDevice.Viewport.Width + _texture.Width)
            {
                _position.X = 0 - _texture.Width;
            }
            //top to bottom
            if (_position.Y < 0 - _texture.Height)
            {
                _position.Y = _graphics.GraphicsDevice.Viewport.Height + _texture.Height;
            }
            //bottom to top
            if (_position.Y > _graphics.GraphicsDevice.Viewport.Height + _texture.Height)
            {
                _position.Y = 0 - _texture.Height;
            }
            
            
		}

		static public void Draw(SpriteBatch spriteBatch)
		{
			foreach(Asteroid a in largeAsteroids)
			{
				spriteBatch.Draw(a._texture, a._position, null , Color.White, a._angle, a._center, 1, SpriteEffects.None, 0);
			}
			foreach (Asteroid a in mediumAsteroids)
            {
				spriteBatch.Draw(a._texture, a._position, null, Color.White, a._angle, a._center, 1, SpriteEffects.None, 0);
            }
			foreach (Asteroid a in smallAsteroids)
            {
				spriteBatch.Draw(a._texture, a._position, null, Color.White, a._angle, a._center, 1, SpriteEffects.None, 0);
            }
		}
        
    }
    
}
