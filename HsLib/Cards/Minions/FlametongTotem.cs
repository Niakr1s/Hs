using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Containers.Base;
using HsLib.Types.Stats.Base;
using System.Collections.Specialized;

namespace HsLib.Cards.Minions
{
    public class FlametongTotem : Minion
    {
        public FlametongTotem() : base(2, 0, 3)
        {
        }

        public override void AfterContainerInsert(Battlefield bf)
        {
            base.AfterContainerInsert(bf);
            if (PlaceInContainer?.Loc == Loc.Field)
            {
                ReapplyEnchants(bf);
                bf.CollectionChanged += Bf_CollectionChanged;
            }
        }
        public override void AfterContainerRemove(Battlefield bf)
        {
            base.AfterContainerRemove(bf);
            bf.CollectionChanged -= Bf_CollectionChanged;
            ReapplyEnchants(bf);
        }

        private void Bf_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            IContainer c = (IContainer)sender!;
            ReapplyEnchants(c.Bf);
        }

        private readonly List<IEnchantHandler> _appliedEnchants = new();

        private void ReapplyEnchants(Battlefield bf)
        {
            _appliedEnchants.ForEach(e => e.Active = false);
            _appliedEnchants.Clear();

            foreach (Minion target in GetEnchantTargets(bf))
            {
                _appliedEnchants.Add(target.Atk.AddAura(2));
            }
        }

        private IEnumerable<Minion> GetEnchantTargets(Battlefield bf)
        {
            if (PlaceInContainer is null) { yield break; }
            Field f = bf[PlaceInContainer.Pid].Field;
            Minion? left = f.Left(PlaceInContainer.Index);
            Minion? right = f.Right(PlaceInContainer.Index);
            if (left is not null) yield return left;
            if (right is not null) yield return right;
        }
    }
}
