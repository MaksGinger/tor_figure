using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public static class CoordinatesHandling
    {
        private const double ConvertionToRadian = Math.PI / 180;
        private const double ZofCamera=200;//мировой координаты z камеры 
        private const double ZofPlane = 50;//мировой координаты плоскости проецирования

        public static Point3D CreateCoordinatesByFormula(double R, double r, double alphaDegree, double betaDegree)
        {
            double alpha = alphaDegree * ConvertionToRadian;
            double beta = betaDegree * ConvertionToRadian;
            double x = (R + r * Math.Cos(alpha)) * Math.Cos(beta);
            double y = (R + r * Math.Cos(alpha)) * Math.Sin(beta);
            double z = r * Math.Sin(alpha);
            Point3D point = new Point3D(x, y, z);
            return point;
        }

        public static Point3D[,] AxonometricProjection(Point3D[,] coordinates,double alpha,double beta)
        {
            Point3D[,] axonometricCoordinates = new Point3D[coordinates.GetLength(0), coordinates.GetLength(1)];
            for (int i = 0; i < coordinates.GetLength(0); i++)
            {
                for (int j = 0; j < coordinates.GetLength(1); j++)
                {                  
                    double x = coordinates[i, j].X * Math.Cos(alpha)+ coordinates[i, j].Y*Math.Sin(alpha);
                    double y = (-coordinates[i, j].X * Math.Sin(alpha)*Math.Cos(beta))+ (coordinates[i, j].Y * Math.Cos(alpha) * Math.Cos(beta))+(coordinates[i, j].Z * Math.Sin(beta));
                    double z = (coordinates[i, j].X * Math.Sin(alpha) * Math.Sin(beta)) - (coordinates[i, j].Y * Math.Cos(alpha) * Math.Sin(beta)) + (coordinates[i, j].Z * Math.Cos(beta));
                    axonometricCoordinates[i, j] = new Point3D(x, y, z);
                }

            }
            return axonometricCoordinates;
        }

        //Перспективное (центральное) линейное преобразование
        public static Point3D[,] PerspectiveProjection(Point3D[,] coordinates)
        {
            Point3D[,] perspectiveCoordinates=new Point3D[coordinates.GetLength(0),coordinates.GetLength(1)];
            for(int i = 0; i < coordinates.GetLength(0); i++)
            {
                for(int j = 0; j < coordinates.GetLength(1); j++)
                {
                    double coefPerspective = (ZofCamera - ZofPlane) / (ZofCamera - coordinates[i, j].Z);
                    double x = coordinates[i, j].X * coefPerspective;
                    double y = coordinates[i, j].Y * coefPerspective;
                    double z = coordinates[i, j].Z - ZofPlane;
                    perspectiveCoordinates[i, j] = new Point3D(x,y,z);
                }

            }
            return perspectiveCoordinates;
        }

        public static void ScreenProjection(int clientWidth,int clientHeight,
                                                Point3D[,] coordinates,out double k,
                                                   out double dx,out double dy)
        {
            double xMin=0;
            double xMax = 0;
            double yMin = 0;
            double yMax = 0;
            Point3D point;
            
            for(int i = 0; i < coordinates.GetLength(0); i++)
            {
                for(int j = 0; j < coordinates.GetLength(1); j++)
                {
                    point = coordinates[i, j];
                    if (point.X < xMin) xMin = point.X;
                    if (point.X > xMax) xMax = point.X;
                    if (point.Y < yMin) yMin = point.Y;
                    if (point.Y > yMax) yMax = point.Y;
                }
            }
            k = Math.Min((clientWidth - 0) / (xMax - xMin), (clientHeight - 0) / (yMax - yMin));
            dx = (0 - k * xMin + clientWidth - k * xMax) / 2;
            dy = (0 - k * yMin + clientHeight - k * yMax) / 2;
        }

        
        
    }
}
