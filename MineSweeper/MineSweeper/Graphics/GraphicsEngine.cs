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

namespace MineSweeper.Graphics
{
    public static class GraphicsEngine
    {
        static BasicEffect effect;
        public static RasterizerState s_Regular = new RasterizerState();
        public static RasterizerState s_ScissorsOn = new RasterizerState() { ScissorTestEnable = true };

        public static void Initialize()
        {
            effect = new BasicEffect(MineSweeper.graphics.GraphicsDevice);
            effect.VertexColorEnabled = true;
            effect.Projection = Matrix.CreateOrthographicOffCenter
                (0, MineSweeper.graphics.GraphicsDevice.Viewport.Width,
                MineSweeper.graphics.GraphicsDevice.Viewport.Height, 0,
                0, 1);

            GUI.GUIEngine.Initialize();
        }

        public static void LoadContent(ContentManager content)
        {
            Game.Tiles.TileSquare.tileFont = content.Load<SpriteFont>("Fonts/TileFont");
            GUI.Elements.Button.font = content.Load<SpriteFont>("Fonts/ButtonFont");
            GUI.Elements.TextBox.font = content.Load<SpriteFont>("Fonts/ButtonFont");
            GUI.Elements.Label.defaultFont = content.Load<SpriteFont>("Fonts/ButtonFont");

            Game.Tiles.TileSquare.texture = content.Load<Texture2D>("Game/square");
            Entity.EntityManager.sprites = content.Load<Texture2D>("Game/entityes");
            GUI.Elements.Button.texture = content.Load<Texture2D>("GUI/button");
            GUI.Elements.TextBox.texture = content.Load<Texture2D>("GUI/textbox");
            GUI.Elements.Panel.texture = content.Load<Texture2D>("GUI/button");

            GUI.GUIEngine.LoadContent(content);
        }

        public static void Draw()
        {
            MineSweeper.spriteBatch.Begin();
            if (MineSweeper.IsField())
            {
                MineSweeper.gameField.Draw();
                Entity.EntityManager.Draw();
            }
            GUI.GUIEngine.Draw();
            MineSweeper.spriteBatch.End();
        }
    }
}
