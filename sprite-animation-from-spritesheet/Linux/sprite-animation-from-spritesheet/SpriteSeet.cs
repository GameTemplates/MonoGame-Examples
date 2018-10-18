/*
 This original source code to load and play spritesheet animation is from this article at http://rbwhitaker.wikidot.com/monogame-texture-atlases
*/

using System;

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace sprite_animation_from_spritesheet.Desktop
{
    public class SpriteSheet
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;

        private double _initialTime = 0.1d; //value to control speed of animation using a timer
        private double _timer;

        public SpriteSheet(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            _timer = _initialTime;
        }

        public void Update(GameTime gameTime)
        {
			//update animation number by increasing the currentFrame value
            _timer += gameTime.ElapsedGameTime.TotalSeconds; //increase the value of timer
			if (_timer >= _initialTime) //if timer is >= the initial time then update animation frame
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
                _timer = 0; //after frame is updated, reset the timer
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
			/*
			 using the width, height, row and column values we are calculating the position of the currentFrame on the sprite sheet
			 then we are creating a rectangle in that position using the width and height of a frame.
			 then, we use the reactangle to draw that piece (frame) of the sprite sheet on to the screen
            */
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

        }

        //we can use this method to set current frame manually by entering the frame number
        public void setFrame(int newFrame)
        {
            currentFrame = newFrame;
        }
    }
}