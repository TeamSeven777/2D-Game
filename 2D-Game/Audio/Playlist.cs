using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class Playlist
{
        private Dictionary<string, Song> _playlist;
        private static Playlist instance = new Playlist();

        private AudioManager myAudioMananger;
        public static Playlist Instance
        { get { return instance; } }

        private Playlist() { }

        public void LoadSongs(ContentManager content, AudioManager audioManager) {
            myAudioMananger = audioManager;
            _playlist = new Dictionary<string, Song>();
            //_playlist.Add("song name", content.Load<Song>("file in content"));
        }
        public void ChangeSong(string songName) {
            if (_playlist.ContainsKey(songName)) myAudioMananger.PlaySong(_playlist[songName]);
        }
    }
}
