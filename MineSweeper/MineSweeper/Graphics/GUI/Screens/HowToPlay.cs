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
    public class HowToPlay : GUIScreen
    {
        Elements.Label title;
        Elements.Button back;
        public override void Initialize()
        {
            title = new Elements.Label(5, 5, "Controls");
            title.font = MineSweeper.contentManager.Load<SpriteFont>("Fonts/TitleFont");
            controls.Add(title);

            Elements.Label info = new Elements.Label(5, 45, "<LMB> - Open tile  \n<RMB> - Mark mine  \n<LMB> + <RMB> - Open nearby tiles if possible  \nHold <LMB> to drag field  \n[ESC] - Exit to menu");
            controls.Add(info);

            back = new Elements.Button(0, 285, 256, 40, "Back");
            back.OnClick = this.GetType().GetMethod("backClick");
            back.OnClickInvokeObject = this;
            controls.Add(back);
        }

        public override void Update()
        {
            base.Update();

            int w = MineSweeper.graphics.GraphicsDevice.Viewport.Width;
            back.position.X = (MineSweeper.graphics.GraphicsDevice.Viewport.Width - back.size.X) / 2;
            title.position.X = (w - title.size.X) / 2 - 55;
        }

        public void backClick()
        {
            MineSweeper.curState = "GUISinglePlayer";
            GUIEngine.currentScreen = GUIEngine.s_SP;
        }
    }
}
