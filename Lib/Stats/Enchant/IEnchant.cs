namespace Models.Stats.Enchant
{
    public interface IEnchant<T>
    {
        T Apply(T statValue);

        bool Active { get; set; }
    }
}