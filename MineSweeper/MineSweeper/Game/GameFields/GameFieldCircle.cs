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
using MineSweeper.Game.Tiles;

namespace MineSweeper.Game
{
    public class GameFieldCircle : GameField
    {
        float radius;
        float radsqr;

        public GameFieldCircle(float rad, int minecount)
        {
            radius = rad;
            radsqr = rad * rad;
            fieldSize = new Vector3(rad * 2, rad * 2, 1);
            minesCount = minecount;
            minesLeft = minecount;
            tileScaledSize = new Vector3(16, 16, 0);
            Initialize();
        }

        public override unsafe void Initialize()
        {
            base.Initialize();

            GameEngine.firstClick = true;
            isLost = false;
            isWon = false;

            tiles = new Tile[(int)fieldSize.X][][];
            for (int i = 0; i < fieldSize.X; i++)
            {
                tiles[i] = new Tile[(int)fieldSize.Y][];
                for (int j = 0; j < fieldSize.Y; j++)
                {
                    tiles[i][j] = new Tile[1];
                    tiles[i][j][0] = new TileSquare(new Vector3(i * 16, j * 16, 0), new Vector3(16, 16, 0));

                    if (Math.Pow(i - radius + .5, 2) + Math.Pow(j - radius + .5, 2) > radsqr)
                        tiles[i][j][0].CurrentState = -20;
                }
            }
        }

        public override unsafe void Generate()
        {
            int generated = 0, x1 = 0, y1 = 0;
            RandomFast r1 = new RandomFast(), r2 = new RandomFast(r1.Next());

            while (generated < minesCount)
            {
                x1 = r1.Next((int)fieldSize.X);
                y1 = r2.Next((int)fieldSize.Y);
                if (tiles[x1][y1][0].CurrentState != -1 && tiles[x1][y1][0].CurrentState != -20)
                {
                    tiles[x1][y1][0].CurrentState = -1;
                    generated++;
                }
            }

            for (int x = 0; x < fieldSize.X; x++)
            {
                for (int y = 0; y < fieldSize.Y; y++)
                {
                    if (tiles[x][y][0].CurrentState != -1 && tiles[x][y][0].CurrentState != -20)
                    {
                        tiles[x][y][0].CurrentState = GetSurroundingMinesCount(x, y, 0);
                    }
                }
            }
        }

    }
}
