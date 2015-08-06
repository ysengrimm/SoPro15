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


        // Songs
        SoundDisk Kampf_1;

        // All FX Sounds
        SoundDisk Hit_1, Plop_1;


        public void LoadJukebox(ContentManager content)
        {
            this.Content = content;

            Kampf_1 = new SoundDisk("Kampf_1", SoundDisk.SoundType.Music, "Sounds/music/Kampf_1", new TimeSpan(0, 0, 0, 0, 0));
            Kampf_1.LoadSoundDisk(Content);
            Console.WriteLine();

            Hit_1 = new SoundDisk("Hit_1", SoundDisk.SoundType.FX, "Sounds/fx/Schlag_1", new TimeSpan(0, 0, 0, 0, 200));
            Hit_1.LoadSoundDisk(Content);
            Plop_1 = new SoundDisk("Plop_1", SoundDisk.SoundType.FX, "Sounds/fx/Plop_1", new TimeSpan(0, 0, 0, 0, 0));
            Plop_1.LoadSoundDisk(Content);

            Kampf_1.Play();
        }

        public void UpdateJukebox(GameTime gameTime)
        {
            Kampf_1.Update(gameTime);

            if(Keyboard.GetState().IsKeyDown(Keys.P) || Player.Instance.GameIsInFocus == false)
            {
                Kampf_1.Pause();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R) || Player.Instance.GameIsInFocus == true || EmodiaQuest_Game.Gamestate_Game != GameStates_Overall.IngameScreen)
            {
                Kampf_1.Resume();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Kampf_1.Stop();
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

    }
}
