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
    class KoopaGreenSpriteFactory : ISpriteFactory
    {
        private Texture2D _koopaGreenWalkLeft;
        private Texture2D _koopaGreenWalkRight;
        private Texture2D _koopaGreenShell;
        private Texture2D _koopaGreenShellLegs;

        public KoopaGreenSpriteFactory(ContentManager manager)
        {
            _koopaGreenWalkLeft = manager.Load<Texture2D>("koopa_green_walk_left");
            _koopaGreenWalkRight = manager.Load<Texture2D>("koopa_green_walk_right");
            _koopaGreenShell = manager.Load<Texture2D>("koopa_green_shell");
            _koopaGreenShellLegs = manager.Load<Texture2D>("koopa_green_shell_legs");
        }

        public ISprite build(string state)
        {
            ISprite sprite;
            switch (state)
            {
                case "right":
                    sprite = new SpriteAnimated(_koopaGreenWalkRight, 1, 2, 4, true);
                    break;
                case "shell":
                    sprite = new SpriteStatic(_koopaGreenShell, true);
                    break;
                case "legs":
                    sprite = new SpriteStatic(_koopaGreenShellLegs, true);
                    break;
                default:
                    sprite = new SpriteAnimated(_koopaGreenWalkLeft, 1, 2, 4, true);
                    break;
            }
            return sprite;
        }
    }
}
