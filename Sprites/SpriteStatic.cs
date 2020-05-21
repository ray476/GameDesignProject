using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace template_test
{
    class SpriteStatic : ISprite
    {
        private Texture2D _texture;
        private bool _rightFace;

        public SpriteStatic(Texture2D texture, bool facingRight)
        {
            _texture = texture;
            _rightFace = facingRight;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(Vector2 position, float opacity, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, position, Color.White * opacity);
        }
    }
}
