using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using RebarComponentInserter.Extensions;

namespace RebarComponentInserter
{
    public static class TestClass
    {
        public static void Create()
        {
            // Create 10 walls with 1000mm offset on Y coordinate
            const int wallCount = 10;
            const double yOffset = 1000; // 1 meter offset

            for (int i = 0; i < wallCount; i++)
            {
                double y = i * yOffset;
                Point startPoint = new Point(0, y, 0);
                Point endPoint = new Point(5000, y, 0); // 5m x 3m wall

                CreateWall(startPoint, endPoint);
            }

            new Model().CommitChanges();
        }

        private static void CreateWall(Point startPoint, Point endPoint)
        {
            Beam beam = CreateTestBeam(startPoint, endPoint);

            ComponentInserterData data = new()
            {
                ComponentAttributes = "standard",
                ComponentPointsInputType = ComponentPointsInputType.TwoPointsByOnePointInput,
                ComponentName = "Каркас_1",
                ComponentNumber = -1,
                Wall = beam,
                StartPoint = startPoint,
                EndPoint = endPoint,
                Spacing = 200,
                SpacingType = RebarSpacing.SpacingType.EXACT_FLEXIBLE_FIRST,
                ExcludeType = RebarSpacing.ExcludeTypeEnum.EXCLUDE_TYPE_NONE,
                StartPoint = (panel as Beam).StartPoint,
                EndPoint = (panel as Beam).EndPoint
            };

            ComponentInserter.Insert(data);
        }

        private static Beam CreateTestBeam(Point startPoint, Point endPoint)
        {
            // Create a concrete beam (wall) using provided points
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
