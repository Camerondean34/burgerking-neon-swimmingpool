using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace BurgerPoolGame
{
    public class SoundManager
    {
        private Dictionary<string, SoundEffect> mSoundEffects = null;

        public SoundManager()
        {
            mSoundEffects = new Dictionary<string, SoundEffect>();
        }

        public void Add(string pName)
        {
            SoundEffect soundEffect = BurgerGame.Instance().CM().Load<SoundEffect>(pName);
            mSoundEffects.Add(pName, soundEffect);
        }

        public SoundEffectInstance GetSoundEffectInstance(string pName)
        {
            return mSoundEffects[pName].CreateInstance();
        }
    }
}
