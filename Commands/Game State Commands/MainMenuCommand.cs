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
    class MainMenuCommand : ICommand
    {

        GraphicsDevice graphics;
        GraphicsDeviceManager graphicsManger;

        public MainMenuCommand(GraphicsDevice graphicsDevice, GraphicsDeviceManager gManager)
        {
            graphics = graphicsDevice;
            graphicsManger = gManager;
        }

        public void Execute()
        {
            GameStateManager.GetInstance().Add(new MainMenuState(graphics, graphicsManger));
        }
    }
}
