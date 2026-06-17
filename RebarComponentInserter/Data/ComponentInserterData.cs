using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Distance = Tekla.Structures.Datatype.Distance;

namespace RebarComponentInserter.Data
{
    internal class ComponentInserterData
    {
        /// <summary>
        /// Стена
        /// </summary>
        public Beam Wall { get; set; }

        /// <summary>
        /// Начальная точка раскладки каркасов
        /// </summary>
        public Point StartPoint { get; set; }

        /// <summary>
        /// Конечная точка раскладки каркасов
        /// </summary>
        public Point EndPoint { get; set; }

        /// <summary>
        /// Имя компонента, который будет вставлен в стену
        /// </summary>
        public string ComponentName { get; set; }

        /// <summary>
        /// Имя атрибута компонента
        /// </summary>
        public string ComponentAttributeName { get; set; }

        /// <summary>
        /// Список шагов каркаса, в которых будут вставлены компоненты
        /// </summary>
        public Distance[] Spacings { get; set; }

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

        /// <summary>
        /// Добавлять деталь из каркаса в сборку панели
        /// </summary>
        public bool AddPartToAssembly { get; internal set; }

        /// <summary>
        /// Данные атрибутов,
        /// которые будут записаны в каркас компонента
        /// </summary>
        public AttributesData Attributes { get; internal set; }
    }
}
