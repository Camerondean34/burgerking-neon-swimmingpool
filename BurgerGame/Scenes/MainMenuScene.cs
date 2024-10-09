using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public MainMenuScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _background = BurgerGame.Instance().CM().Load<Texture2D>("background");
            _backgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);
            _burgerTexture = BurgerGame.Instance().CM().Load<Texture2D>("3D Cheeseburger Texture");
            _burger = BurgerGame.Instance().CM().Load<Model>("3D Cheeseburger");

            _world = Matrix.CreateTranslation(new Vector3(0, -5.0f, 0));
            _view = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 50.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), (float)screenWidth / (float)screenHeight, 0.0000001f, 100000000f);
        }

        public void Draw(float pSeconds)
        {
            BurgerGame.Instance().GDM().GraphicsDevice.Clear(Color.CornflowerBlue);
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_background, _backgroundRect, Color.White);
            _SpriteBatch.End();
            Draw3DModel(_burger, _world, _view, _projection);
        }

        private void Draw3DModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
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
            _Controller.UpdateController(pSeconds);
            if (_Controller.IsPressed(Control.ESCAPE))
            {
                BurgerGame.Instance().SM().ChangeScene(null);
            }
            _world = Matrix.CreateRotationY(pSeconds) * _world;
        }
    }
}
