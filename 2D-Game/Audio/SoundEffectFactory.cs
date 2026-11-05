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
        private Dictionary<SoundName, SoundEffect> _soundEffects;
        private static SoundEffectFactory instance = new SoundEffectFactory();

        private AudioManager myAudioMananger;
        public static SoundEffectFactory Instance
        { get { return instance; } }

        private SoundEffectFactory() { }

        public void LoadSFX(ContentManager content, AudioManager audioManager)
        {
            myAudioMananger = audioManager;
            _soundEffects = new Dictionary<SoundName, SoundEffect>();
            _soundEffects.Add(SoundName.StickSwing, content.Load<SoundEffect>("SFX/test-sfx"));
            _soundEffects.Add(SoundName.MosquitoProjectile, content.Load<SoundEffect>("SFX/gunshottttt"));
            _soundEffects.Add(SoundName.SpiderProjectile, content.Load<SoundEffect>("SFX/gunshot-meme"));
        }
        public void PlaySoundEffect(SoundName soundName, bool canOverlap) //canOverlap true if can play the same SFX more than once at a time
        {
            if (_soundEffects.ContainsKey(soundName)) myAudioMananger.PlaySoundEffect(_soundEffects[soundName], canOverlap);
        }
    }
}
