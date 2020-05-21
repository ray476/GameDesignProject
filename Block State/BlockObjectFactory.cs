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
    class BlockObjectFactory
    {
        ContentManager _content;
        AudioManager audio;

        public BlockObjectFactory(ContentManager manager, AudioManager audio)
        {
            _content = manager;
            this.audio = audio;
        }

        public BlockObject build(Vector2 position, string state)
        {
            BlockObject block;
            block = new BlockObject(position, _content,audio, state);
            return block;

        }
    }
}
