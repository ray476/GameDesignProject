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
    class KoopaRedSpriteFactory : ISpriteFactory
    {
        private Texture2D _koopaRedWalkLeft;
        private Texture2D _koopaRedWalkRight;
        private Texture2D _koopaRedShell;
        private Texture2D _koopaRedShellLegs;

        public KoopaRedSpriteFactory(ContentManager manager)
        {
            _koopaRedWalkLeft = manager.Load<Texture2D>("koopa_red_walk_left");
            _koopaRedWalkRight = manager.Load<Texture2D>("koopa_red_walk_right");
            _koopaRedShell = manager.Load<Texture2D>("koopa_red_shell");
            _koopaRedShellLegs = manager.Load<Texture2D>("koopa_red_shell_legs");
        }

        public ISprite build(string state)
        {
            ISprite sprite;
            switch (state)
            {
                case "right":
                    sprite = new SpriteAnimated(_koopaRedWalkRight, 1, 2, 4, true);
                    break;
                case "shell":
                    sprite = new SpriteStatic(_koopaRedShell, true);
                    break;
                case "legs":
                    sprite = new SpriteStatic(_koopaRedShellLegs, true);
                    break;
                default:
                    sprite = new SpriteAnimated(_koopaRedWalkLeft, 1, 2, 4, true);
                    break;
            }
            return sprite;
        }
    }
}
