using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public static class AthenTransformations
    {
        private const double ConvertionToRadian = Math.PI / 180;


        //для поворота
        private static Point3D RotateX(Point3D coordinate,double sinAngle,double cosAngle)
        {
            double yChanged = (coordinate.Y * cosAngle) + (coordinate.Z * sinAngle);
            double zChanged = (-coordinate.Y * sinAngle) + (coordinate.Z * cosAngle);
            Point3D pointChanged = new Point3D(coordinate.X, yChanged, zChanged);
            return pointChanged;
        }

        private static Point3D RotateY(Point3D coordinate, double sinAngle, double cosAngle)
        {
            double xChanged = (coordinate.X * cosAngle) + (-coordinate.Z * sinAngle);
            double zChanged = (coordinate.X * sinAngle) + (coordinate.Z * cosAngle);
            Point3D pointChanged = new Point3D(xChanged, coordinate.Y, zChanged);
            return pointChanged;
        }

        private static Point3D RotateZ(Point3D coordinate, double sinAngle, double cosAngle)
        {
            double xChanged = (coordinate.X * cosAngle) + (coordinate.Y * sinAngle);
            double yChanged = (-coordinate.X * sinAngle) + (coordinate.Y * cosAngle);
            Point3D pointChanged = new Point3D(xChanged, yChanged, coordinate.Z);
            return pointChanged;
        }

        public static Point3D[,] RotateXcoordinates(Point3D[,] coordinates,double angle)
        {
            double radianAngle = ConvertionToRadian * angle;
            double cosAngle = Math.Cos(radianAngle);
            double sinAngle = Math.Sin(radianAngle);
            Point3D[,] transformedCoordinates=new Point3D[coordinates.GetLength(0),coordinates.GetLength(1)];
            for (int i = 0; i < coordinates.GetLength(0); i++)
            {
                for(int j = 0; j < coordinates.GetLength(1); j++)
                {
                    transformedCoordinates[i, j] = RotateX(coordinates[i, j], sinAngle, cosAngle);
                }
            }
            return transformedCoordinates;
        }

        public static Point3D[,] RotateYcoordinates(Point3D[,] coordinates, double angle)
        {
            double radianAngle = ConvertionToRadian * angle;
            double cosAngle = Math.Cos(radianAngle);
            double sinAngle = Math.Sin(radianAngle);
            Point3D[,] transformedCoordinates = new Point3D[coordinates.GetLength(0), coordinates.GetLength(1)];
            for (int i = 0; i < coordinates.GetLength(0); i++)
            {
                for (int j = 0; j < coordinates.GetLength(1); j++)
                {
                    transformedCoordinates[i, j] = RotateY(coordinates[i, j], sinAngle, cosAngle);
                }
            }
            return transformedCoordinates;
        }

        public static Point3D[,] RotateZcoordinates(Point3D[,] coordinates, double angle)
        {
            double radianAngle = ConvertionToRadian * angle;
            double cosAngle = Math.Cos(radianAngle);
            double sinAngle = Math.Sin(radianAngle);
            Point3D[,] transformedCoordinates = new Point3D[coordinates.GetLength(0), coordinates.GetLength(1)];
            for (int i = 0; i < coordinates.GetLength(0); i++)
            {
                for (int j = 0; j < coordinates.GetLength(1); j++)
                {
                    transformedCoordinates[i, j] = RotateZ(coordinates[i, j], sinAngle, cosAngle);
                }
            }
            return transformedCoordinates;
        }
    }
}
