﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.engine;
using ForgottenSchism.screen;

namespace ForgottenSchism.control
{
    public class Window: DrawableGameComponent
    {
        /// <summary>
        /// FocusManager (contains Controls)
        /// </summary>
        FocusManager fm;

        /// <summary>
        /// Parent Display
        /// </summary>
        Screen parent;

        /// <summary>
        /// border of the window
        /// </summary>
        Texture2D tbor;

        bool hasFocus;

        /// <summary>
        /// is main window (in this case no border)
        /// </summary>
        bool ismain;

        /// <summary>
        /// backgroud image of the window
        /// </summary>
        Content.Graphics.CachedImage bg;

        /// <summary>
        /// position of the top left corner of the window
        /// </summary>
        protected Vector2 pos;

        /// <summary>
        /// size of the window
        /// </summary>
        protected Vector2 size;

        public Window(Screen display, bool mainWindow): base(Game1.Instance)
        {
            init(display, mainWindow);
        }

        public Window(Screen display): base(Game1.Instance)
        {
            init(display, false);
        }

        public bool FocusSideArrowEnabled
        {
            get { return fm.SideArrowEnable; }
            set { fm.SideArrowEnable = value; }
        }

        public bool FocusArrowEnabled
        {
            get { return fm.ArrowEnable; }
            set { fm.ArrowEnable = value; }
        }

        public bool InputEnabled
        {
            set { fm.InputEnabled = value; }
            get { return fm.InputEnabled; }
        }

        private void init(Screen display, bool fismain)
        {
            parent = display;
            ismain = fismain;

            if (ismain)
            {
                pos = new Vector2(0, 0);
                size = new Vector2(Game1.Instance.Window.ClientBounds.Width, Game1.Instance.Window.ClientBounds.Height);
            }
            else
            {
                pos = new Vector2(10, 10);
                size = new Vector2(100, 100);
            }

            bg = Content.Graphics.Instance.Images.background.black;

            fm = new FocusManager();
            hasFocus = false;
        }

        /// <summary>
        /// position of the window (only used for background and border display)
        /// </summary>
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public Content.Graphics.CachedImage BackgroundImage
        {
            get { return bg; }
            set { bg = value; }
        }

        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;

                if(!ismain)
                    genBorder();
            }
        }

        /// <summary>
        /// size of the window (only used for background and border display)
        /// </summary>
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// (Re)generate border texture
        /// </summary>
        private void genBorder()
        {
            tbor=Graphic.Instance.rect((int)size.X, (int)size.Y, ColorTheme.Default.getColor(true, hasFocus));
        }

        public virtual void show()
        {
            if(!ismain)
                parent.add(this);
        }

        /// <summary>
        /// adds a Control to the Window
        /// </summary>
        /// <param name="c">Control to add</param>
        public void add(Control c)
        {
            fm.add(c);
        }

        /// <summary>
        /// removes a Control from the Window
        /// </summary>
        /// <param name="c">Control to remove</param>
        public void rem(Control c)
        {
            fm.rem(c);
        }

        /// <summary>
        /// closes the current window
        /// </summary>
        public void close()
        {
            if(!ismain)
                parent.rem(this);
        }

        public void handleInput(GameTime gameTime)
        {
            fm.handleInput(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            fm.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            //if(!ismain)
            //    Graphic.Instance.SB.Draw(tbor, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), Color.White);

            if(ismain)
                Graphic.Instance.SB.Draw(bg.Image, new Rectangle(Game1.Instance.Window.ClientBounds.Width - bg.Image.Width, Game1.Instance.Window.ClientBounds.Height - bg.Image.Height, bg.Image.Width, bg.Image.Height), Color.White);
            else
                Graphic.Instance.SB.Draw(bg.Image, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), Color.White); ;

            fm.Draw(gameTime);
        }
    }
}
