using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace template_test
{
    class SMBStartCommand : ICommand
    {
        GraphicsDevice graphics;
        GraphicsDeviceManager graphicsManager;
        public SMBStartCommand(GraphicsDevice graphicsDevice, GraphicsDeviceManager gManager)
        {
            graphics = graphicsDevice;
            graphicsManager = gManager;
        }

        public void Execute()
        {
            GameStateManager.GetInstance().Add(new PlayState(graphics, graphicsManager));

        }
    }
}
