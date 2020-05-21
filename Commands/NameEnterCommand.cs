using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class NameEnterCommand : ICommand
    {
        GraphicsDevice graph;
        NameObject name;
        GraphicsDeviceManager gman;
        AudioManager _audio;
        public NameEnterCommand(GraphicsDevice graphics,NameObject playerName,GraphicsDeviceManager gManager,AudioManager audio)
        {
            graph = graphics;
            name = playerName;
            gman = gManager;
            _audio = audio;
        }

        public void Execute()
        {
            name.name = new string(name.arr);
            GameStateManager.GetInstance().Remove();
            GameStateManager.GetInstance().Add(new LeaderBoardState(graph,name,gman,_audio));
        }
    }
}
