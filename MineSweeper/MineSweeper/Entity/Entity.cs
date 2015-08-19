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

namespace MineSweeper.Entity
{
    public abstract class Entity
    {
        public Vector3 position = new Vector3(), size = new Vector3();
        public int state;
        public bool isDead = false;

        public abstract void Update();

        public abstract void Draw();
    }
}
