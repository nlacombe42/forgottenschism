﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;
using ForgottenSchism.control;
using ForgottenSchism.world;
using Microsoft.Xna.Framework.Media;

namespace ForgottenSchism.screen
{
    public class WorldMap: Screen
    {
        Map map;
        Label lbl_city;
        Label lbl_cityName;
        bool freemode;
        DialogYN yn_battle;
        Point dnp;
        Point lp;

        Label lbl_day;
        Label lbl_dayNum;
        Label lbl_cities;
        Label lbl_citiesNum;
        Label lbl_income;
        Label lbl_incomeNum;
        AI ai;

        public WorldMap()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_smallMenu;
            MainWindow.FocusArrowEnabled = false;


            foreach (Unit u in GameState.CurrentState.mainArmy.Units)
                u.Deployed = false;

            freemode = false;

            yn_battle = new DialogYN(this);
            yn_battle.complete = dialog_ret_battle;
            yn_battle.InputEnabled = false;

            map = new Map(Content.Instance.gen);
            map.ArrowEnabled = false;
            map.SelectionEnabled = false;
            map.Fog = GameState.CurrentState.gen;
            map.changeCurp = changeCurp;

            updateMap();

            map.focus(GameState.CurrentState.mainCharPos.X, GameState.CurrentState.mainCharPos.Y);
            MainWindow.add(map);

            lp = GameState.CurrentState.mainCharPos;

            lbl_day = new Label("Day #");
            lbl_day.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_day.Position = new Vector2(50, 410);
            MainWindow.add(lbl_day);

            lbl_dayNum = new Label(GameState.CurrentState.turn.ToString());
            lbl_dayNum.Position = new Vector2(110, 410);
            MainWindow.add(lbl_dayNum);

            lbl_city = new Label("City");
            lbl_city.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_city.Position = new Vector2(50, 440);
            lbl_city.Visible = false;
            MainWindow.add(lbl_city);

            lbl_cityName = new Label("");
            lbl_cityName.Position = new Vector2(100, 440);
            lbl_cityName.Visible = false;
            MainWindow.add(lbl_cityName);

            lbl_cities = new Label("Cities Owned");
            lbl_cities.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_cities.Position = new Vector2(50, 470);
            MainWindow.add(lbl_cities);

            lbl_citiesNum = new Label(GameState.CurrentState.getCaptureNum("main").ToString());
            lbl_citiesNum.Position = new Vector2(180, 470);
            MainWindow.add(lbl_citiesNum);

            lbl_income = new Label("Income Per Turn");
            lbl_income.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_income.Position = new Vector2(50, 500);
            MainWindow.add(lbl_income);

            lbl_incomeNum = new Label((Content.Instance.money_info.perRegion * GameState.CurrentState.getCaptureNum("main")).ToString());
            lbl_incomeNum.Position = new Vector2(200, 500);
            MainWindow.add(lbl_incomeNum);

            Label lbl_a = new Label("A");
            lbl_a.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_a.Position=new Vector2(400, 440);
            MainWindow.add(lbl_a);

            Label lbl_army = new Label("Army Screen");
            lbl_army.Position = new Vector2(430, 440);
            MainWindow.add(lbl_army);

            Label lbl_m = new Label("M");
            lbl_m.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_m.Position = new Vector2(400, 470);
            MainWindow.add(lbl_m);

            Label lbl_mode = new Label("View/Move mode");
            lbl_mode.Position = new Vector2(430, 470);
            MainWindow.add(lbl_mode);

            /*Label lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_enter.Position = new Vector2(400, 500);
            MainWindow.add(lbl_enter);

            Label lbl_reg = new Label("Enter Region");
            lbl_reg.Position = new Vector2(480, 500);
            MainWindow.add(lbl_reg);*/

            Point p = GameState.CurrentState.mainCharPos;
            changeCurp(this, new EventArgObject(new Point(p.X, p.Y)));

            GameState.CurrentState.mainArmy.undeployAll();

            ai = new AI();
            ai.set(map, Content.Instance.gen);
            ai.done = ai_done;
        }

        private void ai_done(object o, EventArgs e)
        {
            MainWindow.InputEnabled = true;
        }

