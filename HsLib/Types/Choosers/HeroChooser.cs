﻿using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Choosers
{
    public class HeroChooser : Chooser<Pid>
    {
        private static readonly ChooserFunc<Pid> _f =
            (owner, cards) => cards
            .Where(c => c is Hero && c.PlaceInContainer!.Pid == owner && c.PlaceInContainer!.Loc == Loc.Hero);

        public HeroChooser() : base(_f)
        {
        }
    }
}