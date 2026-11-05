using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Channels;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace _2D_Game
{
    public class AudioManager
{
        //TODO fill with audio managing methods
        //gets songs from Playlist and SFX from SFXFactory?

        private readonly List<SoundEffectInstance> activeSoundEffectInstances;
        private Song currentSong;     
        private float songVolume;
        private float songVolumeNotch = .1f;
        private float previousSongVolume;
        public bool SongMuted {  get;  set; }

        public AudioManager(ContentManager content) {
            activeSoundEffectInstances = new List<SoundEffectInstance>();
            Playlist.Instance.LoadSongs(content, this);
            SoundEffectFactory.Instance.LoadSFX(content, this);
            songVolume = 1f;
            previousSongVolume = 1f;

        }
        public void PlaySong(Song song, bool loops)
        {
            //switch song being played to song 
            if (!song.Equals(currentSong)) 
            {
                MediaPlayer.Play(song);
                currentSong = song;
            }
            MediaPlayer.IsRepeating = loops;
        }
        public void StopSong() { 
            MediaPlayer.Stop();
        }
        public void PlaySoundEffect(SoundEffect soundEffect, bool canOverlap) 
        { 
            //play sound effect (also handle duplicate sfx in some way (don't play again if already playing?
            SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
            if (activeSoundEffectInstances.Contains(soundEffectInstance) && !canOverlap)
            {
                //don't play sound if cannot overlap with already existing instance
            }
            else //play soundeffect
            {
                soundEffectInstance.Play();
                if(!activeSoundEffectInstances.Contains(soundEffectInstance))activeSoundEffectInstances.Add(soundEffectInstance);
            }
        }

        public void ToggleMusicMute() {
            if (!SongMuted)
            {
                SongMuted = true;
                previousSongVolume = songVolume;
                songVolume = 0;
            }
            else
            {
                SongMuted = false;
                songVolume = previousSongVolume;
            }
            MediaPlayer.Volume = songVolume;
        }
        public void IncreaseMusicVolume() {
            float nextVolume = previousSongVolume + songVolumeNotch;
            if (nextVolume <= 1f)
            {
                songVolume = nextVolume;
                MediaPlayer.Volume = songVolume;
                previousSongVolume = songVolume;
            }
        }
        public void DecreaseMusicVolume()
        {
            float nextVolume = previousSongVolume - songVolumeNotch;
            if (nextVolume >= -0.1f)
            {
                songVolume = nextVolume;
                MediaPlayer.Volume = songVolume;
                previousSongVolume = songVolume;
            }
        }
        public void Update() 
        {
            //manage active SFX
            List<SoundEffectInstance> listOfFinishedSFX = new List<SoundEffectInstance>();
            foreach (SoundEffectInstance soundEffectInstance in activeSoundEffectInstances) { 
                if(soundEffectInstance.State  != SoundState.Playing) listOfFinishedSFX.Add(soundEffectInstance);
            }
            foreach (SoundEffectInstance soundEffectInstance in listOfFinishedSFX)
            {
                activeSoundEffectInstances.Remove(soundEffectInstance);
            }


        }
}
}
