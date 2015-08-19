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

namespace MineSweeper.Graphics.GUI.Elements
{
    public abstract class Element
    {
        public Vector3 position = new Vector3(), size = new Vector3();
        public bool isFocused = false;
        public bool isVisible = true;

        public virtual void Draw() { }

        public virtual void Update() { }

        public virtual void Initialize() { }

        public virtual void OnButtonDown(Game.InputEngine.MouseArgs e) { }

        public virtual void OnButtonUp(Game.InputEngine.MouseArgs e) { }

        public virtual void OnKeyDown(Game.InputEngine.KeyboardArgs e) { }

        public virtual void OnKeyUp(Game.InputEngine.KeyboardArgs e) { }

        public bool IsIn(int x, int y)
        {
            if (x >= position.X && x <= position.X + size.X &&
                y >= position.Y && y <= position.Y + size.Y)
                return true;
            return false;
        }
    }
}
