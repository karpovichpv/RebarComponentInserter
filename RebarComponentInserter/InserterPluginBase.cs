using System;
using System.Collections.Generic;
using System.IO;
using RebarComponentInserter.Data;
using RebarComponentInserter.Extensions;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Plugins;

namespace RebarComponentInserter
{
    public abstract class InserterPluginBaseClass : PluginBase
    {
        private protected readonly Model _model;

        private readonly WorkPlaneHandler _workPlaneHandler;
        private readonly TransformationPlane _basicTransformationPlane;
        private readonly PluginData _data;

        private System.Reflection.Assembly PluginAssembly => GetType().Assembly;

        public InserterPluginBaseClass(PluginData data)
        {
            _data = data;
            CheckAndFixData(data);
            Model model = new();
            WorkPlaneHandler workPlaneHandler = model.GetWorkPlaneHandler();

            _workPlaneHandler = workPlaneHandler;
            _basicTransformationPlane = workPlaneHandler.GetCurrentTransformationPlane();
            _model = model;
        }

        private protected abstract InserterPluginInputData GetPluginInputData(
            List<InputDefinition> input
        );

        public override bool Run(List<InputDefinition> input)
        {
            try
            {
                CheckIfCanInsertComponents();

                if (input.Count == 0)
                    return false;

                InserterPluginInputData pluginData = GetPluginInputData(input);
                Beam wall = pluginData.Wall;
                Point startPoint = pluginData.StartPoint;
                Point endPoint = pluginData.EndPoint;

                var wallPlane = GetWallPlane(wall);
                _workPlaneHandler.SetCurrentTransformationPlane(wallPlane);
                Point startPointTransformed = wallPlane.TransformationMatrixToLocal.Transform(
                    _basicTransformationPlane.TransformationMatrixToGlobal.Transform(
                        pluginData.StartPoint
                    )
                );
                Point endPointTransformed = wallPlane.TransformationMatrixToLocal.Transform(
                    _basicTransformationPlane.TransformationMatrixToGlobal.Transform(
                        pluginData.EndPoint
                    )
                );

                SpacingType spacingType = InserterHelpers.ConvertSpacingType(_data.SpacingType);

                ComponentInserterData data = new()
                {
                    ComponentAttributeName = _data.ComponentAttribute,
                    ComponentName = _data.ComponentName,
                    Wall = wall,
                    StartPoint = startPointTransformed,
                    EndPoint = endPointTransformed,
                    Spacings = InserterHelpers.ConvertToDistanceList(
                        _data,
                        startPointTransformed,
                        endPointTransformed
                    ),
                    SpacingType = spacingType,
                    RawWidthAttributeName = _data.ComponentRawWidth,
                    RawHeightAttributeName = _data.ComponentRawHeight,
                    OffsetFromEnd = _data.OffsetFromEnd,
                    RotationAngle = _data.ComponentRotationAngle,
                    OffsetFromStart = _data.OffsetFromStart,
                    PartToAssemblyClass = _data.ClassNumber,
                    AddPartToAssembly = _data.AddPartToAssembly == 1,
                    Attributes = new AttributesData
                    {
                        UdaStringName1 = _data.UdaStringName1,
                        UdaStringName2 = _data.UdaStringName2,
                        UdaStringValue1 = _data.UdaStringValue1,
                        UdaStringValue2 = _data.UdaStringValue2,
                        UdaDoubleName1 = _data.UdaDoubleName1,
                        UdaDoubleName2 = _data.UdaDoubleName2,
                        UdaDoubleValue1 = _data.UdaDoubleValue1,
                        UdaDoubleValue2 = _data.UdaDoubleValue2,
                    },
                };

                ComponentInserter.Insert(data);

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"Возникла ошибка выполнения плагина! \r\n Message: {ex.Message}"
                );
                string logPath = Path.Combine(
                    Path.GetDirectoryName(PluginAssembly.Location) ?? string.Empty,
                    "log.txt"
                );

                File.WriteAllText(logPath, ex.ToString());
            }
            finally
            {
                _workPlaneHandler.SetCurrentTransformationPlane(_basicTransformationPlane);
            }

            return false;
        }

        private void CheckIfCanInsertComponents()
        {
            CatalogHandler catalogHandler = new();
            ComponentItemEnumerator enumerator = catalogHandler.GetComponentItems();
            bool ifExist = false;
            while (enumerator.MoveNext())
            {
                ComponentItem item = enumerator.Current;
                if (item.Number == -1 && item.Name == _data.ComponentName)
                {
                    ifExist = true;
                    break;
                }
            }
            if (!ifExist)
            {
                throw new Exception(
                    $"В каталоге компонентов не обнаружено компонента детали с именем \"{_data.ComponentName}\""
                );
            }
        }

        private static TransformationPlane GetWallPlane(Beam wall)
        {
            CoordinateSystem wallCs = wall.GetCoordinateSystem();

            double height = 0;
            bool canGetHeight = wall.GetReportProperty("HEIGHT", ref height);
            if (canGetHeight)
            {
                CoordinateSystem insertionCs = new(
                    wallCs.Origin.Copy(wallCs.AxisY * -1, height / 2),
                    wallCs.AxisX,
                    wallCs.AxisY
                );
                return new TransformationPlane(insertionCs);
            }

            throw new NotImplementedException("Cannot calculate the height of the panel");
        }

        private void CheckAndFixData(PluginData data)
        {
            if (IsDefaultValue(data.AddPartToAssembly))
                data.AddPartToAssembly = 0;
            if (IsDefaultValue(data.OffsetFromEnd))
                data.OffsetFromEnd = 0;
            if (IsDefaultValue(data.SpacingType))
                data.SpacingType = 0;
            if (IsDefaultValue(data.ClassNumber))
                data.ClassNumber = 0;
            if (IsDefaultValue(data.UdaDoubleValue1))
                data.UdaDoubleValue1 = 0;
            if (IsDefaultValue(data.UdaDoubleValue2))
                data.UdaDoubleValue2 = 0;
            if (IsDefaultValue(data.ComponentRotationAngle))
                data.ComponentRotationAngle = 0;
            if (IsDefaultValue(data.OffsetFromStart))
                data.OffsetFromStart = 0;
        }
    }
}
