using HsLib.Types.BoardSubscribers;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Stats;

namespace HsLib.Types.Auras
{
    public class AuraSource : BoardSubscriber<ICard>
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
            if (Board is null) { return; }
            if (Owner.Place.IsNone()) { throw new InvalidOperationException("owner doesn't in container"); }

            foreach (ICard card in _cardsChooser.ChooseCards(Board, Owner))
            {
                _appliedAuras.Add(_auraEffect.GiveAura(Board, Owner, card));
            }
        }
    }
}
