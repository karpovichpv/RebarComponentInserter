using Tekla.Structures.Geometry3d;

namespace RebarComponentInserter.Extensions
{
    internal static class PointExtensions
    {
        /// <summary>
        /// Возвращает копию точки по выбранному направлению на выбранном расстоянии
        /// </summary>
        /// <param name="p">Точка</param>
        /// <param name="dir">Направление</param>
        /// <param name="distance">Расстояниe</param>
        /// <returns>Точка</returns>
        public static Point Copy(this Point p, Vector dir, double distance)
            => p + (dir.GetNormal() * distance);
    }
}
