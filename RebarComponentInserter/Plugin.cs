using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using RebarComponentInserter.Extensions;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;

namespace RebarComponentInserter
{
    [Plugin("RebarComponentInserter")]
    [PluginUserInterface("RebarComponentInserter.MainWindow")]
    public class RebarComponentInserterPlugin : PluginBase
    {
        private readonly Model _model;
        private readonly WorkPlaneHandler _workPlaneHandler;
        private readonly TransformationPlane _basicTransformationPlane;
        private PluginData _data;

        public RebarComponentInserterPlugin(PluginData data)
        {
            _data = data;
            Model model = new();
            WorkPlaneHandler workPlaneHandler = model.GetWorkPlaneHandler();

            _workPlaneHandler = workPlaneHandler;
            _basicTransformationPlane = workPlaneHandler.GetCurrentTransformationPlane();
            _model = model;

            // Serialize all PluginData fields to log.txt near the executed dll
            string logPath = Path.Combine(
                Path.GetDirectoryName(this.GetType().Assembly.Location) ?? string.Empty,
                "log.txt"
            );
            File.WriteAllText(
                logPath,
                $"ComponentName: {_data.ComponentName}{Environment.NewLine}"
                    + $"ComponentAttribute: {_data.ComponentAttribute}{Environment.NewLine}"
                    + $"OffsetFromEnd: {_data.OffsetFromEnd}{Environment.NewLine}"
                    + $"SpacingType: {_data.SpacingType}{Environment.NewLine}"
                    + $"Spacing: {_data.Spacing}{Environment.NewLine}"
                    + $"ClassNumber: {_data.ClassNumber}{Environment.NewLine}"
                    + $"ComponentRawWidth: {_data.ComponentRawWidth}{Environment.NewLine}"
                    + $"ComponentRawHeight: {_data.ComponentRawHeight}{Environment.NewLine}"
            );
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
                        ComponentAttributes = "standard",
                        ComponentPointsInputType =
                            ComponentPointsInputType.TwoPointsByOnePointInput,
                        ComponentName = "Каркас_1",
                        ComponentNumber = -1,
                        Wall = wall,
                        StartPoint = wall.StartPoint,
                        EndPoint = wall.EndPoint,
                        Spacing = 200,
                        SpacingType = SpacingType.FirstFlexibleExist,
                    };

                    ComponentInserter.Insert(data);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                _workPlaneHandler.SetCurrentTransformationPlane(_basicTransformationPlane);
            }

            return false;
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
    }
}
