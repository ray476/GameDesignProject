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
    class EnemySpriteFactory : IEnemyFactory
    {
        private Texture2D _goombaWalkTex;
        private Texture2D _goombaFlatTex;
        private Texture2D _koopaGreenWalkLeft;
        private Texture2D _koopaGreenWalkRight;
        private Texture2D _koopaGreenShell;
        private Texture2D _koopaGreenShellLegs;
        private Texture2D _koopaRedWalkLeft;
        private Texture2D _koopaRedWalkRight;
        private Texture2D _koopaRedShell;
        private Texture2D _koopaRedShellLegs;
        private Texture2D _pirhana;

        public EnemySpriteFactory(ContentManager manager)
        {
            _goombaWalkTex = manager.Load<Texture2D>("goomba_walk");
            _goombaFlatTex = manager.Load<Texture2D>("goomba_flat");
            _koopaGreenWalkLeft = manager.Load<Texture2D>("koopa_green_walk_left");
            _koopaGreenWalkRight = manager.Load<Texture2D>("koopa_green_walk_right");
            _koopaGreenShell = manager.Load<Texture2D>("koopa_green_shell");
            _koopaGreenShellLegs = manager.Load<Texture2D>("koopa_green_shell_legs");
            _koopaRedWalkLeft = manager.Load<Texture2D>("koopa_red_walk_left");
            _koopaRedWalkRight = manager.Load<Texture2D>("koopa_red_walk_right");
            _koopaRedShell = manager.Load<Texture2D>("koopa_red_shell");
            _koopaRedShellLegs = manager.Load<Texture2D>("koopa_red_shell_legs");
            _pirhana = manager.Load<Texture2D>("pirahna");
        }

        public ISprite build(Vector2 position, IEnemyState state)
        {
            ISprite sprite = null;
            if (state is DeadGoomba)
                sprite = new SpriteStatic(_goombaFlatTex, true);
            else if (state is WalkingGoomba)
                sprite = new SpriteAnimated(_goombaWalkTex, 1, 2, 4, true);
            else if (state is WalkingGreenKoopa)
                sprite = new SpriteAnimated(_koopaGreenWalkLeft, 1, 2, 4, true);
            else if (state is WalkingRedKoopa)
                sprite = new SpriteAnimated(_koopaRedWalkLeft, 1, 2, 4, true);
            else if (state is DeadGreenKoopa)
                sprite = new SpriteStatic(_koopaGreenShell, true);
            else if (state is DeadRedKoopa)
                sprite = new SpriteStatic(_koopaRedShell, true);
            else if (state is PirhanaPlant)
                sprite = new SpriteAnimated(_pirhana, 1, 2, 3, true);

            return sprite;
        }
    }
}
