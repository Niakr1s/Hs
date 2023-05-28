using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public class ChooseOneEffect : IEffect
    {

        public ChooseOneEffect(IEnumerable<CardId> cardIds)
        {
            _cardIds = new(cardIds);
        }

        private readonly List<CardId> _cardIds;
        public IEnumerable<CardId> CardIds => _cardIds.AsEnumerable();

        public Action UseEffect(Battlefield bf, ICard owner, ICard? target)
        {
            return () => UseEffect(bf, owner);
        }

        private void UseEffect(Battlefield bf, ICard owner)
        {
            CardId transformToId = bf[owner.PlaceInContainer!.Pid].Player.ChooseOne(CardIds);
            //bf[owner.PlaceInContainer!][owner.PlaceInContainer!.Index] = CardBuilder.FromId(transformToId);
            // todo
        }
    }
}
