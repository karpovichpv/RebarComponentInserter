using System;
using RebarComponentInserter.Extensions;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace RebarComponentInserter
{
    public static class TestClass
    {
        public static void Create()
        {
            // Create walls based on SpacingType enum values count
            int wallCount = Enum.GetValues(typeof(SpacingType)).Length;
            const double yOffset = 1000; // 1 meter offset

            for (int i = 0; i < wallCount; i++)
            {
                double y = i * yOffset;
                Point startPoint = new Point(0, y, 0);
                Point endPoint = new Point(5000, y, 0); // 5m x 3m wall
                SpacingType spacingType = (SpacingType)i;

                CreateWall(startPoint, endPoint, spacingType);
            }

            new Model().CommitChanges();
        }

        private static void CreateWall(Point startPoint, Point endPoint, SpacingType spacingType)
        {
            Beam beam = CreateTestBeam(startPoint, endPoint);

            ComponentInserterData data = new()
            {
                ComponentAttributeName = "standard",
                ComponentName = "Каркас_1",
                Wall = beam,
                StartPoint = startPoint,
                EndPoint = endPoint,
                Spacing = 200,
                SpacingType = spacingType,
            };

            ComponentInserter.Insert(data);
        }

        private static Beam CreateTestBeam(Point startPoint, Point endPoint)
        {
            // Create a concrete beam (wall) using provided points
            Beam beam = new Beam(startPoint, endPoint)
            {
                Profile = { ProfileString = "3000*300" }, // Rectangular profile
                Material = { MaterialString = "Concrete_Undefined" }, // Concrete material
                Name = "ConcreteWall",
                Class = "1",
                Finish = "PAINTED",
                PartNumber = { Prefix = "W" },
                Position = { Depth = Position.DepthEnum.FRONT, Plane = Position.PlaneEnum.MIDDLE },
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
