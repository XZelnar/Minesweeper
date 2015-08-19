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
    public class SquareClassicCustom : GUIScreen
    {
        Elements.Label title;
        Elements.Label lx, ly, lm;
        Elements.NumericTextBox x;
        Elements.NumericTextBox y;
        Elements.NumericTextBox mines;
        Elements.Label errors;
        Elements.Button back, start;
        public override void Initialize()
        {
            title = new Elements.Label(5, 5, "Custom rectangle");
            title.font = MineSweeper.contentManager.Load<SpriteFont>("Fonts/TitleFont");
            controls.Add(title);

            lx = new Elements.Label(0, 38, "Width");
            controls.Add(lx);
            ly = new Elements.Label(0, 78, "Height");
            controls.Add(ly);
            lm = new Elements.Label(0, 118, "Mines");
            controls.Add(lm);

            x = new Elements.NumericTextBox(100, 35, 100, 20, "10");
            x.maxLength = 2;
            controls.Add(x);
            y = new Elements.NumericTextBox(100, 75, 100, 20, "10");
            controls.Add(y);
            y.maxLength = 2;
            mines = new Elements.NumericTextBox(100, 115, 100, 20, "20");
            controls.Add(mines);
            mines.maxLength = 3;

            errors = new Elements.Label(250, 210, "");
            errors.foreground = Color.DarkRed;
            controls.Add(errors);

            back = new Elements.Button(0, 155, 100, 40, "Back");
            back.OnClick = this.GetType().GetMethod("backClick");
            back.OnClickInvokeObject = this;
            controls.Add(back);

            start = new Elements.Button(120, 155, 100, 40, "Play");
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
            ly.position.X -= 100;
            lm.position.X -= 100;
            back.position.X -= 55;
            x.position.X += 55;
            y.position.X += 55;
            mines.position.X += 55;
            start.position.X += 55;
            title.position.X -= 102;

            errors.position.X -= 105;
        }

        public void startClick()
        {
            if (x.text == "" || y.text == "" || mines.text == "")
            {
                errors.text = "Fields can't be empty!";
                return;
            }
            else
            {
                errors.text = "";
            }
            MineSweeper.curState = "GameSPSquareClassicCustom";
            int ix = Convert.ToInt32(x.text),
                iy = Convert.ToInt32(y.text),
                im = Convert.ToInt32(mines.text);
            if (ix < 10)
                ix = 10;
            if (iy < 10)
                iy = 10;
            if (im < 10)
                im = 10;
            if (im > ix * iy / 4)
                im = ix * iy / 4;
            MineSweeper.gameField = new Game.GameFieldSquare(new Vector3(ix, iy, 1), im);
            MineSweeper.gameField.Generate();
        }

        public void backClick()
        {
            MineSweeper.curState = "GUISquareClassic";
            GUIEngine.currentScreen = GUIEngine.s_squareClassic;
        }


    }
}
