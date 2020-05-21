
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class NameObject
    {
        
        public char[] arr = { 'A', 'A', 'A', 'A', 'A' };
        public string name;
        public int score;
        Texture2D _texture;
        public Rectangle rect = new Rectangle(125, 550, 30, 15);
        int cursorPos = 0;
        public NameObject(GraphicsDevice graphicsDevice)
        {
            _texture = new Texture2D(graphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });
            name = new string(arr);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_texture, rect, Color.White);
            
        }
        public void CursorRight()
        {
            if(rect.X < 525)
            {
                rect.X += 100;
                cursorPos++;
            }
        }
        public void CursorLeft()
        {
            if (rect.X > 125)
            { 
                rect.X -= 100;
                cursorPos--;
            }
        }
        public void CharUp()
        {
            switch (cursorPos)
            {
                case 0:
                    arr[cursorPos] = (char)(arr[cursorPos] + 1);
                    break;
                case 1:
                    arr[cursorPos] = (char)(arr[cursorPos] + 1);
                    break;
                case 2:
                    arr[cursorPos] = (char)(arr[cursorPos] + 1);
                    break;
                case 3:
                    arr[cursorPos] = (char)(arr[cursorPos] + 1);
                    break;
                case 4:
                    arr[cursorPos] = (char)(arr[cursorPos] + 1);
                    break;
            }
            if(arr[cursorPos] > 90)
            {
                arr[cursorPos] = (char) 65;
            }
            if(arr[cursorPos] < 65)
            {
                arr[cursorPos] = (char) 90;
            }
        }

        public void CharDown()
        {
            switch (cursorPos)
            {
                case 0:
                    arr[cursorPos] = (char)(arr[cursorPos] - 1);
                    break;
                case 1:
                    arr[cursorPos] = (char)(arr[cursorPos] - 1);
                    break;
                case 2:
                    arr[cursorPos] = (char)(arr[cursorPos] - 1);
                    break;
                case 3:
                    arr[cursorPos] = (char)(arr[cursorPos] - 1);
                    break;
                case 4:
                    arr[cursorPos] = (char)(arr[cursorPos] - 1);
                    break;
            }
            if (arr[cursorPos] > 90)
            {
                arr[cursorPos] = (char)65;
            }
            if (arr[cursorPos] < 65)
            {
                arr[cursorPos] = (char)90;
            }
        }

        public void Update(GameTime gameTime)
        {
           
        }
    }
}
