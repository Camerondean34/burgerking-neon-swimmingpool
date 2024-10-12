using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BurgerPoolGame.Scenes
{
    internal class SpidersScene : IScene
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
        private uint _DialougeIndex = 0;

        public SpidersScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _BackgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);
            _Backgrounds = new List<Texture2D>
            {
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/Spiders"),
            };

            _CharacterTexture = BurgerGame.Instance().CM().Load<Texture2D>("Characters/Number15");
            _CharacterRect = new Rectangle(2 * screenWidth / 5, 0, screenWidth / 4, screenHeight * 3 / 4);

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
                        return "(Its a Saturday night - you know what you have to do.)";
                    case 1:
                        return "(You turn up at Spiders in your best pair of black jeans and graphic t-shirt.\r\n" +
                            "You fit right in. Nice.)";
                    case 2:
                        return "(The music is loud and tinny, the drinks are flowing - and suspiciously cheap.)";
                    case 3:
                        return "01101000 01100101 01111001 00100000 01100010 01110010 01101111";
                    case 4:
                        return "01100110 01100001 01101110 01100011 01111001 00100000 01110011 01100101 01100101 01101001\r\n" +
                            "01101110 01100111 00100000 01111001 01101111 01110101 00100000 01101000 01100101 01110010\r\n" +
                            "01100101";
                    case 5:
                        return "(You give a nervous laugh.)";
                    case 6:
                        return "01110111 01100001 01101110 01101110 01100001 00100000 01100111 01100101 01110100 00100000\r\n" +
                            "01100001 00100000 01100100 01110010 01101001 01101110 01101011";
                    case 7:
                        return "(Number 15: Burger King Foot Lettuce starts walking towards the bar. You follow after him, you guess.)";
                    case 8:
                        return "(He hands you what you think is a vodka coke. You hope its a vodka coke.)";
                    case 9:
                        return "01100111 01110010 01100101 01100001 01110100 00100000 01101101 01110101 01110011 01101001\r\n" +
                            "01100011 00100000 01101000 01110101 01101000";
                    case 10:
                        return "(You dance with Number 15: Burger King Foot Lettuce for a bit.\r\n" +
                            "For a guy with lettuce on his feet, hes a pretty good dancer.\r\n" +
                            "After a while, you head outside.)";
                    case 11:
                        return "(Number 15: Burger King Foot Lettuce seems to relax a bit in the fresh air.\r\n" +
                            "Or the cigarette smoke air. Same difference innit)";
                    case 12:
                        return "01100001 01101000 01101000 00100000 01110100 01101000 01101001 01110011 00100000 01101001 01110011";
                    case 13:
                        return "(Number 15: Burger King Foot Lettuce clears his throat.)";
                        case 14:
                        return "Ahem.. Sorry, Im a bit..";
                    case 15:
                        return "Kind of socially anxious.. I get a bit nervous and talk in binary..\r\n" +
                            "You know how it is.";
                    case 16:
                        return "(You dont know how it is, but you smile and nod anyway.\r\n" +
                            "Number 15 shifts nervously from foot to foot on his lettuce.\r\n" +
                            "Its probably pretty unsanitary.)";
                    case 17:
                        return "(Maybe you should say something..)";
                    case 18:
                        return "Yeah, Im standing on lettuce";
                    case 19:
                        return "Makes me feel comfortable";
                    case 20:
                        return "i do it all the time";
                    case 21:
                        return "Got a problem with that??????";
                    case 22:
                        return "(You quickly shake your head)";
                    case 23:
                        return "Good. Im kinda insecure about it.";
                    case 24:
                        return "but you seem cool, I guess.";
                    case 25:
                        return "(Number 15: Burger King Foot Lettuce thinks youre..\r\n" +
                            "Cool? That makes you feel strangely happy)";
                    case 26:
                        return "lets go dance";
                    case 27:
                        return "theyre playing misery business";
                    case 28:
                        return "thats my jam";
                    case 29:
                        return "(You go inside and dance with Number 15: Burger King Foot Lettuce for the rest of the night.\r\n" +
                            "You get a few more drinks, dance some more,\r\n" +
                            "and scream-sing the songs you know at the top of your lungs.)";
                    case 30:
                        return "(Its a good night.)";
                    case 31:
                        return "Ill see you at work, yeah?";
                    case 32:
                        return "stay cool";
                    case 33:
                        return "01101001 00100000 01101100 01101111 01110110 01100101 00100000 01111001 01101111 01110101";
                    case 34:
                        return "(Walking home from Spiders, theres a spring in your step.)";
                    case 35:
                        return "(Youre excited to see Number 15: Burger King Foot Lettuce again..)";
                    case 36:
                        return "(Maybe he could be a friend.)";
                    case 37:
                        return "(Maybe he could be.. Something more..)";
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
                    case 1:
                    case 2:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 11:
                    case 13:
                    case 16:
                    case 17:
                    case 22:
                    case 25:
                    case 29:
                    case 30:
                    case 34:
                    case 35:
                    case 36:
                    case 37:
                        return String.Empty;
                    default:
                        return "Number 15";
                }
            }
        }

        public void Draw(float pSeconds)
        {
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_Background, _BackgroundRect, Color.White);
            _SpriteBatch.Draw(_CharacterTexture, _CharacterRect, Color.White);
            _SpriteBatch.Draw(_DialougeBox, _DialougeBoxRect, Color.White);
            _SpriteBatch.Draw(_DialougeBox, _NameBox, Color.DarkSalmon);
            _SpriteBatch.DrawString(_DialougeFont, _Dialouge, new Vector2(40, _DialougeBoxRect.Y), Color.Black);
            _SpriteBatch.DrawString(_DialougeFont, _Name, new Vector2(40, _NameBox.Y), Color.White);
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

            if (_DialougeIndex > 37)
            {
                BurgerGame.Instance().SM().ChangeScene(new EndingScene());
            }
        }
    }
}
