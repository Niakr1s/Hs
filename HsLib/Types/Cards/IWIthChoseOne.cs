namespace HsLib.Types.Cards
{
    public interface IWithChoseOne
    {
        IEnumerable<CardId>? ChoseOne { get; }
    }
}
