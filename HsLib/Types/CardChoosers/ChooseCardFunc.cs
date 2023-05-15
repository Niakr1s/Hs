using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.CardChoosers
{

    public delegate ICard? ChooseCardFunc(Battlefield bf, Pid pid);
}
