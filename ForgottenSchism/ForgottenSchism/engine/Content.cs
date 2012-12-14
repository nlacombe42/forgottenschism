﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
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
                    public CachedImage cursorRed;
                    public CachedImage cursorBlue;
                    public CachedImage cursorGreen;
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
            SpriteFont turnFont;
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

            public SpriteFont TurnFont
            {
                get { return turnFont; }
            }

            public CachedImage TestImage
            {
                get { return testimg; }
            }

            private void loadContent()
            {
                defFont = Game1.Instance.Content.Load<SpriteFont>(@"font\\arial12norm");
                monoFont = Game1.Instance.Content.Load<SpriteFont>(@"font\\mono12norm");
                turnFont = Game1.Instance.Content.Load<SpriteFont>(@"font\\arial42bold");


                testimg =new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\test"));

                images.background.black = new CachedImageInst(Graphic.Instance.rect(Graphic.Instance.GDM.PreferredBackBufferWidth, Graphic.Instance.GDM.PreferredBackBufferHeight, Color.Black));

                images.gui.cursor = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\gui\\cur"));
                images.gui.cursorRed = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\gui\\curRed"));
                images.gui.cursorGreen = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\gui\\curGreen"));
                images.gui.selCursor = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\gui\\sel"));

                images.characters.healer = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\healer"));
                images.characters.fighter = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\fighter"));
                images.characters.archer = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\archer"));
                images.characters.caster = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\caster"));
                images.characters.scout = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\scout"));
                
                images.tiles = new Dictionary<Tile.TileType, CachedImage>();

                images.tiles.Add(Tile.TileType.CITY, new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\tile\\town")));
                images.tiles.Add(Tile.TileType.FOREST, new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\tile\\forest")));
                images.tiles.Add(Tile.TileType.MOUNTAIN, new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\tile\\mountain")));
                images.tiles.Add(Tile.TileType.PLAIN, new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\tile\\plains")));
                images.tiles.Add(Tile.TileType.ROADS, new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\tile\road2")));
                images.tiles.Add(Tile.TileType.WATER, new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\tile\sea")));
                images.fog = new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Black));
            }
        }

        public struct Audio
        {
            public struct Songs
            {
                public Song test;
            }

            public struct Sounds
            {
                //
            }

            public Songs songs;
            public Sounds sounds;

            public Audio(int t)
            {
                songs = new Songs();
                sounds = new Sounds();
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
        public SpellList spls;
        public Audio audio;

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
            //spls = spellList_load(".\\spell\\spell_list.spells");

            audio = new Audio();
            //audio.songs.test = Game1.Instance.Content.Load<Song>(@"audio\\song\\test");
        }

        /*private SpellList spellList_load(String path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            return 
        }*/

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
