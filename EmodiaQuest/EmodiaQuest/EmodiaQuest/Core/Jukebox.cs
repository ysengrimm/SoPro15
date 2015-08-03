﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace EmodiaQuest.Core
{
    public class Jukebox
    {
        // Implement nice methods to play adaptive sound, from everywhere of the game. Fade over and max of playing sounds at one time (Janos)

       private static Jukebox instance;

        private Jukebox() { }

        public static Jukebox Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Jukebox();
                }
                return instance;
            }
        }
        public ContentManager Content;

        public struct FXSound
        {
            public int Index;
            public String Name;
            public SoundEffect SoundEffect;
            public float Timer;
            public float Delay;

            private float activeTimer;
            public float ActiveTimer
            {
                set { activeTimer = value; }
                get { return activeTimer; }
            }

            private float activeDelay;
            public float ActiveDelay
            {
                set { activeDelay = value; }
                get { return activeDelay; }
            }

            private bool playing;
            public bool Playing
            {
                set { playing = value; }
                get { return playing; }
            }

            public FXSound (int index, String name, SoundEffect soundEffect, float timer, float delay, bool playing)
            {
                this.Index = index;
                this.Name = name;
                this.SoundEffect = soundEffect;
                this.Delay = delay;
                this.Timer = timer;
                this.activeTimer = Timer;
                this.activeDelay = delay;
                this.playing = playing;
            }
        }

        // Number of FX Sounds
        int FXNum;
        // FX 
        FXSound[] FXSounds;

        FXSound Hit_1;

        public void LoadJukebox(ContentManager content)
        {
            this.Content = content;
            FXNum = 100;
            FXSounds = new FXSound [FXNum];
            Hit_1 = new FXSound(0, "Hit_1", Content.Load<SoundEffect>("Sounds/fx/Schlag_1"), Player.Instance.standingDuration / 1.5f, Player.Instance.standingDuration / 3f, false);
            FXSounds[0] = Hit_1;

        }

        public void UpdateJukebox(GameTime gameTime)
        {

            if (Player.Instance.ActivePlayerState == PlayerState.Swordfighting || FXSounds[0].Playing)
            {
                playFX(0, gameTime);
            }
        }


        public void playFX(int index, GameTime gametime)
        {
            if (FXSounds[index].Playing == false)
            {
                //Console.WriteLine("Start");
                FXSounds[index].ActiveDelay -= gametime.ElapsedGameTime.Milliseconds;
                FXSounds[index].Playing = true;
            }
            else if (FXSounds[index].Playing == true && FXSounds[index].ActiveDelay > 0)
            {
                //Console.WriteLine("Fading");
                FXSounds[index].ActiveDelay -= gametime.ElapsedGameTime.Milliseconds;
            }
            else if (FXSounds[index].Playing == true && FXSounds[index].ActiveDelay <= 0 && FXSounds[index].ActiveTimer == FXSounds[index].Timer)
            {
                //Console.WriteLine("PlayStart");
                FXSounds[index].ActiveDelay = 0;
                SoundEffectInstance tempInstance = FXSounds[index].SoundEffect.CreateInstance();
                tempInstance.Volume = Settings.Instance.Volume;
                tempInstance.Play();
                FXSounds[index].ActiveTimer -= gametime.ElapsedGameTime.Milliseconds;
            }
            else if (FXSounds[index].Playing == true && FXSounds[index].ActiveDelay == 0 && FXSounds[index].ActiveTimer > 0)
            {
                //Console.WriteLine("Setting Timer back");
                FXSounds[index].ActiveTimer -= gametime.ElapsedGameTime.Milliseconds;
            }
            else if (FXSounds[index].Playing == true && FXSounds[index].ActiveTimer <= 0)
            {
                //Console.WriteLine("Reset");
                FXSounds[index].ActiveDelay = FXSounds[index].Delay;
                FXSounds[index].ActiveTimer = FXSounds[index].Timer;
                FXSounds[index].Playing = false;
            }

        }

    }
}
