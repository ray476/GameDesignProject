using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
   public class AudioManager
    {
        private Song theme;
        private Song doodleTheme;
        private Song altTheme;
        private SoundEffect smallJump;
        private SoundEffect superJump;
        private SoundEffect bouncyJump;
        private SoundEffect cannon;
        private SoundEffect copter;
        private SoundEffect potion;
        private SoundEffect oneUp;
        private SoundEffect breakBlock;
        private SoundEffect bump;
        private SoundEffect coin;
        private SoundEffect gameOver;
        private SoundEffect kick;
        private SoundEffect marioDie;
        private SoundEffect powerUp;
        private SoundEffect powerUpAppear;
        private SoundEffect timeWarning;
        private SoundEffect pipe;
        private SoundEffect pause;
        private SoundEffect stageClear;
        private SoundEffect restart;
        public bool mute;
        public bool themeChanged;

        public AudioManager(ContentManager manager)
        {
            theme = manager.Load<Song>("SMBtheme");
            copter = manager.Load<SoundEffect>("copter");
            cannon = manager.Load<SoundEffect>("boom");
            potion = manager.Load<SoundEffect>("potion");
            altTheme = manager.Load<Song>("SMBtheme_alt");
            doodleTheme = manager.Load<Song>("bouncy_themep3");
            smallJump = manager.Load<SoundEffect>("smb_jump-small (1)");
            superJump = manager.Load<SoundEffect>("smb_jump-super");
            bouncyJump = manager.Load<SoundEffect>("jump");
            oneUp = manager.Load<SoundEffect>("smb_1-up");
            breakBlock = manager.Load<SoundEffect>("smb_breakblock");
            bump = manager.Load<SoundEffect>("smb_bump");
            coin = manager.Load<SoundEffect>("smb_coin");
            gameOver = manager.Load<SoundEffect>("smb_gameover");
            kick = manager.Load<SoundEffect>("smb_kick");
            marioDie = manager.Load<SoundEffect>("smb_mariodie");
            powerUp = manager.Load<SoundEffect>("smb_powerup");
            powerUpAppear = manager.Load<SoundEffect>("smb_powerup_appears");
            timeWarning = manager.Load<SoundEffect>("smb_warning");
            pipe = manager.Load<SoundEffect>("smb_pipe");
            pause  = manager.Load<SoundEffect>("smb_pause");
            stageClear =  manager.Load<SoundEffect>("smb_stage_clear");
            restart = manager.Load<SoundEffect>("restart_sound");
            MediaPlayer.Play(theme);
            MediaPlayer.IsRepeating = true;
            mute = false;
            themeChanged = false;

        }

        public void Mute()
        {
            if (!mute)
                MediaPlayer.Pause();
            else
            {
                MediaPlayer.Resume();
            }
            mute = !mute;
        }

        public void PlaySound(string name)
        {
            if (!mute){
                switch (name)
                {
                    case "theme":
                        MediaPlayer.Stop();
                        MediaPlayer.Play(theme);
                        MediaPlayer.IsRepeating = true;
                        break;
                    case "doodleTheme":
                        MediaPlayer.Stop();
                        MediaPlayer.Play(doodleTheme);
                        MediaPlayer.IsRepeating = true;
                        break;
                    case "altTheme":
                        MediaPlayer.Stop();
                        MediaPlayer.Play(altTheme);
                        MediaPlayer.IsRepeating = true;
                        break;
                    case "smallJump":
                        var smallJumpInstance = smallJump.CreateInstance();
                        smallJumpInstance.Play();
                        break;
                    case "cannon":
                        var cannonInstance = cannon.CreateInstance();
                        cannonInstance.Play();
                        break;
                    case "copter":
                        var copterInstance = copter.CreateInstance();
                        copterInstance.Play();
                        break;
                     case "potion":
                        var potionInstance = potion.CreateInstance();
                        potionInstance.Play();
                        break;
                    case "superJump":
                        var superJumpInstance = superJump.CreateInstance();
                        superJumpInstance.Play();
                        break;
                     case "bouncyJump":
                        var bouncyJumpInstance = bouncyJump.CreateInstance();
                        bouncyJumpInstance.Volume = 0.5f;
                        bouncyJumpInstance.Play();
                        break;
                    case "oneUp":
                        var oneUpInstance = oneUp.CreateInstance();
                        oneUpInstance.Play();
                        break;
                    case "breakBlock":
                        var breakBlockInstance = breakBlock.CreateInstance();
                        breakBlockInstance.Play();
                        break;
                    case "bump":
                        var bumpInstance = bump.CreateInstance();
                        bumpInstance.Play();
                        break;
                    case "coin":
                        var coinInstance = coin.CreateInstance();
                        coin.Play();
                        break;
                    case "gameOver":
                        var gameOverInstance = gameOver.CreateInstance();
                        gameOverInstance.Play();
                        break;
                    case "kick":
                        var kickInstance = kick.CreateInstance();
                        kickInstance.Play();
                        break;
                    case "marioDie":
                        var marioDieInstance = marioDie.CreateInstance();
                        marioDie.Play();
                        break;
                    case "powerUp":
                        var powerUpInstance = powerUp.CreateInstance();
                        powerUpInstance.Play();
                        break;
                    case "powerUpAppear":
                        var powerUpAppearInstance = powerUpAppear.CreateInstance();
                        powerUpAppearInstance.Play();
                        break;
                    case "timeWarning":
                        var timeWarningInstance = timeWarning.CreateInstance();
                        timeWarningInstance.Play();
                        break;
                    case "pipe":
                        var pipeInstance = pipe.CreateInstance();
                        pipeInstance.Play();
                        break;
                    case "powerDown":
                        var powerDownInstance = pipe.CreateInstance();
                        powerDownInstance.Play();
                        break;
                    case "pause":
                        var pauseInstance = pause.CreateInstance();
                        pauseInstance.Play();
                        break;
                    case "stageClear":
                        var stageClearInstance = stageClear.CreateInstance();
                        stageClearInstance.Play();
                        break;
                    case "restart":
                        var restartInstance = restart.CreateInstance();
                        restartInstance.Play();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
