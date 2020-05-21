using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace template_test
{
        public abstract class AbsAvatarObject : AbsObject
        {

        public bool tookDamage;
        public int damageCounter;
        public IPowerState powerUpState;
        public IMovementState movementState;
        public IMovementState origMoveState;
        public ISprite origSprite;
        public ContentManager content;
        public AudioManager audio;
        public HudObject hud;
        public MiscObject PipeContacted;
        public bool isGrounded;


        abstract public void Up();

        abstract public void Down();

        abstract public void Left();
 
        abstract public void Right();

        abstract public void TakeDamage();

        abstract public void Super();

        abstract public void Fire();

        abstract public void Small();

        abstract public void FindCheckpoint();
    }
}


