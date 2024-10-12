using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace BurgerPoolGame.Scenes
{
    public class LoadingScene : IScene
    {
        Texture2D _LogoTexture = null;
        SpriteBatch _SpriteBatch = null;

        private Matrix _world;
        private Matrix _view;
        private Matrix _projection;

        private Model _burger;
        private Texture2D _burgerTexture;

        private SpriteFont _font;
        private Vector2 _fontPos;

        private Texture2D _loadingBox;
        private Texture2D _loadingBar;
        private Rectangle _loadingBoxRect;
        private Rectangle _loadingBarRect;

        const float _LoadingTime = 2.6f;
        float _SecondsLeft;

        public LoadingScene()
        {
            IGame game = BurgerGame.Instance();
            game.StopMusic();

            _LogoTexture = game.CM().Load<Texture2D>("logo");
            _SpriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);
            int screenWidth = game.GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = game.GDM().GraphicsDevice.Viewport.Height;

            _burgerTexture = BurgerGame.Instance().CM().Load<Texture2D>("3DModels/BurgerTexture");
            _burger = BurgerGame.Instance().CM().Load<Model>("3DModels/Burger");

            _world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            _view = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 50.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), (float)screenWidth / (float)screenHeight, 0.1f, 100f);

            _font = game.CM().Load<SpriteFont>("DialougeFont");
            _fontPos = _font.MeasureString("Loading...");
            _fontPos = new Vector2(screenWidth / 2 - (_fontPos.X / 2), screenHeight / 2 + _fontPos.Y);

            _loadingBox = game.CM().Load<Texture2D>("Minigame/NeonBurger");
            _loadingBar = game.CM().Load<Texture2D>("Minigame/Burger");
            _loadingBoxRect = new Rectangle(screenWidth / 2 - ((2 * screenWidth / 3) / 2), screenHeight * 8 / 10, 2 * screenWidth / 3, screenHeight / 10);
            _loadingBarRect = _loadingBoxRect;

            _SecondsLeft = _LoadingTime;
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
            for (int i = 1; i <= 150; ++i)
            {
                game.CM().Load<Texture2D>("Video/ezgif-frame-" + i.ToString("D3"));
            }
        }

        public void Update(float pSeconds)
        {
            _world = Matrix.CreateRotationY(pSeconds) * _world;

            _SecondsLeft -= pSeconds;
            if (_SecondsLeft < 0.0f)
                _SecondsLeft = 0.0f;
            _loadingBarRect.Width = (int)(_loadingBoxRect.Width * (1 - _SecondsLeft / _LoadingTime));

            if (_SecondsLeft <= 0.0f && _LoadingDone)
            {
                IGame game = BurgerGame.Instance();
                game.SM().ChangeScene(new CarDriveScene());

            }
        }
        public void Draw(float pSeconds)
        {
            BurgerGame.Instance().GDM().GraphicsDevice.Clear(Color.Black);
            _SpriteBatch.Begin();
            _SpriteBatch.DrawString(_font, "Loading...", _fontPos, Color.White);
            _SpriteBatch.Draw(_loadingBox, _loadingBoxRect, Color.White);
            _SpriteBatch.Draw(_loadingBar, _loadingBarRect, Color.White);
            _SpriteBatch.End();
            Draw3DModel(_burger, _world, _view, _projection);
        }

        private void Draw3DModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            BurgerGame.Instance().GDM().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.TextureEnabled = true;
                    effect.Texture = _burgerTexture;
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }
    }
}
