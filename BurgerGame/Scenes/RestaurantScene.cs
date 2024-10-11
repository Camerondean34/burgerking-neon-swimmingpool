using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BurgerPoolGame.Scenes
{
    internal class RestaurantScene : IScene
    {
        private IController _Controller;
        private SpriteBatch _SpriteBatch;

        private Rectangle _BackgroundRect;

        private List<Texture2D> _Backgrounds;

        private List<Texture2D> _Characters;
        private Rectangle _BurgerKingRect;
        private Rectangle _CharacterRect;

        private Texture2D _DialougeBox;
        private Rectangle _DialougeBoxRect;
        private Rectangle _NameBox;
        private SpriteFont _DialougeFont;
        private uint _DialougeIndex = 0;

        public RestaurantScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _BackgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);
            _Backgrounds = new List<Texture2D>
            {
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/Resturant"),
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/RestaurantInside"),
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/Counter"),
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/Kitchen"),
            };

            _Characters = new List<Texture2D>
            {
                BurgerGame.Instance().CM().Load<Texture2D>("Characters/BurgerKing"),
                BurgerGame.Instance().CM().Load<Texture2D>("Characters/Darryl"),
                BurgerGame.Instance().CM().Load<Texture2D>("Characters/Michelle"),
                BurgerGame.Instance().CM().Load<Texture2D>("Characters/Number15"),
            };
            _BurgerKingRect = new Rectangle(screenWidth / 5, screenHeight / 4, screenWidth / 4, screenHeight * 3 / 4);
            _CharacterRect = new Rectangle(3 * screenWidth / 5, screenHeight / 4, screenWidth / 4, screenHeight * 3 / 4);

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
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return _Backgrounds[1];
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        return _Backgrounds[2];
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                        return _Backgrounds[3];
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
                        return "Welcome to the world of Poke-- I mean Burger King,\r\nsorry.";
                    case 1:
                        return "Here, you can have it your way. I mean,\r\nabout as much as you can for a minimum wage job,\r\nI guess.";
                    case 2:
                        return "Basically, I need you to make burgers, and prioritise this over your degree.\r\nWhat are you studying again? ";
                    case 3:
                        return "Oh, computer science? Haha... Yeah, we get a lot of you guys in here.\r\nMcDonald's seems to get all the humanities,\r\n" +
                            "but the STEM students just have to Have It Their Way.";
                    case 4:
                        return "Anyway. Go meet your coworkers.";
                    case 5:
                        return "This is Darryl, he's--";
                    case 6:
                        return "Hey there! Welcome to Burger King, home of the Whopper, and- Uh...";
                    case 7:
                        return "Did you know that Whoppers are kind of like...";
                    case 8:
                        return "Structs in Rust? I mean, stay with me here.";
                    case 9:
                        return "Think about it - every Whopper has the same components, right?\r\n" +
                            "Bun, patty, lettuce, but you can modify them with different traits, like adding cheese or extra pickles!";
                    case 10:
                        return "This is why we don't let you talk to the customers, Darryl.";
                    case 11:
                        return "There's also Michelle, and she's--";
                    case 12:
                        return " -- Experiencing job dissatisfaction. This place is hardly a stepping stone to success.\r\nNothing screams \"software engineering prodigy\" like flipping burgers for minimum wage.\r\nJust what I dreamed of while learning C++.";
                    case 13:
                        return "And yet we're all here wearing the same hat and uniform.";
                    case 14:
                        return "You wear a crown.";
                    case 15:
                        return "Anyway! Finally, I'd like to introduce you to number 15, burger king foot lettuce--";
                    case 16:
                        return "01101000 01100101 01101100 01110000 00100000 01101000 01100101 01101100 01110000 00100000\r\n" +
                            "01101101 01100101 00100000 01101001 01101101 00100000 01110100 01110010 01100001 01110000\r\n" +
                            "01110000 01100101 01100100 00100000 01101001 01101110 00100000 01100001 00100000 01100100\r\n" +
                            "01100001 01110100 01101001 01101110 01100111 00100000 01110011 01101001 01101101 00100000\r\n" +
                            "01101000 01100101 01101100 01110000";
                    case 17:
                        return "Real funny guy. Don't talk to him.";
                    case 18:
                        return "So, I guess those are your coworkers. Now... I have some stuff for you to be getting on with...\r\n" +
                            "You gotta earn that 5.23 pounds minimum wage?";
                    case 19:
                        return "Huh? Minimum wage is 11.44 pounds?";
                    case 20:
                        return "You're a real joker, you.";
                    case 21:
                        return "Don't do that. I don't like that.";
                    case 22:
                        return "Off to work!";
                    default:
                        return " ";
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
                    case 3:
                    case 4:
                    case 5:
                    case 10:
                    case 11:
                    case 13:
                    case 15:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                        return "Burger King";
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        return "Darryl";
                    case 12:
                    case 14:
                        return "Michelle";
                    case 16:
                        return "15";
                    default:
                        return String.Empty;
                }
            }
        }

        public void Draw(float pSeconds)
        {
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_Background, _BackgroundRect, Color.White);
            _SpriteBatch.Draw(_Characters[0], _BurgerKingRect, Color.White);
            switch (_DialougeIndex)
            {
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    _SpriteBatch.Draw(_Characters[1], _CharacterRect, Color.White);
                    break;
                case 12:
                case 13:
                case 14:
                    _SpriteBatch.Draw(_Characters[2], _CharacterRect, Color.White);
                    break;
                case 16:
                    _SpriteBatch.Draw(_Characters[3], _CharacterRect, Color.White);
                    break;
            }
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

            if (_DialougeIndex > 22)
            {
                BurgerGame.Instance().SM().ChangeScene(new MovingBurgerScene());
            }
        }
    }
}
