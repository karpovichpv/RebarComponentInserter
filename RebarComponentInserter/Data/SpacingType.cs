using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Distance = Tekla.Structures.Datatype.Distance;

namespace RebarComponentInserter.Data
{
    internal enum SpacingType
    {
        /// <summary>
        /// Not set
        /// </summary>
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

        /// <summary>
        /// Spacing list 
        /// </summary>
        DistanceList,
    }
}
