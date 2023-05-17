using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Stats.Base;
using System.Collections.Specialized;

namespace HsLib.Types.Auras.Base
{
    public class MinionAura : Aura
    {
        public MinionAura(Minion owner, IAuraEffect auraEffect, ICardsChooser cardsChooser)
        {
            Owner = owner;
            _auraEffect = auraEffect;
            _cardsChooser = cardsChooser;
        }

        public Minion Owner { get; }

        private Battlefield? _bf;

        private readonly IAuraEffect _auraEffect;
        private readonly ICardsChooser _cardsChooser;

        private readonly List<IEnchantHandler> _appliedAuras = new();

        protected override void Start(Battlefield bf)
        {
            bf.CollectionChanged += Bf_CollectionChanged;
            _bf = bf;
        }

        protected override void Stop(Battlefield bf)
        {
            bf.CollectionChanged -= Bf_CollectionChanged;
            _bf = null;
        }

        private void Bf_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            ReapplyAuras();
        }

        /// <summary>
        /// Reapplies enchants and populate <see cref="_auraEffect"/> with new enchants.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        private void ReapplyAuras()
        {
            // cleaning
            _appliedAuras.ForEach(e => e.Active = false);
            _appliedAuras.Clear();

            // applying
            if (_bf is null) { throw new InvalidOperationException("bf is null"); }
            foreach (ICard card in _cardsChooser.ChooseCards(Owner.Place!.Pid, _bf.Cards))
            {
                _auraEffect.GiveAura(_bf, card);
            }
        }
    }
}
