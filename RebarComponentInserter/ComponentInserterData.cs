using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using static Tekla.Structures.Model.RebarSpacing;

namespace RebarComponentInserter
{
    internal class ComponentInserterData
    {
        public Beam Wall { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public string ComponentName { get; set; }
        public string ComponentAttributes { get; set; }
        public int ComponentNumber { get; set; }
        public ComponentPointsInputType ComponentPointsInputType { get; set; }
        public double Spacing { get; set; }
        public SpacingType SpacingType { get; set; }
        public ExcludeTypeEnum ExcludeType { get; set; }
    }
}