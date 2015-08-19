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
    public class Panel : Element
    {
        public static Texture2D texture;

        public Color background = new Color(230,230,230);

        public Panel(int x, int y, int w, int h)
        {
            position = new Vector3(x, y, 0);
            size = new Vector3(w, h, 0);
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

        }

    }
}
