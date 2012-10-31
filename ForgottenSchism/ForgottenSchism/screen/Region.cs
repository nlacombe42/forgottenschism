﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;
using ForgottenSchism.control;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    public class Region : Screen
    {
        Map map;
        Label lbl_city;
        Label lbl_cityName;
        bool freemode;
        Point mcp;

        public Region()
        {
            mcp = new Point(3, 2);

            freemode = false;

            cm.ArrowEnable = false;

            map = new Map(Content.Instance.gen);
            map.ArrowEnabled = false;
            map.SelectionEnabled = false;
            map.changeCurp = changeCurp;
            map.CharLs.Add(mcp, Graphic.getSprite(GameState.CurrentState.mainChar));
            map.focus(mcp.X, mcp.Y);
            cm.add(map);

            lbl_city = new Label("City");
            lbl_city.Color = Color.Blue;
            lbl_city.Position = new Vector2(50, 400);
            lbl_city.Visible = false;
            cm.add(lbl_city);

            lbl_cityName = new Label("");
            lbl_cityName.Color = Color.White;
            lbl_cityName.Position = new Vector2(100, 400);
            lbl_cityName.Visible = false;
            cm.add(lbl_cityName);

            Label lbl_m = new Label("M");
            lbl_m.Color = Color.Blue;
            lbl_m.Position = new Vector2(450, 425);
            cm.add(lbl_m);

            Label lbl_mode = new Label("View/Move mode");
            lbl_mode.Color = Color.White;
            lbl_mode.Position = new Vector2(550, 425);
            cm.add(lbl_mode);

            Label lbl_enter = new Label("Enter");
            lbl_enter.Color = Color.Blue;
            lbl_enter.Position = new Vector2(450, 450);
            cm.add(lbl_enter);

            Label lbl_reg = new Label("Enter Region");
            lbl_reg.Color = Color.White;
            lbl_reg.Position = new Vector2(550, 450);
            cm.add(lbl_reg);

            Point p = mcp;
            changeCurp(this, new EventArgObject(new Point(p.X, p.Y)));
        }

        private void moveChar(Point np)
        {
            Tilemap tm = Content.Instance.gen;

            if (np.X < 0 || np.X >= tm.NumX || np.Y < 0 || np.Y > tm.NumY)
                return;

            map.CharLs.Remove(mcp);
            map.CharLs.Add(np, Graphic.getSprite(GameState.CurrentState.mainChar));

            mcp = np;

            changeCurp(this, new EventArgObject(new Point(np.X, np.Y)));

            map.focus(np.X, np.Y);
        }

        private void changeCurp(object o, EventArgs e)
        {
            Point p = (Point)(((EventArgObject)e).o);

            City c = Content.Instance.gen.get(p.X, p.Y).City;

            if (c != null && !GameState.CurrentState.gen.get(p.X, p.Y))
            {
                lbl_city.Visible = true;

                lbl_cityName.Text = c.Name;
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

            if (InputHandler.keyReleased(Keys.Escape))
                ;
            else if (InputHandler.keyReleased(Keys.M))
            {
                freemode = !freemode;

                Point p = mcp;

                map.focus(p.X, p.Y);

                map.ArrowEnabled = freemode;
            }

            if (!freemode)
            {
                if (InputHandler.keyReleased(Keys.Up))
                {
                    Point cp = mcp;

                    moveChar(new Point(cp.X, --cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Down))
                {
                    Point cp = mcp;

                    moveChar(new Point(cp.X, ++cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Left))
                {
                    Point cp = mcp;

                    moveChar(new Point(--cp.X, cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Right))
                {
                    Point cp = mcp;

                    moveChar(new Point(++cp.X, cp.Y));
                }
            }
        }
    }
}
