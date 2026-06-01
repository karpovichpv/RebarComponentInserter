using System;
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

        /// <summary>
        /// Вычисляет расстояние между двумя точками
        /// </summary>
        /// <param name="p1">Первая точка</param>
        /// <param name="p2">Первая точка</param>
        /// <returns>Расстояние между двумя точками</returns>
        public static double GetDistance(this Point p1, Point p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double dz = p2.Z - p1.Z;

            return Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
        }
    }
}
