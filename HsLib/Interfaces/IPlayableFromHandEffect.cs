﻿using HsLib.Systems;
using HsLib.Types;

namespace HsLib.Interfaces
{
    public interface IPlayableFromHandEffect : IActiveEffect<Pid>
    {
        /// <summary>
        /// Validates of validity or effect target while playing from hand.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="ownerPid"></param>
        /// <param name="effectTarget"></param>
        /// <exception cref="ValidationException"></exception>
        /// 
        void ValidatePlayFromHandEffectTarget(Battlefield bf, Pid ownerPid, ICard? effectTarget);
    }
}