        public override void start()
        {
            base.start();

            MediaPlayer.Play(Content.Instance.audio.songs.worldMap);
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// Executed at the end of each player turn (each time the players character moves)
        /// </summary>
        private void turn()
        {
            MainWindow.InputEnabled = false;
            ai.world(this, "enemy");

            GameState.CurrentState.att++;

            System.Console.Out.WriteLine("att: " + GameState.CurrentState.att);

            GameState.CurrentState.turn++;
            GameState.CurrentState.saved = false;

            GameState.CurrentState.mainArmy.Money += (Content.Instance.money_info.perRegion * GameState.CurrentState.getCaptureNum("main"));

            lbl_dayNum.Text = GameState.CurrentState.turn.ToString();
            lbl_citiesNum.Text = GameState.CurrentState.getCaptureNum("main").ToString();
            lbl_incomeNum.Text = (Content.Instance.money_info.perRegion * GameState.CurrentState.getCaptureNum("main")).ToString();

            updateMap();
        }

        private void updateMap()
        {
            map.CharLs.Clear();
            map.MiscLs.Clear();

            map.CharLs.Add(GameState.CurrentState.mainCharPos, Graphic.getSprite(GameState.CurrentState.mainChar));
            map.MiscLs.Add(GameState.CurrentState.mainCharPos, Graphic.Instance.getMisc(GameState.CurrentState.mainArmy.MainCharUnit));

            CityMap cmap=GameState.CurrentState.citymap["gen"];

            City c;

            for (int i = 0; i < cmap.NumX; i++)
                for (int e = 0; e < cmap.NumY; e++)
                {
                    c=cmap.get(i, e);

                    if (c!=null)
                    {
                        if (c.Owner == "enemy")
                        {
                            if(Content.Instance.emap.ContainsKey(c.Name) && Content.Instance.emap[c.Name].Count>0)
                                map.CharLs.Add(new Point(i, e), Graphic.getSprite(Character.genClass(Content.Instance.emap[c.Name][0].ClassType, "")));
                            else
                                map.CharLs.Add(new Point(i, e), Content.Graphics.Instance.Images.characters.caster);
                            
                            map.MiscLs.Add(new Point(i, e), Graphic.Instance.getMisc("enemy", 1));
                        }
                        else if (c.Owner == "main" && GameState.CurrentState.mainCharPos != new Point(i, e))
                        {
                            map.MiscLs.Add(new Point(i, e), Graphic.Instance.getMisc("main", 1));
                        }
                    }
                 }
        }

        public override void resume()
        {
            base.resume();

            GameState.CurrentState.mainArmy.undeployAll();

            updateMap();

            lbl_citiesNum.Text = GameState.CurrentState.getCaptureNum("main").ToString();
            lbl_incomeNum.Text = (Content.Instance.money_info.perRegion * GameState.CurrentState.getCaptureNum("main")).ToString();

            if (GameState.CurrentState.isCaptured("Silenda", "main"))
            {
                if (GameState.CurrentState.alignment > 0)
                {
                    Story s = new Story("VetusBadEnding");
                    s.Done = ending;
                    StateManager.Instance.goForward(s);
                }
                else
                {
                    Story s = new Story("NovumBadEnding");
                    s.Done = ending;
                    StateManager.Instance.goForward(s);
                }
            }
            else if (GameState.CurrentState.isCaptured("Pestis Woods", "main"))
            {
                if (GameState.CurrentState.alignment >= 0)
                {
                    Story s = new Story("VetusGoodEnding");
                    s.Done = ending;
                    StateManager.Instance.goForward(s);
                }
                else
                {
                    Story s = new Story("NovumGoodEnding");
                    s.Done = ending;
                    StateManager.Instance.goForward(s);
                }
            }

            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(Content.Instance.audio.songs.worldMap);
                MediaPlayer.IsRepeating = true;
            }
        }

        private void ending(object o, EventArgs e)
        {
            Story credit = new Story("Credits");
            credit.Done = game_end;
            StateManager.Instance.goForward(credit);
        }

        private void game_end(object o, EventArgs e)
        {
            StateManager.Instance.reset(new MainMenu());
        }

        private void dialog_ret_battle(bool b)
        {
            if (b)
            {
                GameState.CurrentState.att = 0;
                lp = GameState.CurrentState.mainCharPos;

                if (map.CharLs.ContainsKey(dnp))
                    map.CharLs.Remove(dnp);

                map.CharLs.Remove(GameState.CurrentState.mainCharPos);
                map.CharLs.Add(dnp, Graphic.getSprite(GameState.CurrentState.mainChar));

                System.Console.Out.WriteLine(GameState.CurrentState.mainCharPos+" "+dnp);
                City.CitySide atts=City.opposed(City.move2side(GameState.CurrentState.mainCharPos, dnp));

                GameState.CurrentState.mainCharPos = dnp;

                changeCurp(this, new EventArgObject(new Point(dnp.X, dnp.Y)));

                clearFog(dnp);

                map.focus(dnp.X, dnp.Y);

                Tilemap tm = new Tilemap(GameState.CurrentState.citymap["gen"].get(dnp.X, dnp.Y).Name);

                Objective goal = new Objective();
                goal.setDefeatAll();

                StateManager.Instance.goForward(new Region(tm, atts, true, GameState.CurrentState.citymap["gen"].get(dnp.X, dnp.Y).EnnemyFactor, goal));
            }
            yn_battle.InputEnabled = false;
        }

