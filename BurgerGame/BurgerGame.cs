using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BurgerPoolGame.Scenes;
using System.Collections.Generic;
using System.Net;
using Microsoft.Xna.Framework.Audio;

namespace BurgerPoolGame;

public class BurgerGame : Game, IGame
{
    private static IGame _GameInstance = null;
    private GraphicsDeviceManager _Graphics;
    private SceneManager _SceneManager;
    private SoundManager _SoundManager;
    private IController _Controller;
    private SoundEffectInstance _MusicInstance;

    public static IGame Instance()
    {
        if (_GameInstance == null)
        {
            _GameInstance = new BurgerGame();
        }
        return _GameInstance;
    }

    public ContentManager CM() { return Content; }
    public GraphicsDeviceManager GDM() { return _Graphics; }
    public SceneManager SM() { return _SceneManager; }
    public SoundManager GetSoundManager() { return _SoundManager; }
    public IController GetController() { return _Controller; }
    public BurgerGame()
    {
        _Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _SceneManager = new SceneManager();
        _SoundManager = new SoundManager();
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        //_Graphics.PreferredBackBufferHeight = SCREENHEIGHT;
        //_Graphics.PreferredBackBufferWidth = SCREENWIDTH;
        _Graphics.IsFullScreen = false;
        this.Window.Title = "Burger Game";
        _Graphics.ApplyChanges();
        base.Initialize();

        Dictionary<Keys, Control> buttonMap = new Dictionary<Keys, Control>();
        buttonMap.Add(Keys.Enter, Control.ENTER); ;
        buttonMap.Add(Keys.Escape, Control.ESCAPE);
        buttonMap.Add(Keys.E, Control.CLICK);
        _Controller = new Controller(buttonMap);
    }

    protected override void LoadContent()
    {
        //Add sounds here
        //_SoundManager.Add("SoundName");
        _SceneManager.ChangeScene(new FlashScreenScene(), false);
    }

    protected override void Update(GameTime gameTime)
    {
        float seconds = 0.001f * gameTime.ElapsedGameTime.Milliseconds;
        _SceneManager.Update(seconds);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        float seconds = 0.001f * gameTime.ElapsedGameTime.Milliseconds;
        _SceneManager.Draw(seconds);
        base.Draw(gameTime);
    }

    public void StopMusic()
    {
        if (_MusicInstance != null)
        {
            _MusicInstance.Stop();
            _MusicInstance = null;
        }
    }

    public void SetMusic(string pMusicName)
    {
        StopMusic();
        _MusicInstance = _SoundManager.GetSoundEffectInstance(pMusicName);
        _MusicInstance.IsLooped = true;
        _MusicInstance.Play();
    }
}
