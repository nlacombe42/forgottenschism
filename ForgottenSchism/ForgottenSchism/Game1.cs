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

using ForgottenSchism.engine;
using ForgottenSchism.screen;

namespace ForgottenSchism
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static Game1 instance;

        GraphicsDeviceManager gdm;
        public UnitManage unitManage;
        public CharManage charManage;

        private Game1()
        {
            Graphic.instanciate();
            unitManage = new UnitManage(this);
            charManage = new CharManage(this);

            Content.RootDirectory = "Content";

            Components.Add(unitManage);
            Components.Add(charManage);
            Components.Add(new InputHandler(this));

            gdm = new GraphicsDeviceManager(this);
            Graphic.Instance.GDM = gdm;

            gdm.PreferredBackBufferWidth = 12 * 64;
            gdm.PreferredBackBufferHeight = (int)(8.5 * 64.0);
        }

        public static Game1 Instance
        {
            get
            {
                if (instance == null)
                    instance = new Game1();

                return instance;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();


            LoadContent();
        }

        protected override void LoadContent()
        {
            Graphic.Content.instanciate();

            Graphic.Instance.SB = new SpriteBatch(gdm.GraphicsDevice);

            MainMenu mainMenu = new MainMenu();

            System.Console.Out.WriteLine("state manager called");
            StateManager.Instance.reset(mainMenu);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            System.Console.Out.WriteLine("game draw called");
            Screen sc = StateManager.Instance.State;
            
            if (sc != null)
                sc.Draw(gameTime);
        }
    }
}
