using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BurgerPoolGame.Scenes
{
    public class MainMenuScene : IScene
    {
        private IController _Controller;
        public MainMenuScene()
        {
            _Controller = BurgerGame.Instance().GetController();
        }

        public void Draw(float pSeconds)
        {
            BurgerGame.Instance().GDM().GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        public void Update(float pSeconds)
        {
            _Controller.UpdateController(pSeconds);
            if (_Controller.IsPressed(Control.ESCAPE))
            {
                BurgerGame.Instance().SM().ChangeScene(null);
            }
        }
    }
}
