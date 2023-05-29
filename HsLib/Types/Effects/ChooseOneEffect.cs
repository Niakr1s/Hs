using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;

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

        public Action UseEffect(IBoard board, ICard owner, ICard? target)
        {
            return () => UseEffect(board, owner);
        }

        private void UseEffect(IBoard board, ICard owner)
        {
            CardId transformToId = board[owner.Place.Pid].Player.ChooseOne(CardIds);
            IContainer? container = board[owner.Place.Pid].GetContainer(owner);
            if (container is null) { return; }

            int index = container.IndexOf(owner);
            if (index == -1) { return; }

            container[index] = CardBuilder.FromId(transformToId);
        }
    }
}
