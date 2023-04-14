﻿using Models.Cards;
using Models.Common;
using Models.Containers.Base;

namespace Models.Containers
{
    public class Graveyard : MultiContainer<Card>
    {
        public Graveyard(Pid pid) : base(pid, Loc.Graveyard)
        {
        }
    }
}