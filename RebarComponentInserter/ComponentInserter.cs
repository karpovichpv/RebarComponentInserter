using System;
using System.Collections.Generic;
using RebarComponentInserter.Data;
using RebarComponentInserter.Extensions;
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
            AttachToWallAssembly(data, components);
            WriteAttributes(data, components);
        }

        private static void WriteAttributes(ComponentInserterData data, List<CustomPart> components)
        {
            foreach (CustomPart part in components)
            {
                SetAttributesToCustomPart(
                    data.Attributes.UdaStringName1,
                    data.Attributes.UdaStringValue1,
                    part
                );
                SetAttributesToCustomPart(
                    data.Attributes.UdaStringName2,
                    data.Attributes.UdaStringValue2,
                    part
                );
                SetAttributesToCustomPart(
                    data.Attributes.UdaDoubleName1,
                    data.Attributes.UdaDoubleValue1,
                    part
                );
                SetAttributesToCustomPart(
                    data.Attributes.UdaDoubleName2,
                    data.Attributes.UdaDoubleValue2,
                    part
                );
            }
        }

        private static void SetAttributesToCustomPart<T>(string name, T value, CustomPart part)
        {
            if (name is not null)
            {
                if (value is string stringValue)
                    part.SetUserProperty(name, stringValue);
                else if (value is double doubleValue)
                    part.SetUserProperty(name, doubleValue);
            }
        }

        private static void AttachToWallAssembly(
            ComponentInserterData data,
            List<CustomPart> components
        )
        {
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

            Point startPoint = data.StartPoint.Copy(axisX, data.OffsetFromStart);
            double currentDistance = 0;
            int totalCount = data.Spacings.Length;
            for (int i = 0; i <= totalCount; i++)
            {
                if (InserterHelpers.CheckIfCreationNeeded(data.SpacingType, totalCount, i))
                {
                    insertedComponents.Add(
                        InsertComponent(startPoint.Copy(axisX, currentDistance), axisY, data)
                    );
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
                RotationOffset = data.RotationAngle + 90,
            };

            double wallWidth = GetReportProperty(data.Wall, "WIDTH");
            if (!string.IsNullOrEmpty(data.RawWidthAttributeName))
                customPart.SetAttribute(data.RawWidthAttributeName, wallWidth);

            double wallHeight = GetReportProperty(data.Wall, "HEIGHT");
            if (!string.IsNullOrEmpty(data.RawHeightAttributeName))
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
