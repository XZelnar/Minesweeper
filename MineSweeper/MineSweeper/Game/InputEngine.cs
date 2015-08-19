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
    public static class InputEngine
    {
        //==================================================Mouse
        public static MouseState curMouse, lastMouse;
        public class MouseArgs
        {
            public MouseState curState;
            public int button;
        }
        public delegate void MouseEventHandler(MouseArgs e);
        public static event MouseEventHandler onButtonDown;
        public static event MouseEventHandler onButtonUp;
        public static bool eventHandled = false;


        //==================================================KB
        public static KeyboardState curKeyboard, lastKeyboard;
        public class KeyboardArgs
        {
            public KeyboardState curState;
            public int key;
        }
        public delegate void KeyboardEventHandler(KeyboardArgs e);
        public static event KeyboardEventHandler onKeyDown;
        public static event KeyboardEventHandler onKeyUp;

        public static void Update()
        {
            //==================================================Mouse
            lastMouse = curMouse;
            curMouse = Mouse.GetState();
            if (lastMouse == null) return;
            //left
            if (curMouse.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released)
                if (onButtonDown != null)
                {
                    onButtonDown(new MouseArgs() { curState = curMouse, button = 0 });
                    eventHandled = false;
                }
            if (curMouse.LeftButton == ButtonState.Released && lastMouse.LeftButton == ButtonState.Pressed)
                if (onButtonUp != null)
                {
                    onButtonUp(new MouseArgs() { curState = curMouse, button = 0 });
                    eventHandled = false;
                }
            //right
            if (curMouse.RightButton == ButtonState.Pressed && lastMouse.RightButton == ButtonState.Released)
                if (onButtonDown != null)
                {
                    onButtonDown(new MouseArgs() { curState = curMouse, button = 1 });
                    eventHandled = false;
                }
            if (curMouse.RightButton == ButtonState.Released && lastMouse.RightButton == ButtonState.Pressed)
                if (onButtonUp != null)
                {
                    onButtonUp(new MouseArgs() { curState = curMouse, button = 1 });
                    eventHandled = false;
                }
            //==================================================KB
            lastKeyboard = curKeyboard;
            curKeyboard = Keyboard.GetState();
            if (lastKeyboard == null) return;
            for (int i = 0; i < 256; i++)
            {
                if (curKeyboard.IsKeyDown((Keys)i) != lastKeyboard.IsKeyDown((Keys)i))
                {
                    if (curKeyboard.IsKeyDown((Keys)i))
                    {
                        if (onKeyDown != null)
                        {
                            onKeyDown(new KeyboardArgs() { curState = curKeyboard, key = i });
                            eventHandled = false;
                        }
                    }
                    else
                    {
                        if (onKeyUp != null)
                        {
                            onKeyUp(new KeyboardArgs() { curState = curKeyboard, key = i });
                            eventHandled = false;
                        }
                    }
                }
            }
        }

        public static bool WereBothMouseButtonsClicked()
        {
            return curMouse.RightButton == ButtonState.Released && lastMouse.RightButton == ButtonState.Pressed &&
                curMouse.LeftButton == ButtonState.Released && lastMouse.LeftButton == ButtonState.Pressed;
        }
    }
}
