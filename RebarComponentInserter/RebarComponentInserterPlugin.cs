using System;
using System.Collections.Generic;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;

namespace RebarComponentInserter
{
    [Plugin("pk_RebarComponentInserter")]
    [PluginUserInterface("RebarComponentInserter.MainWindow")]
    public class RebarComponentInserterPlugin(PluginData data) : InserterPluginBaseClass(data)
    {
        public override List<InputDefinition> DefineInput()
        {
            Picker picker = new();
            ModelObject modelObject = picker.PickObject(
                Picker.PickObjectEnum.PICK_ONE_PART,
                "Select a wall"
            );
            return [new InputDefinition(modelObject.Identifier)];
        }

        private protected override InserterPluginInputData GetPluginInputData(
            List<InputDefinition> input
        )
        {
            Identifier firstElement = input[0].GetInput() as Identifier;
            ModelObject selectedModelObject = _model.SelectModelObject(firstElement);

            if (selectedModelObject is Beam wall)
                return new InserterPluginInputData(wall, wall.StartPoint, wall.EndPoint);

            throw new ArgumentNullException("Первым элементов был выбран не балка");
        }
    }
}
