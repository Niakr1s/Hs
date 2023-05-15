using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface ICardChooser
    {
        ICard? ChooseCard(Battlefield bf, Pid pid);
    }
}
