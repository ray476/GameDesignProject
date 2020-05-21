using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class GameStateManager
    {
        private static GameStateManager instance;
        ContentManager content;
        public  Stack<GameState> gameStates = new Stack<GameState>();
        public static GameStateManager GetInstance()
        {
            if (instance == null)
            {
                instance = new GameStateManager();
            }
            return instance;
        }

        public void setContent(ContentManager content)
        {
            this.content = content;

        }

        public void Add(GameState state)
        {
            gameStates.Push(state);
            gameStates.Peek().Initialize();
            gameStates.Peek().LoadContent(content);

        }

        public void Clear()
        {
            while(gameStates.Count != 0)
            {
                gameStates.Pop();
            }
        }

        public void Remove()
        {
            if(gameStates.Count != 0)
            {
                gameStates.Pop();
            }
        }

        public void Update(GameTime gameTime)
        {
            if(gameStates.Count != 0)
            {
                gameStates.Peek().Update(gameTime);
            }
                
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(gameStates.Count != 0)
            {
                gameStates.Peek().Draw(spriteBatch);
            }
                
        }

        public void UnloadContent()
        {
            foreach(GameState state in gameStates)
            {
                state.UnloadContent();
            }
        }
    }
}
