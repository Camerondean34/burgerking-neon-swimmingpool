using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BurgerPoolGame.Scenes
{
    internal class WeatherspoonsScene : IScene
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

        public WeatherspoonsScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _BackgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);
            _Backgrounds = new List<Texture2D>
            {
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/Weatherspoons"),
            };

            _CharacterTexture = BurgerGame.Instance().CM().Load<Texture2D>("Characters/Darryl");
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
                        return "(You walk into Wetherspoons on your day off, expecting to have a quiet drink when you spot Darryl\r\n" +
                            "at a corner table, laptop open, with a pint next to him. Hes clearly deep into something,\r\n" +
                            "but when he sees you, his face lights up.)";
                    case 1:
                        return "Hey! Fancy seeing you here! Wow, I didnt expect to run into you outside of Burger King!";
                    case 2:
                        return "You want to join me? Im just... You know, working on some stuff in Rust. Its my idea of a good time.\r\n" +
                            "But I guess I can close it for now. Its just some boring memory management stuff anyway...";
                    case 3:
                        return "(You try to sound interested in what hes doing, and Darryl looks pleased.)";
                    case 4:
                        return "So, what are you drinking? Im just a beer guy myself, but you have to admit, the prices here are...\r\n" +
                            "Competitive. Better than any other student union bar, right? I usually just get whatevers cheap,\r\n" +
                            "I prefer to invest in my projects...";
                    case 5:
                        return "(You grab a drink, and sit down with Darryl.)";
                    case 6:
                        return "You know, seeing you outside of work is... Kinda refreshing. No grease, no uniform, just... Us.";
                    case 7:
                        return "Feels like were in a different context, you know? Like in Rust,\r\n" +
                            "where you can change contexts safely between threads.";
                    case 8:
                        return "And right now, I think this context is... Pretty nice.";
                    case 9:
                        return "(You chat for a while, about everything and nothing. Darryl grabs you both another drink. And another.)";
                    case 10:
                        return "Man, its wild how much Ive been thinking about Rust lately.";
                    case 11:
                        return "I mean, Ive got work, and uni, and life, and yet... My brain keeps going back to it.";
                    case 12:
                        return "It's just so satisfying. Like, it's this language that respects you.\r\n" +
                            "Doesn't treat you like you don't know what you're doing, but still makes sure you don't mess up.\r\n" +
                            "You know how rare that is? It's like a good partner...";
                    case 13:
                        return "(You ask Darryl more about his life outside of work.)";
                    case 14:
                        return "Me? Oh, outside of work? Uh... I'm a pretty rustic guy.";
                    case 15:
                        return "I like tinkering with code in my free time, especially Rust";
                    case 16:
                        return "But, uh, when I'm not glued to my laptop, I'm usually here. Wetherspoons is kinda my spot.\r\n" +
                            "Cheap drinks, decent food, and... Free Wi-Fi. Plus, I hate my flatmates,\r\n" +
                            "so any chance to get out of there is good.";
                    case 17:
                        return "(You chat for a while longer, discussing your recent assignments,\r\n" +
                            "and your plans for Design, Develop, Deploy.)";
                    case 18:
                        return "You know... I never thought I'd enjoy just... Hanging out with someone like this.\r\n" +
                            "I'm usually too busy with code or work to really connect with people.";
                    case 19:
                        return "People usually think I'm kinda weird, aha...";
                    case 20:
                        return "(You smile at him, and Darryl gives a nervous smile back.)";
                    case 21:
                        return "But with you, it's different. Like, I don't feel the need to optimise every second.\r\n" +
                            "I can just... Be.";
                    case 22:
                        return "It's kinda like in Rust, where you trust the compiler to handle the tough stuff,\r\n" +
                            "so you can focus on the bigger picture.";
                    case 23:
                        return "And, uh... Right now, you're definitely part of that bigger picture.";
                    case 24:
                        return "(A while later, you decide it's time to head out.)";
                    case 25:
                        return "Hey, this was fun. It was really nice chatting to you, about... You know.s";
                    case 26:
                        return "Rust.";
                    case 27:
                        return "If you ever want to hang out again, I'd be up for it... Or I'll see you at work, I guess.";
                    case 28:
                        return "Feel free to follow me on Github, if you want.";
                    case 29:
                        return "(As you walk home, you feel a little lighter. You've made a friend.)";
                    case 30:
                        return "(Or maybe... Something more.)";
                    case 31:
                        return "(You make a mental note to try to learn about Rust.)";
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
                    case 5:
                    case 9:
                    case 13:
                    case 17:
                    case 20:
                    case 24:
                    case 29:
                    case 30:
                    case 31:
                        return String.Empty;
                    default:
                        return "Darryl";
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

            if (_DialougeIndex > 31)
            {
                BurgerGame.Instance().SM().ChangeScene(new EndingScene());
            }
        }
    }
}
