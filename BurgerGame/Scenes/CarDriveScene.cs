using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerPoolGame.Scenes
{
    internal class CarDriveScene : IScene
    {
        private const int _FrameCount = 150;
        private const int _FPS = 10;
        private Texture2D[] _VideoFrame;
        private Rectangle _VideoRect;

        private IController _Controller;

        private SpriteBatch _SpriteBatch;

        public CarDriveScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();
            _VideoRect = new Rectangle(0, 0, BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width, BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height);
            _VideoFrame = new Texture2D[_FrameCount];
            for (int i = 1; i <= _FrameCount; ++i)
            {
                _VideoFrame[i - 1] = BurgerGame.Instance().CM().Load<Texture2D>("Video/ezgif-frame-" + i.ToString("D3"));
            }
        }

        private float _elapsed = 0;
        private int _currentFrame = 0;
        public void Update(float pSeconds)
        {
            _elapsed += pSeconds;
            if (_elapsed > 1.0f / _FPS)
            {
                _elapsed = 0;
                _currentFrame += 1;
                if (_currentFrame >= _FrameCount)
                {
                    _currentFrame = 0;
                    BurgerGame.Instance().SM().ChangeScene(new MiniGameScene());
                }
            }
            _Controller.UpdateController(pSeconds);
            if (_Controller.IsPressed(Control.CLICK) && !_Controller.WasPressed(Control.CLICK))
            {
                BurgerGame.Instance().SM().ChangeScene(new MiniGameScene());
            }
        }

        public void Draw(float pSeconds)
        {
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_VideoFrame[_currentFrame], _VideoRect, Color.White);
            _SpriteBatch.End();
        }

    }
}
