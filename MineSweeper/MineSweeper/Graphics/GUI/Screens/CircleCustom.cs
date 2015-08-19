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
    public class CircleCustom : GUIScreen
    {
        Elements.Label title;
        Elements.Label lx, lm;
        Elements.NumericTextBox r;
        Elements.NumericTextBox mines;
        Elements.Label errors;
        Elements.Button back, start;
        public override void Initialize()
        {
            title = new Elements.Label(5, 5, "Custom circle");
            title.font = MineSweeper.contentManager.Load<SpriteFont>("Fonts/TitleFont");
            controls.Add(title);

            lx = new Elements.Label(0, 48, "Radius");
            controls.Add(lx);
            lm = new Elements.Label(0, 78, "Mines");
            controls.Add(lm);

            r = new Elements.NumericTextBox(100, 45, 100, 20, "10");
            r.maxLength = 2;
            controls.Add(r);
            mines = new Elements.NumericTextBox(100, 75, 100, 20, "45");
            controls.Add(mines);
            mines.maxLength = 3;

            errors = new Elements.Label(250, 170, "");
            errors.foreground = Color.DarkRed;
            controls.Add(errors);

            back = new Elements.Button(0, 115, 100, 40, "Back");
            back.OnClick = this.GetType().GetMethod("backClick");
            back.OnClickInvokeObject = this;
            controls.Add(back);

            start = new Elements.Button(120, 115, 100, 40, "Play");
            start.OnClick = this.GetType().GetMethod("startClick");
            start.OnClickInvokeObject = this;
            controls.Add(start);
        }

        public override void Update()
        {
            base.Update();

            int w = MineSweeper.graphics.GraphicsDevice.Viewport.Width;
            for (int i = 0; i < controls.Count; i++)
                controls[i].position.X = (w - controls[i].size.X) / 2;
            lx.position.X -= 100;
            lm.position.X -= 100;
            back.position.X -= 55;
            r.position.X += 55;
            mines.position.X += 55;
            start.position.X += 55;
            title.position.X -= 85;

            errors.position.X -= 105;
        }

        public void startClick()
        {
            if (r.text == "" || mines.text == "")
            {
                errors.text = "Fields can't be empty!";
                return;
            }
            else
            {
                errors.text = "";
            }
            MineSweeper.curState = "GameSPCircleCustom";
            int ir = Convert.ToInt32(r.text),
                im = Convert.ToInt32(mines.text);
            if (ir < 5)
                ir = 5;
            if (im < 10)
                im = 10;
            if (im >      Math.PI * ir * ir / 4)
                im = (int)Math.PI * ir * ir / 4;
            MineSweeper.gameField = new Game.GameFieldCircle(ir, im);
            MineSweeper.gameField.Generate();
        }

        public void backClick()
        {
            MineSweeper.curState = "GUICircle";
            GUIEngine.currentScreen = GUIEngine.s_circle;
        }


    }
}
