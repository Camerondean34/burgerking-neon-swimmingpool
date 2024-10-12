using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using BurgerPoolGame.Scenes;

namespace BurgerPoolGame
{
    public interface IGame
    {
        SoundManager GetSoundManager();
        SceneManager SM();
        ContentManager CM();
        GraphicsDeviceManager GDM();
        IController GetController();

        void StopMusic();
        void SetMusic(string pMusicName);

        void Exit();
    }
}
