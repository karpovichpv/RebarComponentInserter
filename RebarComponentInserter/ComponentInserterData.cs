using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using static Tekla.Structures.Model.RebarSpacing;

namespace RebarComponentInserter
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
    }

    internal class ComponentInserterData
    {
        public Beam Wall { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public string ComponentName { get; set; }
        public string ComponentAttributeName { get; set; }

        /// <summary>
        /// Шаг каркасов
        /// </summary>
        public double Spacing { get; set; }

        /// <summary>
        /// Тип шага каркаса
        /// </summary>
        public SpacingType SpacingType { get; set; }

        /// <summary>
        /// Имя атрибута каркаса компонента,
        /// в который будет записана ширина панели
        /// </summary>
        public string RawWidthAttributeName { get; internal set; }

        /// <summary>
        /// Имя атрибута каркаса компонента,
        /// в который будет записана высота панели
        /// </summary>
        public string RawHeightAttributeName { get; internal set; }

        /// <summary>
        /// Отступ каркасов от начала и конца панели
        /// </summary>
        public double OffsetFromEnd { get; internal set; }

        /// <summary>
        /// Класс детали, который будет включен в сборку панели
        /// </summary>
        public int PartToAssemblyClass { get; internal set; }
    }
}
