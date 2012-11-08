﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.world;

namespace ForgottenSchism.engine
{
    public class Content
    {
        public class Graphics
        {
            public struct SImages
            {
                public struct SGUI
                {
                    public CachedImage cursor;
                    public CachedImage selCursor;
                }

                public struct SBG
                {
                    public CachedImage black;
                }

                public struct SCharacters
                {
                    public CachedImage healer;
                    public CachedImage fighter;
                    public CachedImage archer;
                    public CachedImage caster;
                    public CachedImage scout;
                }

                public Dictionary<Tile.TileType, CachedImage> tiles;
                public CachedImage fog;
                public SCharacters characters;
                public SGUI gui;
                public SBG background;
            }

            public abstract class CachedImage
            {
                protected Texture2D img;

                public Texture2D Image
                {
                    get { return img; }
                    set { img = value; }
                }
            }

            private class CachedImageInst : CachedImage
            {
                public CachedImageInst(Texture2D t)
                {
                    img = t;
                }
            }

            static Graphics instance;

            SpriteFont defFont;
            SpriteFont monoFont;
            SImages images;
            CachedImage testimg;

            private Graphics()
            {
                loadContent();
            }

            public static Graphics Instance
            {
                get
                {
                    if (instance == null)
                        instance = new Graphics();

                    return instance;
                }
            }

            public static void instantiate()
            {
                if (instance == null)
                    instance = new Graphics();
            }

            public SImages Images
            {
                get { return images; }
            }

            public SpriteFont DefaultFont
            {
                get { return defFont; }
            }

            public SpriteFont MonoFont
            {
                get { return monoFont; }
            }

            public CachedImage TestImage
            {
                get { return testimg; }
            }

            private void loadContent()
            {
                defFont = Game1.Instance.Content.Load<SpriteFont>(@"font\\arial12norm");
                monoFont = Game1.Instance.Content.Load<SpriteFont>(@"font\\mono12norm");

                testimg =new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\test"));

                images.background.black = new CachedImageInst(Graphic.Instance.rect(Graphic.Instance.GDM.PreferredBackBufferWidth, Graphic.Instance.GDM.PreferredBackBufferHeight, Color.Black));

                images.gui.cursor = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\gui\\cur"));
                images.gui.selCursor = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\gui\\sel"));

                images.characters.healer = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\healer"));
                images.characters.fighter = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\fighter"));
                images.characters.archer = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\archer"));
                images.characters.caster = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\caster"));
                images.characters.scout = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\scout"));
                
                images.tiles = new Dictionary<Tile.TileType, CachedImage>();
                
                images.tiles.Add(Tile.TileType.CITY, new CachedImageInst(Graphic.Instance.rect(64, 64, Color.White)));
                images.tiles.Add(Tile.TileType.FOREST, new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Green)));
                images.tiles.Add(Tile.TileType.MOUNTAIN, new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Brown)));
                images.tiles.Add(Tile.TileType.PLAIN, new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Yellow)));
                images.tiles.Add(Tile.TileType.ROADS, new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Gray)));
                images.tiles.Add(Tile.TileType.WATER, new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Blue)));
                images.fog = new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Black));
            }
        }

        public struct Class_info
        {
            public Character.Stats.Traits start;
            public Character.Stats.Traits levelup;
            public int lvl_exp;
        }

        public struct Classes_Info
        {
            public Class_info fighter;
            public Class_info archer;
            public Class_info healer;
            public Class_info caster;
            public Class_info scout;
        }

        static Content instance;
        
        public Tilemap gen;
        public Classes_Info cinfo;

        private Content()
        {
            loadContent();
        }

        public static  Content Instance
        {
            get
            {
                if (instance == null)
                    instance = new Content();

                return instance;
            }
        }

        public static void instantiate()
        {
            if (instance == null)
                instance = new Content();
        }

        private void loadContent()
        {
            Graphics.instantiate();

            gen = new Tilemap("gen");
            
            cinfo=cinfo_load(".\\class\\class_info.class");
        }

        private Classes_Info cinfo_load(String path)
        {
            XmlDocument doc = new XmlDocument();
            Classes_Info cinfo = new Classes_Info();

            doc.Load(path);

            cinfo.fighter.start = XmlTransaltor.traits(doc.DocumentElement["Fighter"]["Start"]);
            cinfo.fighter.levelup = XmlTransaltor.traits(doc.DocumentElement["Fighter"]["LevelUp"]);
            cinfo.fighter.lvl_exp = int.Parse(doc.DocumentElement["Fighter"]["Exp"].GetAttribute("exp"));

            cinfo.archer.start = XmlTransaltor.traits(doc.DocumentElement["Archer"]["Start"]);
            cinfo.archer.levelup = XmlTransaltor.traits(doc.DocumentElement["Archer"]["LevelUp"]);
            cinfo.archer.lvl_exp = int.Parse(doc.DocumentElement["Archer"]["Exp"].GetAttribute("exp"));

            cinfo.healer.start = XmlTransaltor.traits(doc.DocumentElement["Healer"]["Start"]);
            cinfo.healer.levelup = XmlTransaltor.traits(doc.DocumentElement["Healer"]["LevelUp"]);
            cinfo.healer.lvl_exp = int.Parse(doc.DocumentElement["Healer"]["Exp"].GetAttribute("exp"));

            cinfo.caster.start = XmlTransaltor.traits(doc.DocumentElement["Caster"]["Start"]);
            cinfo.caster.levelup = XmlTransaltor.traits(doc.DocumentElement["Caster"]["LevelUp"]);
            cinfo.caster.lvl_exp = int.Parse(doc.DocumentElement["Caster"]["Exp"].GetAttribute("exp"));

            cinfo.scout.start = XmlTransaltor.traits(doc.DocumentElement["Scout"]["Start"]);
            cinfo.scout.levelup = XmlTransaltor.traits(doc.DocumentElement["Scout"]["LevelUp"]);
            cinfo.scout.lvl_exp = int.Parse(doc.DocumentElement["Scout"]["Exp"].GetAttribute("exp"));

            return cinfo;
        }
    }
}
