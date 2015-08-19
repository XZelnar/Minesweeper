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
    public abstract class GUIScreen
    {
        public List<Elements.Element> controls = new List<Elements.Element>();

        public virtual void Initialize()
        {
            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].Initialize();
            }
        }

        public virtual void Draw()
        {
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].isVisible)
                {
                    controls[i].Draw();
                }
            }
        }

        public virtual void Update()
        {
            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].Update();
            }
        }

        public virtual void OnButtonDown(Game.InputEngine.MouseArgs e)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].isVisible)
                {
                    controls[i].OnButtonDown(e);
                }
            }
        }

        public virtual void OnButtonUp(Game.InputEngine.MouseArgs e)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].isVisible)
                {
                    controls[i].OnButtonUp(e);
                }
            }
        }

        public virtual void OnKeyDown(Game.InputEngine.KeyboardArgs e)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].isVisible)
                {
                    controls[i].OnKeyDown(e);
                }
            }
        }

        public virtual void OnKeyUp(Game.InputEngine.KeyboardArgs e)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].isVisible)
                {
                    controls[i].OnKeyUp(e);
                }
            }
        }
    }
}