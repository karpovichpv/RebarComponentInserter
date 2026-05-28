using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace RebarComponentInserter.Extensions
{
    public static class DrawExtensions
    {
        public static void DrawPoint(this Point p)
        {
            ControlPoint cp = new(p);
            cp.Insert();
        }
    }
}
