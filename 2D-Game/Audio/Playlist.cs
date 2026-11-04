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
        private Dictionary<SongName, Song> _playlist;
        private static Playlist instance = new Playlist();

        private AudioManager myAudioMananger;
        public static Playlist Instance
        { get { return instance; } }

        private Playlist() { }

        public void LoadSongs(ContentManager content, AudioManager audioManager) {
            myAudioMananger = audioManager;
            _playlist = new Dictionary<SongName, Song>();
            _playlist.Add(SongName.CozyFire, content.Load<Song>("Music/cozyFire"));
            _playlist.Add(SongName.CozyFireSR, content.Load<Song>("Music/cozyFire(slow+reverb)"));
            _playlist.Add(SongName.EndTheme, content.Load<Song>("Music/endThemeSample1"));
            _playlist.Add(SongName.Lost, content.Load<Song>("Music/lost"));
            _playlist.Add(SongName.TheDead, content.Load<Song>("Music/theDead"));
            _playlist.Add(SongName.StarterRoomTheme, content.Load<Song>("Music/titleSample1"));
            _playlist.Add(SongName.TitleTheme, content.Load<Song>("Music/titleTheme"));
            _playlist.Add(SongName.OminousLair, content.Load<Song>("Music/ominousLair"));
        }
        public void ChangeSong(SongName songName, bool loops) {
            if (_playlist.ContainsKey(songName)) myAudioMananger.PlaySong(_playlist[songName], loops);
            else myAudioMananger.StopSong();
        }
    }
}
