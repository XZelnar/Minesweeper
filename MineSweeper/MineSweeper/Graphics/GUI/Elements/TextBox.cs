using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MineSweeper.Graphics.GUI.Elements
{
    public class TextBox : Element
    {
        public static Texture2D texture;
        public static SpriteFont font;

        public String text = "";
        public Color foreground = Color.Black, background = Color.Silver;
        public int maxLength = 0;

        protected int state = 0;
        protected Vector2 stringSize;

        public TextBox(int x, int y, int w, int h, String txt)
        {
            position = new Vector3(x, y, 0);
            size = new Vector3(w, h, 0);
            text = txt;
            stringSize = font.MeasureString(text);
        }

        protected TextBox() { }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            state++;
            if (state >= 40) state -= 40;
        }

        public override void Draw()
        {
            MineSweeper.spriteBatch.Draw(texture, 
                new Rectangle((int)position.X, (int)position.Y, (int)size.X - 4, (int)size.Y - 4), 
                new Rectangle(0,0,(int)size.X-4,(int)size.Y-4),
                background);
            MineSweeper.spriteBatch.Draw(texture,
                new Rectangle((int)(position.X + size.X - 4), (int)position.Y, 4, (int)size.Y - 4),
                new Rectangle(252, 0, 4, (int)size.Y - 4),
                background);
            MineSweeper.spriteBatch.Draw(texture,
                new Rectangle((int)position.X, (int)(position.Y + size.Y - 4), (int)size.X - 4, 4),
                new Rectangle(0, 252, (int)size.X - 4, 4),
                background);
            MineSweeper.spriteBatch.Draw(texture,
                new Rectangle((int)(position.X + size.X - 4), (int)(position.Y + size.Y - 4), 4, 4),
                new Rectangle(252, 252, 4, 4),
                background);

            if (stringSize.X < size.X)
            {
                MineSweeper.spriteBatch.DrawString(font, text, new Vector2(position.X+1, position.Y+1), foreground);
            }
            else
            {
                MineSweeper.spriteBatch.End();
                Rectangle curST = MineSweeper.graphics.GraphicsDevice.ScissorRectangle;
                MineSweeper.graphics.GraphicsDevice.ScissorRectangle = new Rectangle((int)position.X, (int)position.Y,
                    (int)size.X, (int)size.Y);
                MineSweeper.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null,
                    null, Graphics.GraphicsEngine.s_ScissorsOn);

                MineSweeper.spriteBatch.DrawString(font, text, new Vector2(position.X+1, position.Y+1), foreground);

                MineSweeper.spriteBatch.End();
                MineSweeper.graphics.GraphicsDevice.ScissorRectangle = curST;
                MineSweeper.spriteBatch.Begin();
            }

            if (isFocused && state < 20)
            {
                MineSweeper.spriteBatch.Draw(texture,
                    new Rectangle((int)(position.X + stringSize.X + 1), (int)position.Y+3, 1, (int)size.Y - 6),
                    new Rectangle(0, 0, 1, (int)size.Y - 6),
                    Color.Black);
            }

        }

        public override void OnButtonDown(Game.InputEngine.MouseArgs e)
        {
            if (IsIn(e.curState.X, e.curState.Y))
                isFocused = true;
            else
                isFocused = false;
        }
    }
}
