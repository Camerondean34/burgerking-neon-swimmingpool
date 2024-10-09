using System.Collections.Generic;
using System.Linq;

namespace BurgerPoolGame.Scenes
{
    public class SceneManager
    {
        private List<IScene> _Scenes;

        public SceneManager()
        {
            _Scenes = new List<IScene>();
        }

        private void Push(IScene p_Scene)
        {
            _Scenes.Add(p_Scene);
        }

        public void ChangeScene(IScene pNextScene, bool replaceCurrent = true)
        {
            if (pNextScene == null)
            {
                pNextScene = Previous;
            }
            if (replaceCurrent)
            {
                Pop();
            }
            if (pNextScene != null)
            {
                _Scenes.Add(pNextScene);
            }
            else // If there is no scene to transition to exit the game
            {
                BurgerGame game = (BurgerGame)BurgerGame.Instance();
                game.Exit();
            }
        }

        private void Pop()
        {
            if (_Scenes.Count > 0)
            {
                _Scenes.RemoveAt(_Scenes.Count - 1);
            }
        }

        public IScene Top
        {
            get
            {
                if (_Scenes.Count > 0)
                {
                    return _Scenes.Last();
                }
                return null;
            }
        }
        public IScene Previous
        {
            get
            {
                if (_Scenes.Count > 1)
                {
                    return _Scenes[_Scenes.Count - 2];
                }
                return null;
            }
        }

        public void Update(float pSeconds)
        {
            if (_Scenes.Count > 0)
            {
                Top.Update(pSeconds);
            }
        }

        public void Draw(float pSeconds)
        {
            if (_Scenes.Count > 0)
            {
                Top.Draw(pSeconds);
            }
        }
    }
}
