using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BurgerPoolGame.Scenes
{
    internal class Button
    {
        private Texture2D _Texture;
        private Texture2D _TexturePressed;
        private Color _SelectedColour;
        private Rectangle _Rect;
        public bool Selected { get; set; }
        public delegate void Action();
        private Action doButton;

        public Button(Texture2D pTexture, Texture2D pTexturePressed, Rectangle pRect, Color pColour, Action pDoButton)
        {
            _Texture = pTexture;
            _TexturePressed = pTexturePressed;
            _Rect = pRect;
            _SelectedColour = pColour;
            doButton = pDoButton;
        }

        public void Update(int x, int y)
        {
            Selected = _Rect.Contains(x, y);
        }

        public bool PressButton()
        {
            if (doButton != null)
            {

                doButton();
                return true;

            }
            return false;
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            if (Selected) pSpriteBatch.Draw(_TexturePressed, _Rect, _SelectedColour);
            else pSpriteBatch.Draw(_Texture, _Rect, Color.White);
        }
    }
}
