using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace move_sprite_to_position.Desktop
{
    public class Player
    {
		private Texture2D _texture;
		private Vector2 _position;
		private Vector2 _mouse;
		private int _speed = 150;
		private bool _mouseClicked = false; //it is to avoid the sprite being moved until a mouse button is clicked

		public Player(Vector2 newPos, Texture2D newTexture)
        {
			_texture = newTexture;
			_position = newPos;
        }
        
		public void Update(GameTime gameTime)
		{
			//get delta time to move the sprite independently from framerate
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			
			//get the state of the mouse
			MouseState mState = Mouse.GetState();
            
			//if left mouse button is pressed, store X and Y of mouse and set mouseclicked to true
			if (mState.LeftButton == ButtonState.Pressed)
            {
                _mouse.X = mState.X;
                _mouse.Y = mState.Y;
				_mouseClicked = true;

            }
            
			//move the sprite to the positio of the last mouse click
			if (_mouseClicked ==  true)
			{
				//calculate the center of the sprite as we want to move the center of the sprite to the position
				var posX = _position.X + _texture.Width / 2;
                var posY = _position.Y + _texture.Height / 2;

                //calculate direction of movement
				var rotation = Math.Atan2(_mouse.Y - posY, _mouse.X - posX);

				//move sprite to position
				if (Vector2.Distance(new Vector2(posX, posY), _mouse) > 2) //only if the distance is > 2 to avoid the sprite shaking when reach position
				{
					_position.X += (float)Math.Cos(rotation) * _speed * deltaTime;
					_position.Y += (float)Math.Sin(rotation) * _speed * deltaTime;
				}
				else 
				{ 
					_mouseClicked = false; //set mouse clicked to false so we no longer trigger this code and do calculations until the mouse is clicked again
				}
			}
		}
        
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, Color.White);
		}
    }
}
