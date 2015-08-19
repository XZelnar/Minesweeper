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
using MineSweeper.Game;

namespace MineSweeper.Graphics.GUI
{
    public unsafe class GUIEngine
    {
        public static bool firstClick = false;
        public static Screens.GUIScreen currentScreen;

        #region Screens
        public static GUI.Screens.MainMenu s_mainMenu = new Screens.MainMenu();
        public static GUI.Screens.SinglePlayer s_SP = new Screens.SinglePlayer();
        public static GUI.Screens.SquareClassic s_squareClassic = new Screens.SquareClassic();
        public static GUI.Screens.SquareClassicCustom s_squareClassicCustom = new Screens.SquareClassicCustom();
        public static GUI.Screens.Circle s_circle = new Screens.Circle();
        public static GUI.Screens.CircleCustom s_circleCustom = new Screens.CircleCustom();
        public static GUI.Screens.HowToPlay s_howToPlay = new Screens.HowToPlay();
        public static GUI.Screens.HUD s_hud = new Screens.HUD();
        #endregion

        public static void Initialize()
        {
            InputEngine.onButtonDown += new InputEngine.MouseEventHandler(InputEngine_onButtonDown);
            InputEngine.onButtonUp += new InputEngine.MouseEventHandler(InputEngine_onButtonUp);
            InputEngine.onKeyDown += new InputEngine.KeyboardEventHandler(InputEngine_onKeyDown);
            InputEngine.onKeyUp += new InputEngine.KeyboardEventHandler(InputEngine_onKeyUp);

            s_mainMenu.Initialize();
            s_SP.Initialize();
            s_squareClassic.Initialize();
            s_squareClassicCustom.Initialize();
            s_circle.Initialize();
            s_circleCustom.Initialize();
            s_howToPlay.Initialize();
            s_hud.Initialize();
        }

        public static void Update()
        {
            if (MineSweeper.IsGUI())
                currentScreen.Update();
            if (MineSweeper.IsField())
                s_hud.Update();
        }

        static void InputEngine_onKeyUp(InputEngine.KeyboardArgs e)
        {
            if (MineSweeper.IsGUI())
                currentScreen.OnKeyUp(e);
            if (MineSweeper.IsField())
                s_hud.OnKeyUp(e);
        }

        static void InputEngine_onKeyDown(InputEngine.KeyboardArgs e)
        {
            if (MineSweeper.IsGUI())
                currentScreen.OnKeyDown(e);
            if (MineSweeper.IsField())
                s_hud.OnKeyDown(e);
        }

        static void InputEngine_onButtonUp(InputEngine.MouseArgs e)
        {
            if (MineSweeper.IsGUI())
                currentScreen.OnButtonUp(e);
            if (MineSweeper.IsField())
                s_hud.OnButtonUp(e);
        }

        static void InputEngine_onButtonDown(InputEngine.MouseArgs e)
        {
            if (MineSweeper.IsGUI())
                currentScreen.OnButtonDown(e);
            if (MineSweeper.IsField())
                s_hud.OnButtonDown(e);
        }

        public static void Draw()
        {
            if (MineSweeper.IsGUI())
                currentScreen.Draw();
            if (MineSweeper.IsField())
                s_hud.Draw();
        }

        public static void LoadContent(ContentManager content)
        {
        }
    }
}
