using Models.Common;

namespace Models.Containers
{
    public class BattlefieldSide
    {
        public BattlefieldSide(Battlefield bf, Pid pid, StartingDeck startingDeck)
        {
            Bf = bf;
            Pid = pid;

            Deck = new(bf, pid, cards: startingDeck.Cards);
            Hand = new(bf, pid);
            Field = new(bf, pid);
            Graveyard = new(bf, pid);
        }

        public Pid Pid { get; }

        public Battlefield Bf { get; }

        public Deck Deck { get; }

        public Hand Hand { get; }

        public Field Field { get; }

        public Graveyard Graveyard { get; }
    }
}
