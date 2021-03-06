﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.world;

namespace ForgottenSchism.engine
{
    public class Graphic
    {

        static Graphic instance;

        SpriteBatch sb;
        GraphicsDeviceManager gdm;

        private Graphic()
        {
            //
        }

        public static Graphic Instance
        {
            get
            {
                if (instance == null)
                    instance = new Graphic();

                return instance;
            }
        }

        public static void instanciate()
        {
            if (instance == null)
                instance = new Graphic();
        }

        public SpriteBatch SB
        {
            get { return sb; }
            set { sb = value; }
        }

        public GraphicsDeviceManager GDM
        {
            get { return gdm; }
            set { gdm = value; }
        }

        /// <summary>
        /// Get the image corresponding to the character
        /// </summary>
        /// <param name="c">Character to get the image from</param>
        /// <returns>The image of the character</returns>
        public static Content.Graphics.CachedImage getSprite(Character c)
        {
            if (c.Type == Character.Class_Type.FIGHTER)
                return Content.Graphics.Instance.Images.characters.fighter;
            else if (c.Type == Character.Class_Type.HEALER)
                return Content.Graphics.Instance.Images.characters.healer;
            else if (c.Type == Character.Class_Type.ARCHER)
                return Content.Graphics.Instance.Images.characters.archer;
            else if (c.Type == Character.Class_Type.CASTER)
                return Content.Graphics.Instance.Images.characters.caster;
            else if (c.Type == Character.Class_Type.SCOUT)
                return Content.Graphics.Instance.Images.characters.scout;
            else
                return Content.Graphics.Instance.Images.characters.healer;
        }

        /// <summary>
        /// Gets misc for WorldMap
        /// </summary>
        /// <param name="org"></param>
        /// <param name="movement"></param>
        /// <returns></returns>
        public Texture2D getMisc(String org, int movement)
        {
            Texture2D t = new Texture2D(gdm.GraphicsDevice, 64, 64, true, SurfaceFormat.Color);
            Color[] color = new Color[64 * 64];

            Color c;

            int i = 0;

            for (int y = 0; y < 64; y++)
                for (int x = 0; x < 64; x++)
                {
                    if (y >= 5 && y <= 10 && x >= 54 && x <= 59)
                    {
                        if (movement == 0)
                        {
                            if (y == 5 || y == 10 || x == 54 || x == 59)
                            {
                                c = Color.Black;
                            }
                            else
                            {
                                c = Color.Gold;
                            }
                        }
                        else
                        {
                            c = Color.Transparent;
                        }
                    }
                    else if (y >= 5 && y <= 10 && x >= 5 && x <= 10)
                    {
                        if (y == 5 || y == 10 || x == 5 || x == 10)
                        {
                            c = Color.Black;
                        }
                        else
                        {
                            if (org == "main")
                            {
                                c = Color.Blue;
                            }
                            else if (org == "enemy")
                            {
                                c = Color.Red;
                            }
                            else
                            {
                                c = Color.Green;
                            }
                        }
                    }
                    else
                        c = Color.Transparent;

                    color[i] = c;

                    i++;
                }

            t.SetData(color);

            return t;
        }

        public Texture2D getMisc(Unit unit)
        {
            Texture2D t = new Texture2D(gdm.GraphicsDevice, 64, 64, true, SurfaceFormat.Color);
            Color[] color = new Color[64 * 64];

            Color c;

            int i = 0;

            for (int y = 0; y < 64; y++)
                for (int x = 0; x < 64; x++)
                {
                    if (y >= 5 && y <= 10 && x >= 54 && x <= 59)
                    {
                        if (unit.movement == 0)
                        {
                            if (y == 5 || y == 10 || x == 54 || x == 59)
                            {
                                c = Color.Black;
                            }
                            else
                            {
                                c = Color.Gold;
                            }
                        }
                        else
                        {
                            c = Color.Transparent;
                        }
                    }
                    else if (y >= 5 && y <= 10 && x >= 5 && x <= 10)
                    {
                        if (y == 5 || y == 10 || x == 5 || x == 10)
                        {
                            c = Color.Black;
                        }
                        else
                        {
                            if (unit.Organization == "main")
                            {
                                c = Color.Blue;
                            }
                            else if (unit.Organization == "enemy")
                            {
                                c = Color.Red;
                            }
                            else
                            {
                                c = Color.Green;
                            }
                        }
                    }
                    else
                        c = Color.Transparent;

                    color[i] = c;

                    i++;
                }

            t.SetData(color);

            return t;
        }

