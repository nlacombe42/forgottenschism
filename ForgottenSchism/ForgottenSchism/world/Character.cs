﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public abstract class Character
    {
        public enum Class_Type {FIGHTER, CASTER, HEALER, SCOUT, ARCHER};

        public struct Stats
        {
            public struct Traits
            {
                public int str;
                public int dex;
                public int intel;
                public int wis;
                public int spd;
                public int con;
            };

            public enum State {NORMAL, SLEEP, PARALYZED, STONE, SILENCED, POISONED};

            public int maxHp;
            public int hp;
            public int maxMana;
            public int mana;
            public int movement;
            public Traits traits;
            public State state;
        };

        protected String name;
        protected int level;
        protected int exp;
        protected Stats stats;
        protected Class_Type type;
        private String org;
        protected Content.Class_info cinfo;
        
        bool moved;

        public Character(String fname, Content.Class_info fcinfo, Class_Type ftype)

        {
            stats=new Stats();
            
            moved = false;

            stats.traits = fcinfo.start;
            level = 1;
            exp = 0;

            calcStat();

            stats.hp = stats.maxHp;
            stats.mana = stats.maxMana;

            init(fname, fcinfo, ftype);
        }

        public Character(String fname, Stats fstats, Content.Class_info fcinfo, Class_Type ftype)
        {
            stats = fstats;

            init(fname, fcinfo, ftype);
        }

        private void init(String fname, Content.Class_info fcinfo, Class_Type ftype)
        {
            type = ftype;
            name = fname;
            level = 0;
            exp = 0;
            org = "";
            cinfo = fcinfo;
            moved = false;
        }

        public String Organization
        {
            get { return org; }
            set { org = value; }
        }

        public Class_Type Type
        {
            get { return type; }
        }

        public String Name
        {
            get {return name;}
        }

        public int Lvl
        {
            get { return level; }
            set { level = value; }
        }

        public int Exp
        {
            get { return exp; }
            set { exp = value; }
        }

        public Stats Stat
        {
            get { return stats; }
            set { stats = value; }
        }
        
        public bool Moved
        {
        	get { return moved; }
        	set { moved = value; }
        }

        protected int hit(Character c)
        {
            return Gen.d(1, 20)+(c.stats.traits.dex-stats.traits.dex);
        }

        public bool gainExp(Character c)
        {
            int gexp=100;

            if(c.Lvl<level)
                gexp/=(level-c.Lvl);
            else if(c.Lvl>level)
                gexp *= (c.Lvl-level);

            return gainExp(gexp);
        }

        public bool gainExp(int fexp)
        {
            exp += fexp;

            bool ret = false;

            while (exp >= level * cinfo.lvl_exp)
            {
                ret = true;

                levelUp();
            }

            return ret;
        }

        private void calcStat()
        {
            int nmhp=stats.traits.con * 10;

            stats.hp+=(nmhp-stats.maxHp);
            stats.maxHp = nmhp;

            int nmmana = stats.traits.wis * 10;

            stats.mana += (nmmana - stats.maxMana);
            stats.maxMana = nmmana;
        }

        public void levelUp()
        {
            if(exp<level*cinfo.lvl_exp)
                exp=level*cinfo.lvl_exp;

            level++;

            stats.traits.str+=cinfo.levelup.str;
            stats.traits.dex+=cinfo.levelup.dex;
            stats.traits.con+=cinfo.levelup.con;
            stats.traits.wis+=cinfo.levelup.wis;
            stats.traits.intel+=cinfo.levelup.intel;
            stats.traits.spd+=cinfo.levelup.spd;

            calcStat();
        }

        public bool isAlive()
        {
            return stats.hp > 0;
        }

        public int getHp(int hp)
        {
            stats.hp += hp;

            if (stats.hp > stats.maxHp)
            {
                hp -= stats.hp - stats.maxHp;
                stats.hp = stats.maxHp;
            }

            return hp;
        }

        public int recMagicDmg(int dmg)
        {
            int fd = dmg - stats.traits.wis;

            if (fd < 0)
                fd = 0;

            stats.hp -= fd;

            if (stats.hp < 0)
                stats.hp = 0;

            return fd;
        }

        public int recPhyDmg(int dmg)
        {
            int fd = dmg - stats.traits.con;

            if (fd < 0)
                fd = 0;

            stats.hp -= fd;

            if (stats.hp < 0)
                stats.hp = 0;

            return fd;
        }
    }
}
