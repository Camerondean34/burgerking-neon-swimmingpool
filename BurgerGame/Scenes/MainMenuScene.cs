using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BurgerPoolGame.Scenes
{
    public class MainMenuScene : IScene
    {
        private IController _Controller;
        private SpriteBatch _SpriteBatch;

        // 3D Drawing Matricies
        private Matrix _world;
        private Matrix _view;
        private Matrix _projection;

        private Model _burger;
        private Texture2D _burgerTexture;

        private Texture2D _background;
        private Rectangle _backgroundRect;

        private Button _startButton;
        private Button _exitButton;

        public MainMenuScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _background = BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/MainMenu");
            _backgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);

            _burgerTexture = BurgerGame.Instance().CM().Load<Texture2D>("3DModels/BurgerTexture");
            _burger = BurgerGame.Instance().CM().Load<Model>("3DModels/Burger");

            _startButton = new Button(BurgerGame.Instance().CM().Load<Texture2D>("StartButton"),
                BurgerGame.Instance().CM().Load<Texture2D>("StartButtonPressed"),
                new Rectangle(2 * screenWidth / 7, 2 * screenHeight / 5, screenWidth / 7, screenHeight / 5), Color.White,
                StartGame);
            _exitButton = new Button(BurgerGame.Instance().CM().Load<Texture2D>("ExitButton"),
                BurgerGame.Instance().CM().Load<Texture2D>("ExitButtonPressed"),
                new Rectangle(4 * screenWidth / 7, 2 * screenHeight / 5, screenWidth / 7, screenHeight / 5), Color.White,
                ExitGame);

            _world = Matrix.CreateTranslation(new Vector3(-50.0f, -10.0f, 0));
            _view = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 100.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), (float)screenWidth / (float)screenHeight, 0.1f, 100f);
        }

        private void ExitGame()
        {
            BurgerGame.Instance().SM().ChangeScene(null);
        }

        private void StartGame()
        {
            BurgerGame.Instance().SM().ChangeScene(new WakeUpScene(), false);
        }

        public void Draw(float pSeconds)
        {
            BurgerGame.Instance().GDM().GraphicsDevice.Clear(Color.CornflowerBlue);
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_background, _backgroundRect, Color.White);
            _startButton.Draw(_SpriteBatch);
            _exitButton.Draw(_SpriteBatch);
            _SpriteBatch.End();
            Draw3DModel(_burger, _world, _view, _projection);
            Draw3DModel(_burger, _world * Matrix.CreateTranslation(new Vector3(100.0f, 0.0f, 0.0f)), _view, _projection);
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

        public void Update(float pSeconds)
        {
            _world = Matrix.CreateRotationY(pSeconds) * _world;

            MouseState cursor = Mouse.GetState();
            _startButton.Update(cursor.X, cursor.Y);
            _exitButton.Update(cursor.X, cursor.Y);

            _Controller.UpdateController(pSeconds);
            if (!_Controller.IsPressed(Control.CLICK) && _Controller.WasPressed(Control.CLICK))
            {
                if (_startButton.Selected)
                    _startButton.PressButton();
                else if (_exitButton.Selected)
                    _exitButton.PressButton();
            }
        }
    }
}
