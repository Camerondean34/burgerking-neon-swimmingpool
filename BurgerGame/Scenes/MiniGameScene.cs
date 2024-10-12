using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace BurgerPoolGame.Scenes
{
    public class MiniGameScene : IScene
    {
        private int _screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
        private int _screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
        private IController _Controller;
        private SpriteBatch _SpriteBatch;

        private Texture2D _background;
        private Rectangle _backgroundRect;

        private Texture2D _explosion;
        private Rectangle _explosionRect;

        private Texture2D _crosshair;
        private Rectangle _crosshairRect;

        private Texture2D _handGun;
        private Rectangle _handRect;

        private List<Rectangle> _targets = new List<Rectangle>();
        private Texture2D _targetTexture = BurgerGame.Instance().CM().Load<Texture2D>("Minigame/Burger");

        private float _score = 0;
        private int _health = 5;
        private int _spawnedThisSeccond = 0;
        private DateTime _previousTime = new DateTime();

        public MiniGameScene()
        {
            _SpriteBatch = new SpriteBatch(BurgerGame.Instance().GDM().GraphicsDevice);
            _Controller = BurgerGame.Instance().GetController();

            int screenWidth = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = BurgerGame.Instance().GDM().GraphicsDevice.Viewport.Height;
            _background = BurgerGame.Instance().CM().Load<Texture2D>("Backgrounds/Pool");
            _backgroundRect = new Rectangle(0, 0, screenWidth, screenHeight);

            _handGun = BurgerGame.Instance().CM().Load<Texture2D>("Hands/Gun");
            _handRect = new Rectangle(0, 0, screenWidth / 5, screenHeight / 2);

            _explosion = BurgerGame.Instance().CM().Load<Texture2D>("Minigame/Explosion");
            _explosionRect = new Rectangle(0, 0, screenWidth / 5, screenHeight / 5);

            _crosshair = BurgerGame.Instance().CM().Load<Texture2D>("Minigame/Crosshair");
            _crosshairRect = new Rectangle(0, 0, screenWidth / 10, screenWidth / 10);
        }

        public void Draw(float pSeconds)
        {
            BurgerGame.Instance().GDM().GraphicsDevice.Clear(Color.CornflowerBlue);
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_background, _backgroundRect, Color.White);
            _SpriteBatch.Draw(_crosshair, _crosshairRect, Color.White);
            foreach (Rectangle target in _targets)
            {
                _SpriteBatch.Draw(_targetTexture, target, Color.White);
            }
            
            if (_Controller.IsPressed(Control.CLICK))
                _SpriteBatch.Draw(_explosion, _explosionRect, Color.White);

            _SpriteBatch.Draw(_handGun, _handRect, Color.White);
            _SpriteBatch.End();
        }

        public void Update(float pSeconds)
        {
            _Controller.UpdateController(pSeconds);
            if (_Controller.IsPressed(Control.ESCAPE) && !_Controller.WasPressed(Control.ESCAPE))
            {
                BurgerGame.Instance().SM().ChangeScene(null);
            }

            MouseState cursor = Mouse.GetState();
            _handRect.X = cursor.X;
            _handRect.Y = cursor.Y;
            _crosshairRect.X = cursor.X - _crosshairRect.Width / 2;
            _crosshairRect.Y = cursor.Y - _crosshairRect.Height / 2;
            _explosionRect.X = cursor.X - _explosionRect.Width / 2;
            _explosionRect.Y = cursor.Y - _explosionRect.Height / 2;

            if (_Controller.IsPressed(Control.CLICK))
            {
                List<Rectangle> toRemove = new List<Rectangle>();
                foreach(Rectangle target in _targets)
                {
                    if (target.Contains(cursor.X, cursor.Y))
                    {
                        toRemove.Add(target);
                    }
                }
                foreach(Rectangle target in toRemove)
                {
                    _targets.Remove(target);
                }
            }

            // Make targets fall
            for (int i = 0; i < _targets.Count; i++)
            {
                Rectangle burger = _targets[i];
                burger.Y = burger.Y + 1;
                _targets[i] = burger;
            }

            // Make targets spawn randomly
            TimeSpan timeSinceLastSpawn = DateTime.Now - _previousTime;
            int spawnRate = (int)((timeSinceLastSpawn.Seconds / 5000) + 1);
            if (_spawnedThisSeccond < spawnRate)
            {
                Random rng = new Random();
                for (int i = 0; i < spawnRate - _spawnedThisSeccond; i++)
                {
                    Rectangle newBurger = new Rectangle(rng.Next(0, _screenWidth), 0, 100, 100);
                    _targets.Add(newBurger);
                }
            }
        }
    }
}