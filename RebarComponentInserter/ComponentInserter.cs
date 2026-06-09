using RebarComponentInserter.Extensions;
using System;
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
            int startIndex = data.SpacingType switch
            {
                SpacingType.NotSet => 0,
                SpacingType.FirstFlexibleExist => 0,
                SpacingType.LastFlexibleExist => 0,
                SpacingType.FirstFlexibleExclude => 1,
                SpacingType.LastFlexibleExclude => 0,
                _ => 0
            };

            int endIndex = data.SpacingType switch
            {
                SpacingType.NotSet => count,
                SpacingType.FirstFlexibleExist => count,
                SpacingType.LastFlexibleExist => count,
                SpacingType.FirstFlexibleExclude => count,
                SpacingType.LastFlexibleExclude => count - 1,
                _ => count
            };

            for (int i = startIndex; i < endIndex; i++)
            {
                Point insertPoint = data.StartPoint.Copy(coordinateSystem.AxisX, data.Spacing * i);
                InsertComponent(insertPoint, coordinateSystem.AxisY, data.ComponentName, data.ComponentNumber);
            }
        }

        private static void InsertComponent(Point insertPoint, Vector direction, string name, int number)
        {
            CustomPart customPart = new()
            {
                Name = name,
                Number = number
            };

            customPart.SetInputPositions(insertPoint, insertPoint.Copy(direction, 100));
            customPart.Insert();

            //Component component = new()
            //{
            //    Name = data.ComponentName,
            //    Number = data.ComponentNumber,
            //};

            //ComponentInput input = new();
            //input.AddInputObject(data.Wall);
            //if (data.ComponentPointsInputType is ComponentPointsInputType.TwoPointsByOnePointInput)
            //{
            //    input.AddOneInputPosition(data.Wall.StartPoint);
            //    input.AddOneInputPosition(data.Wall.EndPoint);
            //}
            //component.SetComponentInput(input);

            //component.Insert();

        }
    }
}
