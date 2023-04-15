﻿using Models.Containers;
using Models.Services.Battle;
using Models.Stats;
using Models.Stats.Base;

namespace Models.Cards
{
    public abstract class Weapon : Card, IAttacker
    {
        protected Weapon(int mp, int atk, int hp) : base(mp)
        {
            Atk = new Atk(atk);
            Hp = new Hp(hp);
        }

        public Atk Atk { get; }

        public Hp Hp { get; }

        public BoolStat Charge => new BoolStat(true);
        public BoolStat Windfury { get; init; } = new BoolStat(false);

        public bool Dead => Hp.Value <= 0;

        public void AfterAttack(Battlefield bf)
        {
            Hp.Decrease();
        }

        public override void OnTurnEnd(Battlefield bf)
        {
            Atk.AtksThisTurn = 0;
        }
    }
}