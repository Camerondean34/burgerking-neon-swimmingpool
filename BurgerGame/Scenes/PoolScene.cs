using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BurgerPoolGame.Scenes
{
    internal class PoolScene : IScene
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

        public PoolScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _BackgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);
            _Backgrounds = new List<Texture2D>
            {
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/Pool"),
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/PoolAttack1"),
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/PoolAttack2"),
                BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/PoolAttack3"),

            };

            _CharacterTexture = BurgerGame.Instance().CM().Load<Texture2D>("Characters/Michelle");
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
                    case 22:
                        return _Backgrounds[1];
                    case 23:
                        return _Backgrounds[2];
                    case 24:
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
                        return "(You head to the Burger King Employee Swimming Pool.)";
                    case 1:
                        return "(You find Michelle there, shooting some burgers.)";
                    case 2:
                        return "Hey! Fancy seeing you here. On your day off, too. Youre pretty dedicated, Ill give you that";
                    case 3:
                        return "(Michelle shoots another burger, and you both watch as it plops down into the swimming pool.)";
                    case 4:
                        return "Oh? You wanted to swim? Nah, we dont really use the pool for that.";
                    case 5:
                        return "Me? I just come here to destress. Im working on my PDD right now, and it is.";
                    case 6:
                        return "Its not going well.";
                    case 7:
                        return "I chose one of the projects from the catalogue -\r\n" +
                            "I was hoping to get David Parker as my supervisor, but instead I got-";
                    case 8:
                        return "(Michelle laughs and shakes her head.)";
                    case 9:
                        return "Nevermind. I wont bore you with that.\r\n" +
                            "Youre only a first year, right? You got a lot of stress coming, Ill give you that...";
                    case 10:
                        return "So.. How are your modules coming along?";
                    case 11:
                        return "...";
                    case 12:
                        return "Wait, you LIKE Programming Portfolio?";
                    case 13:
                        return "God, what is wrong with you????";
                    case 14:
                        return "(You start telling Michelle about how you have a crush on one of the demonstrators\r\n" +
                            "and she cuts you off with a laugh.)";
                    case 15:
                        return "Ahahahahahahahaha";
                    case 16:
                        return "AhahahahahahahahaAhahahahahahahaha";
                    case 17:
                        return "Yeah, youre not special, EVERYONE has a crush on the greenshirts...";
                    case 18:
                        return "Theres just something about those green demonstrator shirts, you know.\r\n" +
                            "I swear, green is the most attractive colour.";
                    case 19:
                        return "You know, I demonstrate for Advanced Programming.. Its pretty advanced.";
                    case 20:
                        return "(You and Michelle chat for a while, and she gives you some tips on how to improve your code.\r\n" +
                            "Its all going pretty well, until.....)";
                    case 21:
                        return "WATCH OUT!!!!";
                    case 22:
                        return "(At that moment a rouge burger sneaks up behind you)";
                    case 23:
                        return "(BLAM!)";
                    case 24:
                        return "(The burger sneaking up behind you drops to the floor, dead..)";
                    case 25:
                        return "You gotta be careful..";
                    case 26:
                        return "These burgers can be devious, sneaking up on ya..";
                    case 27:
                        return "Sneaking up on you like a runtime error...";
                    case 28:
                        return "(You thank Michelle profusely for saving your life.)";
                    case 29:
                        return "Dont worry about it.";
                    case 30:
                        return "We all gotta look after each other, you know? BK employees and CS students alike.";
                    case 31:
                        return "Theres Nothing Stronger Than Family, after all.";
                    case 32:
                        return "(Michelle gives you a knowing look.)";
                    case 33:
                        return "Ill see you around, i guess. At Burger King, or maybe we could...";
                    case 34:
                        return "... I mean, if you want to hang out in Fenner or something, thatd be cool...";
                    case 35:
                        return "We could enter Three Thing Game together, maybe?";
                    case 36:
                        return "(You wave Michelle goodbye and head on your way.)";
                    case 37:
                        return "(Heading home, you feel.. Happy?)";
                    case 38:
                        return "(Its nice.)";
                    case 39:
                        return "(Youre kind of looking forward to work.)";
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
                    case 3:
                    case 8:
                    case 10:
                    case 14:
                    case 20:
                    case 25:
                    case 28:
                    case 32:
                    case 36:
                    case 37:
                    case 38:
                    case 39:
                        return String.Empty;
                    default:
                        return "Michelle";
                }
            }
        }

        public void Draw(float pSeconds)
        {
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_Background, _BackgroundRect, Color.White);
            if (_DialougeIndex < 22 || _DialougeIndex > 24)
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

            if (_DialougeIndex > 39)
            {
                BurgerGame.Instance().SM().ChangeScene(new EndingScene());
            }
        }
    }
}
