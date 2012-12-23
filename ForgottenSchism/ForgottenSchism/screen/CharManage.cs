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
    public class CharManage : Screen
    {
        Character c;

        Label lbl_charMng;
        PictureBox charPic;

        Label lbl_name;

        Label lbl_class;

        Label lbl_level;
        Label lbl_clevel;

        Label lbl_exp;
        Label lbl_cexp;

        Label lbl_hp;
        Label lbl_currHp;
        Label lbl_hpSlash;
        Label lbl_maxHp;

        Label lbl_mana;
        Label lbl_currMana;
        Label lbl_manaSlash;
        Label lbl_maxMana;

        Label lbl_state;
        Label lbl_cstate;

        Label lbl_force;
        Label lbl_cforce;

        Label lbl_dex;
        Label lbl_cdex;

        Label lbl_intel;
        Label lbl_cintel;

        Label lbl_sag;
        Label lbl_csag;

        Label lbl_spd;
        Label lbl_cspd;

        Label lbl_con;
        Label lbl_ccon;

        Label lbl_move;
        Label lbl_cmove;

        Label lbl_org;
        Label lbl_corg;

        public CharManage(Character selectedChar)
        {
            c = selectedChar;

            lbl_charMng = new Label("Character Management");
            lbl_charMng.Color = Color.Gold;
            lbl_charMng.Position = new Vector2(50, 30);

            charPic = new PictureBox();
            charPic.Image = Graphic.getSprite(c);
            charPic.Size = new Vector2(384, 384);
            charPic.Position = new Vector2(0, 60);

            MainWindow.add(lbl_charMng);
            MainWindow.add(charPic);

            lbl_name = new Label(c.Name);
            lbl_name.Color = Color.White;
            lbl_name.Position = new Vector2(400, 50);
            MainWindow.add(lbl_name);

            lbl_class = new Label(c.Type.ToString());
            lbl_class.Color = Color.White;
            lbl_class.Position = new Vector2(400, 80);
            MainWindow.add(lbl_class);

            lbl_level = new Label("Level:");
            lbl_level.Position = new Vector2(400, 110);
            MainWindow.add(lbl_level);

            lbl_clevel = new Label(c.Lvl.ToString());
            lbl_clevel.Color = Color.White;
            lbl_clevel.Position = new Vector2(460, 110);
            MainWindow.add(lbl_clevel);

            lbl_exp = new Label("Exp:");
            lbl_exp.Position = new Vector2(520, 110);
            MainWindow.add(lbl_exp);

            lbl_cexp = new Label(c.Exp.ToString());
            lbl_cexp.Color = Color.White;
            lbl_cexp.Position = new Vector2(565, 110);
            MainWindow.add(lbl_cexp);

            lbl_hp = new Label("HP:");
            lbl_hp.Position = new Vector2(400, 140);
            MainWindow.add(lbl_hp);

            lbl_currHp = new Label(c.stats.hp.ToString());
            lbl_currHp.Color = Color.White;
            lbl_currHp.Position = new Vector2(520, 140);
            MainWindow.add(lbl_currHp);

            lbl_hpSlash = new Label("/");
            lbl_hpSlash.Position = new Vector2(560, 140);
            MainWindow.add(lbl_hpSlash);

            lbl_maxHp = new Label(c.stats.maxHp.ToString());
            lbl_maxHp.Color = Color.White;
            lbl_maxHp.Position = new Vector2(575, 140);
            MainWindow.add(lbl_maxHp);

            lbl_mana = new Label("Mana:");
            lbl_mana.Position = new Vector2(400, 170);
            MainWindow.add(lbl_mana);

            lbl_currMana = new Label(c.stats.mana.ToString());
            lbl_currMana.Color = Color.White;
            lbl_currMana.Position = new Vector2(520, 170);
            MainWindow.add(lbl_currMana);

            lbl_manaSlash = new Label("/");
            lbl_manaSlash.Position = new Vector2(560, 170);
            MainWindow.add(lbl_manaSlash);

            lbl_maxMana = new Label(c.stats.maxMana.ToString());
            lbl_maxMana.Color = Color.White;
            lbl_maxMana.Position = new Vector2(575, 170);
            MainWindow.add(lbl_maxMana);

            lbl_state = new Label("State:");
            lbl_state.Position = new Vector2(400, 200);
            MainWindow.add(lbl_state);

            lbl_cstate = new Label(c.stats.state.ToString());
            lbl_cstate.Color = Color.White;
            lbl_cstate.Position = new Vector2(520, 200);
            MainWindow.add(lbl_cstate);

            lbl_force = new Label("Strength:");
            lbl_force.Position = new Vector2(400, 230);
            MainWindow.add(lbl_force);

            lbl_cforce = new Label(c.stats.traits.str.ToString());
            lbl_cforce.Color = Color.White;
            lbl_cforce.Position = new Vector2(520, 230);
            MainWindow.add(lbl_cforce);

            lbl_dex = new Label("Dexterity:");
            lbl_dex.Position = new Vector2(400, 260);
            MainWindow.add(lbl_dex);

            lbl_cdex = new Label(c.stats.traits.dex.ToString());
            lbl_cdex.Color = Color.White;
            lbl_cdex.Position = new Vector2(520, 260);
            MainWindow.add(lbl_cdex);

            lbl_intel = new Label("Intelligence:");
            lbl_intel.Position = new Vector2(400, 290);
            MainWindow.add(lbl_intel);

            lbl_cintel = new Label(c.stats.traits.intel.ToString());
            lbl_cintel.Color = Color.White;
            lbl_cintel.Position = new Vector2(520, 290);
            MainWindow.add(lbl_cintel);

            lbl_sag = new Label("Wisdom:");
            lbl_sag.Position = new Vector2(400, 320);
            MainWindow.add(lbl_sag);

            lbl_csag = new Label(c.stats.traits.wis.ToString());
            lbl_csag.Color = Color.White;
            lbl_csag.Position = new Vector2(520, 320);
            MainWindow.add(lbl_csag);

            lbl_spd = new Label("Speed:");
            lbl_spd.Position = new Vector2(400, 350);
            MainWindow.add(lbl_spd);

            lbl_cspd = new Label(c.stats.traits.spd.ToString());
            lbl_cspd.Color = Color.White;
            lbl_cspd.Position = new Vector2(520, 350);
            MainWindow.add(lbl_cspd);

            lbl_con = new Label("Constitution:");
            lbl_con.Position = new Vector2(400, 380);
            MainWindow.add(lbl_con);

            lbl_ccon = new Label(c.stats.traits.con.ToString());
            lbl_ccon.Color = Color.White;
            lbl_ccon.Position = new Vector2(520, 380);
            MainWindow.add(lbl_ccon);

            lbl_move = new Label("Movement:");
            lbl_move.Position = new Vector2(400, 410);
            MainWindow.add(lbl_move);

            lbl_cmove = new Label(c.stats.movement.ToString());
            lbl_cmove.Color = Color.White;
            lbl_cmove.Position = new Vector2(520, 410);
            MainWindow.add(lbl_cmove);

            lbl_org = new Label("Organization:");
            lbl_org.Position = new Vector2(50, 410);
            MainWindow.add(lbl_org);

            lbl_corg = new Label(c.Organization);
            lbl_corg.Color = Color.White;
            lbl_corg.Position = new Vector2(170, 410);
            MainWindow.add(lbl_corg);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
            {
                StateManager.Instance.goBack();
            }
        }
    }
}
