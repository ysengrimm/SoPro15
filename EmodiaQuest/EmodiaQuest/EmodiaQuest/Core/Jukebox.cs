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

        ContentManager Content;

        private int changeTimer;

        public bool GameIsActive = true;

        private Keys lastMusicKey = Keys.R;

        private SoundDisk activeMusic;
        private int activeMusicIndex;
        private SoundDisk lastMusic;
        private SoundDisk activeSpeach;


        // Songs
        SoundDisk Kampf_1, Going2Easy;

        public List<SoundDisk> Music;

        // FX Sounds
        SoundDisk Hit_1, Plop_1, Die_1, Die_2, Shot_1, Thunder_1, Klick_1, Coin_1, Walk_long, Growl_1, Growl_2;

        public List<SoundDisk> FXSounds;

        // All Speach Sounds

        public List<SoundDisk> Speaches;

        public void LoadJukebox(ContentManager content)
        {
            changeTimer = 250;
            activeMusicIndex = 0;
            this.Content = content;
            Music = new List<SoundDisk>();
            FXSounds = new List<SoundDisk>();
            Speaches = new List<SoundDisk>();

            // Load the music
            Kampf_1 = new SoundDisk("Kampf_1", SoundDisk.SoundType.Music, "Sounds/music/Kampf_1", new TimeSpan(0, 0, 0, 0, 0));
            Music.Add(Kampf_1);
            Going2Easy = new SoundDisk("Going2Easy", SoundDisk.SoundType.Music, "Sounds/music/Going2easy", new TimeSpan(0, 0, 0, 0, 0));
            Music.Add(Going2Easy);


            // Load the FX
            Die_1 = new SoundDisk("Die_1", SoundDisk.SoundType.FX, "Sounds/fx/die_1", new TimeSpan(0, 0, 0, 0, 0));
            FXSounds.Add(Die_1);
            Die_2 = new SoundDisk("Die_2", SoundDisk.SoundType.FX, "Sounds/fx/die_2", new TimeSpan(0, 0, 0, 0, 0));
            FXSounds.Add(Die_2);
            Shot_1 = new SoundDisk("Shot_1", SoundDisk.SoundType.FX, "Sounds/fx/shot", new TimeSpan(0, 0, 0, 0, 300));
            FXSounds.Add(Shot_1);
            Thunder_1 = new SoundDisk("Thunder_1", SoundDisk.SoundType.FX, "Sounds/fx/thunder", new TimeSpan(0, 0, 0, 0, 0));
            FXSounds.Add(Thunder_1);
            Hit_1 = new SoundDisk("Hit_1", SoundDisk.SoundType.FX, "Sounds/fx/Schlag_1", new TimeSpan(0, 0, 0, 0, 300));
            FXSounds.Add(Hit_1);
            Plop_1 = new SoundDisk("Plop_1", SoundDisk.SoundType.FX, "Sounds/fx/Plop_1", new TimeSpan(0, 0, 0, 0, 0));
            FXSounds.Add(Plop_1);
            Klick_1 = new SoundDisk("Klick_1", SoundDisk.SoundType.FX, "Sounds/fx/klick", new TimeSpan(0, 0, 0, 0, 0));
            FXSounds.Add(Klick_1);
            Coin_1 = new SoundDisk("Coin_1", SoundDisk.SoundType.FX, "Sounds/fx/coin", new TimeSpan(0, 0, 0, 0, 0));
            FXSounds.Add(Coin_1);
            Walk_long = new SoundDisk("Walk_long", SoundDisk.SoundType.FX, "Sounds/fx/steps_long", new TimeSpan(0, 0, 0, 0, 0));
            FXSounds.Add(Walk_long);
            Growl_1 = new SoundDisk("Growl_1", SoundDisk.SoundType.FX, "Sounds/fx/growl", new TimeSpan(0, 0, 0, 0, 0));
            FXSounds.Add(Growl_1);
            Growl_2 = new SoundDisk("Growl_2", SoundDisk.SoundType.FX, "Sounds/fx/growl_v2", new TimeSpan(0, 0, 0, 0, 0));
            FXSounds.Add(Growl_2);

            foreach(SoundDisk music in Music)
            {
                music.LoadSoundDisk(Content);
            }
            foreach (SoundDisk fxSound in FXSounds)
            {
                fxSound.LoadSoundDisk(Content);
                //Console.WriteLine(fxSound.Name);
            }
            foreach (SoundDisk speach in Speaches)
            {
                speach.LoadSoundDisk(Content);
            }

            activeMusic = Music.ElementAt(activeMusicIndex);
            activeMusic.Play();
            lastMusic = Music.ElementAt(activeMusicIndex);
            lastMusic.Stop();
        }

        public void UpdateJukebox(GameTime gameTime, bool isActive)
        {
            //Console.WriteLine(IsMusicPlaying() + ", " + Music.Count + ", " + changeTimer + ", " + activeMusicIndex + ", " + lastMusicKey);
            updateMusicKeys();
            GameIsActive = isActive;

            // music update
            activeMusic.Update(gameTime);
            lastMusic.Update(gameTime);

            if (GameIsActive)
            {
                if (changeTimer < 250)
                {
                    changeTimer -= gameTime.ElapsedGameTime.Milliseconds;
                    lastMusicKey = Keys.R;
                }
                if (changeTimer <= 0)
                {
                    changeTimer = 250;
                    lastMusicKey = Keys.R;
                }
                if (lastMusicKey == Keys.P)
                {
                    activeMusic.Pause();
                }
                if (lastMusicKey == Keys.R)
                {
                    activeMusic.Resume();
                }
                if (lastMusicKey == Keys.Q)
                {
                    activeMusic.Stop();
                }
                if (lastMusicKey == Keys.Left && changeTimer == 250)
                {
                    //Console.WriteLine("Change Music");
                    activeMusicIndex += 1;
                    ChangeMusic((activeMusicIndex) % Music.Count);
                    lastMusicKey = Keys.R;
                    changeTimer--;
                }
            }
            else if (!GameIsActive)
            {
                activeMusic.Pause();
            }

            // Always updated
            Klick_1.Update(gameTime);
            Plop_1.Update(gameTime);
            // Screen depending updates
            switch (EmodiaQuest_Game.Gamestate_Game)
            {
                case GameStates_Overall.MenuScreen:
                    // Button Pressed Sound
                    
                    break;
                case GameStates_Overall.OptionsScreen:
                    // Button Pressed Sound
                    break;
                
                case GameStates_Overall.IngameScreen:
                    // Button Pressed Sound
                    Thunder_1.Update(gameTime);
                    Shot_1.Update(gameTime);
                    Hit_1.Update(gameTime);
                    Die_1.Update(gameTime);
                    Die_2.Update(gameTime);
                    Coin_1.Update(gameTime);
                    Walk_long.Update(gameTime);
                    Growl_1.Update(gameTime);
                    Growl_2.Update(gameTime);

                    break;
            }

        }
        public void PlayGrowl_2()
        {
            Growl_2.Play();
        }
        public void PlayGrowl_1()
        {
            Growl_1.Play();
        }
        public void PlayWalk_Long()
        {
            Walk_long.Play();
        }
        public void PlayWalk_Long(TimeSpan timeD, float speed)
        {
            Walk_long.Play(timeD , speed);
        }
        public void PlayCoin()
        {
            Coin_1.Play();
        }
        public void PlayAudioMouseFeedback()
        {
            Klick_1.Play();
        }
        public void PlaySwordFightSound()
        {
            Hit_1.Play();
        }
        public void PlayDie_1()
        {
            Die_1.Play();
        }
        public void PlayDie_2()
        {
            Die_2.Play();
        }
        public void PlayShot_1()
        {
            Shot_1.Play();
            //Console.WriteLine("Shoot!");
        }
        public void PlayThunder_1()
        {           
            Thunder_1.Play();
            //Console.WriteLine("Thunder! " + Thunder_1.IsPlaying);
        }

        /// <summary>
        /// updates the key input, up to now, all keys are used for changing the music, not speach or fx
        /// </summary>
        private void updateMusicKeys()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.P))
            {
                lastMusicKey = Keys.P;
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.R))
            {
                lastMusicKey = Keys.R;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                lastMusicKey = Keys.Q;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                lastMusicKey = Keys.Left;
            }
        }

        /// <summary>
        /// Returns the index of the Music in the Music list when its playing, else returns -1;
        /// </summary>
        /// <returns></returns>
        public int IsMusicPlaying()
        {
            int temp = -1;
            for ( int i = 0; i < Music.Count; i++)
            {
                if (Music.ElementAt(i).IsPlaying)
                {
                    temp = i;
                }
            }
            return temp;
        }

        /// <summary>
        /// Returns the index of the Speach in the Speach list when its playing, else returns -1;
        /// </summary>
        /// <returns></returns>
        public int IsSpeachPlaying()
        {
            int temp = -1;
            for (int i = 0; i < Speaches.Count; i++)
            {
                if (Speaches.ElementAt(i).IsPlaying)
                {
                    temp = i;
                }
            }
            return temp;
        }



        public void ChangeMusic(int i)
        {
            lastMusic = activeMusic;
            activeMusic.Stop();
            activeMusic = Music.ElementAt(i);
            activeMusic.Play();
        }
    }
}
