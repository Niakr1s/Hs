﻿using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Places;
using HsLib.Types.Stats;
using System.Collections.Specialized;

namespace HsLib.Types.LingeringEffects.Auras
{
    public class AuraSource : LingeringEffectSource<Minion>
    {
        public AuraSource(Minion owner, IAuraEffect auraEffect, IChooser<PlaceInContainer> cardsChooser)
            : base(owner)
        {
            _auraEffect = auraEffect;
            _cardsChooser = cardsChooser;
        }

        private readonly IAuraEffect _auraEffect;
        private readonly IChooser<PlaceInContainer> _cardsChooser;
        private readonly List<IEnchantHandler> _appliedAuras = new();

        protected override void DoSubscribe(Battlefield bf)
        {
            bf.CollectionChanged += Bf_CollectionChanged;
            ReapplyAuras();
        }

        protected override void DoUnsubscribe(Battlefield bf, Place previousPlace)
        {
            bf.CollectionChanged -= Bf_CollectionChanged;
            CleanAuras();
        }

        private void Bf_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
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
            if (Owner.PlaceInContainer is null) { throw new InvalidOperationException("owner doesn't in container"); }

            foreach (ICard card in _cardsChooser.ChooseCards(Owner.PlaceInContainer, Bf.Cards))
            {
                _appliedAuras.Add(_auraEffect.GiveAura(Bf, Owner, card));
            }
        }
    }
}
