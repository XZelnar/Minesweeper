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

namespace MineSweeper.Game
{
    public static class GameEngine
    {
        public static bool firstClick = false;
        public static bool allowDnD = false, isDnD = false;
        public static Vector2 DnDstart = new Vector2();
        public static Vector2 offset = new Vector2();

        public static void Initialize()
        {
            InputEngine.onButtonDown += new InputEngine.MouseEventHandler(InputEngine_onButtonDown);
            InputEngine.onButtonUp += new InputEngine.MouseEventHandler(InputEngine_onButtonUp);
        }

        public static void Update()
        {
            if (MineSweeper.IsField())
            {
                if (MineSweeper.gameField.isLost || MineSweeper.gameField.isWon)
                    return;

                if (allowDnD && !isDnD)
                {
                    if (InputEngine.curMouse.LeftButton == ButtonState.Pressed &&
                        InputEngine.curMouse.RightButton == ButtonState.Pressed)
                    {
                        allowDnD = false;
                    }
                    else
                    {
                        if (InputEngine.curMouse.LeftButton == ButtonState.Released)
                        {
                            allowDnD = false;
                        }
                        else
                        {
                            if (Math.Abs(InputEngine.curMouse.X - DnDstart.X) > 5 ||
                                Math.Abs(InputEngine.curMouse.Y - DnDstart.Y) > 5)
                            {
                                isDnD = true;
                            }
                        }
                    }
                }
                else
                {
                    if (isDnD)
                    {
                        if (InputEngine.curMouse.LeftButton == ButtonState.Released)
                        {
                            allowDnD = false;
                            isDnD = false;
                            offset.X += InputEngine.curMouse.X - DnDstart.X;
                            offset.Y += InputEngine.curMouse.Y - DnDstart.Y;
                            LimitOffset();
                        }
                        else
                        {
                            if (InputEngine.curMouse.RightButton == ButtonState.Pressed)
                            {
                                allowDnD = false;
                                isDnD = false;
                            }
                            else
                            {
                                offset.X += InputEngine.curMouse.X - DnDstart.X;
                                offset.Y += InputEngine.curMouse.Y - DnDstart.Y;
                                DnDstart.X = InputEngine.curMouse.X;
                                DnDstart.Y = InputEngine.curMouse.Y;

                                LimitOffset();
                            }
                        }
                    }
                }
            }
        }

        static void LimitOffset()
        {
            if (offset.X < Math.Min(0, MineSweeper.graphics.GraphicsDevice.Viewport.Width - MineSweeper.gameField.fieldSize.X  * 16 * MineSweeper.sizeModifier.X))
                offset.X = Math.Min(0, MineSweeper.graphics.GraphicsDevice.Viewport.Width - MineSweeper.gameField.fieldSize.X  * 16 * MineSweeper.sizeModifier.X);
            if (offset.Y < Math.Min(0, MineSweeper.graphics.GraphicsDevice.Viewport.Height - MineSweeper.gameField.fieldSize.Y * 16 * MineSweeper.sizeModifier.Y))
                offset.Y = Math.Min(0, MineSweeper.graphics.GraphicsDevice.Viewport.Height - MineSweeper.gameField.fieldSize.Y * 16 * MineSweeper.sizeModifier.Y);
            if (offset.X > Math.Max(0, MineSweeper.graphics.GraphicsDevice.Viewport.Width - MineSweeper.gameField.fieldSize.X  * 16 * MineSweeper.sizeModifier.X))
                offset.X = Math.Max(0, MineSweeper.graphics.GraphicsDevice.Viewport.Width - MineSweeper.gameField.fieldSize.X  * 16 * MineSweeper.sizeModifier.X);
            if (offset.Y > Math.Max(0, MineSweeper.graphics.GraphicsDevice.Viewport.Height - MineSweeper.gameField.fieldSize.Y * 16 * MineSweeper.sizeModifier.Y))
                offset.Y = Math.Max(0, MineSweeper.graphics.GraphicsDevice.Viewport.Height - MineSweeper.gameField.fieldSize.Y * 16 * MineSweeper.sizeModifier.Y);
        }

        static bool leftPressed = false, rightPressed = false;
        static bool ignoreNextClick = false;
        static void InputEngine_onButtonUp(InputEngine.MouseArgs e)
        {
            if (MineSweeper.IsField() && !InputEngine.eventHandled && !ignoreNextClick)
            {
                MineSweeper.gameField.HitTest(new Vector3(e.curState.X, e.curState.Y, 0), e.button);
            }
            ignoreNextClick = leftPressed && rightPressed;
            if (e.button == 0) leftPressed = false;
            if (e.button == 1) rightPressed = false;
        }

        static void InputEngine_onButtonDown(InputEngine.MouseArgs e)
        {
            leftPressed |= (e.button == 0);
            rightPressed |= (e.button == 1);
            if (MineSweeper.IsField())
            {
                if (e.button == 0 && e.curState.RightButton == ButtonState.Released)
                {
                    allowDnD = true;
                    DnDstart = new Vector2(e.curState.X,e.curState.Y);
                }
            }
        }
    }
}
