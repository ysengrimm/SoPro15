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
    public class SoundDisk
    {

        public enum SoundState { Fading, Playing, Paused, Stopped, Delaying, Idle, Resumed}
        /// <summary>
        /// The active state of the Sound, mostly for intern calculations
        /// </summary>
        SoundState activeSoundState;
        /// <summary>
        /// Enum which declares the type of the Sounddisk
        /// </summary>
        public enum SoundType { FX, Music, Speach}
        /// <summary>
        /// The kind Sound, this disk has
        /// </summary>
        SoundType SoundTyp;
        /// <summary>
        /// Content manager, needed for burning music data on the Sounddisk
        /// </summary>
        public ContentManager Content;
        /// <summary>
        /// The Path where the sounddata is in the Contentmanager
        /// </summary>
        public String SoundPath;
        /// <summary>
        /// Name of this SoundDisk
        /// </summary>
        public String Name;
        /// <summary>
        /// The actual Sound which is Stored on this Disk
        /// </summary>
        public SoundEffect SoundEffect;
        /// <summary>
        /// How long it take to play this SoundDisk from beginning to end
        /// </summary>
        public TimeSpan Duration;
        /// <summary>
        /// How long it take from the call of playing until it really plays the sound. Needs to be set manualy
        /// </summary>
        private TimeSpan delay;
        public TimeSpan Delay
        {
            set { delay = value; }
            get { return delay; }
        }

        private float speed = 1;
        /// <summary>
        /// the active playtime of the sounddisk
        /// start = full soundlength
        /// end = 0
        /// </summary>
        private float activeTimer;
        public float ActiveTimer
        {
            set { activeTimer = value; }
            get { return activeTimer; }
        }
        /// <summary>
        /// the active time of the delay
        /// 0 = no delay left, the sound starts playing or is already playing
        /// </summary>
        private float activeDelay;
        public float ActiveDelay
        {
            set { activeDelay = value; }
            get { return activeDelay; }
        }
        /// <summary>
        /// returns true, when the sound is playing
        /// </summary>
        private bool isPlaying;
        public bool IsPlaying
        {
            get { return isPlaying; }
        }
        /// <summary>
        /// returns true, when the sound is paused
        /// </summary>
        private bool isPaused;
        public bool IsPaused
        {
            get { return isPaused; }
        }
        /// <summary>
        /// returns true, when the sound is delaying
        /// </summary>
        private bool isDelaying;
        public bool IsDelaying
        {
            get { return isDelaying; }
        }
        /// <summary>
        /// initial method to tell the jukebox it has to be played in the next update of the Jukebox
        /// </summary>
        private bool forceToPlay;
        public bool ForceToPlay
        {
            set { forceToPlay = value; }
            get { return forceToPlay; }
        }
        /// <summary>
        /// a instance of a sound, which provides more functionallity like pause and resume
        /// </summary>
        private SoundEffectInstance soundInstance;
        /// <summary>
        /// Constructor to create a new Sounddisk
        /// </summary>
        public SoundDisk(String name, SoundType soundType, String soundPath, TimeSpan delay)
        {
            this.Name = name;
            this.SoundTyp = soundType;
            this.SoundPath = soundPath;
            this.Delay = delay;
        }
        /// <summary>
        /// Loads everything onm the Sounddsik and sets the durations of the sound
        /// </summary>
        /// <param name="content"></param>
        public void LoadSoundDisk(ContentManager content)
        {
            Content = content;
            SoundEffect = Content.Load<SoundEffect>(SoundPath);
            Duration = SoundEffect.Duration;
            ActiveDelay = Delay.Milliseconds;
            ActiveTimer = (float) Duration.TotalMilliseconds;
            activeSoundState = SoundState.Idle;
            if(SoundTyp == SoundType.Music)
            {
                soundInstance = SoundEffect.CreateInstance();
            }
            else if (SoundTyp == SoundType.Speach)
            {
                soundInstance = SoundEffect.CreateInstance();
            }
        }
        /// <summary>
        /// Plays the Sounddisk
        /// </summary>
        public void Play()
        {
            ForceToPlay = true;
        }

        public void Play(TimeSpan timeD, float speed)
        {
            ForceToPlay = true;
            Duration = timeD;
            this.speed = speed;
        }
        /// <summary>
        /// Stops the Sounddisk, you need to Press "Play again" if you want to "resume"
        /// </summary>
        public void Stop()
        {
            switch (SoundTyp)
            {
                case SoundType.FX:
                    Console.WriteLine("The Sound: " + Name + " is FXSound and doesn´t feature Stop");
                    break;
                case SoundType.Music:
                    activeSoundState = SoundState.Stopped;
                    break;
                case SoundType.Speach:
                    activeSoundState = SoundState.Stopped;
                    break;
            }
        }
        /// <summary>
        /// Pause the Song, calling "Resume" will play from this paused point
        /// </summary>
        public void Pause()
        {
            switch (SoundTyp)
            {
                case SoundType.FX:
                    Console.WriteLine("The Sound: " + Name + " is FXSound and doesn´t feature Pause");
                    break;
                case SoundType.Music:
                    activeSoundState = SoundState.Paused;
                    break;
                case SoundType.Speach:
                    activeSoundState = SoundState.Paused;
                    break;
            }
        }
        /// <summary>
        /// Resumes the sound if it was paused
        /// </summary>
        public void Resume()
        {
            switch (SoundTyp)
            {
                case SoundType.FX:
                    Console.WriteLine("The Sound: " + Name + " is FXSound and doesn´t feature Resume");
                    break;
                case SoundType.Music:
                    activeSoundState = SoundState.Resumed;
                    break;
                case SoundType.Speach:
                    activeSoundState = SoundState.Resumed;
                    break;
            }
        }
        /// <summary>
        /// The update of this SoundDisk
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // update the getters
            if (activeSoundState == SoundState.Playing || activeSoundState == SoundState.Resumed)
            {
                isPlaying = true;
            }
            else {isPlaying = false;}
            if (activeSoundState == SoundState.Paused)
            {
                isPaused = true;
            }
            else { isPaused = false; }
            if (activeSoundState == SoundState.Delaying)
            {
                isDelaying = true;
            }
            else { isDelaying = false;}



            switch (SoundTyp)
            {
                // updates if the Sounddisk has FX (no pause or stop functionality)
                case SoundType.FX:
                    if (Name == "Klick_1")
                    {
                        //Console.WriteLine(Name + " : " + IsPlaying + " : " + activeSoundState);
                    }
                    if (ForceToPlay && activeSoundState == SoundState.Idle)
                    {
                        if (activeDelay != 0)
                        {
                            activeSoundState = SoundState.Delaying;
                        }
                        else if (ActiveDelay == 0)
                        {
                            activeSoundState = SoundState.Playing;
                        }
                        ForceToPlay = false;
                    }
                    else if (activeSoundState == SoundState.Delaying && ActiveDelay > 0)
                    {
                        ActiveDelay -= gameTime.ElapsedGameTime.Milliseconds;
                    }
                    else if ((activeSoundState == SoundState.Delaying && ActiveDelay <= 0) || (activeSoundState == SoundState.Playing && ActiveTimer >= (float)Duration.TotalMilliseconds))
                    {
                        activeSoundState = SoundState.Playing;
                        ActiveDelay = 0;
                        SoundEffectInstance temp = SoundEffect.CreateInstance();
                        //temp.Pitch = speed;
                        temp.Play();
                        temp.Volume = Settings.Instance.MainVolume * Settings.Instance.FXVolume;
                        ActiveTimer -= 1;
                    }
                    else if (activeSoundState == SoundState.Playing && activeTimer > 0)
                    {
                        activeSoundState = SoundState.Playing;
                        ActiveTimer -= gameTime.ElapsedGameTime.Milliseconds;
                    }
                    else if (activeSoundState == SoundState.Playing && activeTimer <= 0)
                    {
                        ActiveTimer = (float)Duration.TotalMilliseconds;
                        activeDelay = (float)Delay.TotalMilliseconds;
                        activeSoundState = SoundState.Idle;
                    }
                    break;
                // updates the Sounddisk if it is from the type "music"
                case SoundType.Music:
                    //Console.WriteLine(ActiveTimer + ", " + soundInstance.State + ", " + Name + ", " + soundInstance.Volume);
                    soundInstance.Volume = Settings.Instance.MainVolume * Settings.Instance.MusicVolume;
                    if (activeSoundState == SoundState.Paused)
                    {
                        soundInstance.Pause();
                    }
                    else if (activeSoundState == SoundState.Resumed)
                    {
                        soundInstance.Resume();
                        activeSoundState = SoundState.Playing;
                    }
                    else if ( activeSoundState == SoundState.Stopped)
                    {
                        soundInstance.Stop();
                        ActiveTimer = (float) Duration.TotalMilliseconds;
                        activeDelay = (float) Delay.TotalMilliseconds;
                        activeSoundState = SoundState.Idle;
                    }
                    else if (ForceToPlay && activeSoundState == SoundState.Idle)
                    {
                        if (activeDelay != 0)
                        {
                            activeSoundState = SoundState.Delaying;
                        }
                        else if (ActiveDelay == 0)
                        {
                            activeSoundState = SoundState.Playing;
                        }
                        ForceToPlay = false;
                    }
                    else if (activeSoundState == SoundState.Delaying && ActiveDelay > 0)
                    {
                        ActiveDelay -= gameTime.ElapsedGameTime.Milliseconds;
                    }
                    else if ((activeSoundState == SoundState.Delaying && ActiveDelay <= 0) || (activeSoundState == SoundState.Playing && ActiveTimer == Duration.TotalMilliseconds))
                    {
                        activeSoundState = SoundState.Playing;
                        ActiveDelay = 0;
                        soundInstance.Play();
                        soundInstance.Volume = Settings.Instance.MainVolume * Settings.Instance.MusicVolume;
                        ActiveTimer -= 1;
                    }
                    else if (activeSoundState == SoundState.Playing && activeTimer > 0)
                    {
                        ActiveTimer -= gameTime.ElapsedGameTime.Milliseconds;
                        soundInstance.Volume = Settings.Instance.MainVolume * Settings.Instance.MusicVolume;
                    }
                    else if (activeSoundState == SoundState.Playing && activeTimer <= 0)
                    {
                        ActiveTimer = (float)Duration.TotalMilliseconds;
                        activeDelay = (float)Delay.TotalMilliseconds;
                        activeSoundState = SoundState.Idle;
                        soundInstance.Stop();
                    }
                    break;
                // updates the Sounddisk if it is from the type "speach"
                case SoundType.Speach:
                    //Console.WriteLine(ActiveTimer + ", " + soundInstance.State + ", " + Name + ", " + soundInstance.Volume);
                    soundInstance.Volume = Settings.Instance.MainVolume * Settings.Instance.MusicVolume;
                    if (activeSoundState == SoundState.Paused)
                    {
                        soundInstance.Pause();
                    }
                    else if (activeSoundState == SoundState.Resumed)
                    {
                        soundInstance.Resume();
                        activeSoundState = SoundState.Playing;
                    }
                    else if (activeSoundState == SoundState.Stopped)
                    {
                        soundInstance.Stop();
                        ActiveTimer = (float)Duration.TotalMilliseconds;
                        activeDelay = (float)Delay.TotalMilliseconds;
                        activeSoundState = SoundState.Idle;
                    }
                    else if (ForceToPlay && activeSoundState == SoundState.Idle)
                    {
                        if (activeDelay != 0)
                        {
                            activeSoundState = SoundState.Delaying;
                        }
                        else if (ActiveDelay == 0)
                        {
                            activeSoundState = SoundState.Playing;
                        }
                        ForceToPlay = false;
                    }
                    else if (activeSoundState == SoundState.Delaying && ActiveDelay > 0)
                    {
                        ActiveDelay -= gameTime.ElapsedGameTime.Milliseconds;
                    }
                    else if ((activeSoundState == SoundState.Delaying && ActiveDelay <= 0) || (activeSoundState == SoundState.Playing && ActiveTimer == Duration.TotalMilliseconds))
                    {
                        activeSoundState = SoundState.Playing;
                        ActiveDelay = 0;
                        soundInstance.Play();
                        soundInstance.Volume = Settings.Instance.MainVolume * Settings.Instance.MusicVolume;
                        ActiveTimer -= 1;
                    }
                    else if (activeSoundState == SoundState.Playing && activeTimer > 0)
                    {
                        ActiveTimer -= gameTime.ElapsedGameTime.Milliseconds;
                        soundInstance.Volume = Settings.Instance.MainVolume * Settings.Instance.MusicVolume;
                    }
                    else if (activeSoundState == SoundState.Playing && activeTimer <= 0)
                    {
                        ActiveTimer = Duration.Milliseconds;
                        activeDelay = Delay.Milliseconds;
                        activeSoundState = SoundState.Idle;
                        soundInstance.Stop();
                    }
                    break;
            }
        }
    }
}
