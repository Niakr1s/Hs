using HsLib.Types.Containers.Base;

namespace HsLib.Types
{
    public record PlaceInContainer(Pid Pid, Loc Loc, int AddedTurnNo, int Index)
    {
        public PlaceInContainer(IContainer container, int index) :
            this(container.Place.Pid, container.Place.Loc, container.Bf.Turn.No, index)
        {
        }

        public static implicit operator Place(PlaceInContainer placeInContainer)
        {
            return new Place(placeInContainer.Pid, placeInContainer.Loc);
        }
    }
}
