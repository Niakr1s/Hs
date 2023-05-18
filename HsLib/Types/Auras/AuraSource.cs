using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Places;
using HsLib.Types.Stats;
using System.Collections.Specialized;

namespace HsLib.Types.Auras
{
    public class AuraSource : IAuraSource
    {
        public AuraSource(ICard owner, IAuraEffect auraEffect, IChooser<PlaceInContainer> cardsChooser)
        {
            Owner = owner;
            _auraEffect = auraEffect;
            _cardsChooser = cardsChooser;
        }

        public bool IsActive { get; private set; }

        public ICard Owner { get; }

        private Battlefield? _bf;

        private readonly IAuraEffect _auraEffect;
        private readonly IChooser<PlaceInContainer> _cardsChooser;

        private readonly List<IEnchantHandler> _appliedAuras = new();

        protected void Start(Battlefield bf)
        {
            bf.CollectionChanged += Bf_CollectionChanged;

            _bf = bf;
            ReapplyAuras();
        }

        protected void Stop(Battlefield bf)
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
            if (_bf is null) { return; }
            if (Owner.PlaceInContainer is null) { throw new InvalidOperationException("owner doesn't in container"); }

            foreach (ICard card in _cardsChooser.ChooseCards(Owner.PlaceInContainer, _bf.Cards))
            {
                _appliedAuras.Add(_auraEffect.GiveAura(_bf, Owner, card));
            }
        }

        /// <summary>
        /// Activates aura.
        /// </summary>
        /// <param name="bf"></param>
        /// <exception cref="Exception">Throws, if anything unexpected occurs.</exception>
        /// <returns>True, if was success activated.</returns>
        public bool Activate(Battlefield bf)
        {
            if (IsActive) { return false; }

            try
            {
                Start(bf);
                IsActive = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deactivates aura.
        /// </summary>
        /// <param name="bf"></param>
        /// <exception cref="Exception">Throws, if anything unexpected occurs.</exception>
        /// <returns>True, if was success deactivated.</returns>
        public bool Deactivate(Battlefield bf)
        {
            if (!IsActive) { return false; }

            try
            {
                Stop(bf);
                IsActive = false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
