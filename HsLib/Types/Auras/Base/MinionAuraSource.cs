using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Stats.Base;
using System.Collections.Specialized;

namespace HsLib.Types.Auras.Base
{
    public class MinionAuraSource : AuraSource
    {
        public MinionAuraSource(Minion owner, IAuraEffect auraEffect, ICardsChooser<PlaceInContainer> cardsChooser)
        {
            Owner = owner;
            _auraEffect = auraEffect;
            _cardsChooser = cardsChooser;
        }

        public Minion Owner { get; }

        private Battlefield? _bf;

        private readonly IAuraEffect _auraEffect;
        private readonly ICardsChooser<PlaceInContainer> _cardsChooser;

        private readonly List<IEnchantHandler> _appliedAuras = new();

        protected override void Start(Battlefield bf)
        {
            bf.CollectionChanged += Bf_CollectionChanged;
            CleanAuras();
            _bf = bf;
        }

        protected override void Stop(Battlefield bf)
        {
            bf.CollectionChanged -= Bf_CollectionChanged;
            CleanAuras();
            _bf = null;
        }

        private void Bf_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CleanAuras();
            ReapplyAuras();
        }

        private void CleanAuras()
        {
            _appliedAuras.ForEach(e => e.Active = false);
            _appliedAuras.Clear();
        }

        /// <summary>
        /// Reapplies enchants and populate <see cref="_auraEffect"/> with new enchants.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        private void ReapplyAuras()
        {
            if (_bf is null) { throw new InvalidOperationException("bf is null"); }
            if (Owner.PlaceInContainer is null) { throw new InvalidOperationException("owner doesn't in container"); }

            foreach (ICard card in _cardsChooser.ChooseCards(Owner.PlaceInContainer, _bf.Cards))
            {
                _appliedAuras.Add(_auraEffect.GiveAura(_bf, Owner, card));
            }
        }
    }
}
