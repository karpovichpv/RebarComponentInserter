using System;
using RebarComponentInserter.Extensions;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace RebarComponentInserter
{
    internal static class ComponentInserter
    {
        public static void Insert(ComponentInserterData data)
        {
            double distance = data.StartPoint.GetDistance(data.EndPoint);
            int count = (int)Math.Floor(distance / data.Spacing);

            CoordinateSystem coordinateSystem = data.Wall.GetCoordinateSystem();

            // Handle SpacingType for insert point calculation
            int startIndex = GetStartIndex(data);
            int endIndex = GetEndIndex(data, count);

            for (int i = startIndex; i < endIndex; i++)
            {
                Point insertPoint = data.StartPoint.Copy(coordinateSystem.AxisX, data.Spacing * i);
                InsertComponent(insertPoint, coordinateSystem.AxisY, data);
            }
        }

        private static int GetStartIndex(ComponentInserterData data)
        {
            return data.SpacingType switch
            {
                SpacingType.NotSet => 0,
                SpacingType.FirstFlexibleExist => 0,
                SpacingType.LastFlexibleExist => 0,
                SpacingType.FirstFlexibleExclude => 1,
                SpacingType.LastFlexibleExclude => 0,
                _ => 0,
            };
        }

        private static int GetEndIndex(ComponentInserterData data, int count)
        {
            return data.SpacingType switch
            {
                SpacingType.NotSet => count,
                SpacingType.FirstFlexibleExist => count,
                SpacingType.LastFlexibleExist => count,
                SpacingType.FirstFlexibleExclude => count,
                SpacingType.LastFlexibleExclude => count - 1,
                _ => count,
            };
        }

        private static void InsertComponent(
            Point insertPoint,
            Vector direction,
            ComponentInserterData data
        )
        {
            CustomPart customPart = new() { Name = data.ComponentName, Number = -1 };

            customPart.SetInputPositions(insertPoint, insertPoint.Copy(direction, 100));
            customPart.Insert();
            customPart.Position = new()
            {
                Plane = Position.PlaneEnum.MIDDLE,
                Depth = Position.DepthEnum.MIDDLE,
                DepthOffset = 0,
                PlaneOffset = 0,
                Rotation = Position.RotationEnum.BELOW,
                RotationOffset = 90,
            };

            double wallWidth = GetReportProperty(data.Wall, "WIDTH");
            double wallHeight = GetReportProperty(data.Wall, "HEIGHT");

            customPart.SetAttribute(data.RawWidthAttributeName, wallWidth);
            customPart.SetAttribute(data.RawHeightAttributeName, wallHeight);

            customPart.Modify();
        }

        public static double GetReportProperty(Beam wall, string propertyName)
        {
            double value = 0;
            bool canGet = wall.GetReportProperty(propertyName, ref value);
            if (canGet)
                return value;

            throw new NotImplementedException(
                $"Невозможно получить ReportProperty {propertyName} из объекта {wall.Identifier.ID}"
            );
        }
    }
}
