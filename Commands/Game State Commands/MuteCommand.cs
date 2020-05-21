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
    class MuteCommand : ICommand
    {
        //will probably not be a marioObject once this gets implemented in the future
        public AudioManager audio;

        public MuteCommand(AudioManager audio)
        {
           this.audio = audio;
        }

        public void Execute()
        {
            audio.Mute();
        }
    }
}
