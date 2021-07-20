using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class RepresentationOf4Points
    {
        public Point3D Point1 { get; set; }
        public Point3D Point2 { get; set; }
        public Point3D Point3 { get; set; }
        public Point3D Point4 { get; set; }

        public RepresentationOf4Points(Point3D point1,Point3D point2,Point3D point3,Point3D point4)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
            Point4 = point4;
        }
    }
}
