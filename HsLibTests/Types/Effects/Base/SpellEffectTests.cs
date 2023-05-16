using HsLib.Cards.Minions;
using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;
using HsLib.Types.Effects.Base;
using HsLibTests.Helpers;

namespace HsLibTests.Types.Effects.Base
{
    [TestClass()]
    public class SpellEffectTests
    {

        Battlefield _bf = null!;

        public TestContext? TestContext { get; set; }

        [TestInitialize()]
        public void Setup()
        {
            _bf = TestBattlefield.New();
        }

        [TestMethod()]
        public void ValidatePlayFromHandEffectTargetTest()
        {
            Minion? nullMinion = null;

            Minion validMinion = new ChillwindYeti();
            _bf.Enemy.Field.Add(validMinion);

            Minion invalidMinion = new ChillwindYeti();
            _bf.Player.Field.Add(invalidMinion);

            Targets validTargets = new Targets() { Sides = PidSide.He, Locs = Loc.Field };
            // valid effect to target minion
            ActiveEffect havePossibleTargetsEffect = new ActiveEffect(new DealDamageEffect(), validTargets);
            // invalid effect to target minion
            ActiveEffect noPossibleTargetsEffect = new ActiveEffect(new DealDamageEffect());

            List<(SpellEffect, ICard?, bool)> testCases = new()
            {
                (new (), nullMinion, true),
                (new (), validMinion, false),
                (new (), invalidMinion, false),

                // now validMinion is in possible targets
                (new (havePossibleTargetsEffect), nullMinion, false),
                (new (havePossibleTargetsEffect), validMinion, true),
                (new (havePossibleTargetsEffect), invalidMinion, false),

                // now validMinion is in not possible targets
                (new (noPossibleTargetsEffect), nullMinion, true),
                (new (noPossibleTargetsEffect), validMinion, false),
                (new (noPossibleTargetsEffect), invalidMinion, false),
            };

            for (int i = 0; i < testCases.Count; i++)
            {
                (SpellEffect effect, ICard? target, bool shouldPass) = testCases[i];

                void doTest() => effect.ValidatePlayFromHandEffectTarget(_bf, Pid.P1, target);

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