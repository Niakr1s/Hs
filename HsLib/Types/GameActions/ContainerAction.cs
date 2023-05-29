using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.GameActions
{
    public abstract class ContainerAction : GameAction
    {
        protected ContainerAction(ICard card, Place place)
        {
            Card = card;
            Place = place;
        }

        public ICard Card { get; }
        public Place Place { get; }
    }

    public class ContainerRemoveAction : ContainerAction
    {
        public ContainerRemoveAction(ICard card, Place place) : base(card, place)
        {
        }

        public override void Process(Board board)
        {
            board[Place].Remove(Card);
        }
    }

    public class ContainerAddAction : ContainerAction
    {
        public ContainerAddAction(ICard card, Place place) : base(card, place)
        {
        }

        public override void Process(Board board)
        {
            board[Place].Add(Card);
        }
    }

    public class ContainerSetAction : ContainerAction
    {
        public ContainerSetAction(ICard card, Place place, int index = 0) : base(card, place)
        {
            Index = index;
        }

        public int Index { get; }

        public override void Process(Board board)
        {
            board[Place][Index] = Card;
        }
    }

    public class ContainerInsertAction : ContainerAction
    {
        public ContainerInsertAction(ICard card, Place place, int index = 0) : base(card, place)
        {
            Index = index;
        }

        public int Index { get; }

        public override void Process(Board board)
        {
            board[Place].Insert(Index, Card);
        }
    }
}
