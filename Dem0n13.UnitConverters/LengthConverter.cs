namespace Dem0n13.UnitConverters
{
    public class LengthConverter : UnitConverter<Length, double>
    {
        public override Length BaseUnit
        {
            get { return Length.Meters; }
        }

        public LengthConverter()
        {
            Register(Length.Kilometers, input => input*1000.0, input => input/1000.0);
            Register(Length.Miles, Length.Kilometers, input => input*1.609344, input => input/1.609344);
        }
    }
}
