using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace play_animation_from_image_array.Desktop
{
    public class Player
    {
		private Vector2 _position;
		private Texture2D[] _animation;
		private int _currentAnimation = 1;
		private float _animationTimer = 0;
		
		public Player(Vector2 newPos, Texture2D[] newAnimation)
        {
			_position = newPos;
			_animation = newAnimation;
        }

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			//increas the animationTimer with deltaTime to control the speed of the animation
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			_animationTimer += deltaTime;

			//go trough each animation in the array at certain speed and draw it on the screen
			spriteBatch.Draw(_animation[_currentAnimation], _position, Color.White);

			if (_animationTimer >= 0.1f) //if timer is > then 0.1, increase the current animation by 1
			{
				_currentAnimation += 1;

				if (_currentAnimation > 9) //if current animation is > then the number of frames, reset current animation back to 1
				{
					_currentAnimation = 1;
				}
            
				_animationTimer = 0; //reset timer so we need to wait an other 0.1 seconds to change animation
			}
            

		}
    }
}
