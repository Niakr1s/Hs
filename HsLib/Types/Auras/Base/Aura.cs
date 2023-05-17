using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Auras.Base
{
    public abstract class Aura : IAura
    {
        public bool IsActive { get; private set; }

        /// <summary>
        /// Activates aura.
        /// </summary>
        /// <param name="bf"></param>
        /// <exception cref="Exception">Throws, if anything unexpected occurs.</exception>
        /// <returns>True, if was success activated.</returns>
        public bool Activate(Battlefield bf)
        {
            if (IsActive) { return false; }

            try
            {
                Start(bf);
                IsActive = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deactivates aura.
        /// </summary>
        /// <param name="bf"></param>
        /// <exception cref="Exception">Throws, if anything unexpected occurs.</exception>
        /// <returns>True, if was success deactivated.</returns>
        public bool Deactivate(Battlefield bf)
        {
            if (!IsActive) { return false; }

            try
            {
                Stop(bf);
                IsActive = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Starts aura.
        /// </summary>
        /// <param name="bf"></param>
        protected abstract void Start(Battlefield bf);

        /// <summary>
        /// Stops aura.
        /// </summary>
        /// <param name="bf"></param>
        protected abstract void Stop(Battlefield bf);
    }
}
