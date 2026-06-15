using RebarComponentInserter.Extensions;
using System;
using System.Collections.Generic;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

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
            double distance = data.StartPoint.GetDistance(data.EndPoint) - data.OffsetFromEnd * 2;
            int count = (int)Math.Ceiling(distance / data.Spacing);
            double flexibleSpacing = distance - data.Spacing * (count - 1);

            Vector axisX = data.Wall.GetCoordinateSystem().AxisX;
            Vector axisY = data.Wall.GetCoordinateSystem().AxisY;

            Point startPoint = data.StartPoint.Copy(axisX, data.OffsetFromEnd);
            double currentDistance = data.Spacing;
            for (int i = 0; i <= count; i++)
            {
                if (i == 0)
                    currentDistance = 0;
                else if (CheckIfNeedToAddFlexibleSpacing(data, count, i))
                    currentDistance += flexibleSpacing;
                else
                    currentDistance += data.Spacing;

                if (CheckIfCreationNeeded(data, count, i))
                    continue;

                insertedComponents.Add(
                    InsertComponent(startPoint.Copy(axisX, currentDistance), axisY, data)
                );
            }
            return insertedComponents;
        }

        private static bool CheckIfNeedToAddFlexibleSpacing(
            ComponentInserterData data,
            int count,
            int i
        )
        {
            bool isFirstFlexible =
                i == 1
                && (
                    data.SpacingType == SpacingType.FirstFlexibleExist
                    || data.SpacingType == SpacingType.FirstFlexibleExclude
                );
            bool isLastFlexible =
                i == count
                && (
                    data.SpacingType == SpacingType.LastFlexibleExclude
                    || data.SpacingType == SpacingType.LastFlexibleExist
                );
            return isFirstFlexible || isLastFlexible;
        }

        private static bool CheckIfCreationNeeded(ComponentInserterData data, int count, int i)
        {
            bool isFirstExcluded = data.SpacingType == SpacingType.FirstFlexibleExclude && i == 0;
            bool isLastExcluded = data.SpacingType == SpacingType.LastFlexibleExclude && i == count;
            return isFirstExcluded || isLastExcluded;
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
            double wallHeight = GetReportProperty(data.Wall, "HEIGHT");

            customPart.SetAttribute(data.RawWidthAttributeName, wallWidth);
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
