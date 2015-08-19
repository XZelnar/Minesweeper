using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MineSweeper.Game.Tiles
{
    public class TileSquare : Tile
    {
        public static Texture2D texture;
        public static SpriteFont tileFont;
        public static Color[] textColors = new Color[]{
            Color.Blue, new Color(255,50,50), Color.Brown, 
            Color.Purple, Color.Yellow, Color.Orange,
            Color.Olive, Color.Black};

        public TileSquare(Vector3 pos, Vector3 size)
        {
            Position = pos;
            Size = size;
        }

        public override void Draw()
        {
            if (CurrentState == -20)
                return;

            if (IsOpened)
            {
                MineSweeper.spriteBatch.Draw(texture, new Rectangle(
                    (int)(Position.X * MineSweeper.sizeModifier.X + Game.GameEngine.offset.X),
                    (int)(Position.Y * MineSweeper.sizeModifier.Y + Game.GameEngine.offset.Y),
                    (int)(Size.X * MineSweeper.sizeModifier.X),
                    (int)(Size.Y * MineSweeper.sizeModifier.Y)),
                    new Rectangle(0, 0, 64, 64), Color.White);
                if (CurrentState > 0)
                    MineSweeper.spriteBatch.DrawString(tileFont, CurrentState.ToString(),
                        new Vector2(
                            (Position.X + 3) * MineSweeper.sizeModifier.X + Game.GameEngine.offset.X,
                            Position.Y * MineSweeper.sizeModifier.Y + Game.GameEngine.offset.Y), 
                            textColors[CurrentState-1], 0, new Vector2(), 
                            Math.Min(MineSweeper.sizeModifier.X, MineSweeper.sizeModifier.Y) / 2, SpriteEffects.None, 0);
            }
            else
            {
                //if (CurrentState == -1) return;
                MineSweeper.spriteBatch.Draw(texture, new Rectangle(
                    (int)(Position.X * MineSweeper.sizeModifier.X + Game.GameEngine.offset.X),
                    (int)(Position.Y * MineSweeper.sizeModifier.Y + Game.GameEngine.offset.Y),
                    (int)(Size.X * MineSweeper.sizeModifier.X),
                    (int)(Size.Y * MineSweeper.sizeModifier.Y)),
                    new Rectangle(64, 0, 64, 64), Color.White);
                if (IsFlagged)
                    MineSweeper.spriteBatch.Draw(texture, new Rectangle(
                    (int)(Position.X * MineSweeper.sizeModifier.X + Game.GameEngine.offset.X),
                    (int)(Position.Y * MineSweeper.sizeModifier.Y + Game.GameEngine.offset.Y),
                    (int)(Size.X * MineSweeper.sizeModifier.X),
                    (int)(Size.Y * MineSweeper.sizeModifier.Y)),
                        new Rectangle(128, 0, 64, 64), Color.White);
            }
        }

        public void DrawOpened()
        {
            if (!IsFlagged)
            {
                MineSweeper.spriteBatch.Draw(texture, new Rectangle(
                    (int)(Position.X * MineSweeper.sizeModifier.X + Game.GameEngine.offset.X),
                    (int)(Position.Y * MineSweeper.sizeModifier.Y + Game.GameEngine.offset.Y),
                    (int)(Size.X * MineSweeper.sizeModifier.X),
                    (int)(Size.Y * MineSweeper.sizeModifier.Y)),
                    new Rectangle(0, 0, 64, 64), Color.White);
                if (IsOpened && CurrentState > 0)
                    MineSweeper.spriteBatch.DrawString(tileFont, CurrentState.ToString(),
                        new Vector2(
                            (Position.X + 3) * MineSweeper.sizeModifier.X + Game.GameEngine.offset.X,
                            Position.Y * MineSweeper.sizeModifier.Y + Game.GameEngine.offset.Y),
                            textColors[CurrentState - 1], 0, new Vector2(),
                            Math.Min(MineSweeper.sizeModifier.X, MineSweeper.sizeModifier.Y) / 2, SpriteEffects.None, 0);
            }
        }

        public override bool HitTest(Vector3 pos)
        {
            return pos.X >= Position.X && pos.X < Position.X + Size.X &&
                pos.Y >= Position.Y && pos.Y < Position.Y + Size.Y &&
                pos.Z >= Position.Z && pos.Z < Position.Z + Size.Z;
        }

        public override void OnOpened()
        {
            IsOpened = true;
            Entity.EntityManager.AddSquareFadeOut(Position, Size);
            if (CurrentState == -1)
            {
                MineSweeper.InvokeLevelLost();
            }
            else
            {
                MineSweeper.gameField.CheckForVictory();
            }
        }

        public override void OnOpenedWOAnimation()
        {
            IsOpened = true;
            if (CurrentState == -1)
            {
                MineSweeper.InvokeLevelLost();
            }
            else
            {
                MineSweeper.gameField.CheckForVictory();
            }
        }


    }
}
