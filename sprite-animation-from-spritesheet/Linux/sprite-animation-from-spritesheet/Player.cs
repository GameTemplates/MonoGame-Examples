using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace sprite_animation_from_spritesheet.Desktop
{
    public class Player
    {
		private Vector2 _position;
		private Texture2D _texture;
		private SpriteSheet _spriteSheet;
		
		public Player(Vector2 newPos, Texture2D newTexture)
        {
			_position = newPos;
			_texture = newTexture;
			//create a sprite sheet from the texture
			_spriteSheet = new SpriteSheet(_texture, 1, 9);
            
        }

		public void PlayAnimation(GameTime gameTime)
		{
			//call the update method of the sprite sheet to play the animation
			_spriteSheet.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			//call the draw method of the sprite sheet to draw the animation on the screen
			_spriteSheet.Draw(spriteBatch, _position);
		}
    }
}
