using System;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace RebarComponentInserter
{
    internal class InserterPluginInputData(Beam wall, Point startPoint, Point endPoint)
    {
        public Beam Wall { get; } = wall ?? throw new ArgumentNullException(nameof(wall));
        public Point StartPoint { get; } = startPoint ?? throw new ArgumentNullException(nameof(startPoint));
        public Point EndPoint { get; } = endPoint ?? throw new ArgumentNullException(nameof(endPoint));
    }
}
