using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BurgerPoolGame.Scenes
{
    internal class RaveScene : IScene
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

        public RaveScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _BackgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);
            _Backgrounds = new List<Texture2D>
            {
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/Rave"),
            };

            _CharacterTexture = BurgerGame.Instance().CM().Load<Texture2D>("Characters/BurgerKing");
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
                        return "(You turn up at Burger King, and find the place full of people.\r\nThere's neon lights, loud music, and the place is full of people.)";
                    case 1:
                        return "(Just what exactly is going on???)";
                    case 2:
                        return "HEY (PLAYER) - WHAT ARE YOU DOING HERE???";
                    case 3:
                        return "SORRY IM YELLING, ITS A BIT LOUD.";
                    case 4:
                        return "(You ask the Burger King whats going on..)";
                    case 5:
                        return "AH, BURGER KING DOESNT MAKE MUCH MONEY. WE CANT REALLY COMPETE WITH MCDONALDS";
                    case 6:
                        return "OR LIKE, PRIMA. PIZZA HOT. AL PACINOS PIZZA.";
                    case 7:
                        return "OR ANY OF THOSE 30 WENDYS THAT HAVE POPPED UP IN HULL OVERNIGHT.";
                    case 8:
                        return "SERIOUSLY, WHATS UP WITH THAT???";
                    case 9:
                        return "YEAH SO I MAKE MY MONEY DOING THESE NEON RAVES FOR FRESHERS";
                    case 10:
                        return "ITS PRETTY GOOD MONEY. I SELL THESE WRISTBANDS THROUGH\r\nFATSOMA AND FRESHERS GROUP CHATS AND STUFF";
                    case 11:
                        return "(.. You bought one of those wristbands. It was kind of a scam.)";
                    case 12:
                        return "HAHA YEAH IT SURE WAS.";
                    case 13:
                        return "ANYWAY WANNA GO EARN SOME MOENEY";
                    case 14:
                        return "GO SELL THESE SHOTS TO FRESHERS";
                    case 15:
                        return "(You take the tray of shots and head out onto the dance floor.\r\nAre you actually gonna get paid for this? You dont know.)";
                    case 16:
                        return "(You look back at the Burger King and he gives you an encouraging smile.)";
                    case 17:
                        return "(. . .)";
                    case 18:
                        return "(After managing to sell all the shots to freshers, aka,\r\n" +
                            "18 year olds with free access to alcohol for the first time,\r\nyou head back to the Burger King.)";
                    case 19:
                        return "GOOD JOB.";
                    case 20:
                        return "YOU GOT PIZZAZ KID. I LIKE YOU.";
                    case 21:
                        return "(The Burger King gives you a hearty pat on the back.)";
                    case 22:
                        return "M PRETTY DRUNK RIGHT NOW BUT ILL SEE YOU AROUND";
                    case 23:
                        return "BYE";
                    case 24:
                        return "(You leave the Burger King, and head back to your flat.\r\n" +
                            "Youre not sure if youll ever see him again.)";
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
                    case 4:
                    case 11:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 21:
                    case 24:
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

            if (_DialougeIndex > 24)
            {
                BurgerGame.Instance().SM().ChangeScene(new EndingScene());
            }
        }
    }
}
