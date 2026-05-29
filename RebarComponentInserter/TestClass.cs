using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace RebarComponentInserter
{
    public static class TestClass
    {
        public static void Create()
        {
            Picker picker = new();
            ModelObject panel = picker.PickObject(Picker.PickObjectEnum.PICK_ONE_PART);

            ComponentInserterData data = new()
            {
                ComponentAttributes = "standard",
                ComponentPointsInputType = ComponentPointsInputType.TwoPointsByOnePointInput,
                ComponentName = "Каркас_1",
                ComponentNumber = -1,
                Wall = panel as Beam,
                Spacing = 200,
                SpacingType = RebarSpacing.SpacingType.EXACT_FLEXIBLE_FIRST,
                ExcludeType = RebarSpacing.ExcludeTypeEnum.EXCLUDE_TYPE_NONE,
                StartPoint = (panel as Beam).StartPoint,
                EndPoint = (panel as Beam).EndPoint
            };

            ComponentInserter.Insert(data);
        }
    }
}
