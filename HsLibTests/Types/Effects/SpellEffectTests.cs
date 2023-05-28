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

            Minion enemyMinion = new ChillwindYeti();
            _bf.Enemy.Field.Add(enemyMinion);

            Minion playerMinion = new ChillwindYeti();
            _bf.Player.Field.Add(playerMinion);

            //Spell havePossibleTargetsEffectSpell = new SpellWithPossibleTargetsEffect();
            //Spell haveNoPossibleTargetsEffectSpell = new SpellWithNoPossibleTargetsEffect();

            //_bf.Player.Hand.Add(havePossibleTargetsEffectSpell);
            //_bf.Player.Hand.Add(haveNoPossibleTargetsEffectSpell);
            Targets validTargets = new Targets() { Sides = PidSide.He, Locs = Loc.Field };

            // valid effect to target minion
            SpellEffect havePossibleTargetsEffect = new(playerMinion, new DamageEffect(1), validTargets);
            // invalid effect to target minion
            SpellEffect noPossibleTargetsEffect = new(playerMinion, new DamageEffect(1));

            List<(SpellEffect?, ICard?, bool)> testCases = new()
            {
                (null, nullMinion, true),
                (null, enemyMinion, false),
                (null, playerMinion, false),

                // now validMinion is in possible targets
                (havePossibleTargetsEffect, nullMinion, false),
                (havePossibleTargetsEffect, enemyMinion, true),
                (havePossibleTargetsEffect, playerMinion, false),

                // now validMinion is in not possible targets
                (noPossibleTargetsEffect, nullMinion, true),
                (noPossibleTargetsEffect, enemyMinion, false),
                (noPossibleTargetsEffect, playerMinion, false),
            };

            for (int i = 0; i < testCases.Count; i++)
            {
                (SpellEffect? effect, ICard? target, bool shouldPass) = testCases[i];

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