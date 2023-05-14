﻿using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class AbilityContainer : SingleContainer<Ability>
    {
        public AbilityContainer(Battlefield bf, Pid pid, Ability card) : base(bf, new Place(pid, Loc.Ability), card)
        {
        }
    }
}
