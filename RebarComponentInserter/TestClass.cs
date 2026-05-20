using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace RebarComponentInserter
{
    public static class TestClass
    {
        public static void Create()
        {
            Beam beam = new Beam()
            {
                StartPoint = new Point(),
                EndPoint = new Point(10000, 0, 0),
                AssemblyNumber = new NumberingSeries(),
                Class = "2",
                CastUnitType = Part.CastUnitTypeEnum.CAST_IN_PLACE,
                EndPointOffset = new Offset(),
                StartPointOffset = new Offset(),
                Material = new Material() { MaterialString = "Steel_Undefined" },
                Name = "BEAM",
                Profile = new Profile() { ProfileString = "200*500" },
                PartNumber = new NumberingSeries(),
                Position = new Position(),
            };

            beam.Insert();
        }
    }
}
