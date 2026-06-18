using RebarComponentInserter.Extensions;
using System;
using System.Collections.Generic;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;

namespace RebarComponentInserter
{
    [Plugin("pk_RebarComponentInserterByTwoPoints")]
    [PluginUserInterface("RebarComponentInserter.MainWindow")]
    public class RebarComponentInserterPluginByTwoPoints(PluginData data)
        : InserterPluginBaseClass(data)
    {
        public override List<InputDefinition> DefineInput()
        {
            Picker picker = new();
            ModelObject modelObject = picker.PickObject(
                Picker.PickObjectEnum.PICK_ONE_PART,
                "Select a wall"
            );

            Point firstPoint = picker.PickPoint("First point");
            Point secondPoint = picker.PickPoint("First point");
            return [new InputDefinition(modelObject.Identifier), new InputDefinition(firstPoint), new InputDefinition(secondPoint)];
        }

        private protected override InserterPluginInputData GetPluginInputData(List<InputDefinition> input)
        {
            Identifier firstElement = input[0].GetInput() as Identifier;
            ModelObject selectedModelObject = _model.SelectModelObject(firstElement);

            object pointObject1 = input[1].GetInput();
            object pointObject2 = input[2].GetInput();

            if (selectedModelObject is Beam wall
                && pointObject1 is Point p1
                && pointObject2 is Point p2)
            {
                double height = 0;
                bool canGetHeight = wall.GetReportProperty("HEIGHT", ref height);

                Line centerLine = new(wall.GetCoordinateSystem().Origin.Copy(wall.GetCoordinateSystem().AxisY * -1, height / 2), wall.GetCoordinateSystem().AxisX);
                Point startPoint = Projection.PointToLine(p1, centerLine);
                Point endPoint = Projection.PointToLine(p2, centerLine);
                return new InserterPluginInputData(wall, startPoint, endPoint);
            }

            throw new ArgumentNullException("Первым элементов был выбран не балка");
        }
    }
}
