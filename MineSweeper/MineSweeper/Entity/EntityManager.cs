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
    public static class EntityManager
    {
        public static List<Entity> entities = new List<Entity>();
        public static Texture2D sprites;

        public static void Update()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update();
                if (entities[i].isDead)
                    entities.RemoveAt(i);
            }
        }

        public static void Draw()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw();
            }
        }

        public static void AddSquareCross(Vector3 p, Vector3 s)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if ((entities[i].GetType() == typeof(BlinkingCross2D)) && entities[i].position == p)
                {
                    entities[i].state = 0;
                    return;
                }
            }
            entities.Add(new BlinkingCross2D(p, s));
        }

        public static void AddSquareFadeOut(Vector3 p, Vector3 s)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if ((entities[i].GetType() == typeof(SquareFadeOut2D)) && entities[i].position == p)
                {
                    entities[i].state = 0;
                    return;
                }
            }
            entities.Add(new SquareFadeOut2D(p, s));
        }

        public static void AddMine(Vector3 p, Vector3 s)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if ((entities[i].GetType() == typeof(Mine)) && entities[i].position == p)
                {
                    return;
                }
            }
            entities.Add(new Mine(p, s));
        }

        public static void ClearMines()
        {
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                if (entities[i] is Mine)
                    entities.RemoveAt(i);
            }
        }
    }
}
