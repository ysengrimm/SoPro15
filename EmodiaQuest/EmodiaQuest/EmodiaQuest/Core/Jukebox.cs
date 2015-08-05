using System;
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

        MediaLibrary sampleMediaLibrary;

        Random rand;

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

            private bool forceToPlay;
            public bool ForceToPlay
            {
                set { forceToPlay = value; }
                get { return forceToPlay; }
            }

            public FXSound (int index, String name, SoundEffect soundEffect, float timer, float delay, bool playing, bool forceToPlay)
            {
                this.Index = index;
                this.Name = name;
                this.SoundEffect = soundEffect;
                this.Delay = delay;
                this.Timer = timer;
                this.activeTimer = Timer;
                this.activeDelay = delay;
                this.playing = playing;
                this.forceToPlay = forceToPlay;
            }
        }

        // Number of FX Sounds
        int FXNum;
        // Number of Songs
        int MusicNum;
        // FX 
        FXSound[] FXSounds;
        // Songs
        Song Kampf_1;

        // All FX Sounds
        FXSound Hit_1, Plop_1;

        // All songs
        Song[] Songs;

        public void LoadJukebox(ContentManager content)
        {
            this.Content = content;

            // FX Loading
            FXNum = 100;
            FXSounds = new FXSound [FXNum];
            Hit_1 = new FXSound(0, "Hit_1", Content.Load<SoundEffect>("Sounds/fx/Schlag_1"), Player.Instance.standingDuration / 1.5f, Player.Instance.standingDuration / 3f, false, false);
            Plop_1 = new FXSound(1, "Plop_1", Content.Load<SoundEffect>("Sounds/fx/Plop_1"), 200, 0, false, false);
            FXSounds[0] = Hit_1;
            FXSounds[1] = Plop_1;

            // Music Loading
            Kampf_1 = Content.Load<Song>("Sounds/music/Kampf_1");

            MediaPlayer.Play(Kampf_1);
            Console.WriteLine();
        }

        public void UpdateJukebox(GameTime gameTime)
        {
            MediaPlayer.Volume = Settings.Instance.Volume;

            //Console.WriteLine(MediaPlayer.Queue.Count);
            switch (EmodiaQuest_Game.Gamestate_Game)
            {
                case GameStates_Overall.MenuScreen:
                    // Button Pressed Sound
                    for(int i = 0; i < FXSounds.Length; i++)
                    {
                        if(FXSounds[i].ForceToPlay)
                        {
                            playFX(i, gameTime);
                        }
                    }
                    break;
                case GameStates_Overall.OptionsScreen:
                    // Button Pressed Sound
                    for (int i = 0; i < FXSounds.Length; i++)
                    {
                        if (FXSounds[i].ForceToPlay)
                        {
                            playFX(i, gameTime);
                        }
                    }
                    break;

                case GameStates_Overall.IngameScreen:
                    // Button Pressed Sound
                    for (int i = 0; i < FXSounds.Length; i++)
                    {
                        if (FXSounds[i].ForceToPlay)
                        {
                            playFX(i, gameTime);
                        }
                    }

                    // PlayerSounds
                    if (Player.Instance.ActivePlayerState == PlayerState.Swordfighting || FXSounds[0].Playing)
                    {
                        playFX(0, gameTime);
                    }
                    break;
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
                FXSounds[index].ForceToPlay = false;
            }

        }

        public void PlayAudioMouseFeedback(int i)
        {
            FXSounds[i].ForceToPlay = true;
        }

    }
}
