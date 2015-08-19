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
    public class Label : Element
    {
        public static SpriteFont defaultFont;
        public SpriteFont font;

        public String text = "";
        public Color foreground = Color.Black;

        public Label(int x, int y, String txt)
        {
            position = new Vector3(x, y, 0);
            text = txt;
            font = defaultFont;
        }

        public override void Draw()
        {
            MineSweeper.spriteBatch.DrawString(font, text, new Vector2((int)position.X, (int)position.Y), foreground);
        }

    }
}
