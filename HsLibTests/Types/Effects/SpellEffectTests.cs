using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Effects;
using HsLib.Types.Places;

namespace HsLibTests.Types.Effects
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
        public void ValidateEffectTargetTest()
        {
            Minion? nullMinion = null;

            Minion validMinion = new ChillwindYeti();
            _bf.Enemy.Field.Add(validMinion);

            Minion invalidMinion = new ChillwindYeti();
            _bf.Player.Field.Add(invalidMinion);

            Targets validTargets = new Targets() { Sides = PidSide.He, Locs = Loc.Field };
            // valid effect to target minion
            SpellEffect havePossibleTargetsEffect = new(null!, new DamageEffect(), validTargets);
            // invalid effect to target minion
            SpellEffect noPossibleTargetsEffect = new(null!, new DamageEffect());

            List<(SpellEffect?, ICard?, bool)> testCases = new()
            {
                (null, nullMinion, true),
                (null, validMinion, false),
                (null, invalidMinion, false),

                // now validMinion is in possible targets
                (havePossibleTargetsEffect, nullMinion, false),
                (havePossibleTargetsEffect, validMinion, true),
                (havePossibleTargetsEffect, invalidMinion, false),

                // now validMinion is in not possible targets
                (noPossibleTargetsEffect, nullMinion, true),
                (noPossibleTargetsEffect, validMinion, false),
                (noPossibleTargetsEffect, invalidMinion, false),
            };

            for (int i = 0; i < testCases.Count; i++)
            {
                (SpellEffect? effect, ICard? target, bool shouldPass) = testCases[i];

                void doTest() => TargetableEffectValidator.ValidateEffectTarget(effect, _bf, Pid.P1, target);

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