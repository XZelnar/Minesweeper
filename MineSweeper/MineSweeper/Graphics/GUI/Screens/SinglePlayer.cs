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
    public class SinglePlayer : GUIScreen
    {
        Elements.Label title;
        public override void Initialize()
        {
            title = new Elements.Label(5, 5, "Select field type");
            title.font = MineSweeper.contentManager.Load<SpriteFont>("Fonts/TitleFont");
            controls.Add(title);

            Elements.Button squareClassic = new Elements.Button(0, 45, 256, 40, "Rectangle");
            squareClassic.OnClick = this.GetType().GetMethod("scClick");
            squareClassic.OnClickInvokeObject = this;
            controls.Add(squareClassic);

            Elements.Button circle = new Elements.Button(0, 105, 256, 40, "Circle");
            circle.OnClick = this.GetType().GetMethod("cClick");
            circle.OnClickInvokeObject = this;
            controls.Add(circle);

            Elements.Button howToPlay = new Elements.Button(0, 205, 256, 40, "Controls");
            howToPlay.OnClick = this.GetType().GetMethod("htpClick");
            howToPlay.OnClickInvokeObject = this;
            controls.Add(howToPlay);
        }

        public override void Update()
        {
            base.Update();

            int w = MineSweeper.graphics.GraphicsDevice.Viewport.Width;
            for (int i = 0; i < controls.Count; i++)
                controls[i].position.X = (w - controls[i].size.X) / 2;
            title.position.X -= 110;
        }

        public void scClick()
        {
            MineSweeper.curState = "GUISquareClassic";
            GUIEngine.currentScreen = GUIEngine.s_squareClassic;
        }

        public void cClick()
        {
            MineSweeper.curState = "GUICircle";
            GUIEngine.currentScreen = GUIEngine.s_circle;
        }

        public void htpClick()
        {
            MineSweeper.curState = "GUIHowToPlay";
            GUIEngine.currentScreen = GUIEngine.s_howToPlay;
        }
    }
}
