﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.control;
using ForgottenSchism.engine;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    public class UnitManage : Screen
    {
        const int MAXCHAR = 5;
        int charCount;

        Label lbl_unitMng;

        Label lbl_mainUnit;
        Label lbl_deployed;

        Label lbl_standby;

        Label lbl_unitName;
        Label lbl_unitNameValue;

        Label lbl_unitCapacity;
        Label lbl_currentUnit;
        Label lbl_slash;
        Label lbl_maxUnit;

        Label lbl_leader;

        Label lbl_name;
        Label lbl_charName;
        Label lbl_class;
        Label lbl_charClass;
        Label lbl_a;
        Label lbl_aAction;
        Label lbl_l;
        Label lbl_lAction;
        Label lbl_n;
        Label lbl_nAction;
        Label lbl_r;
        Label lbl_rAction;
        Label lbl_v;
        Label lbl_vAction;
        Label lbl_i;
        Label lbl_iAction;
        Label lbl_enter;
        Label lbl_enterAction;
        Label lbl_esc;
        Label lbl_escAction;

        Map map_unitGrid;

        Point p;
        Point sel;

        Army army;
        Unit unit;

        Character selectedUnit;
        Point selectedPos;

        Boolean selected;
        Boolean adding;

        DialogYN yn_deleteUnit;

        DialogTxt txt_renameUnit;

        Menu menu_standby;

        public UnitManage(Unit u)
        {
            army = new Army();
            unit = u;

            loadStuff();
        }

        public UnitManage(Army a, int selectedUnit)
        {
            army = a;
            unit = a.Units[selectedUnit];

            loadStuff();
        }

        private void loadStuff()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_bigMenu;

            yn_deleteUnit = new DialogYN(this);
            yn_deleteUnit.complete = dialog_ret;
            yn_deleteUnit.InputEnabled = false;


            txt_renameUnit = new DialogTxt(this);
            txt_renameUnit.complete = dialog_complete;
            txt_renameUnit.InputEnabled = false;

            p = new Point(2, 2);
            sel = new Point(-1, -1);
            selected = false;
            adding = false;

            lbl_unitMng = new Label("Unit Management");
            lbl_unitMng.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_unitMng.Position = new Vector2(50, 30);
            MainWindow.add(lbl_unitMng);

            lbl_standby = new Label("Standby Units");
            lbl_standby.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_standby.Position = new Vector2(400, 110);
            MainWindow.add(lbl_standby);

            menu_standby = new Menu(9);
            menu_standby.Position = new Vector2(400, 130);
            updateMenu();
            MainWindow.add(menu_standby);

            charCount = unit.Characters.Count;

            lbl_mainUnit = new Label("MAIN UNIT");
            lbl_mainUnit.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.SPECIAL;
            lbl_mainUnit.Position = new Vector2(90, 55);
            if (unit.isMainUnit())
                lbl_mainUnit.Visible = true;
            else
                lbl_mainUnit.Visible = false;

            if (unit.Organization == "enemy")
            {
                lbl_mainUnit.Text = "ENEMY UNIT";
                lbl_mainUnit.Color = Color.Red;
                lbl_mainUnit.Visible = true;
            }
            MainWindow.add(lbl_mainUnit);

            lbl_deployed = new Label("DEPLOYED");
            lbl_deployed.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.SPECIAL;
            lbl_deployed.Position = new Vector2(400, 55);
            if (unit.Deployed)
                lbl_deployed.Visible = true;
            else
                lbl_deployed.Visible = false;
            MainWindow.add(lbl_deployed);

            lbl_unitCapacity = new Label("Unit Capacity:");
            lbl_unitCapacity.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_unitCapacity.Position = new Vector2(400, 80);
            MainWindow.add(lbl_unitCapacity);

            lbl_currentUnit = new Label(charCount.ToString());
            lbl_currentUnit.Position = new Vector2(530, 80);
            MainWindow.add(lbl_currentUnit);

            lbl_slash = new Label("/");
            lbl_slash.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_slash.Position = new Vector2(545, 80);
            MainWindow.add(lbl_slash);

            lbl_maxUnit = new Label(MAXCHAR.ToString());
            lbl_maxUnit.Position = new Vector2(555, 80);
            MainWindow.add(lbl_maxUnit);

            lbl_unitName = new Label("Unit Name:");
            lbl_unitName.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_unitName.Position = new Vector2(90, 80);
            MainWindow.add(lbl_unitName);

            lbl_unitNameValue = new Label(unit.Name);
            lbl_unitNameValue.Position = new Vector2(195, 80);
            MainWindow.add(lbl_unitNameValue);

            lbl_name = new Label("Name");
            lbl_name.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_name.Position = new Vector2(50, 400);
            MainWindow.add(lbl_name);

            lbl_charName = new Label("");
            lbl_charName.Position = new Vector2(115, 400);
            MainWindow.add(lbl_charName);

            lbl_leader = new Label("LEADER");
            lbl_leader.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.SPECIAL;
            lbl_leader.Position = new Vector2(50, 430);
            MainWindow.add(lbl_leader);

            lbl_v = new Label("V");
            lbl_v.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_v.Position = new Vector2(50, 460);
            MainWindow.add(lbl_v);

            lbl_vAction = new Label("View Character");
            lbl_vAction.Position = new Vector2(80, 460);
            MainWindow.add(lbl_vAction);

            lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_enter.Position = new Vector2(50, 490);
            MainWindow.add(lbl_enter);

            lbl_enterAction = new Label("Move Character");
            lbl_enterAction.Position = new Vector2(130, 490);
            MainWindow.add(lbl_enterAction);

            lbl_esc = new Label("ESC");
            lbl_esc.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_esc.Position = new Vector2(50, 520);
            MainWindow.add(lbl_esc);

            lbl_escAction = new Label("Go Back");
            lbl_escAction.Position = new Vector2(100, 520);
            MainWindow.add(lbl_escAction);

            lbl_class = new Label("Class");
            lbl_class.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_class.Position = new Vector2(400, 400);
            MainWindow.add(lbl_class);

            lbl_charClass = new Label("");
            lbl_charClass.Position = new Vector2(460, 400);
            MainWindow.add(lbl_charClass);

            lbl_l = new Label("L");
            lbl_l.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_l.Position = new Vector2(400, 430);
            lbl_l.Visible = false;
            MainWindow.add(lbl_l);

            lbl_lAction = new Label("Make Leader");
            lbl_lAction.Position = new Vector2(430, 430);
            lbl_lAction.Visible = false;
            MainWindow.add(lbl_lAction);

            lbl_i = new Label("I");
            lbl_i.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_i.Position = new Vector2(400, 460);
            MainWindow.add(lbl_i);

            lbl_iAction = new Label("Inventory");
            lbl_iAction.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_iAction.Position = new Vector2(430, 460);
            MainWindow.add(lbl_iAction);

            lbl_a = new Label("A");
            lbl_a.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_a.Position = new Vector2(400, 490);
            lbl_a.Visible = false;
            MainWindow.add(lbl_a);

            lbl_aAction = new Label("Add Character");
            lbl_aAction.Position = new Vector2(430, 490);
            lbl_aAction.Visible = false;
            MainWindow.add(lbl_aAction);

            lbl_r = new Label("R");
            lbl_r.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_r.Position = new Vector2(400, 490);
            MainWindow.add(lbl_r);

            lbl_rAction = new Label("Remove Character");
            lbl_rAction.Position = new Vector2(430, 490);
            MainWindow.add(lbl_rAction);

            lbl_n = new Label("N");
            lbl_n.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_n.Position = new Vector2(400, 520);
            MainWindow.add(lbl_n);

            lbl_nAction = new Label("Rename Unit");
            lbl_nAction.Position = new Vector2(430, 520);
            MainWindow.add(lbl_nAction);

            map_unitGrid = new Map(new Tilemap(4, 4), 4, 4);
            map_unitGrid.Position = new Vector2(90, 110);
            map_unitGrid.changeCurp = changeCurp;
            map_unitGrid.curSelection = curSelection;
            updateGrid();
            MainWindow.add(map_unitGrid);

            if (unit.isChar(2, 2))
            {
                visible();
            }
            else
            {
                invisible();
            }
        }

        public void updateGrid()
        {
            map_unitGrid.CharLs.Clear();
            for (int x = 0; x < map_unitGrid.NumX; x++)
            {
                for (int y = 0; y < map_unitGrid.NumY; y++)
                {
                    if (unit.isChar(x, y))
                    {
                        map_unitGrid.CharLs.Add(new Point(x, y), Graphic.getSprite(unit.get(x, y)));
                    }
                }
            }
        }

        public void updateMenu()
        {
            menu_standby.clear();
            foreach (Character c in army.Standby)
            {
                menu_standby.add(new Link(c.Name));
            }
            menu_standby.Enabled = false;
            menu_standby.unfocusLink();
        }

        public void visible()
        {
            if (unit.Organization == "enemy")
            {
                lbl_r.Visible = false;
                lbl_rAction.Visible = false;

                lbl_enter.Visible = false;
                lbl_enterAction.Visible = false;

                lbl_i.Visible = false;
                lbl_iAction.Visible = false;

                lbl_l.Visible = false;
                lbl_lAction.Visible = false;

                lbl_a.Visible = false;
                lbl_aAction.Visible = false;

                lbl_n.Visible = false;
                lbl_nAction.Visible = false;

                if (unit.isLeader(p.X, p.Y))
                {
                    lbl_leader.Visible = true;
                }
                else
                {
                    lbl_leader.Visible = false;
                }
            }
            else
            {
                if (unit.Deployed)
                {
                    lbl_r.Visible = false;
                    lbl_rAction.Visible = false;

                    lbl_l.Visible = false;
                    lbl_lAction.Visible = false;

                    if (unit.isLeader(p.X, p.Y))
                    {
                        lbl_leader.Visible = true;
                    }
                    else
                    {
                        lbl_leader.Visible = false;
                    }
                }
                else if (unit.isMainUnit())
                {
                    lbl_l.Visible = false;
                    lbl_lAction.Visible = false;

                    if (unit.isLeader(p.X, p.Y))
                    {
                        lbl_r.Visible = false;
                        lbl_rAction.Visible = false;
                        lbl_leader.Visible = true;
                    }
                    else
                    {
                        lbl_r.Visible = true;
                        lbl_rAction.Visible = true;
                        lbl_leader.Visible = false;
                    }
                }
                else
                {
                    lbl_r.Visible = true;
                    lbl_rAction.Visible = true;

                    if (unit.isLeader(p.X, p.Y))
                    {
                        lbl_l.Visible = false;
                        lbl_lAction.Visible = false;
                        lbl_leader.Visible = true;
                    }
                    else
                    {
                        lbl_l.Visible = true;
                        lbl_lAction.Visible = true;
                        lbl_leader.Visible = false;
                    }
                }
                
                lbl_enter.Visible = true;
                lbl_enterAction.Text = "Move Character";
                lbl_enterAction.Visible = true;

                lbl_a.Visible = false;
                lbl_aAction.Visible = false;

                lbl_n.Visible = true;
                lbl_nAction.Visible = true;
            }
            selected = true;
            lbl_name.Visible = true;
            lbl_charName.Text = unit.get(p.X, p.Y).Name;
            lbl_charName.Visible = true;
            lbl_class.Visible = true;
            lbl_charClass.Text = unit.get(p.X, p.Y).Type.ToString();
            lbl_charClass.Visible = true;
            lbl_v.Visible = true;
            lbl_vAction.Visible = true;
        }

        public void invisible()
        {
            selected = false;
            lbl_name.Visible = false;
            lbl_charName.Visible = false;
            lbl_class.Visible = false;
            lbl_charClass.Visible = false;
            lbl_enter.Visible = false;
            lbl_enterAction.Visible = false;
            lbl_r.Visible = false;
            lbl_rAction.Visible = false;
            lbl_l.Visible = false;
            lbl_lAction.Visible = false;
            lbl_leader.Visible = false;
            lbl_v.Visible = false;
            lbl_vAction.Visible = false;

            if (unit.Organization == "enemy")
            {
                lbl_i.Visible = false;
                lbl_iAction.Visible = false;

                lbl_a.Visible = false;
                lbl_aAction.Visible = false;

                lbl_n.Visible = false;
                lbl_nAction.Visible = false;
            }
            else
            {
                if (sel == new Point(-1, -1) && army.Standby.Count > 0 && charCount < MAXCHAR && unit.Deployed == false)
                {
                    lbl_a.Visible = true;
                    lbl_aAction.Visible = true;
                }

                if (adding)
                {
                    lbl_a.Visible = false;
                    lbl_aAction.Visible = false;

                    lbl_enter.Visible = true;
                    lbl_enterAction.Text = "Add Character";
                    lbl_enterAction.Visible = true;

                    lbl_n.Visible = false;
                    lbl_nAction.Visible = false;
                }
                else
                {
                    lbl_n.Visible = true;
                    lbl_nAction.Visible = true;
                }
            }
        }

        private void changeCurp(object o, EventArgs e)
        {
            p = (Point)(((EventArgObject)e).o);
            
            invisible();

            if (sel == new Point(-1, -1) && unit.isChar(p.X, p.Y))
            {
                visible();
            }

            if (selectedUnit != null)
            {
                if (unit.isChar(p.X, p.Y))
                {
                    if (selectedUnit == unit.get(p.X, p.Y))
                    {
                        lbl_enterAction.Text = "Cancel Move";
                        lbl_enter.Visible = true;
                        lbl_enterAction.Visible = true;
                    }
                    else
                    {
                        lbl_enterAction.Text = "Swap Characters";
                        lbl_enter.Visible = true;
                        lbl_enterAction.Visible = true;
                    }
                }
                else
                {
                    lbl_enterAction.Text = "Move Character";
                    lbl_enter.Visible = true;
                    lbl_enterAction.Visible = true;
                }
            }
            else
            {
                if (unit.isChar(p.X, p.Y))
                {
                    lbl_enterAction.Text = "Move Character";
                    lbl_enter.Visible = true;
                    lbl_enterAction.Visible = true;
                }
            }

            if (unit.Organization == "enemy")
            {
                lbl_enter.Visible = false;
                lbl_enterAction.Visible = false;
            }
        }

        private void curSelection(object o, EventArgs e)
        {
            if (unit.Organization == "enemy")
                return;

            sel = (Point)(((EventArgObject)e).o);

            if (sel != new Point(-1, -1))
            {
                if (unit.isChar(p.X, p.Y))
                {
                    selectedUnit = unit.get(p.X, p.Y);
                    lbl_enterAction.Text = "Cancel Move";
                }
                else
                {
                    selectedUnit = null;
                }

                selectedPos = sel;
                invisible();
                if (selectedUnit != null)
                {
                    lbl_enter.Visible = true;
                    lbl_enterAction.Visible = true;
                }
            }
            else if (unit.isChar(p.X, p.Y))
            {
                unit.set(selectedPos.X, selectedPos.Y, unit.get(p.X, p.Y));

                if (selectedUnit == null)
                {
                    unit.delete(p.X, p.Y);
                    invisible();
                }
                else
                {
                    unit.set(p.X, p.Y, selectedUnit);
                    selectedUnit = null;
                    visible();
                }
                selectedPos = new Point(-1, -1);
                updateGrid();

                GameState.CurrentState.saved = false;
            }
            else if (selectedUnit != null)
            {
                unit.set(p.X, p.Y, selectedUnit);
                unit.delete(map_unitGrid.LastSelection.X, map_unitGrid.LastSelection.Y);
                selectedUnit = null;
                updateGrid();

                visible();

                GameState.CurrentState.saved = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (yn_deleteUnit.InputEnabled)
            {
                if (InputHandler.keyReleased(Keys.Escape))
                {
                    yn_deleteUnit.InputEnabled = false;
                    yn_deleteUnit.close();
                }
            }
            else if (txt_renameUnit.InputEnabled)
            {
                if (InputHandler.keyReleased(Keys.Escape))
                {
                    txt_renameUnit.InputEnabled = false;
                    txt_renameUnit.close();
                }
            }
            else
            {
                if (adding)
                {
                    menu_standby.Enabled = true;
                    map_unitGrid.Enabled = false;
                    menu_standby.HasFocus = true;
                    map_unitGrid.HasFocus = false;
                    menu_standby.refocusLink();
                }
                else
                {
                    menu_standby.Enabled = false;
                    map_unitGrid.Enabled = true;
                    menu_standby.HasFocus = false;
                    map_unitGrid.HasFocus = true;
                    menu_standby.unfocusLink();
                }

                if (InputHandler.keyReleased(Keys.Escape))
                {
                    if (adding)
                    {
                        adding = false;
                        invisible();
                    }
                    else
                        StateManager.Instance.goBack();
                }
                if (InputHandler.keyReleased(Keys.V) && selected)
                {
                    StateManager.Instance.goForward(new CharManage(unit.get(p.X, p.Y), unit));
                }
                if (InputHandler.keyReleased(Keys.R) && lbl_r.Visible)
                {
                    if (charCount == 1 || unit.isLeader(p.X, p.Y))
                    {
                        dialog_show(null, null);
                    }
                    else
                    {
                        army.Standby.Add(unit.get(p.X, p.Y));
                        unit.delete(p.X, p.Y);
                        charCount--;
                        lbl_currentUnit.Text = charCount.ToString();
                        updateGrid();
                        updateMenu();
                        invisible();

                        GameState.CurrentState.saved = false;
                    }
                }

                if (InputHandler.keyReleased(Keys.I) && lbl_i.Visible)
                {
                    StateManager.Instance.goForward(new UnitInventory(unit));
                }

                if (InputHandler.keyReleased(Keys.L) && selected)
                {
                    if (lbl_l.Visible)
                    {
                        unit.setLeader(p.X, p.Y);
                        visible();
                    }
                }
                if (InputHandler.keyReleased(Keys.A) && lbl_a.Visible)
                {
                    adding = true;
                    invisible();
                }
                if (InputHandler.keyReleased(Keys.Enter) && adding)
                {
                    unit.set(p.X, p.Y, army.Standby[menu_standby.Selected]);
                    army.Standby.Remove(army.Standby[menu_standby.Selected]);
                    updateMenu();
                    updateGrid();

                    charCount++;
                    lbl_currentUnit.Text = charCount.ToString();

                    adding = false;
                    menu_standby.Enabled = false;
                    map_unitGrid.Enabled = true;

                    GameState.CurrentState.saved = false;

                    visible();
                }
                if (InputHandler.keyReleased(Keys.N) && lbl_n.Visible)
                {
                    dialog_showTxt(null, null);
                }
            }
        }

        private void dialog_show(object sender, EventArgs e)
        {
            yn_deleteUnit.InputEnabled = true;
            yn_deleteUnit.show("Removing this character will also \n remove the unit. Are you sure?");
        }

        private void dialog_ret(bool b)
        {
            if (b)
            {
                foreach (Character c in unit.Characters)
                {
                    army.Standby.Add(c);
                }
                army.Units.Remove(unit);

                GameState.CurrentState.saved = false;

                StateManager.Instance.goBack();
            }
        }

        private void dialog_complete(char[] str)
        {
            String s = new String(str).Trim();
            if (s != String.Empty)
            {
                unit.Name = s;
                lbl_unitNameValue.Text = s;
            }

            GameState.CurrentState.saved = false;

            txt_renameUnit.InputEnabled = false;
            InputHandler.flush();
        }

        private void dialog_showTxt(object sender, EventArgs e)
        {
            txt_renameUnit.InputEnabled = true;
            txt_renameUnit.show("Rename unit: ");
        }
    }
}
