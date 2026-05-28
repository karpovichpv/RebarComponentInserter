using RebarComponentInserter.Extensions;
using Tekla.Structures.Model;

namespace RebarComponentInserter
{
    internal static class ComponentInserter
    {
        public static void Insert(ComponentInserterData data)
        {
            InsertComponent(data);
        }

        private static void InsertComponent(ComponentInserterData data)
        {
            CustomPart customPart = new()
            {
                Name = data.ComponentName,
                Number = data.ComponentNumber
            };
            customPart.SetInputPositions(data.Wall.StartPoint, data.Wall.StartPoint.Copy(data.Wall.GetCoordinateSystem().AxisY, 200));
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
