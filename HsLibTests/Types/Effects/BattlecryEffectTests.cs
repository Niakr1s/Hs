﻿using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Effects;
using HsLib.Types.Places;

namespace HsLibTests.Types.Effects
{
    [TestClass()]
    public class BattlecryEffectTests
    {

        Battlefield _bf = null!;

        public TestContext? TestContext { get; set; }

        [TestInitialize()]
        public void Setup()
        {
            _bf = TestBattlefield.New();
        }

        [TestMethod()]
        public void ValidateEffectTargetTest()
        {
            Minion? nullMinion = null;

            Minion enemyMinion = new ChillwindYeti();
            _bf.Enemy.Field.Add(enemyMinion);

            Minion playerMInion = new ChillwindYeti();
            _bf.Player.Field.Add(playerMInion);

            Targets validTargets = new Targets() { Sides = PidSide.He, Locs = Loc.Field };
            // valid effect to target minion
            BattlecryEffect? havePossibleTargetsEffect = new(playerMInion, new DamageEffect(1), validTargets);
            // invalid effect to target minion
            BattlecryEffect? noPossibleTargetsEffect = new(playerMInion, new DamageEffect(1));

            List<(BattlecryEffect?, ICard?, bool)> testCases = new()
            {
                (null, nullMinion, true),
                (null, enemyMinion, false),
                (null, playerMInion, false),

                // now validMinion is in possible targets
                (havePossibleTargetsEffect, nullMinion, false),
                (havePossibleTargetsEffect, enemyMinion, true),
                (havePossibleTargetsEffect, playerMInion, false),

                // now validMinion is in not possible targets
                (noPossibleTargetsEffect, nullMinion, true),
                (noPossibleTargetsEffect, enemyMinion, false),
                (noPossibleTargetsEffect, playerMInion, false),
            };

            for (int i = 0; i < testCases.Count; i++)
            {
                (BattlecryEffect? effect, ICard? target, bool shouldPass) = testCases[i];

                void doTest() => TargetableEffectValidator.ValidateEffectTarget(effect, _bf, target);

                if (shouldPass)
                {
                    doTest();
                }
                else
                {
                    Assert.ThrowsException<ValidationException>(doTest);
                }

                TestContext?.WriteLine($"{i} => success");
            }
        }
    }
}