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
        private float previousSongVolume;
        public bool SongMuted {  get;  set; }

        public AudioManager(ContentManager content) {
            activeSoundEffectInstances = new List<SoundEffectInstance>();
            Playlist.Instance.LoadSongs(content, this);
            SoundEffectFactory.Instance.LoadSFX(content, this);

        }
        public void PlaySong(Song song)
        {
            //switch song being played to song (fade out previous?)
        }
        public void PlaySoundEffect(SoundEffect soundEffect) 
        { 
            //play sound effect (also handle duplicate sfx in some way (don't play again if already playing?
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
}
}
