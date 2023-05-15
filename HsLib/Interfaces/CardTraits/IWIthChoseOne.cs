using HsLib.Types.Cards;

namespace HsLib.Interfaces.CardTraits
{
    public interface IWithChoseOne
    {
        IEnumerable<CardId>? ChoseOne { get; }
    }
}
