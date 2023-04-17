﻿using HsLib.Battle;
using HsLib.Common.Place;

namespace HsLib.Common.Interfaces
{
    public interface IAttacker : IWithPlace, IWithAtk, IWithCharge, IWithWindfury
    {
        /// <summary>
        /// Will be called after minion successfully attacked.
        /// </summary>
        /// <param name="bf"></param>
        void AfterAttack(Battlefield bf);
    }
}