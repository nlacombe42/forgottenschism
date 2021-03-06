using System;
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
    public class ArmyManage : Screen
    {
        public delegate bool TestEventHandler(object sender, object args);

        bool fromRegion = false;

        Army army;
        Label lbl_UnitList;
        Menu menu_units;
        Label lbl_unitComp;
        Menu menu_chars;
        int sel;

        Label lbl_armyManage;

        Label lbl_a;
        Label lbl_aAction;
        Label lbl_h;
        Label lbl_hAction;
        Label lbl_enter;
        Label lbl_enterAction;
        Label lbl_r;
        Label lbl_rAction;
        Label lbl_n;
        Label lbl_nAction;
        Label lbl_d;
        Label lbl_dAction;
        Label lbl_i;
        Label lbl_iInventory;
        Label lbl_s;
        Label lbl_sAction;
        Label lbl_esc;
        Label lbl_escAction;

        Boolean standby = false;

        DialogTxt dtxt_renameUnit;

        public TestEventHandler deploy;

        public ArmyManage()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_bigMenu;

            dtxt_renameUnit = new DialogTxt(this);
            dtxt_renameUnit.complete = dialog_complete;
            dtxt_renameUnit.InputEnabled = false;

            army = GameState.CurrentState.mainArmy;

            lbl_armyManage = new Label("Army Manage");
            lbl_armyManage.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_armyManage.Position = new Vector2(50, 30);
            MainWindow.add(lbl_armyManage);

            lbl_UnitList = new Label("Unit List");
            lbl_UnitList.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_UnitList.Position = new Vector2(50, 60);
            MainWindow.add(lbl_UnitList);
            
            menu_units = new Menu(11);
            menu_units.Position = new Vector2(70, 60);

            foreach (Unit u in army.Units)
            {
                Link l = new Link(u.Name);
                if (u.Deployed)
                {
                    l.GEnable = false;
                    fromRegion = true;
                }
                menu_units.add(l);
            }

            menu_units.add(new Link("Standby Soldiers"));
            sel = menu_units.Selected;
            MainWindow.add(menu_units);

            lbl_unitComp = new Label("Unit Composition");
            lbl_unitComp.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_unitComp.Position = new Vector2(430, 60);
            MainWindow.add(lbl_unitComp);

            lbl_d = new Label("D");
            lbl_d.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_d.Position = new Vector2(50, 440);
            MainWindow.add(lbl_d);

            lbl_dAction = new Label("Deploy Unit");
            lbl_dAction.Position = new Vector2(80, 440);
            MainWindow.add(lbl_dAction);

            lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_enter.Position = new Vector2(50, 470);
            MainWindow.add(lbl_enter);

            lbl_enterAction = new Label("Manage Unit");
            lbl_enterAction.Position = new Vector2(130, 470);
            MainWindow.add(lbl_enterAction);

            lbl_esc = new Label("ESC");
            lbl_esc.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_esc.Position = new Vector2(50, 500);
            MainWindow.add(lbl_esc);

            lbl_escAction = new Label("Go Back");
            lbl_escAction.Position = new Vector2(110, 500);
            MainWindow.add(lbl_escAction);

            lbl_r = new Label("R");
            lbl_r.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_r.Position = new Vector2(300, 440);
            MainWindow.add(lbl_r);

            lbl_rAction = new Label("Remove Unit");
            lbl_rAction.Position = new Vector2(330, 440);
            MainWindow.add(lbl_rAction);

            lbl_n = new Label("N");
            lbl_n.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_n.Position = new Vector2(300, 470);
            MainWindow.add(lbl_n);

            lbl_nAction = new Label("Rename Unit");
            lbl_nAction.Position = new Vector2(330, 470);
            MainWindow.add(lbl_nAction);

            lbl_a = new Label("A");
            lbl_a.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_a.Position = new Vector2(300, 500);
            MainWindow.add(lbl_a);

            lbl_aAction = new Label("Add Unit");
            lbl_aAction.Position = new Vector2(330, 500);
            MainWindow.add(lbl_aAction);

            lbl_s = new Label("S");
            lbl_s.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_s.Position = new Vector2(550, 440);
            MainWindow.add(lbl_s);

            lbl_sAction = new Label("Shop");
            lbl_sAction.Position = new Vector2(580, 440);
            MainWindow.add(lbl_sAction);

            lbl_h = new Label("H");
            lbl_h.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_h.Position = new Vector2(550, 470);
            MainWindow.add(lbl_h);

            lbl_hAction = new Label("Hire Soldiers");
            lbl_hAction.Position = new Vector2(580, 470);
            MainWindow.add(lbl_hAction);

            lbl_i = new Label("I");
            lbl_i.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_i.Position = new Vector2(550, 500);
            MainWindow.add(lbl_i);

            lbl_iInventory = new Label("Inventory");
            lbl_iInventory.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_iInventory.Position = new Vector2(580, 500);
            MainWindow.add(lbl_iInventory);

            menu_chars = new Menu(11);
            menu_chars.Position = new Vector2(450, 60);

            if (army.Units.Count > 0)
            {
                foreach (Character c in army.Units[sel].Characters)
                {
                    menu_chars.add(new Link(c.Name));
                }
                visible();
            }
            else
            {
                foreach (Character c in army.Standby)
                {
                    menu_chars.add(new Link(c.Name));
                }
                invisible();
                lbl_enterAction.Text = "View Standby";
            }

            menu_chars.TabStop = false;
            menu_chars.unfocusLink();
            MainWindow.add(menu_chars);

            if (GameState.CurrentState.mainArmy.MainCharUnit.Deployed)
            {
                lbl_s.Visible = false;
                lbl_sAction.Visible = false;
            }
        }

        public override void resume()
        {
            base.resume();

            menu_units.clear();

            if (army.Units.Count > 0)
            {
                foreach (Unit u in army.Units)
                {
                    Link l = new Link(u.Name);
                    
                    if (u.Deployed)
                        l.GEnable = false;
                    
                    menu_units.add(l);
                }

                sel = menu_units.Selected;
            }
            else
            {
                sel = 0;
            }

            menu_units.add(new Link("Standby Soldiers"));

            if (sel < army.Units.Count)
            {
                menu_chars.clear();
                foreach (Character c in army.Units[sel].Characters)
                {
                    menu_chars.add(new Link(c.Name));
                }
                menu_chars.unfocusLink();

                menu_units.TabStop = true;
                menu_chars.TabStop = false;
                standby = false;

                lbl_enterAction.Text = "Manage Unit";
                visible();
            }
            else
            {
                menu_chars.clear();
                foreach (Character c in army.Standby)
                {
                    menu_chars.add(new Link(c.Name));
                }

                lbl_enterAction.Text = "View Standby";
                invisible();
            }
        }

        public void visible()
        {
            lbl_n.Visible = true;
            lbl_nAction.Visible = true;

            if (army.Standby.Count > 0)
            {
                lbl_a.Visible = true;
                lbl_aAction.Visible = true;
            }
            else
            {
                lbl_a.Visible = false;
                lbl_aAction.Visible = false;
            }

            if (army.Units[menu_units.Selected].Deployed)
            {
                lbl_d.Visible = false;
                lbl_dAction.Text = "DEPLOYED";
                lbl_dAction.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.SPECIAL;
                lbl_dAction.Visible = true;

                lbl_r.Visible = false;
                lbl_rAction.Visible = false;
            }
            else
            {
                if (army.Units[menu_units.Selected].isMainUnit())
                {
                    lbl_r.Visible = false;
                    lbl_rAction.Visible = false;
                }
                else
                {
                    lbl_r.Visible = true;
                    lbl_rAction.Visible = true;
                }
                lbl_d.Visible = true;
                lbl_dAction.Text = "Deploy Unit";
                lbl_dAction.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
                
                lbl_dAction.Visible = true;

                if (deploy == null)
                {
                    lbl_d.Visible = false;
                    lbl_dAction.Visible = false;
                }
            }
        }

        public void invisible()
        {
            lbl_r.Visible = false;
            lbl_rAction.Visible = false;
            lbl_n.Visible = false;
            lbl_nAction.Visible = false;
            lbl_d.Visible = false;
            lbl_dAction.Visible = false;
            
            lbl_a.Visible = false;
            lbl_aAction.Visible = false;
            
            if(!standby && army.Standby.Count > 0)
            {
                lbl_a.Visible = true;
                lbl_aAction.Visible = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (dtxt_renameUnit.InputEnabled)
            {
                if (InputHandler.keyReleased(Keys.Escape))
                {
                    dtxt_renameUnit.InputEnabled = false;
                    dtxt_renameUnit.close();
                }
            }
            else
            {
                if (menu_units.Selected != sel)
                {
                    sel = menu_units.Selected;
                    if (sel < army.Units.Count)
                    {
                        menu_chars.clear();
                        foreach (Character c in army.Units[sel].Characters)
                        {
                            menu_chars.add(new Link(c.Name));
                        }
                        menu_chars.unfocusLink();

                        lbl_enterAction.Text = "Manage Unit";
                        visible();
                    }
                    else
                    {
                        menu_chars.clear();
                        foreach (Character c in army.Standby)
                        {
                            menu_chars.add(new Link(c.Name));
                        }
                        menu_chars.unfocusLink();

                        lbl_enterAction.Text = "View Standby";
                        invisible();
                    }
                }

                if (InputHandler.keyReleased(Keys.Escape))
                {
                    if (standby)
                    {
                        menu_units.TabStop = true;
                        menu_chars.TabStop = false;
                        menu_chars.unfocusLink();
                        standby = false;
                        lbl_enterAction.Text = "View Standby";
                        invisible();
                    }
                    else
                        StateManager.Instance.goBack();
                }

                if (InputHandler.keyReleased(Keys.S)&&lbl_s.Visible)
                {
                    StateManager.Instance.goForward(new Shop());
                }

                if (InputHandler.keyReleased(Keys.Enter))
                {
                    if (standby)
                    {
                        if (army.Standby.Count > 0)
                            StateManager.Instance.goForward(new CharManage(army.Standby[menu_chars.Selected], null));
                    }
                    else
                    {
                        if (lbl_enterAction.Text == "View Standby")
                        {
                            if (menu_chars.Count > 0)
                            {
                                menu_units.TabStop = false;
                                menu_chars.TabStop = true;
                                menu_chars.refocusLink();
                                standby = true;
                                lbl_enterAction.Text = "View Character";
                                invisible();
                            }
                        }
                        else
                        {
                            StateManager.Instance.goForward(new UnitManage(army, menu_units.Selected));
                        }
                    }
                }

                if (InputHandler.keyReleased(Keys.R) && lbl_r.Visible)
                {
                    foreach (Character c in army.Units[menu_units.Selected].Characters)
                    {
                        army.Standby.Add(c);
                    }

                    army.Units.Remove(army.Units[menu_units.Selected]);
                    GameState.CurrentState.saved = false;

                    resume();
                }

                if (InputHandler.keyReleased(Keys.N) && lbl_n.Visible)
                {
                    dialog_showTxt(null, null);
                }

                if (InputHandler.keyReleased(Keys.I))
                {
                    StateManager.Instance.goForward(new ArmyInventory());
                }

                if (InputHandler.keyReleased(Keys.D) && menu_units.Selected < army.Units.Count)
                {
                    if (army.Units[menu_units.Selected].Deployed)
                        army.Units[menu_units.Selected].Deployed = false;
                    else
                    {
                        if (deploy != null && deploy(this, army.Units[menu_units.Selected]))
                            army.Units[menu_units.Selected].Deployed = true;
                    }

                    resume();
                }

                if (InputHandler.keyReleased(Keys.A) && lbl_a.Visible)
                {
                    StateManager.Instance.goForward(new UnitCreation(army));
                }

                if (InputHandler.keyReleased(Keys.H) && lbl_h.Visible)
                {
                    StateManager.Instance.goForward(new Recruitment());
                }
            }
        }

        private void dialog_complete(char[] str)
        {
            String s = new String(str).Trim();
            if(s != String.Empty)
                army.Units[sel].Name = s;

            GameState.CurrentState.saved = false;
            resume();

            dtxt_renameUnit.InputEnabled = false;
            InputHandler.flush();
        }

        private void dialog_showTxt(object sender, EventArgs e)
        {
            dtxt_renameUnit.InputEnabled = true;
            dtxt_renameUnit.show("Rename unit: ");
        }
    }
}
