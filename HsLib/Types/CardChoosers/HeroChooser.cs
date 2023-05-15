using HsLib.Types.Cards;

namespace HsLib.Types.CardChoosers
{
    public class HeroChooser : CardsChooser
    {
        public HeroChooser() :
            base((pid, cards) => cards.Where(c => c is Hero && c.Place!.Pid == pid))
        {
        }
    }
}
