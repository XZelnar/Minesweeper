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
    public class MainMenu : GUIScreen
    {
        public override void Initialize()
        {
            Elements.Button sp = new Elements.Button(0, 20, 100, 20, "Single Player");
            sp.OnClick = this.GetType().GetMethod("spClick");
            sp.OnClickInvokeObject = this;
            controls.Add(sp);
        }

        public void spClick()
        {
            MineSweeper.curState = "GUISinglePlayer";
            GUIEngine.currentScreen = GUIEngine.s_SP;
        }
    }
}
