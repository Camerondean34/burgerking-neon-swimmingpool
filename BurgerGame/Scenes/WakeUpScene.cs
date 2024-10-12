using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BurgerPoolGame.Scenes
{
    internal class WakeUpScene : IScene
    {
        private IController _Controller;
        private SpriteBatch _SpriteBatch;

        private Texture2D _Background;
        private Rectangle _BackgroundRect;

        private Texture2D _DialougeBox;
        private Rectangle _DialougeBoxRect;
        private SpriteFont _DialougeFont;
        private uint _DialougeIndex = 0;

        public WakeUpScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _Background = BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/WakeUp");
            _BackgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);

            _DialougeBox = BurgerGame.Instance().CM().Load<Texture2D>("DialougeBox");
            _DialougeBoxRect = new Rectangle(20, screenHeight - screenHeight / 4, screenWidth - 40, (screenHeight / 4) - 20);

            _DialougeFont = BurgerGame.Instance().CM().Load<SpriteFont>("DialougeFont");

            BurgerGame.Instance().SetMusic("GameMusic");
        }

        public void Update(float pSeconds)
        {
            if (_Controller != null)
            {
                _Controller.UpdateController(pSeconds);
                if (_Controller.IsPressed(Control.CLICK) && !_Controller.WasPressed(Control.CLICK))
                {
                    ++_DialougeIndex;
                    if (_DialougeIndex == 5)
                    {
                        _Background = BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/Bedroom");
                    }
                }
            }

            if (_DialougeIndex > 6)
            {
                BurgerGame.Instance().SM().ChangeScene(new ClothesScene());
            }
        }

        public void Draw(float pSeconds)
        {
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_Background, _BackgroundRect, Color.White);
            _SpriteBatch.Draw(_DialougeBox, _DialougeBoxRect, Color.White);
            _SpriteBatch.DrawString(_DialougeFont, GetDialouge(), new Vector2(40, _DialougeBoxRect.Y + 20), Color.Black);
            _SpriteBatch.End();
        }

        private string GetDialouge()
        {
            switch (_DialougeIndex)
            {
                case 0:
                    return "Mmmmnmn... amm...... burgers........\npickl........";
                case 1:
                    return "HUH!";
                case 2:
                    return "Oh no! I am going to be late for my new job.\nAt the Burger King.";
                case 3:
                    return "I need to get up!\nHow else am I going to pay the tuition for my computer science degree???\nWithout the big burger bucks.";
                case 5:
                    return "My room is a mess\nNow where did I put my uniform.....";
                case 6:
                    return "Oh of course!\nIn the clothes cupboard.";
                default:
                    return "...";
            }
        }
    }
}
