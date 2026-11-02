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
    public class SoundEffectFactory
    {
        private Dictionary<string, SoundEffect> _soundEffects;
        private static SoundEffectFactory instance = new SoundEffectFactory();

        private AudioManager myAudioMananger;
        public static SoundEffectFactory Instance
        { get { return instance; } }

        private SoundEffectFactory() { }

        public void LoadSFX(ContentManager content, AudioManager audioManager)
        {
            myAudioMananger = audioManager;
            _soundEffects = new Dictionary<string, SoundEffect>();
            //_soundEffects.Add("sound name", content.Load<SoundEffect>("file in content"));
        }
        public void ChangeSong(string soundName)
        {
            if (_soundEffects.ContainsKey(soundName)) myAudioMananger.PlaySoundEffect(_soundEffects[soundName]);
        }
    }
}
