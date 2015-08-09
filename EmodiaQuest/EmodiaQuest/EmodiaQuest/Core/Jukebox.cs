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
        SoundDisk Hit_1, Plop_1;

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
            Hit_1 = new SoundDisk("Hit_1", SoundDisk.SoundType.FX, "Sounds/fx/Schlag_1", new TimeSpan(0, 0, 0, 0, 200));
            FXSounds.Add(Hit_1);
            Plop_1 = new SoundDisk("Plop_1", SoundDisk.SoundType.FX, "Sounds/fx/Plop_1", new TimeSpan(0, 0, 0, 0, 0));
            FXSounds.Add(Plop_1);

            foreach(SoundDisk music in Music)
            {
                music.LoadSoundDisk(Content);
            }
            foreach (SoundDisk fxSound in FXSounds)
            {
                fxSound.LoadSoundDisk(Content);
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
            Console.WriteLine(IsMusicPlaying() + ", " + Music.Count + ", " + changeTimer + ", " + activeMusicIndex + ", " + lastMusicKey);
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
                    Console.WriteLine("Change Music");
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

            switch (EmodiaQuest_Game.Gamestate_Game)
            {
                case GameStates_Overall.MenuScreen:
                    // Button Pressed Sound
                    Plop_1.Update(gameTime);
                    break;
                case GameStates_Overall.OptionsScreen:
                    // Button Pressed Sound
                    Plop_1.Update(gameTime);
                    break;

                case GameStates_Overall.IngameScreen:
                    // Button Pressed Sound
                    Plop_1.Update(gameTime);
                    // PlayerSounds
                    if (Player.Instance.ActivePlayerState == PlayerState.Swordfighting)
                    {
                        Hit_1.Update(gameTime);
                        Hit_1.Play();
                    }
                    break;
            }

        }

        public void PlayAudioMouseFeedback()
        {
            Plop_1.Play();
        }

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
