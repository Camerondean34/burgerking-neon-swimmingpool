using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BurgerPoolGame.Scenes
{
    internal class EndingScene : IScene
    {
        private IController _Controller;
        private SpriteBatch _SpriteBatch;

        private Rectangle _BackgroundRect;

        private List<Texture2D> _Backgrounds;

        private Texture2D _CharacterTexture;
        private Rectangle _CharacterRect;

        private Texture2D _DialougeBox;
        private Rectangle _DialougeBoxRect;
        private Rectangle _NameBox;
        private SpriteFont _DialougeFont;
        private int _DialougeIndex = -1;

        public EndingScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _BackgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);
            _Backgrounds = new List<Texture2D>
            {
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/RestaurantInside"),
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/NEXTDAY"),
            };

            _CharacterTexture = BurgerGame.Instance().CM().Load<Texture2D>("Characters/BurgerKing");
            _CharacterRect = new Rectangle(2 * screenWidth / 5, screenHeight / 4, screenWidth / 4, screenHeight * 3 / 4);

            _DialougeBox = BurgerGame.Instance().CM().Load<Texture2D>("DialougeBox");
            _DialougeBoxRect = new Rectangle(20, screenHeight - screenHeight / 4, screenWidth - 40, (screenHeight / 4) - 20);
            _NameBox = new Rectangle(20, screenHeight - (screenHeight / 4) - 20, screenWidth / 3, 20);

            _DialougeFont = BurgerGame.Instance().CM().Load<SpriteFont>("DialougeFont");
        }

        private Texture2D _Background
        {
            get
            {
                switch (_DialougeIndex)
                {
                    case -1:
                        return _Backgrounds[1];
                    default:
                        return _Backgrounds[0];
                }
            }
        }

        private string _Dialouge
        {
            get
            {
                switch (_DialougeIndex)
                {
                    case 0:
                        return "(You turn up at Burger King for your next shift.)";
                    case 1:
                        return "What the hell are you doing here?!";
                    case 2:
                        return "I thought I fired you!!!!";
                    case 3:
                        return "(You must look confused, because the Burger King sighs)";
                    case 4:
                        return "You were shooting burgers in the employee swimming pool !!!!!!";
                    case 5:
                        return "That swimming pool is fer swimming only!!! Not for killing my burger children";
                    case 6:
                        return "Get outta here before I call the police";
                    case 7:
                        return "(You slink out of burger king sadly..\r\n" +
                            "At least you can make some money off demonstrating programming portfolio next year.\r\n" +
                            "Or maybe you can become a School of Computer Science Student Ambassador.)";
                    case 8:
                        return "THE END.";
                    default:
                        return "";
                }
            }
        }

        private string _Name
        {
            get
            {
                switch (_DialougeIndex)
                {
                    case 0:
                    case 3:
                    case 7:
                    case 8:
                        return String.Empty;
                    default:
                        return "Burger King";
                }
            }
        }

        public void Draw(float pSeconds)
        {
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_Background, _BackgroundRect, Color.White);
            if(_DialougeIndex != -1)
            {
                _SpriteBatch.Draw(_CharacterTexture, _CharacterRect, Color.White);
                _SpriteBatch.Draw(_DialougeBox, _DialougeBoxRect, Color.White);
                _SpriteBatch.Draw(_DialougeBox, _NameBox, Color.DarkSalmon);
                _SpriteBatch.DrawString(_DialougeFont, _Dialouge, new Vector2(40, _DialougeBoxRect.Y), Color.Black);
                _SpriteBatch.DrawString(_DialougeFont, _Name, new Vector2(40, _NameBox.Y), Color.White);
            }
            _SpriteBatch.End();
        }

        public void Update(float pSeconds)
        {
            if (_Controller != null)
            {
                _Controller.UpdateController(pSeconds);
                if (_Controller.IsPressed(Control.CLICK) && !_Controller.WasPressed(Control.CLICK))
                {
                    ++_DialougeIndex;
                }
            }

            if (_DialougeIndex > 8)
            {
                BurgerGame.Instance().SM().ChangeScene(null);
            }
        }
    }
}
