﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    public class ControlManager: DrawableGameComponent
    {
        private List<Control> cls;
        private int sel;
        public bool are;

        public event EventHandler focusChange;

        public ControlManager(Game1 game): base(game)
        {
            cls=new List<Control>();
            sel=-1;
            are = true;
        }

        public bool ArrowEnable
        {
            get { return are; }
            set { are = value; }
        }

        public void loadContent()
        {
            foreach (Control c in cls)
                c.loadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (Control c in cls)
                c.Initialize();
        }

        public void add(Control c)
        {
            cls.Add(c);

            if (sel == -1 && c.Enabled && c.TabStop)
            {
                sel = cls.Count - 1;

                c.HasFocus = true;
            }
        }

        public void rem(Control c)
        {
            cls.Remove(c);

            if (cls.Count == 0)
                sel = -1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!Enabled)
                return;

            if (cls.Count == 0)
                return;

            foreach (Control c in cls)
            {
                if (c.Enabled)
                    c.Update(gameTime);

                if (c.HasFocus)
                {
                    c.HandleInput(gameTime);
                }
            }

            if (are)
            {
                if (InputHandler.keyPressed(Keys.Up))
                    focusPrev();

                if (InputHandler.keyPressed(Keys.Down))
                    focusNext();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (Control c in cls)
            {
                if (c.Visible)
                    c.Draw(gameTime);
            }
        }

        public void focusFirst()
        {
            if (cls.Count < 1)
                return;

            if (cls.Count == 1)
            {
                sel = 0;
                cls[0].HasFocus = true;
                return;
            }

            foreach (Control c in cls)
                c.HasFocus = false;

            sel = cls.Count-1;
            focusNext();
        }

        public void focusNext()
        {
            if (cls.Count < 2)
                return;

            bool s = false;
            bool m1 = false;

            if (sel == -1)
            {
                sel = 0;
                m1 = true;
            }

            int i = sel + 1;

            if (i > cls.Count - 1)
                i = 0;

            while(i != sel)
            {
                if (cls[i].Enabled && cls[i].TabStop)
                {
                    cls[sel].HasFocus = false;
                    sel = i;
                    cls[sel].HasFocus = true;

                    if (focusChange != null)
                        focusChange(cls[sel], null);

                    s = true;

                    break;
                }

                i++;

                if (i > cls.Count - 1)
                    i = 0;
            }

            if (cls[i].Enabled && cls[i].TabStop)
            {
                cls[sel].HasFocus = false;
                sel = i;
                cls[sel].HasFocus = true;

                if (focusChange != null)
                    focusChange(cls[sel], null);

                s = true;
            }

            if (m1&&!s)
                sel = -1;
        }

        public void focusPrev()
        {
            if (cls.Count < 2)
                return;

            bool s = false;
            bool m1 = false;

            if (sel == -1)
            {
                sel = 0;
                m1 = true;
            }

            int i = sel - 1;

            if (i < 0)
                i = cls.Count - 1;

            while(i != sel)
            {
                if (cls[i].Enabled && cls[i].TabStop)
                {
                    cls[sel].HasFocus = false;
                    sel = i;
                    cls[sel].HasFocus = true;

                    if (focusChange != null)
                        focusChange(cls[sel], null);

                    s = true;

                    break;
                }

                i--;

                if (i < 0)
                    i = cls.Count - 1;
            }

            if (cls[i].Enabled && cls[i].TabStop)
            {
                cls[sel].HasFocus = false;
                sel = i;
                cls[sel].HasFocus = true;

                if (focusChange != null)
                    focusChange(cls[sel], null);

                s = true;
            }

            if (m1 && !s)
                sel = -1;
        }
    }
}
