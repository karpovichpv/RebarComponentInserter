using RebarComponentInserter.Extensions;
using System;
using System.Collections.Generic;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Distance = Tekla.Structures.Datatype.Distance;

namespace RebarComponentInserter
{
    internal static class ComponentInserter
    {
        public static void Insert(ComponentInserterData data)
        {
            List<CustomPart> components = CreateComponents(data);
            List<Part> targetParts = GetTargetPartsForAttachingThemToAssembly(data, components);

            Assembly mainAssembly = data.Wall.GetAssembly();
            foreach (Part part in targetParts)
            {
                mainAssembly.Remove(part);
                mainAssembly.Modify();
                if (data.AddPartToAssembly)
                {
                    mainAssembly.Add(part);
                    mainAssembly.Modify();
                }
            }
        }

        private static List<Part> GetTargetPartsForAttachingThemToAssembly(
            ComponentInserterData data,
            List<CustomPart> components
        )
        {
            List<Part> targetParts = [];
            foreach (CustomPart customPart in components)
            {
                foreach (ModelObject modelObject in customPart.GetChildren())
                {
                    if (modelObject is Part part && part.Class == $"{data.PartToAssemblyClass}")
                        targetParts.Add(part);
                }
            }

            return targetParts;
        }

        private static List<CustomPart> CreateComponents(ComponentInserterData data)
        {
            List<CustomPart> insertedComponents = [];

            Vector axisX = data.Wall.GetCoordinateSystem().AxisX;
            Vector axisY = data.Wall.GetCoordinateSystem().AxisY;

            Point startPoint = data.StartPoint.Copy(axisX, data.OffsetFromEnd);
            double currentDistance = 0;
            int totalCount = data.Spacings.Length;
            for (int i = 0; i <= totalCount; i++)
            {
                if (InserterHelpers.CheckIfCreationNeeded(data.SpacingType, totalCount, i))
                {
                    insertedComponents.Add(
                        InsertComponent(startPoint.Copy(axisX, currentDistance), axisY, data));
                }

                if (i < totalCount)
                {
                    Distance spacing = data.Spacings[i];
                    currentDistance += spacing.Value;
                }
            }

            return insertedComponents;
        }

        private static CustomPart InsertComponent(
            Point insertPoint,
            Vector direction,
            ComponentInserterData data
        )
        {
            CustomPart customPart = new() { Name = data.ComponentName, Number = -1 };

            customPart.SetInputPositions(insertPoint, insertPoint.Copy(direction, 100));
            customPart.LoadAttributesFromFile(data.ComponentAttributeName);
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
            if (string.IsNullOrEmpty(data.RawWidthAttributeName))
                customPart.SetAttribute(data.RawWidthAttributeName, wallWidth);

            double wallHeight = GetReportProperty(data.Wall, "HEIGHT");
            if (string.IsNullOrEmpty(data.RawHeightAttributeName))
                customPart.SetAttribute(data.RawHeightAttributeName, wallHeight);

            customPart.Modify();

            return customPart;
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