        /// <summary>
        /// Returns the HP bar
        /// </summary>
        /// <param name="c">Character where the HP is from</param>
        /// <returns>HP Bar Texture</returns>
        public Texture2D getMisc(Character ch)
        {
            int npp = (int)(44 * (((double)ch.stats.hp) / (double)ch.stats.maxHp)) + 10;
            int manapp = (int)(44 * (((double)ch.stats.mana) / (double)ch.stats.maxMana)) + 10;

            Texture2D t = new Texture2D(gdm.GraphicsDevice, 64, 64, true, SurfaceFormat.Color);
            Color[] color = new Color[64 * 64];

            Color c;

            int i = 0;

            for (int y = 0; y < 64; y++)
                for (int x = 0; x < 64; x++)
                {
                    if (y >= 5 && y <= 10 && x >= 54 && x <= 59)
                    {
                        if (ch.stats.movement == 0)
                        {
                            if (y == 5 || y == 10 || x == 54 || x == 59)
                            {
                                c = Color.Black;
                            }
                            else
                            {
                                c = Color.Gold;
                            }
                        }
                        else
                        {
                            c = Color.Transparent;
                        }
                    }
                    else if (y >= 5 && y <= 10 && x >= 5 && x <= 10)
                    {
                        if (y == 5 || y == 10 || x == 5 || x == 10)
                        {
                            c = Color.Black;
                        }
                        else
                        {
                            if (ch.Organization == "main")
                            {
                                c = Color.Blue;
                            }
                            else if (ch.Organization == "enemy")
                            {
                                c = Color.Red;
                            }
                            else
                            {
                                c = Color.Green;
                            }
                        }
                    }
                    else if (y >= 40 && y <= 45 && x >=10 && x <=54)
                    {
                        if(y == 40 || y == 45 || x == 10 || x == 54)
                        {
                            c = Color.Black;
                        }
                        else
                        {
                            if (x <= npp)
                                c = Color.OrangeRed;
                            else
                                c = Color.Black;
                        }
                    }
                    else if (y >= 50 && y <= 55 && x >= 10 && x <= 54)
                    {
                        if(y == 50 || y == 55 || x == 10 || x == 54)
                        {
                            c = Color.Black;
                        }
                        else
                        {
                            if(ch.stats.maxMana == 0)
                            {
                                c = Color.Black;
                            }
                            else
                            {
                                if (x <= manapp)
                                    c = Color.Cyan;
                                else
                                    c = Color.Black;
                            }
                        }
                    }
                    else
                        c = Color.Transparent;

                    color[i] = c;

                    i++;
                }

            t.SetData(color);

            return t;
        }

        public Texture2D arrowUp(int w, int h, Color fg)
        {
            Texture2D t = new Texture2D(gdm.GraphicsDevice, w, h, true, SurfaceFormat.Color);
            Color[] color = new Color[w * h];

            Color c;

            int i = 0;

            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    if (x < (w / 2))
                        if (y >= h - x - 1)
                            c = fg;
                        else
                            c = Color.Transparent;
                    else
                        if (y>=x)
                            c = fg;
                        else
                            c = Color.Transparent;
                    
                        

                    color[i] = c;

                    i++;
                }

            t.SetData(color);

            return t;
        }

        public Texture2D arrowDown(int w, int h, Color fg)
        {
            Texture2D t = new Texture2D(gdm.GraphicsDevice, w, h, true, SurfaceFormat.Color);
            Color[] color = new Color[w * h];

            Color c;

            int i = 0;

            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    if (x < (w / 2))
                        if (y <= x )
                            c = fg;
                        else
                            c = Color.Transparent;
                    else
                        if (y<=w-x)
                            c = fg;
                        else
                            c = Color.Transparent;

                    color[i] = c;

                    i++;
                }

            t.SetData(color);

            return t;
        }

        public Texture2D arrowLeft(int w, int h, Color fg)
        {
            Texture2D t = new Texture2D(gdm.GraphicsDevice, w, h, true, SurfaceFormat.Color);
            Color[] color = new Color[w * h];

            Color c;

            int i = 0;
            int ty;

            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    if (y < (h / 2))
                        ty = y;
                    else
                        ty = h - y;

                    if ((w-x) <= ty)
                        c = fg;
                    else
                        c = Color.Transparent;

                    color[i] = c;

                    i++;
                }

            t.SetData(color);

            return t;
        }

        public Texture2D arrowRight(int w, int h, Color fg)
        {
            Texture2D t = new Texture2D(gdm.GraphicsDevice, w, h, true, SurfaceFormat.Color);
            Color[] color = new Color[w * h];

            Color c;

            int i = 0;
            int ty;

            for (int y = 0; y < h; y++)
                for(int x = 0; x < w; x++)
                {
                    if (y < (h / 2))
                        ty = y;
                    else
                        ty = h - y;

                        if (x <= ty)
                            c = fg;
                        else
                            c = Color.Transparent;

                    color[i]=c;

                    i++;
                }

            t.SetData(color);

            return t;
        }

        public Texture2D rect(int w, int h, Color c)
        {
            Texture2D rectangleTexture = new Texture2D(gdm.GraphicsDevice, w, h, true, SurfaceFormat.Color);
            Color[] color = new Color[w*h];
            
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = c;
            }

            rectangleTexture.SetData(color);
            
            return rectangleTexture;
        }
    }
}
