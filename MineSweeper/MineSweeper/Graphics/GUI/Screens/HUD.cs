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

namespace MineSweeper.Graphics.GUI.Screens
{
    public class HUD : GUIScreen
    {
        Elements.Panel pMines;
        Elements.Label mines;
        Elements.Panel pTime;
        Elements.Label time;
        Elements.Panel panel;
        Elements.Label state;
        Elements.Label gameTime;
        Elements.Button menu;
        public override void Initialize()
        {
            pMines = new Elements.Panel(0, 0, 160, 20);
            controls.Add(pMines);

            mines = new Elements.Label(5, 20, "");
            controls.Add(mines);

            pTime = new Elements.Panel(0, 0, 120, 20);
            controls.Add(pTime);

            time = new Elements.Label(5, 20, "");
            controls.Add(time);

            panel = new Elements.Panel(0, 0, 200, 100);
            panel.isVisible = false;
            controls.Add(panel);

            state = new Elements.Label(0, 0, "");
            state.isVisible = false;
            controls.Add(state);

            gameTime = new Elements.Label(0, 0, "");
            gameTime.isVisible = false;
            controls.Add(gameTime);

            menu = new Elements.Button(0, 0, 180, 20, "Menu");
            menu.isVisible = false;
            menu.OnClick = this.GetType().GetMethod("menuClick");
            menu.OnClickInvokeObject = this;
            controls.Add(menu);

            MineSweeper.OnLevelLost += new MineSweeper.VoidEventHandler(MineSweeper_OnLevelLost);
            MineSweeper.OnLevelWon += new MineSweeper.VoidEventHandler(MineSweeper_OnLevelWon);
        }

        void MineSweeper_OnLevelWon()
        {
            panel.isVisible = true;
            state.isVisible = true;
            menu.isVisible = true;
            gameTime.isVisible = true;
            gameTime.text = time.text;
            state.text = "Victory";
        }

        void MineSweeper_OnLevelLost()
        {
            panel.isVisible = true;
            state.isVisible = true;
            menu.isVisible = true;
            gameTime.isVisible = true;
            gameTime.text = time.text;
            state.text = "You blew up :(";
        }

        public override void Update()
        {
            base.Update();

            pMines.position.Y = mines.position.Y = MineSweeper.graphics.GraphicsDevice.Viewport.Height - 20;
            mines.text = "Mines left: " + MineSweeper.gameField.minesLeft.ToString();

            pTime.position.X = MineSweeper.graphics.GraphicsDevice.Viewport.Width - pTime.size.X;
            pTime.position.Y = time.position.Y = MineSweeper.graphics.GraphicsDevice.Viewport.Height - pTime.size.Y;
            time.position.X = pTime.position.X + 5;
            TimeSpan t;
            if (MineSweeper.gameField.isLost || MineSweeper.gameField.isWon)
                t = MineSweeper.gameField.duration;
            else
                t = DateTime.Now - MineSweeper.gameField.start;
            if (t.TotalMinutes >= 100)
            {
                time.text = "Time: 99:59";
            }
            else
            {
                String s1 = ((int)t.TotalMinutes).ToString();
                if (s1.Length == 1) s1 = "0" + s1;
                String s2 = t.Seconds.ToString();
                if (s2.Length == 1) s2 = "0" + s2;
                time.text = "Time: " + s1 + ":" + s2;
            }

            panel.position.X = (MineSweeper.graphics.GraphicsDevice.Viewport.Width - panel.size.X) / 2;
            panel.position.Y = (MineSweeper.graphics.GraphicsDevice.Viewport.Height - panel.size.Y) / 2;
            menu.position.X = panel.position.X + 10;
            menu.position.Y = panel.position.Y + panel.size.Y - menu.size.Y - 10;
            state.position.X = panel.position.X + 10;
            state.position.Y = panel.position.Y + 10;
            gameTime.position.X = state.position.X;
            gameTime.position.Y = state.position.Y + 30;
        }

        public override void OnKeyDown(Game.InputEngine.KeyboardArgs e)
        {
            if (e.key == Keys.Escape.GetHashCode())
            {
                menuClick();
            }

            base.OnKeyDown(e);
        }

        public void menuClick()
        {
            panel.isVisible = false;
            state.isVisible = false;
            menu.isVisible = false;
            gameTime.isVisible = false;
            Entity.EntityManager.ClearMines();
            MineSweeper.curState = "GUISinglePlayer";
            GUIEngine.currentScreen = GUIEngine.s_SP;
        }
    }
}
