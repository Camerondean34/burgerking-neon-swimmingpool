using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BurgerPoolGame.Scenes
{
    public class MovingBurgerScene : IScene
    {
        private IController _Controller;
        private SpriteBatch _SpriteBatch;

        // 3D Drawing Matricies
        private Matrix _world;
        private Matrix _view;
        private Matrix _projection;

        private Model _burger;
        private Texture2D _burgerTexture;
        private Vector3 _burgerPos;

        private Texture2D _background;
        private Rectangle _backgroundRect;

        private Texture2D _handOpen;
        private Texture2D _handGrab;
        private Rectangle _handRect;

        public MovingBurgerScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _background = BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/Resturant");
            _backgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);

            _handOpen = BurgerGame.Instance().CM().Load<Texture2D>("Hands/Open");
            _handGrab = BurgerGame.Instance().CM().Load<Texture2D>("Hands/Grab");
            _handRect = new Rectangle(0, 0, screenWidth / 5, screenHeight / 2);

            _burgerTexture = BurgerGame.Instance().CM().Load<Texture2D>("3DModels/BurgerTexture");
            _burger = BurgerGame.Instance().CM().Load<Model>("3DModels/Burger");
            _burgerPos = new Vector3(0, 0, 0);

            _world = Matrix.CreateTranslation(_burgerPos);
            _view = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 50.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), (float)screenWidth / (float)screenHeight, 0.1f, 100f);
        }

        public void Draw(float pSeconds)
        {
            BurgerGame.Instance().GDM().GraphicsDevice.Clear(Color.CornflowerBlue);
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_background, _backgroundRect, Color.White);
            _SpriteBatch.End();
            Draw3DModel(_burger, _world, _view, _projection);
            _SpriteBatch.Begin();
            if (_Controller.IsPressed(Control.CLICK))
                _SpriteBatch.Draw(_handGrab, _handRect, Color.White);
            else
                _SpriteBatch.Draw(_handOpen, _handRect, Color.White);
            _SpriteBatch.End();
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

        private Ray GetMouseRay(int msX, int msY)
        {
            Vector3 nearScreenPoint = new Vector3(msX, msY, 0);
            Vector3 farScreenPoint = new Vector3(msX, msY, 1);
            Vector3 nearWorldPoint = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Unproject(nearScreenPoint,
                _projection, _view, Matrix.CreateTranslation(new Vector3(0, 0, 0)));
            Vector3 farWorldPoint = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Unproject(farScreenPoint,
                _projection, _view, Matrix.CreateTranslation(new Vector3(0, 0, 0)));

            Vector3 direction = farWorldPoint - nearWorldPoint;
            direction.Normalize();

            return new Ray(nearWorldPoint, direction);
        }

        private float? _rayLength;
        private Vector3 _burgerDifference;
        public void Update(float pSeconds)
        {
            _Controller.UpdateController(pSeconds);
            if (_Controller.IsPressed(Control.ESCAPE) && !_Controller.WasPressed(Control.ESCAPE))
            {
                BurgerGame.Instance().SM().ChangeScene(null);
            }
            if (_Controller.IsPressed(Control.ENTER))
            {
                BurgerGame.Instance().SM().ChangeScene(new CarDriveScene(), false);
            }

            MouseState cursor = Mouse.GetState();
            _handRect.X = cursor.X - (_handRect.Width / 2);
            _handRect.Y = cursor.Y - (_handRect.Height / 3);
            if (_Controller.IsPressed(Control.CLICK) && !_Controller.WasPressed(Control.CLICK))
            {
                Ray mouseRay = GetMouseRay(cursor.X, cursor.Y);
                BoundingBox burgerBounds = new BoundingBox(_burgerPos - new Vector3(5.0f, 0.0f, 5.0f), _burgerPos + new Vector3(5.0f, 10.0f, 5.0f));
                _rayLength = mouseRay.Intersects(burgerBounds);
                if (_rayLength != null)
                    _burgerDifference = _burgerPos - (mouseRay.Position + mouseRay.Direction * (float)_rayLength);
            }
            else if (_Controller.IsPressed(Control.CLICK) && _rayLength != null)
            {
                Ray mouseRay = GetMouseRay(cursor.X, cursor.Y);
                Vector3 clickPos = mouseRay.Position + mouseRay.Direction * (float)_rayLength;
                _burgerPos = clickPos + _burgerDifference;
                _world = Matrix.CreateTranslation(_burgerPos);
            }
            _world = Matrix.CreateRotationY(pSeconds) * _world;
        }
    }
}
