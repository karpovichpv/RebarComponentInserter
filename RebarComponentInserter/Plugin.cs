
using System.Collections.Generic;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;

namespace RebarComponentInserter
{
    [Plugin("RebarComponentInserter")]
 [PluginUserInterface("RebarComponentInserter.MainWindow")]
    public class RebarComponentInserter : Tekla.Structures.Plugins.PluginBase
    {
        private Model _model = new Model();

        public override List<InputDefinition> DefineInput()
        {
            Picker picker = new();
            ModelObject modelObject = picker.PickObject(Picker.PickObjectEnum.PICK_ONE_PART, "Select a wall");
            List<InputDefinition> result = [new InputDefinition(modelObject.Identifier)];
            return result;
        }

        public override bool Run(List<InputDefinition> input)
        {
            if (input.Count == 0)
                return false;

            Identifier firstElement = input[0].GetInput() as Identifier;

            ModelObject selectedModelObject = _model.SelectModelObject(firstElement);
            if (selectedModelObject is Beam wall)
            {
                ComponentInserterData data = new()
                {
                    ComponentAttributes = "standard",
                    ComponentPointsInputType = ComponentPointsInputType.TwoPointsByOnePointInput,
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
            return false;
        }
    }
}