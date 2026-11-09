using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace _2D_Game
{
    /// <summary>
    /// UNUSED CLASS (redundant)
    /// </summary>
    public class GameGPS
{
        //a border might work better than this system
        public Vector2 Position {  get; private set; }
        public int CurrentRoomID { get; private set; } //room number
        public int PreviousRoomID { get; private set; }
        private int RoomWidth;
        private int RoomHeight;

        public GameGPS(Vector2 pos, int roomWidth, int roomHeight)
        {
            Position = pos;
            RoomWidth = roomWidth;
            RoomHeight = roomHeight;
        }
        private void CalculateRoomID()
        {
            PreviousRoomID = CurrentRoomID;
            int xIndex = (int)Position.X / RoomWidth + 1; //1, 2, 3
            int yIndex = Math.Abs(((int)Position.Y / RoomHeight)); // 0, 1, 2 going down
            int roomID = 0;
            if (xIndex > 0 && xIndex < 4 && yIndex >= 0 && yIndex <= 2) {
                roomID = xIndex + yIndex * 3; //TODO replace magic nums later
            }
            CurrentRoomID = roomID;
            //Debug.WriteLine(CurrentRoomID);
        }

        //TODO Update method for handling room update and changes
        public void Update(Vector2 pos)
        {
            Position = pos;
            CalculateRoomID();
            if (CurrentRoomID != PreviousRoomID) UpdateRoomSong();

        }
        private void UpdateRoomSong() {
            //so much magic 
            //TODO run a method from some set of room interace
            SongName roomSongTitle = SongName.Lost;
            switch (CurrentRoomID)//TODO update to appropriate song (rn just testing all)
            {
                case 7:
                    roomSongTitle = SongName.CozyFire;
                    break;
                case 6:
                    roomSongTitle = SongName.TheDead;
                    break;
                case 8:
                    roomSongTitle = SongName.CozyFireSR;
                    break;
                case 1:
                case 3:
                    roomSongTitle = SongName.TitleTheme;
                    break;
                case 4:
                case 5:
                    roomSongTitle = SongName.StarterRoomTheme;
                    break;
                case 9:
                    roomSongTitle = SongName.EndTheme;
                    break;
                case 2:
                    roomSongTitle = SongName.Lost;
                    break;
                default:
                    roomSongTitle = SongName.Lost;
                    break;
            }
            Playlist.Instance.ChangeSong(roomSongTitle, true);

        }
}
}
