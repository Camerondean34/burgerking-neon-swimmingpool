using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BurgerPoolGame.Scenes
{
    internal class ChoiceScene : IScene
    {
        private SpriteBatch _SpriteBatch;
        private IController _Controller;

        private Texture2D _Background;
        private Rectangle _BackgroundRect;

        private Texture2D _DialougeBox;
        private Rectangle _DialougeBoxRect;
        private SpriteFont _DialougeFont;

        private Button _SpoonsButton;
        private Button _SwimmingButton;
        private Button _SpidersButton;
        private Button _BurgerButton;

        public ChoiceScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _Background = BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/Bedroom");
            _BackgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);

            _DialougeBox = BurgerGame.Instance().CM().Load<Texture2D>("DialougeBox");
            _DialougeBoxRect = new Rectangle(20, screenHeight - screenHeight / 4, screenWidth - 40, (screenHeight / 4) - 20);
            _DialougeFont = BurgerGame.Instance().CM().Load<SpriteFont>("DialougeFont");

            int buttonWidth = screenWidth / 3;
            int buttonHeight = screenHeight / 5;

            _SpoonsButton = new Button(_DialougeBox, _DialougeBox,
                new Rectangle(buttonWidth / 3, buttonHeight / 2, buttonWidth, buttonHeight), Color.Black, Spoons);
            _SwimmingButton = new Button(_DialougeBox, _DialougeBox,
                new Rectangle(2 * buttonWidth / 3 + buttonWidth, buttonHeight / 2, buttonWidth, buttonHeight), Color.Black, Swimming);
            _SpidersButton = new Button(_DialougeBox, _DialougeBox,
                new Rectangle(buttonWidth / 3, buttonHeight * 2, buttonWidth, buttonHeight), Color.Black, Spids);
            _BurgerButton = new Button(_DialougeBox, _DialougeBox,
                new Rectangle(2 * buttonWidth / 3 + buttonWidth, buttonHeight * 2, buttonWidth, buttonHeight), Color.Black, Burger);

            BurgerGame.Instance().SetMusic("GameMusic");
        }
        private void Spoons()
        {
            BurgerGame.Instance().SM().ChangeScene(new WeatherspoonsScene());
        }

        private void Swimming()
        {
            BurgerGame.Instance().SM().ChangeScene(null);
        }

        private void Spids()
        {
            BurgerGame.Instance().SM().ChangeScene(null);
        }

        private void Burger()
        {
            BurgerGame.Instance().SM().ChangeScene(null);
        }

        public void Draw(float pSeconds)
        {
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_Background, _BackgroundRect, Color.White);
            _SpriteBatch.Draw(_DialougeBox, _DialougeBoxRect, Color.White);
            _SpriteBatch.DrawString(_DialougeFont, "(Yay! It's Saturday, and youre not working today... What will you do?)", new Vector2(40, _DialougeBoxRect.Y + 20), Color.Black);
            _SpoonsButton.Draw(_SpriteBatch);
            _SwimmingButton.Draw(_SpriteBatch);
            _SpidersButton.Draw(_SpriteBatch);
            _BurgerButton.Draw(_SpriteBatch);
            _SpriteBatch.DrawString(_DialougeFont, "Go to the Wetherspoons on campus\r\nand get way too drunk\r\non cocktail pitchers",
                new Vector2(_SpoonsButton.Rect.X + 10, _SpoonsButton.Rect.Y + 10), Color.Black);
            _SpriteBatch.DrawString(_DialougeFont, "Check out the Burger King\r\nEmployee Swimmming Pool\r\nand try to get some exercise in",
                new Vector2(_SwimmingButton.Rect.X + 10, _SwimmingButton.Rect.Y + 10), Color.Black);
            _SpriteBatch.DrawString(_DialougeFont, "Have a night out at Spiders\r\nand pretend that you\r\nknow all the songs",
                new Vector2(_SpidersButton.Rect.X + 10, _SpidersButton.Rect.Y + 10), Color.Black);
            _SpriteBatch.DrawString(_DialougeFont, "Head to Burger King\r\nand enjoy your staff discount",
                new Vector2(_BurgerButton.Rect.X + 10, _BurgerButton.Rect.Y + 10), Color.Black);

            _SpriteBatch.End();
        }

        public void Update(float pSeconds)
        {
            MouseState cursor = Mouse.GetState();
            _SpoonsButton.Update(cursor.X, cursor.Y);
            _SwimmingButton.Update(cursor.X, cursor.Y);
            _SpidersButton.Update(cursor.X, cursor.Y);
            _BurgerButton.Update(cursor.X, cursor.Y);

            _Controller.UpdateController(pSeconds);
            if (!_Controller.IsPressed(Control.CLICK) && _Controller.WasPressed(Control.CLICK))
            {
                if (_SpoonsButton.Selected)
                    _SpoonsButton.PressButton();
                else if (_SwimmingButton.Selected)
                    _SwimmingButton.PressButton();
                else if (_SpidersButton.Selected)
                    _SpidersButton.PressButton();
                else if (_BurgerButton.Selected)
                    _BurgerButton.PressButton();
            }
        }
    }
}
