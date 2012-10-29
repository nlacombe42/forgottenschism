﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.control;
using ForgottenSchism.engine;

namespace ForgottenSchism.screen
{
    public abstract class Screen : DrawableGameComponent
    {
        protected ControlManager cm;
        private Content.Graphics.CachedImage bgimg;

        protected Screen(): base(Game1.Instance)
        {
            cm = new ControlManager();

            Enabled = false;
            Visible = false;

            bgimg = Content.Graphics.Instance.Images.background.black;
        }

        protected Content.Graphics.CachedImage BgImg
        {
            set { bgimg = value; }
        }

        public override void Initialize()
        {
            base.Initialize();

            cm.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Enabled)
                cm.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible)
                return;

            Graphic.Instance.SB.Begin();

            base.Draw(gameTime);

            if (bgimg != null)
                Graphic.Instance.SB.Draw(bgimg.Image, new Rectangle(0, 0, Graphic.Instance.GDM.PreferredBackBufferWidth, Graphic.Instance.GDM.PreferredBackBufferHeight), Color.White);

            cm.Draw(gameTime);

            Graphic.Instance.SB.End();
        }

        public virtual void start()
        {
            Enabled = true;
            Visible = true;

            cm.focusFirst();
        }

        public virtual void resume()
        {
            Visible = true;
            Enabled = true;

            cm.Enabled = true;

            resumeUpdate();
        }

        public virtual void pause()
        {
            Visible = false;
            Enabled = false;

            cm.Enabled = false;
        }

        public virtual void stop()
        {
            Enabled = false;
            Visible = false;
        }

        public virtual void resumeUpdate()
        {

        }
    }
}
