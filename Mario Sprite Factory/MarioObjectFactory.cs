using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// this whole class feels like a waste.  Is it better to just call new BlockObject in the tokenizer or to have a factory for the object as well????

namespace template_test
{
    class MarioObjectFactory
    {
        ContentManager _content;
        AudioManager audio;

        public MarioObjectFactory(ContentManager manager)
        {
            _content = manager;
            this.audio = audio;
        }

        public MarioObject build(Vector2 position)
        {
            MarioObject block;
            block = new MarioObject(position, _content, audio);
            // it would be nice to take in states as strings and change mario's state.  Not sure how much 'in-game' use that could have as 
            // a new mario would spawn in his default state (which is produced currently)
            return block;

        }
    }
}
