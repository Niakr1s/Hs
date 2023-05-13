using HsLib.Systems;

namespace HsLib.Interfaces
{
    public interface IWithDeathrattle
    {
        /// <summary>
        /// Activates deathrattle.
        /// </summary>
        /// <param name="bf"></param>
        /// <returns>False, if deathrattle didn't activate (coz of silence etc)</returns>
        public bool ActivateDeathrattle(Battlefield bf);
    }
}
