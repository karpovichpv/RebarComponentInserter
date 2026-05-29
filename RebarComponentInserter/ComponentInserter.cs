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
            for (int i = 0; i < count; i++)
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
