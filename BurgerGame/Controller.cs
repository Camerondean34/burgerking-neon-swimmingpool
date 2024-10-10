using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BurgerPoolGame
{
    public enum Control { ENTER, ESCAPE, CLICK };

    public interface IController
    {
        void UpdateController(float pSeconds);

        bool IsPressed(Control pControl);
        bool WasPressed(Control pControl);

    }
    internal class Controller : IController
    {
        private Dictionary<Control, bool> _ControlsDown;
        private Dictionary<Control, bool> _PreviousControlsDown;
        private Dictionary<Keys, Control> _KeyMap;

        public Controller(Dictionary<Keys, Control> pKeyMap)
        {
            _KeyMap = pKeyMap;
            _ControlsDown = new Dictionary<Control, bool>();
            _PreviousControlsDown = new Dictionary<Control, bool>();
            int controlCount = Enum.GetNames(typeof(Control)).Length;
            for (int i = 0; i < controlCount; i++)
            {
                _ControlsDown[(Control)i] = false;
                _PreviousControlsDown[(Control)i] = false;
            }
        }

        public void UpdateController(float pSeconds)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            SetPreviousControlsDown(pSeconds);
            foreach (KeyValuePair<Keys, Control> kvp in _KeyMap)
            {
                _ControlsDown[kvp.Value] = keyboardState.IsKeyDown(kvp.Key);
            }
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
                _ControlsDown[Control.CLICK] = true;

        }

        private void SetPreviousControlsDown(float pSeconds)
        {
            foreach (KeyValuePair<Control, bool> kvp in _ControlsDown)
            {
                _PreviousControlsDown[kvp.Key] = kvp.Value;
            }
        }

        public bool IsPressed(Control pControl)
        {
            return _ControlsDown[pControl];
        }

        public bool WasPressed(Control pControl)
        {
            return _PreviousControlsDown[pControl];
        }
    }
}
