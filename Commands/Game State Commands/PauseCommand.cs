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
    class PauseCommand : ICommand
    {
        GraphicsDevice _graphics;
        HudObject _hud;
        AudioManager audio;
        GraphicsDeviceManager graphicsManager;

        public PauseCommand(GraphicsDevice graphics, HudObject hud, AudioManager audio, GraphicsDeviceManager gManager)
        {
            _graphics = graphics;
            this.audio = audio;
            _hud = hud;
            graphicsManager = gManager;

        }

        public void Execute()
        {
            audio.PlaySound("pause");
            audio.Mute();
            if (GameStateManager.GetInstance().gameStates.Peek() is PauseGameState)
                GameStateManager.GetInstance().Remove();
            else
                GameStateManager.GetInstance().Add(new PauseGameState(_graphics, _hud, audio, graphicsManager));
        }
    }
}
