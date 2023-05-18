using HsLib.Types.Containers;

namespace HsLib.Types.Places
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

        public bool SameSide(PlaceInContainer other)
        {
            return Pid == other.Pid;
        }

        public bool SameLoc(PlaceInContainer other)
        {
            return Loc == other.Loc;
        }

        public bool IsLeftOf(PlaceInContainer other)
        {
            return SameSide(other) && SameLoc(other) && other.Index - Index == 1;
        }

        public bool IsRightOf(PlaceInContainer other)
        {
            return SameSide(other) && SameLoc(other) && other.Index - Index == -1;
        }
    }
}
