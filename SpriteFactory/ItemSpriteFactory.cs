using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace template_test
{
    //-------------------------N.B.---------------------
    //flag is a static sprite, when it comes time to have it slide down the pole
    //the direction of the flag is flipped before moving, might want to change
    //the flag to a animated sprite so the spriteEffects.flip options are available 
    //-------------------------------------------------------

    class ItemSpriteFactory : ISpriteFactory
    {
        private Texture2D _coinSpinningTex;
        private Texture2D _mushroomSuperTex;
        private Texture2D _mushroom1UpTex;
        private Texture2D _fireFlowerTex;
        private Texture2D _starmanTex;
        private Texture2D _smallWarpPipe;
        private Texture2D _mediumWarpPipe;
        private Texture2D _largeWarPipe;
        private Texture2D _castle;
        private Texture2D _flagPole;
        private Texture2D _flag;
        private Texture2D _brickPieceTex;
        private Texture2D _doublePotion;
        private Texture2D _speedPotion;
        private Texture2D _propeller;

        public ItemSpriteFactory(ContentManager manager)
        {
            _coinSpinningTex = manager.Load<Texture2D>("coin_spinning");
            _mushroomSuperTex = manager.Load<Texture2D>("mushroom_super");
            _mushroom1UpTex = manager.Load<Texture2D>("mushroom_1-up");
            _fireFlowerTex = manager.Load<Texture2D>("fire_flower");
            _starmanTex = manager.Load<Texture2D>("starman");
            _smallWarpPipe = manager.Load<Texture2D>("smallWarpPipe");
            _mediumWarpPipe = manager.Load<Texture2D>("medium_pipe");
            _largeWarPipe = manager.Load<Texture2D>("large_pipe");
            _castle = manager.Load<Texture2D>("castle");
            _flagPole = manager.Load<Texture2D>("flag_pole");
            _flag = manager.Load<Texture2D>("flag");
            _brickPieceTex = manager.Load<Texture2D>("brick_piece");
            _doublePotion = manager.Load<Texture2D>("bouncy_potion_jump");
            _speedPotion = manager.Load<Texture2D>("bouncy_potion_run");
            _propeller = manager.Load<Texture2D>("bouncy_propeller");
        }

        public ISprite build(string state)
        {
            ISprite sprite;
            switch (state)
            {
                case "super":
                    sprite = new SpriteStatic(_mushroomSuperTex, true);
                    break;
                case "1up":
                    sprite = new SpriteStatic(_mushroom1UpTex, true);
                    break;
                case "fire":
                    sprite = new SpriteAnimated(_fireFlowerTex, 1, 4, 8, true);
                    break;
                case "star":
                    sprite = new SpriteAnimated(_starmanTex, 1, 4, 8, true);
                    break;
                case "small_pipe":
                    sprite = new SpriteStatic(_smallWarpPipe, true);
                    break;
                case "medium_pipe":
                    sprite = new SpriteStatic(_mediumWarpPipe, true);
                    break;
                case "large_pipe":
                    sprite = new SpriteStatic(_largeWarPipe, true);
                    break;
                case "castle":
                    sprite = new SpriteStatic(_castle, true);
                    break;
                case "flag_pole":
                    sprite = new SpriteStatic(_flagPole, true);
                    break;
                case "flag":
                    sprite = new SpriteStatic(_flag, true);
                    break;
               case "brick_piece":
                    sprite = new SpriteAnimated(_brickPieceTex,1,4,8,true);
                    break;
                case "speed":
                    sprite = new SpriteStatic(_speedPotion, true);
                    break;
                case "jump":
                    sprite = new SpriteStatic(_doublePotion, true);
                    break;
                case "propeller":
                    sprite = new SpriteStatic(_propeller, true);
                    break;
                default:
                    sprite = new SpriteAnimated(_coinSpinningTex, 1, 4, 8, true);
                    break;
            }
            return sprite;
        }
    }
}
