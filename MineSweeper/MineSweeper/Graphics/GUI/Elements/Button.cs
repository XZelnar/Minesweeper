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
    public class Button : Element
    {
        public static Texture2D texture;
        public static SpriteFont font;

        public String text = "";
        public Color foreground = Color.Black, background = new Color(230,230,230);
        public System.Reflection.MethodInfo OnClick;
        public object OnClickInvokeObject;

        private Vector2 stringSize;
        private String textOld = "";

        public Button(int x, int y, int w, int h, String txt)
        {
            position = new Vector3(x, y, 0);
            size = new Vector3(w, h, 0);
            text = txt;
            textOld = txt;
            stringSize = font.MeasureString(text);
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

            if (textOld != text)
            {
                textOld = text;
                stringSize = font.MeasureString(text);
            }

            MineSweeper.spriteBatch.DrawString(font, text, new Vector2(position.X + (size.X - stringSize.X) / 2,
                position.Y + (size.Y - stringSize.Y) / 2), foreground);

        }

        public override void OnButtonUp(Game.InputEngine.MouseArgs e)
        {
            if (OnClick != null && IsIn(e.curState.X, e.curState.Y))
            {
                OnClick.Invoke(OnClickInvokeObject, null);
                Game.InputEngine.eventHandled = true;
            }
        }

    }
}
