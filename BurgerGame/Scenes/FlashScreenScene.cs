using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace BurgerPoolGame.Scenes
{
    public class FlashScreenScene : IScene
    {
        Texture2D _LogoTexture = null;
        SpriteBatch _SpriteBatch = null;
        Rectangle _LogoRect;
        float _SecondsLeft = 2.0f;

        public FlashScreenScene()
        {
            IGame game = BurgerGame.Instance();
            _LogoTexture = game.CM().Load<Texture2D>("logo");
            _SpriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);
            int screenWidth = game.GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = game.GDM().GraphicsDevice.Viewport.Height;
            int height = screenHeight / 2;
            int width = (int)(_LogoTexture.Width * (float)height / _LogoTexture.Height);
            int x = (screenWidth - width) / 2;
            int y = (screenHeight - height) / 2;
            _LogoRect = new Rectangle(x, y, width, height);
            LoadContent();
        }

        private bool _LoadingDone;

        void LoadContent()
        {
            var loading = Task.Factory.StartNew(() =>
            {
                // Load all your content here.
                // NOTE: Be sure to catch any exceptions in here and handle them.
                loadAllAssets();
                _LoadingDone = true;
            });
        }

        private void loadAllAssets()
        {
            IGame game = BurgerGame.Instance();
            // Load all assets for game here
            game.CM().Load<Texture2D>("logo");
            game.CM().Load<Texture2D>("background");
            game.CM().Load<Texture2D>("3D Cheeseburger Texture");
            game.CM().Load<Model>("3D Cheeseburger");
            
        }

        public void Update(float pSeconds)
        {
            _SecondsLeft -= pSeconds;

            if (_SecondsLeft <= 0.0f && _LoadingDone)
            {
                IGame game = BurgerGame.Instance();
                game.SM().ChangeScene(new MainMenuScene());

            }
        }
        public void Draw(float pSeconds)
        {
            BurgerGame.Instance().GDM().GraphicsDevice.Clear(Color.Black);
            _SpriteBatch.Begin();

            _SpriteBatch.Draw(_LogoTexture, _LogoRect, Color.White);

            _SpriteBatch.End();
        }
    }
}
