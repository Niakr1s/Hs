﻿using HsLib.Systems;
using HsLib.Types.Choosers;
using HsLib.Types.Places;

namespace HsLib.Types.Effects
{
    public class DeathrattleEffect
    {
        public DeathrattleEffect(IEffect effect, IChooser targetsChooser)
        {
            Effect = effect;
            TargetsChooser = targetsChooser;
        }

        public IEffect Effect { get; }
        public IChooser TargetsChooser { get; }

        public Action ActivateDeathrattle(Battlefield bf, Pid owner)
        {
            List<Action> effectActions = new();

            // todo
            /*
            foreach (ICard target in TargetsChooser.ChooseCards(bf, owner, bf.Cards))
            {
                // using null! for owner param, coz deathrattle should use only ono-owner effects
                effectActions.Add(Effect.UseEffect(bf, null!, target));
            }
            */
            return () => effectActions.ForEach(a => a.Invoke());
        }
    }
}