namespace HsLib.Types.CardChoosers
{
    public class HeroChooser : CardChooser
    {
        public HeroChooser() :
            base((bf, pid) => bf[pid].Hero.Card)
        {
        }
    }
}
