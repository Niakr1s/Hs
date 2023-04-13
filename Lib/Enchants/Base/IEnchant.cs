namespace Models.Enchants.Base
{
    public interface IEnchant<T>
    {
        T Apply(T statValue);

        bool Active { get; set; }
    }
}