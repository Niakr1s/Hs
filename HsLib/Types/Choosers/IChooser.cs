using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Choosers
{
    public interface IChooser
    {
        IEnumerable<ICard> ChooseCards(Battlefield bf, ICard owner);
    }
}
