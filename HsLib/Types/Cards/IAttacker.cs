﻿using HsLib.Systems;

namespace HsLib.Types.Cards
{
    public interface IAttacker : ICard, IWithAtk
    {
        /// <summary>
        /// Shows if attacker has some attacks left and it is valid and alive.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        bool CanMeleeAttack(IBoard board);

        /// <summary>
        /// Will be called after minion successfully attacked.
        /// </summary>
        /// <param name="board"></param>
        void AfterAttack(IBoard board);

        /// <summary>
        /// Returns defender, who will receive counterattack.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        IDamageable GetDefender(IBoard board);
    }
}
