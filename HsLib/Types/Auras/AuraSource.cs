﻿using HsLib.Types.BattlefieldSubscribers;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Stats;

namespace HsLib.Types.Auras
{
    public class AuraSource : BattlefieldSubscriber<ICard>
    {
        public AuraSource(Minion owner, IAuraEffect auraEffect, IChooser<ICard> cardsChooser)
            : base(owner)
        {
            _auraEffect = auraEffect;
            _cardsChooser = cardsChooser;
        }

        private readonly IAuraEffect _auraEffect;
        private readonly IChooser<ICard> _cardsChooser;
        private readonly List<IEnchantHandler> _appliedAuras = new();

        protected override void OnCollectionChanged()
        {
            CleanAuras();
            ReapplyAuras();
        }

        private void CleanAuras()
        {
            _appliedAuras.ForEach(e => e.Deactivate());
            _appliedAuras.Clear();
        }

        /// <summary>
        /// Reapplies enchants and populate <see cref="_auraEffect"/> with new enchants.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        private void ReapplyAuras()
        {
            if (Bf is null) { return; }
            if (Owner.Place.IsNone()) { throw new InvalidOperationException("owner doesn't in container"); }

            foreach (ICard card in _cardsChooser.ChooseCards(Bf, Owner))
            {
                _appliedAuras.Add(_auraEffect.GiveAura(Bf, Owner, card));
            }
        }
    }
}
