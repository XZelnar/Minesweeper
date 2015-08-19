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
    public class SquareFadeOut2D : Entity
    {
        public SquareFadeOut2D(Vector3 pos, Vector3 s)
        {
            position = pos;
            size = s;
        }

        public override void Update()
        {
            if (!isDead) state++;
            if (state >= 10) isDead = true;
        }

        public override void Draw()
        {
            float c = 1f - state / 10f;
            MineSweeper.spriteBatch.Draw(Game.Tiles.TileSquare.texture,
                new Rectangle(
                    (int)(position.X * MineSweeper.sizeModifier.X + Game.GameEngine.offset.X),
                    (int)(position.Y * MineSweeper.sizeModifier.Y + Game.GameEngine.offset.Y),
                    (int)(size.X * MineSweeper.sizeModifier.X),
                    (int)(size.Y * MineSweeper.sizeModifier.Y)),
                new Rectangle(64, 0, 64, 64), Color.White * c);
        }
    }
}
