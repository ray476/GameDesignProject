using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace template_test
{
    public class SpriteAnimated : ISprite
    {
        private Texture2D _texture;
        private int _rows;
        private int _columns;
        private int _currentFrame;
        private int _totalFrames;
        private int _timeBetweenFrames;
        private int _timeOnCurrFrame;
        private bool _rightFace;
        public bool RightFace
        {
            get
            {
                return _rightFace;
            }
            set
            {
                _rightFace = value;
            }
        }
        public int Fps
        {
            set { _timeBetweenFrames = 1000 / value; }
        }

        public SpriteAnimated(Texture2D texture, int rows, int columns, int fps, bool facingRight)
        {
            _texture = texture;
            _rows = rows;
            _columns = columns;
            _currentFrame = 0;
            _totalFrames = rows * columns;
            _timeBetweenFrames = 1000 / fps;
            _timeOnCurrFrame = 0;
            RightFace = facingRight;
        }


        public void Update(GameTime gameTime)
        {
            _timeOnCurrFrame += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_timeOnCurrFrame >= _timeBetweenFrames)
            {
                _timeOnCurrFrame = 0;
                _currentFrame++;
                if (_currentFrame == _totalFrames)
                {
                    _currentFrame = 0;
                }
            }
        }

        public void Draw(Vector2 position, float opacity, SpriteBatch spriteBatch)
        {
            int width = _texture.Width / _columns;
            int height = _texture.Height / _rows;
            int row = (int)(_currentFrame / _columns);
            int column = _currentFrame % _columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);


            if (_rightFace)
            {
                spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White * opacity);
            }
            else
            {
                //figure out how to skip all the un-needed parameters
                spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White * opacity, 0.0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.0f);
            }
        }
    }
}