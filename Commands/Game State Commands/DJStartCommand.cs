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
    class DJStartCommand : ICommand
    {
        GraphicsDevice graphics;
        GraphicsDeviceManager graphicsManager;

        public DJStartCommand(GraphicsDevice graphicsDevice, GraphicsDeviceManager gManager)
        {
            graphics = graphicsDevice;
            graphicsManager = gManager;
        }

        public void Execute()
        {
            GameStateManager.GetInstance().Add(new DoodleJumpState(graphics, graphicsManager));

        }
    }
}
