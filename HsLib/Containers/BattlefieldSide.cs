using HsLib.Cards;
using HsLib.Cards.KnownCards.Weapons;
using HsLib.Common.Place;

namespace HsLib.Containers
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

            Hero = new Hero(bf, pid);
            Weapon = new WeaponContainer(bf, pid, new NoWeapon());

            Graveyard = new(bf, pid);
        }

        public Pid Pid { get; }

        public Battlefield Bf { get; }

        public Deck Deck { get; }

        public Hand Hand { get; }

        public Field Field { get; }

        public Hero Hero { get; }

        public WeaponContainer Weapon { get; }

        public Graveyard Graveyard { get; }
    }
}
