using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class Point3D
    {
        public double X;
        public double Y;
        public double Z;

        public Point3D(int x,int y,int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(double x,double y,double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(float x,float y,float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D() { }
    }
}
