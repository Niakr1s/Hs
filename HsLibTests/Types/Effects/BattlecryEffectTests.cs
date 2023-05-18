using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.CardsChoosers;
using HsLib.Types.Effects;
using HsLib.Types.Places;
using HsLibTests.Helpers;

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

            Minion validMinion = new ChillwindYeti();
            _bf.Enemy.Field.Add(validMinion);

            Minion invalidMinion = new ChillwindYeti();
            _bf.Player.Field.Add(invalidMinion);

            Targets validTargets = new Targets() { Sides = PidSide.He, Locs = Loc.Field };
            // valid effect to target minion
            BattlecryEffect? havePossibleTargetsEffect = new(new DealDamageEffect(), validTargets);
            // invalid effect to target minion
            BattlecryEffect? noPossibleTargetsEffect = new(new DealDamageEffect());

            List<(BattlecryEffect?, ICard?, bool)> testCases = new()
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
                (BattlecryEffect? effect, ICard? target, bool shouldPass) = testCases[i];

                void doTest() => ActiveEffectValidator.ValidateEffectTarget(effect, _bf, Pid.P1, target);

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