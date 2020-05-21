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
    class ResetDJCommand : ICommand
    {
        GraphicsDevice _graphics;
        GraphicsDeviceManager graphicsManager;
        AudioManager audio;

        public ResetDJCommand(GraphicsDevice graphics, GraphicsDeviceManager gManager, AudioManager audioManager)
        {
            _graphics = graphics;
            graphicsManager = gManager;
            audio = audioManager;
        }

        public void Execute()
        {
            audio.PlaySound("restart");
            GameStateManager.GetInstance().Remove();
            GameStateManager.GetInstance().Add(new DoodleJumpState(_graphics, graphicsManager));
        }
    }
}
