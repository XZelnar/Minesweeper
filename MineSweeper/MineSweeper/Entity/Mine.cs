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

namespace MineSweeper.Entity
{
    public class Mine : Entity
    {
        public Mine(Vector3 pos, Vector3 s)
        {
            position = pos;
            size = s;
        }

        public override void Update()
        {
            state++;
        }

        public override void Draw()
        {
            float c;
            if (state < 100)
                c = (float)state / 100 * .5f;
            else
                c = (float)(Math.Sin((float)(-state % 200) * Math.PI / 100f) / 4f + 0.5f);
            MineSweeper.spriteBatch.Draw(EntityManager.sprites,
                new Rectangle(
                    (int)(position.X * MineSweeper.sizeModifier.X + Game.GameEngine.offset.X),
                    (int)(position.Y * MineSweeper.sizeModifier.Y + Game.GameEngine.offset.Y),
                    (int)(size.X * MineSweeper.sizeModifier.X - 1),
                    (int)(size.Y * MineSweeper.sizeModifier.Y - 1)),
                new Rectangle(64, 0, 64, 64), Color.White * c);
        }
    }
}