        private void moveChar(Point np)
        {
            Tilemap tm=Content.Instance.gen;

            if (np.X < 0 || np.X >= tm.NumX || np.Y < 0 || np.Y >= tm.NumY)
                return;

            Tile t=tm.get(np.X, np.Y);

            if (t.Type != Tile.TileType.ROADS && t.Type != Tile.TileType.CITY)
                return;

            if (GameState.CurrentState.citymap["gen"].isCity(np.X, np.Y) && GameState.CurrentState.citymap["gen"].get(np.X, np.Y).Owner=="enemy")
            {
                dnp = np;

                yn_battle.InputEnabled = true;
                yn_battle.show("Enter battle?");

                return;
            }

            lp = GameState.CurrentState.mainCharPos;

            //GameState.CurrentState.saved = false;

            GameState.CurrentState.mainCharPos = np;

            changeCurp(this, new EventArgObject(new Point(np.X, np.Y)));

            clearFog(np);

            map.focus(np.X, np.Y);

            updateMap();

            turn();
        }

        /// <summary>
        /// Clears the fog around the given point
        /// </summary>
        /// <param name="p">Point to clear the fog around</param>
        private void clearFog(Point p)
        {
            Fog fog=GameState.CurrentState.gen;

            for (int i = -1; i <= 1; i++)
                for (int e = -1; e <= 1; e++)
                    if(i+p.X>=0&&e+p.Y>=0&&i+p.X<fog.NumX&&e+p.Y<fog.NumY)
                        fog.set(i+p.X, e+p.Y, false);
        }

        private void changeCurp(object o, EventArgs e)
        {
            Point p=(Point)(((EventArgObject)e).o);

            if (Content.Instance.gen.CityMap.isCity(p.X, p.Y) && !GameState.CurrentState.gen.get(p.X, p.Y))
            {
                lbl_city.Visible = true;

                lbl_cityName.Text = GameState.CurrentState.citymap["gen"].get(p.X, p.Y).Name;
                lbl_cityName.Visible = true;
            }
            else
            {
                lbl_city.Visible = false;
                lbl_cityName.Visible = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ai.Active)
            {
                ai.Update(gameTime);
                return;
            }

            if (yn_battle.InputEnabled)
            {
                if (InputHandler.keyReleased(Keys.Escape))
                {
                    yn_battle.InputEnabled = false;
                    yn_battle.close();
                }
            }
            else
            {
                if (InputHandler.keyReleased(Keys.Escape))
                    StateManager.Instance.goForward(new PauseMenu());
                else if (InputHandler.keyReleased(Keys.A))
                    StateManager.Instance.goForward(new ArmyManage());
                /*else if (InputHandler.keyReleased(Keys.Enter))
                {
                    Point p = GameState.CurrentState.mainCharPos;
                    Tile t = Content.Instance.gen.get(p.X, p.Y);

                    City.CitySide atts = City.opposed(City.move2side(lp, GameState.CurrentState.mainCharPos));

                    if (GameState.CurrentState.citymap["gen"].isCity(p.X, p.Y))
                    {
                        Tilemap tm = new Tilemap(GameState.CurrentState.citymap["gen"].get(p.X, p.Y).Name);

                        Objective goal = new Objective();
                        goal.setCaptureCity(new Point(1, 3));

                        StateManager.Instance.goForward(new Region(tm, atts, true, GameState.CurrentState.citymap["gen"].get(p.X, p.Y).EnnemyFactor, goal));
                    }
                }*/
                else if (InputHandler.keyReleased(Keys.M))
                {
                    freemode = !freemode;

                    Point p = GameState.CurrentState.mainCharPos;

                    map.focus(p.X, p.Y);

                    map.ArrowEnabled = freemode;
                }

                if (!freemode)
                {
                    if (InputHandler.keyReleased(Keys.Up))
                    {
                        Point cp = GameState.CurrentState.mainCharPos;

                        moveChar(new Point(cp.X, --cp.Y));
                    }

                    if (InputHandler.keyReleased(Keys.Down))
                    {
                        Point cp = GameState.CurrentState.mainCharPos;

                        moveChar(new Point(cp.X, ++cp.Y));
                    }

                    if (InputHandler.keyReleased(Keys.Left))
                    {
                        Point cp = GameState.CurrentState.mainCharPos;

                        moveChar(new Point(--cp.X, cp.Y));
                    }

                    if (InputHandler.keyReleased(Keys.Right))
                    {
                        Point cp = GameState.CurrentState.mainCharPos;

                        moveChar(new Point(++cp.X, cp.Y));
                    }
                }
            }
        }
    }
}
