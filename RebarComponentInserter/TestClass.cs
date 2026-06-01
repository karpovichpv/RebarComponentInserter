using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using RebarComponentInserter.Extensions;

namespace RebarComponentInserter
{
    public static class TestClass
    {
        public static void Create()
        {
            // Create a beam programmatically instead of using picker
            Beam beam = CreateTestBeam();

            ComponentInserterData data = new()
            {
                ComponentAttributes = "standard",
                ComponentPointsInputType = ComponentPointsInputType.TwoPointsByOnePointInput,
                ComponentName = "Каркас_1",
                ComponentNumber = -1,
                Wall = beam,
                Spacing = 200,
                SpacingType = RebarSpacing.SpacingType.EXACT_FLEXIBLE_FIRST,
                ExcludeType = RebarSpacing.ExcludeTypeEnum.EXCLUDE_TYPE_NONE,
            };

            ComponentInserter.Insert(data);

            new Model().CommitChanges();
        }

        private static Beam CreateTestBeam()
        {
            // Create a concrete beam (wall) at a specific position
            // Using coordinates in millimeters (Tekla standard)
            Point startPoint = new Point(0, 0, 0);
            Point endPoint = new Point(5000, 0, 0); // 5m x 3m beam

            Beam beam = new Beam(startPoint, endPoint)
            {
                Profile = { ProfileString = "3000*300" }, // Rectangular profile
                Material = { MaterialString = "Concrete_Undefined" },  // Concrete material
                Name = "ConcreteWall",
                Class = "1",
                Finish = "PAINTED",
                PartNumber = { Prefix = "W" },
                Position = {
                    Depth = Position.DepthEnum.FRONT,
                    Plane = Position.PlaneEnum.MIDDLE
                }
            };

            bool result = beam.Insert();

            if (result)
            {
                // Draw the start point for visualization
                startPoint.DrawPoint();
            }

            return beam;
        }
    }
}
