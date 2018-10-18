using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace move_sprite.Desktop
{
	public class Player
	{
		private Texture2D _texture;
		private Vector2 _position;
		private int _speed = 150;
        

		public Player(Vector2 newPos, Texture2D newTexture)
		{
			_texture = newTexture;
			_position = newPos;
		}

        //update player
		public void Update(KeyboardState kState, GamePadState gState, GameTime gameTIme)
		{
			
			//get delta time to move the sprite independently from the frame rate
			float deltaTime = (float)gameTIme.ElapsedGameTime.TotalSeconds;

			//move the player
            if (kState.IsKeyDown(Keys.Left) || gState.IsButtonDown(Buttons.DPadLeft))
				_position.X -= _speed * deltaTime;
			if (kState.IsKeyDown(Keys.Right) || gState.IsButtonDown(Buttons.DPadRight))
				_position.X += _speed * deltaTime;
			if (kState.IsKeyDown(Keys.Up) || gState.IsButtonDown(Buttons.DPadUp))
				_position.Y -= _speed * deltaTime;
			if (kState.IsKeyDown(Keys.Down) || gState.IsButtonDown(Buttons.DPadDown))
				_position.Y += _speed * deltaTime;
            

		}

        //draw player on the screen
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, Color.White);
		}
	}
}
