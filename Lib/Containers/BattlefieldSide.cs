using Models.Common;

namespace Models.Containers
{
    public class BattlefieldSide
    {
        public BattlefieldSide(Battlefield bf, Pid pid, StartingDeck startingDeck)
        {
            Bf = bf;
            Pid = pid;

            Deck = new(pid, cards: startingDeck.Cards);
            Hand = new(pid);
            Field = new(pid);

            Hero = new Hero(pid);

            Graveyard = new(pid);
        }

        public Pid Pid { get; }

        public Battlefield Bf { get; }

        public Deck Deck { get; }

        public Hand Hand { get; }

        public Field Field { get; }

        public Hero Hero { get; }

        public Graveyard Graveyard { get; }
    }
}
