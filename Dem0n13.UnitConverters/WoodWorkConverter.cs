namespace Dem0n13.UnitConverters
{
    public class WoodWorkConverter : UnitConverter<WoodWork, int>
    {
        public override WoodWork BaseUnit
        {
            get { return WoodWork.Board; }
        }

        public WoodWorkConverter()
        {
            Register(WoodWork.Bench, input => input*2, input => input/2);
            Register(WoodWork.Table, input => input*4, input => input/4);
        }
    }
}
