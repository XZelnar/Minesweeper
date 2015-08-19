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
    public abstract class GameField
    {
        public Tiles.Tile[][][] tiles = new Tiles.Tile[0][][];
        public Vector3 tileScaledSize = new Vector3(5, 5, 5);
        public Vector3 fieldSize = new Vector3(1, 1, 1);
        public int minesCount = 0;
        public int seed;
        public int minesLeft = 0;
        public bool isLost = false, isWon = false;
        public DateTime start = DateTime.MinValue;
        public TimeSpan duration;

        public virtual void Initialize()
        {
            if (start != DateTime.MinValue)
                return;
            start = DateTime.Now;
            duration = new TimeSpan(0);

            GameEngine.offset = new Vector2(
                (MineSweeper.graphics.GraphicsDevice.Viewport.Width - fieldSize.X * 16 * MineSweeper.sizeModifier.X) / 2,
                (MineSweeper.graphics.GraphicsDevice.Viewport.Height - fieldSize.Y * 16 * MineSweeper.sizeModifier.Y) / 2);
        }

        public virtual void Generate() { }

        public virtual void Update() { }

        public virtual void Draw()
        {
            for (int x = 0; x < tiles.Length; x++)
            {
                for (int y = 0; y < tiles[x].Length; y++)
                {
                    if (tiles[x][y][0].CurrentState != -20)
                        tiles[x][y][0].Draw();
                }
            }
            if (InputEngine.curMouse.LeftButton == ButtonState.Pressed && InputEngine.curMouse.RightButton == ButtonState.Pressed && !(isLost || isWon))
            {
                int x1 = (int)((InputEngine.curMouse.X - Game.GameEngine.offset.X) / tileScaledSize.X / MineSweeper.sizeModifier.X);
                int y1 = (int)((InputEngine.curMouse.Y - Game.GameEngine.offset.Y) / tileScaledSize.Y / MineSweeper.sizeModifier.Y);
                for (int x = x1 - 1; x <= x1 + 1; x++)
                {
                    for (int y = y1 - 1; y <= y1 + 1; y++)
                    {
                        if (x >= 0 && x < fieldSize.X && y >= 0 && y < fieldSize.Y && tiles[x][y][0].CurrentState != -20)
                            ((TileSquare)tiles[x][y][0]).DrawOpened();
                    }
                }
            }
        }

        public virtual void OnLevelComplete()
        {
            duration = DateTime.Now - start;
            for (int x = 0; x < tiles.Length; x++)
            {
                for (int y = 0; y < tiles[x].Length; y++)
                {
                    if (tiles[x][y][0].CurrentState == -1)
                    {
                        Entity.Mine m = new Entity.Mine(tiles[x][y][0].Position, new Vector3(16, 16, 0));
                        Entity.EntityManager.AddMine(tiles[x][y][0].Position, tiles[x][y][0].Size);
                    }
                }
            }
        }

        public virtual unsafe bool HitTest(Vector3 pos, int button)
        {
            if (GameEngine.isDnD || isLost || isWon) return false;

            int x = (int)((pos.X - Game.GameEngine.offset.X) / tileScaledSize.X / MineSweeper.sizeModifier.X);
            int y = (int)((pos.Y - Game.GameEngine.offset.Y) / tileScaledSize.Y / MineSweeper.sizeModifier.Y);
            if (x >= fieldSize.X || y >= fieldSize.Y || x < 0 || y < 0) return false;
            if (tiles[x][y][0].CurrentState == -20) return false;
            if (GameEngine.firstClick)
            {
                while (tiles[x][y][0].CurrentState != 0)
                {
                    Initialize();
                    Generate();
                }
                GameEngine.firstClick = false;
            }
            OnHit(x, y, 0, button);
            return false;
        }

        /*
         * Activates tile logic when it's been clicked on. x,y,z - indexes.
         */

        public virtual unsafe void OnHit(int x, int y, int z, int button)
        {
            if (isLost || isWon) return;

            if (((button == 0 && InputEngine.curMouse.RightButton == ButtonState.Pressed) ||
                (button == 1 && InputEngine.curMouse.LeftButton == ButtonState.Pressed) ||
                InputEngine.WereBothMouseButtonsClicked()) && tiles[x][y][z].IsOpened)
            {
                if (GetSurroundingMinesLeftCount(x, y, z) == 0)
                {
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            if (i >= 0 && j >= 0 &&
                                i < fieldSize.X && j < fieldSize.Y &&
                                !tiles[i][j][0].IsOpened && !tiles[i][j][0].IsFlagged)
                            {
                                OnHitRightLeft(i, j, 0);
                            }
                        }
                    }
                }
                else
                {
                    Entity.EntityManager.AddSquareCross(new Vector3(x * 16, y * 16, 0), new Vector3(16, 16, 0));
                }
                return;
            }

            if (button == 0)
            {
                Tile t = tiles[x][y][z];
                if (t.IsFlagged || t.IsOpened) return;
                t.OnOpened();
                if (t.CurrentState == 0)
                {
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            if (i >= 0 && j >= 0 &&
                                i < fieldSize.X && j < fieldSize.Y &&
                                !tiles[i][j][0].IsOpened && !tiles[i][j][0].IsFlagged && tiles[i][j][0].CurrentState >= 0)
                            {
                                OnHit(i, j, 0, button);
                            }
                        }
                    }
                }
            }
            if (button == 1)
            {
                Tile t = tiles[x][y][z];
                if (!t.IsOpened)
                {
                    t.IsFlagged = !t.IsFlagged;
                    minesLeft += t.IsFlagged ? -1 : 1;
                }
            }
        }

        public unsafe void OnHitRightLeft(int x, int y, int z)
        {
            Tile t = tiles[x][y][z];
            if (t.IsFlagged || t.IsOpened) return;
            int x1 = (int)((InputEngine.curMouse.X - Game.GameEngine.offset.X) / tileScaledSize.X / MineSweeper.sizeModifier.X);
            int y1 = (int)((InputEngine.curMouse.Y - Game.GameEngine.offset.Y) / tileScaledSize.Y / MineSweeper.sizeModifier.Y);
            if (Math.Abs(x - x1) > 1 || Math.Abs(y - y1) > 1)
                t.OnOpened();
            else
            {
                t.OnOpenedWOAnimation();
            }
            if (t.CurrentState == 0)
            {
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i >= 0 && j >= 0 &&
                            i < fieldSize.X && j < fieldSize.Y &&
                            !tiles[i][j][0].IsOpened && !tiles[i][j][0].IsFlagged && tiles[i][j][0].CurrentState >= 0)
                        {
                            OnHitRightLeft(i, j, 0);
                        }
                    }
                }
            }
        }

        public virtual void CheckForVictory()
        {
            bool IsClear = true;
            Tile t;
            for (int x = 0; x < fieldSize.X; x++)
            {
                for (int y = 0; y < fieldSize.Y; y++)
                {
                    t = tiles[x][y][0];
                    if (!t.IsOpened && !t.IsFlagged && t.CurrentState > -1)
                    {
                        IsClear = false;
                        break;
                    }
                }
                if (!IsClear) break;
            }
            if (IsClear)
            {
                MineSweeper.InvokeLevelWon();
            }
        }

        public virtual unsafe short GetSurroundingMinesCount(int x, int y, int z)
        {
            Tile t;
            short c = 0;
            if (y > 0)
            {
                t = tiles[x][y - 1][0];
                if (t.CurrentState == -1) c++;              //x;y-
            }
            if (y < fieldSize.Y - 1)
            {
                t = tiles[x][y + 1][0];
                if (t.CurrentState == -1) c++;              //x;y+
            }
            if (x > 0)
            {
                t = tiles[x - 1][y][0];
                if (t.CurrentState == -1) c++;                  //x-;y
                if (y > 0)
                {
                    t = tiles[x - 1][y - 1][0];
                    if (t.CurrentState == -1) c++;              //x-;y-
                }
                if (y < fieldSize.Y - 1)
                {
                    t = tiles[x - 1][y + 1][0];
                    if (t.CurrentState == -1) c++;              //x-;y+-
                }
            }
            if (x < fieldSize.X - 1)
            {
                t = tiles[x + 1][y][0];
                if (t.CurrentState == -1) c++;                  //x+;y
                if (y > 0)
                {
                    t = tiles[x + 1][y - 1][0];
                    if (t.CurrentState == -1) c++;              //x+;y-
                }
                if (y < fieldSize.Y - 1)
                {
                    t = tiles[x + 1][y + 1][0];
                    if (t.CurrentState == -1) c++;              //x+;y+
                }
            }
            return c;
        }

        public unsafe short GetSurroundingMinesLeftCount(int x, int y, int z)
        {
            Tile t;
            short c = 0;
            if (y > 0)
            {
                t = tiles[x][y - 1][0];
                if (t.CurrentState == -1 && !t.IsFlagged) c++;              //x;y-
                if (t.CurrentState != -1 && t.IsFlagged) c--;
            }
            if (y < fieldSize.Y - 1)
            {
                t = tiles[x][y + 1][0];
                if (t.CurrentState == -1 && !t.IsFlagged) c++;              //x;y+
                if (t.CurrentState != -1 && t.IsFlagged) c--;
            }
            if (x > 0)
            {
                t = tiles[x - 1][y][0];
                if (t.CurrentState == -1 && !t.IsFlagged) c++;                  //x-;y
                if (t.CurrentState != -1 && t.IsFlagged) c--;
                if (y > 0)
                {
                    t = tiles[x - 1][y - 1][0];
                    if (t.CurrentState == -1 && !t.IsFlagged) c++;              //x-;y-
                    if (t.CurrentState != -1 && t.IsFlagged) c--;
                }
                if (y < fieldSize.Y - 1)
                {
                    t = tiles[x - 1][y + 1][0];
                    if (t.CurrentState == -1 && !t.IsFlagged) c++;              //x-;y+
                    if (t.CurrentState != -1 && t.IsFlagged) c--;
                }
            }
            if (x < fieldSize.X - 1)
            {
                t = tiles[x + 1][y][0];
                if (t.CurrentState == -1 && !t.IsFlagged) c++;                  //x+;y
                if (t.CurrentState != -1 && t.IsFlagged) c--;
                if (y > 0)
                {
                    t = tiles[x + 1][y - 1][0];
                    if (t.CurrentState == -1 && !t.IsFlagged) c++;              //x+;y-
                    if (t.CurrentState != -1 && t.IsFlagged) c--;
                }
                if (y < fieldSize.Y - 1)
                {
                    t = tiles[x + 1][y + 1][0];
                    if (t.CurrentState == -1 && !t.IsFlagged) c++;              //x+;y+
                    if (t.CurrentState != -1 && t.IsFlagged) c--;
                }
            }
            return c;
        }
    }
}
