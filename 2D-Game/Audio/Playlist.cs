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
            _playlist.Add("cozy fire", content.Load<Song>("Music/cozyFire"));
            _playlist.Add("cozy fire SR", content.Load<Song>("Music/cozyFire(slow+reverb)"));
            _playlist.Add("end theme", content.Load<Song>("Music/endThemeSample1"));
            _playlist.Add("lost", content.Load<Song>("Music/lost"));
            _playlist.Add("the dead", content.Load<Song>("Music/theDead"));
            _playlist.Add("theme 2", content.Load<Song>("Music/titleSample1"));
            _playlist.Add("theme 1", content.Load<Song>("Music/titleTheme"));
        }
        public void ChangeSong(string songName, bool loops) {
            if (_playlist.ContainsKey(songName)) myAudioMananger.PlaySong(_playlist[songName], loops);
        }
    }
}
