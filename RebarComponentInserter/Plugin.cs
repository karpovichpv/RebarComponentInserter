using RebarComponentInserter.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Tekla.Structures;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Datatype;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;
using Distance = Tekla.Structures.Datatype.Distance;

namespace RebarComponentInserter
{
    [Plugin("pk_RebarComponentInserter")]
    [PluginUserInterface("RebarComponentInserter.MainWindow")]
    public class RebarComponentInserterPlugin : PluginBase
    {
        private readonly Model _model;
        private readonly WorkPlaneHandler _workPlaneHandler;
        private readonly TransformationPlane _basicTransformationPlane;
        private readonly PluginData _data;

        private System.Reflection.Assembly PluginAssembly => GetType().Assembly;

        public RebarComponentInserterPlugin(PluginData data)
        {
            _data = data;
            Model model = new();
            WorkPlaneHandler workPlaneHandler = model.GetWorkPlaneHandler();

            _workPlaneHandler = workPlaneHandler;
            _basicTransformationPlane = workPlaneHandler.GetCurrentTransformationPlane();
            _model = model;
        }

        public override List<InputDefinition> DefineInput()
        {
            Picker picker = new();
            ModelObject modelObject = picker.PickObject(
                Picker.PickObjectEnum.PICK_ONE_PART,
                "Select a wall"
            );
            return [new InputDefinition(modelObject.Identifier)];
        }

        public override bool Run(List<InputDefinition> input)
        {
            try
            {
                CheckIfCanInsertComponents();

                if (input.Count == 0)
                    return false;

                Identifier firstElement = input[0].GetInput() as Identifier;
                ModelObject selectedModelObject = _model.SelectModelObject(firstElement);

                if (selectedModelObject is Beam wall)
                {
                    var wallPlane = GetWallPlane(wall);
                    _workPlaneHandler.SetCurrentTransformationPlane(wallPlane);

                    ComponentInserterData data = new()
                    {
                        ComponentAttributeName = _data.ComponentAttribute,
                        ComponentName = _data.ComponentName,
                        Wall = wall,
                        StartPoint = wall.StartPoint,
                        EndPoint = wall.EndPoint,
                        Spacing = ConvertToDistanceList(),
                        SpacingType = ConvertSpacingType(),
                        RawWidthAttributeName = _data.ComponentRawWidth,
                        RawHeightAttributeName = _data.ComponentRawHeight,
                        OffsetFromEnd = _data.OffsetFromEnd,
                        PartToAssemblyClass = _data.ClassNumber,
                        AddPartToAssembly = _data.AddPartToAssembly == 1,
                    };

                    ComponentInserter.Insert(data);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка выполнения плагина. Message: {ex.Message}");
                string logPath = Path.Combine(
                    Path.GetDirectoryName(PluginAssembly.Location) ?? string.Empty,
                    "log.txt"
                );

                File.WriteAllText(logPath, $"{ex.ToString()}");
            }
            finally
            {
                _workPlaneHandler.SetCurrentTransformationPlane(_basicTransformationPlane);
            }

            return false;
        }

        private Distance[] ConvertToDistanceList() => [.. DistanceList.Parse(_data.Spacing)];

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
            if (ifExist == false)
                throw new Exception(
                    $"В каталоге компонентов не обнаружено компонента детали с именем {_data.ComponentName}"
                );
        }

        private SpacingType ConvertSpacingType()
        {
            return _data.SpacingType switch
            {
                0 => SpacingType.FirstFlexibleExist,
                1 => SpacingType.LastFlexibleExist,
                2 => SpacingType.FirstFlexibleExclude,
                3 => SpacingType.LastFlexibleExclude,
                4 => SpacingType.DistanceList,
                _ => throw new NotImplementedException("Тип раскладки шага не поддерживается"),
            };
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

        private static SpacingType ConvertSpacingType(int spacingType)
        {
            return (SpacingType)spacingType;
        }
    }
}
