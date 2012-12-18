﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;
using ForgottenSchism.control;

namespace ForgottenSchism.screen
{
    public class Load: Screen
    {
        Menu m;
        DialogYN dyn;
        bool di;
        Label lbl_stat;

        public Load()
        {
            di = false;

            cm.ArrowEnable = false;

            dyn = new DialogYN("");
            dyn.Position = new Vector2(200, 100);
            dyn.chose = dynChose;
            dyn.Enabled = false;
            dyn.Visible = false;
            cm.add(dyn);
            cm.addLastDraw(dyn);

            Label lbl_title = new Label("Load Game");
            lbl_title.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_title.Position = new Vector2(100, 20);
            cm.add(lbl_title);

            lbl_stat = new Label("");
            lbl_stat.Position = new Vector2(100, 50);
            lbl_stat.Color = Color.Green;
            cm.add(lbl_stat);

            Label lbl_d = new Label("D");
            lbl_d.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_d.Position = new Vector2(80, 500);
            cm.add(lbl_d);

            Label lbl_del = new Label("Delete Save");
            lbl_del.Color = Color.White;
            lbl_del.Position = new Vector2(100, 500);
            cm.add(lbl_del);

            m = new Menu(10);
            m.Position = new Vector2(50, 75);
            list();
            cm.add(m);
        }

        private void del()
        {
            String path = m.Focused.Text;

            File.Delete(".\\save\\"+path+".save");

            lbl_stat.Text = "\""+path+"\" Deleted";

            list();
        }

        private void load(object o, EventArgs e)
        {
            String path = ((Link)o).Text;

            GameState.CurrentState.load(".\\save\\"+path+".save");

            StateManager.Instance.reset(new WorldMap());
        }

        private void list()
        {
            m.clear();

            String s;

            foreach (String str in Directory.EnumerateFiles(".\\save\\", "*.save"))
            {
                s = Path.GetFileNameWithoutExtension(str);
                Link l = new Link(s);
                l.selected = load;
                m.add(l);
            }
        }

        private void dynChose(object o, EventArgs e)
        {
            di = false;
            dyn.Enabled = false;
            dyn.Visible = false;
            m.Enabled = true;
            m.ArrowEnabled = true;

            if ((bool)((EventArgObject)e).o)
                del();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(dyn.Enabled)
                dyn.handleInput(gameTime);

            if (di)
                return;

            if (InputHandler.keyReleased(Keys.Escape))
                    StateManager.Instance.goBack();

            if (InputHandler.keyReleased(Keys.D))
            {
                dyn.Text = "Delete saved game\n" + m.Focused.Text + " ?";
                dyn.Enabled = true;
                dyn.Visible = true;
                m.Enabled = false;
                m.ArrowEnabled = false;
                di = true;
            }
        }
    }
}
