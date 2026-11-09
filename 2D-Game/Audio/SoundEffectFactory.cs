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
            _soundEffects.Add(SoundName.StickSwing, content.Load<SoundEffect>("SFX/stickswing"));
            _soundEffects.Add(SoundName.MosquitoProjectile, content.Load<SoundEffect>("SFX/gunshottttt"));
            _soundEffects.Add(SoundName.SpiderProjectile, content.Load<SoundEffect>("SFX/gunshot-meme"));
            _soundEffects.Add(SoundName.Boomerang, content.Load<SoundEffect>("SFX/boomerang"));
            _soundEffects.Add(SoundName.GhostWail, content.Load<SoundEffect>("SFX/ghostAttack"));
            _soundEffects.Add(SoundName.Explosion, content.Load<SoundEffect>("SFX/explosion"));
            _soundEffects.Add(SoundName.MosquitoBuzz, content.Load<SoundEffect>("SFX/mosquito1"));
            _soundEffects.Add(SoundName.Pickup, content.Load<SoundEffect>("SFX/Pickup"));
            _soundEffects.Add(SoundName.PlayerDamage, content.Load<SoundEffect>("SFX/playerDamage"));
            _soundEffects.Add(SoundName.Punch, content.Load<SoundEffect>("SFX/punch"));
            _soundEffects.Add(SoundName.Dialogue, content.Load<SoundEffect>("SFX/speak"));
            _soundEffects.Add(SoundName.SpiderHiss, content.Load<SoundEffect>("SFX/spiderNoise"));
            _soundEffects.Add(SoundName.ThrowItem, content.Load<SoundEffect>("SFX/throw"));
        }
        public void PlaySoundEffect(SoundName soundName, bool canOverlap) //canOverlap true if can play the same SFX more than once at a time
        {
            if (_soundEffects.ContainsKey(soundName)) myAudioMananger.PlaySoundEffect(_soundEffects[soundName], canOverlap);
        }
    }
}
