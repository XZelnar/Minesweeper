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
    public class NumericTextBox : TextBox
    {

        public NumericTextBox(int x, int y, int w, int h, String txt)
        {
            position = new Vector3(x, y, 0);
            size = new Vector3(w, h, 0);
            text = txt;
            stringSize = font.MeasureString(text);
        }

        public override void OnKeyDown(Game.InputEngine.KeyboardArgs e)
        {
            if (!isFocused) return;

            if (e.key == Keys.Back.GetHashCode() && text.Length > 0)
            {
                text = text.Substring(0, text.Length - 1);
                stringSize = font.MeasureString(text);
            }

            if (maxLength == 0 || text.Length < maxLength)
            {
                if (e.key >= Keys.D0.GetHashCode() && e.key <= Keys.D9.GetHashCode())
                {
                    text += e.key - Keys.D0.GetHashCode();
                    stringSize = font.MeasureString(text);
                }
                if (e.key >= Keys.NumPad0.GetHashCode() && e.key <= Keys.NumPad9.GetHashCode())
                {
                    text += e.key - Keys.NumPad0.GetHashCode();
                    stringSize = font.MeasureString(text);
                }
            }
        }

    }
}
