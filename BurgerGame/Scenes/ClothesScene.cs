using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BurgerPoolGame.Scenes
{
    internal class ClothesScene : IScene
    {
        private IController _Controller;
        private SpriteBatch _SpriteBatch;

        private Texture2D _Background;
        private Rectangle _BackgroundRect;

        private Rectangle _CupboardRect;

        private Texture2D _Clothes;
        private Rectangle _ClothesRect;
        private bool _ClothesSelected;

        private Texture2D _handOpen;
        private Texture2D _handGrab;
        private Rectangle _handRect;
        private bool _handClosed;

        private Texture2D _DialougeBox;
        private Rectangle _DialougeBoxRect;
        private SpriteFont _DialougeFont;
        private string _DialougeText = string.Empty;

        private float _SecondsLeft = 15.0f;

        public ClothesScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _Background = BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/FullCloset");
            _BackgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);

            _CupboardRect = new Rectangle(screenWidth / 2, screenHeight / 12, screenWidth / 6, screenHeight / 2);

            _Clothes = BurgerGame.Instance().CM().Load<Texture2D>("Uniform");
            _ClothesRect = new Rectangle(0, 0, screenWidth / 5, screenWidth / 5);

            _handOpen = BurgerGame.Instance().CM().Load<Texture2D>("Hands/Open");
            _handGrab = BurgerGame.Instance().CM().Load<Texture2D>("Hands/Grab");
            _handRect = new Rectangle(0, 0, screenWidth / 5, screenHeight / 2);

            _DialougeBox = BurgerGame.Instance().CM().Load<Texture2D>("DialougeBox");
            _DialougeBoxRect = new Rectangle(20, screenHeight - screenHeight / 4, screenWidth - 40, (screenHeight / 4) - 20);
            _DialougeFont = BurgerGame.Instance().CM().Load<SpriteFont>("DialougeFont");

            _ClothesSelected = false;
        }

        public void Update(float pSeconds)
        {
            MouseState cursor = Mouse.GetState();
            _handRect.X = cursor.X - (_handRect.Width / 2);
            _handRect.Y = cursor.Y - (_handRect.Height / 3);
            _ClothesRect.X = cursor.X - (_ClothesRect.Width / 2);
            _ClothesRect.Y = cursor.Y - (_ClothesRect.Height / 2);

            _Controller.UpdateController(pSeconds);

            _SecondsLeft -= pSeconds;
            if (_SecondsLeft > 0)
            {
                _handClosed = _Controller.IsPressed(Control.CLICK);
                if (!_ClothesSelected && _handClosed && !_Controller.WasPressed(Control.CLICK))
                {
                    if (_CupboardRect.Contains(cursor.X, cursor.Y))
                    {
                        _ClothesSelected = true;
                        _Background = BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/EmptyCloset");
                        _SecondsLeft = 0.0f;
                    }
                }
            }
            else
            {
                if (_DialougeText == string.Empty)
                {
                    if (_ClothesSelected)
                    {
                        _DialougeText = "I look great! Let's go!";
                    }
                    else
                    {
                        _DialougeText = "Oh well! Who needs uniforms anyways?";
                    }
                }

                if (_Controller.IsPressed(Control.CLICK) && !_Controller.WasPressed(Control.CLICK))
                {
                    BurgerGame.Instance().SM().ChangeScene(new LoadingScene());
                }
            }
        }

        public void Draw(float pSeconds)
        {
            BurgerGame.Instance().GDM().GraphicsDevice.Clear(Color.CornflowerBlue);
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_Background, _BackgroundRect, Color.White);
            if (_ClothesSelected)
                _SpriteBatch.Draw(_Clothes, _ClothesRect, Color.White);
            if (_handClosed)
                _SpriteBatch.Draw(_handGrab, _handRect, Color.White);
            else
                _SpriteBatch.Draw(_handOpen, _handRect, Color.White);
            if (_DialougeText != string.Empty)
            {
                _SpriteBatch.Draw(_DialougeBox, _DialougeBoxRect, Color.White);
                _SpriteBatch.DrawString(_DialougeFont, _DialougeText, new Vector2(40, _DialougeBoxRect.Y + 20), Color.Black);
            }
            _SpriteBatch.End();
        }
    }
}
