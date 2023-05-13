using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface IWithChoseOne
    {
        IEnumerable<CardId>? ChoseOne { get; }
    }
}
