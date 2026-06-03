using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using static Tekla.Structures.Model.RebarSpacing;

namespace RebarComponentInserter
{
    internal enum SpacingType
    {
        NotSet,

        /// <summary>
        /// First flexible spacing and first component exist
        /// </summary>
        FirstFlexibleExist,

        /// <summary>
        /// Last flexible spacing and first component exist
        /// </summary>
        LastFlexibleExist,


        /// <summary>
        /// First flexible spacing and first component exclude
        /// </summary>
        FirstFlexibleExclude,

        /// <summary>
        /// First flexible spacing and last component exclude
        /// </summary>
        LastFlexibleExclude,
    }

    internal class ComponentInserterData
    {
        public Beam Wall { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public string ComponentName { get; set; }
        public string ComponentAttributes { get; set; }

        /// <summary>
        /// Количество каркасов по длине
        /// </summary>
        public int ComponentNumber { get; set; }

        public ComponentPointsInputType ComponentPointsInputType { get; set; }
        public double Spacing { get; set; }
        public SpacingType SpacingType { get; set; }
    }
}