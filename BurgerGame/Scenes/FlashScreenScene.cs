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
            game.CM().Load<Texture2D>("Logo");

            game.CM().Load<Texture2D>("Backgrounds/Resturant");
            game.CM().Load<Texture2D>("Backgrounds/RestaurantInside");
            game.CM().Load<Texture2D>("Backgrounds/Pool");
            game.CM().Load<Texture2D>("Backgrounds/Bedroom");
            game.CM().Load<Texture2D>("Backgrounds/EmptyCloset");
            game.CM().Load<Texture2D>("Backgrounds/FullCloset");
            game.CM().Load<Texture2D>("Backgrounds/WakeUp");
            game.CM().Load<Texture2D>("Backgrounds/Kitchen");
            game.CM().Load<Texture2D>("Backgrounds/KitchenStove");
            game.CM().Load<Texture2D>("Backgrounds/Counter");
            game.CM().Load<Texture2D>("Backgrounds/Weatherspoons");
            game.CM().Load<Texture2D>("Backgrounds/Spiders");
            game.CM().Load<Texture2D>("Backgrounds/NEXTDAY");
            game.CM().Load<Texture2D>("Backgrounds/PoolAttack1");
            game.CM().Load<Texture2D>("Backgrounds/PoolAttack2");
            game.CM().Load<Texture2D>("Backgrounds/PoolAttack3");
            game.CM().Load<Texture2D>("Backgrounds/Rave");

            game.CM().Load<Texture2D>("Characters/BurgerKing");
            game.CM().Load<Texture2D>("Characters/Darryl");
            game.CM().Load<Texture2D>("Characters/Michelle");
            game.CM().Load<Texture2D>("Characters/Number15");

            game.CM().Load<Model>("3DModels/Burger");
            game.CM().Load<Texture2D>("3DModels/BurgerTexture");
            game.CM().Load<Model>("3DModels/Crown");
            game.CM().Load<Texture2D>("3DModels/BK_Color");

            game.CM().Load<Texture2D>("Hands/Grab");
            game.CM().Load<Texture2D>("Hands/Open");
            game.CM().Load<Texture2D>("Hands/Gun");

            game.CM().Load<Texture2D>("Minigame/Burger");
            game.CM().Load<Texture2D>("Minigame/NeonBurger");
            game.CM().Load<Texture2D>("Minigame/Explosion");
            game.CM().Load<Texture2D>("Minigame/Crosshair");
            game.CM().Load<Texture2D>("Minigame/EmptyClip");
            game.CM().Load<Texture2D>("Minigame/OneClip");
            game.CM().Load<Texture2D>("Minigame/TwoClip");
            game.CM().Load<Texture2D>("Minigame/ThreeClip");
            game.CM().Load<Texture2D>("Minigame/FourClip");
            game.CM().Load<Texture2D>("Minigame/FullClip");

            game.CM().Load<Texture2D>("Spatula");
            game.CM().Load<Texture2D>("Uniform");

            game.CM().Load<Texture2D>("StartButton");
            game.CM().Load<Texture2D>("StartButtonPressed");
            game.CM().Load<Texture2D>("ExitButton");
            game.CM().Load<Texture2D>("ExitButtonPressed");

            game.CM().Load<Texture2D>("DialougeBox");
            game.CM().Load<SpriteFont>("DialougeFont");

            game.GetSoundManager().Add("GameMusic");
            game.GetSoundManager().Add("Crash");
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
