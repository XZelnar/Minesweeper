using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MineSweeper.Game.Tiles
{
    public abstract class Tile
    {
        public Vector3 Position = new Vector3();
        public Vector3 Size = new Vector3();
        public short CurrentState = 0;//-1 - mine, 0 - 0 mines and so on, -20 - invisible
        public bool IsOpened = false;
        public bool IsFlagged = false;

        public abstract bool HitTest(Vector3 pos);

        public abstract void Draw();

        /*
         * Used to inc score
         */
        public abstract void OnOpened();

        public abstract void OnOpenedWOAnimation();
    }
}
