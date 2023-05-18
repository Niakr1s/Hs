using HsLib.Systems;

namespace HsLib.Types.Auras
{
    public interface IAuraSource
    {
        bool IsActive { get; }

        /// <summary>
        /// Activates aura.
        /// </summary>
        /// <param name="bf"></param>
        /// <returns>True, if was activated successfully.</returns>
        bool Activate(Battlefield bf);

        /// <summary>
        /// Activates aura.
        /// </summary>
        /// <param name="bf"></param>
        /// <returns>True, if was deactivated successfully.</returns>
        bool Deactivate(Battlefield bf);
    }
}
