using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BurgerPoolGame.Scenes
{
    internal class Button
    {
        private Texture2D _Texture;
        private Texture2D _TexturePressed;
        private Color _SelectedColour;
        public Rectangle Rect { get; private set; }
        public bool Selected { get; set; }
        public delegate void Action();
        private Action doButton;

        public Button(Texture2D pTexture, Texture2D pTexturePressed, Rectangle pRect, Color pColour, Action pDoButton)
        {
            _Texture = pTexture;
            _TexturePressed = pTexturePressed;
            Rect = pRect;
            _SelectedColour = pColour;
            doButton = pDoButton;
        }

        public void Update(int x, int y)
        {
            Selected = Rect.Contains(x, y);
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
            if (Selected) pSpriteBatch.Draw(_TexturePressed, Rect, _SelectedColour);
            else pSpriteBatch.Draw(_Texture, Rect, Color.White);
        }
    }
}
