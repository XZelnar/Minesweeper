using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MineSweeper
{
    public class MineSweeper : Microsoft.Xna.Framework.Game
    {
        #region Variables
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static Game.GameField gameField;
        public static String curState = "GUISP";
        public static Vector2 sizeModifier = new Vector2(1f,1f);
        public static ContentManager contentManager;
        #endregion

        #region Events
        public delegate void VoidEventHandler();
        public static event VoidEventHandler OnLevelWon, OnLevelLost;

        public static void InvokeLevelWon()
        {
            gameField.isWon = true;
            gameField.OnLevelComplete();
            if (OnLevelWon != null)
                OnLevelWon.Invoke();
        }

        public static void InvokeLevelLost()
        {
            gameField.isLost = true;
            gameField.OnLevelComplete();
            if (OnLevelLost != null)
                OnLevelLost.Invoke();
        }
        #endregion

        public MineSweeper()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            contentManager = Content;
            Window.Title = "MineSweeper";
            this.IsMouseVisible = true;
            this.Window.AllowUserResizing = true;

            graphics.PreferredBackBufferHeight = 576;
            graphics.PreferredBackBufferWidth = 1024;

            Graphics.GUI.GUIEngine.currentScreen = Graphics.GUI.GUIEngine.s_SP;
        }


        protected override void Initialize()
        {
            base.Initialize();
            Graphics.GraphicsEngine.Initialize();
            Game.GameEngine.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Graphics.GraphicsEngine.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Game.InputEngine.Update();
            Game.GameEngine.Update();
            Entity.EntityManager.Update();
            Graphics.GUI.GUIEngine.Update();

            sizeModifier.X = graphics.GraphicsDevice.Viewport.Width / (float)graphics.PreferredBackBufferWidth;
            sizeModifier.Y = graphics.GraphicsDevice.Viewport.Height / (float)graphics.PreferredBackBufferHeight;
            sizeModifier *= 2;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Graphics.GraphicsEngine.Draw();

            base.Draw(gameTime);
        }





        public static bool IsField()
        {
            if (MineSweeper.curState.StartsWith("Game")) return true;
            return false;
        }

        public static bool IsGUI()
        {
            if (MineSweeper.curState.StartsWith("GUI")) return true;
            return false;
        }
    }
}
