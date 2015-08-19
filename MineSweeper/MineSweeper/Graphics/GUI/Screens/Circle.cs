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
    public class Circle : GUIScreen
    {
        Elements.Label title;
        public override void Initialize()
        {
            title = new Elements.Label(5, 5, "Circle");
            title.font = MineSweeper.contentManager.Load<SpriteFont>("Fonts/TitleFont");
            controls.Add(title);

            Elements.Button easy = new Elements.Button(0, 45, 256, 40, "Easy");
            easy.OnClick = this.GetType().GetMethod("easyClick");
            easy.OnClickInvokeObject = this;
            controls.Add(easy);

            Elements.Button medium = new Elements.Button(0, 105, 256, 40, "Medium");
            medium.OnClick = this.GetType().GetMethod("mediumClick");
            medium.OnClickInvokeObject = this;
            controls.Add(medium);

            Elements.Button hard = new Elements.Button(0, 165, 256, 40, "Hard");
            hard.OnClick = this.GetType().GetMethod("hardClick");
            hard.OnClickInvokeObject = this;
            controls.Add(hard);

            Elements.Button custom = new Elements.Button(0, 225, 256, 40, "Custom");
            custom.OnClick = this.GetType().GetMethod("customClick");
            custom.OnClickInvokeObject = this;
            controls.Add(custom);

            Elements.Button back = new Elements.Button(0, 285, 256, 40, "Back");
            back.OnClick = this.GetType().GetMethod("backClick");
            back.OnClickInvokeObject = this;
            controls.Add(back);
        }

        public override void Update()
        {
            base.Update();

            int w = MineSweeper.graphics.GraphicsDevice.Viewport.Width;
            for (int i = 0; i < controls.Count; i++)
                controls[i].position.X = (w - controls[i].size.X) / 2;
            title.position.X -= 35;
        }

        public void easyClick()
        {
            MineSweeper.curState = "GameSPCircleEasy";
            MineSweeper.gameField = new Game.GameFieldCircle(5, 10);
            MineSweeper.gameField.Generate();
        }

        public void mediumClick()
        {
            MineSweeper.curState = "GameSPCircleMedium";
            MineSweeper.gameField = new Game.GameFieldCircle(9, 40);
            MineSweeper.gameField.Generate();
        }

        public void hardClick()
        {
            MineSweeper.curState = "GameSPCircleHard";
            MineSweeper.gameField = new Game.GameFieldCircle(12, 85);
            MineSweeper.gameField.Generate();
        }

        public void customClick()
        {
            MineSweeper.curState = "GUICircleCustom";
            GUIEngine.currentScreen = GUIEngine.s_circleCustom;
        }

        public void backClick()
        {
            MineSweeper.curState = "GUISinglePlayer";
            GUIEngine.currentScreen = GUIEngine.s_SP;
        }
    }
}
