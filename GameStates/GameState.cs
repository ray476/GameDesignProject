using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace template_test
{
    abstract class GameState : IGameState
    {
        public GraphicsDevice graphics;

        public GameState(GraphicsDevice graphicsDevice)
        {
            graphics = graphicsDevice;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        
        public abstract void Initialize();

        public abstract void LoadContent(ContentManager content);
        
        public abstract void UnloadContent();
        
        public abstract void Update(GameTime gameTime);
        
            
        
    }
}
