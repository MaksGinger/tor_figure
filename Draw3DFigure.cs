using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class Draw3DFigure
    {
        private Pen pen = new Pen(Color.Red, 1);
        private const double RadiusOfRotation = 150;//R
        private const double RadiusOfSection = 60;//r
        private const double Alpha = (Math.PI / 2);
        private const double Beta = (2*Math.PI);

        private const int N = 50;
        private const int Count = 50;
        private readonly Point3D[,] coordinatesMatrix;

        public double dx;
        private double dy;
        private double k;

        private double kScaling = 0;
        public double KScaling { 
            get 
            {
                return kScaling;
            }
            set 
            {
                kScaling = value;
            } 
        }
        public bool IsScaling { get; set; }

        public double AngleOfRotation { get; set; }

        public enum Rotation
        {
            NoRotation,
            XRotation,
            YRotation,
            ZRotation
        }

        public Draw3DFigure()
        {
            coordinatesMatrix = new Point3D[N, Count];
            FullFillCoordinatesMatrix();
        }


        private void FullFillCoordinatesMatrix()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    double alpha = (i * 360) / (N - 1);
                    double beta = (j * 360) / (Count - 1);
                    coordinatesMatrix[i, j] = CoordinatesHandling.CreateCoordinatesByFormula(RadiusOfRotation, RadiusOfSection,
                        alpha, beta);
                }
            }
        }

        public Bitmap DrawFigure(int clientWidth,int clientHeight,Rotation rotationType)
        {
            Bitmap bitmapWithFigure = new Bitmap(clientWidth, clientHeight);
            Graphics graphics = Graphics.FromImage(bitmapWithFigure);

            Point3D[,] coordinates = coordinatesMatrix;
            Point3D[,] axonometricCoordinates = CoordinatesHandling.AxonometricProjection(coordinates, Alpha, Beta);
            Point3D[,] perspectiveCoordinates = CoordinatesHandling.PerspectiveProjection(axonometricCoordinates);

            switch (rotationType)
            {
                case Rotation.NoRotation:
                    break;
                case Rotation.XRotation:
                    perspectiveCoordinates = AthenTransformations.RotateXcoordinates(perspectiveCoordinates, AngleOfRotation);
                    break;
                case Rotation.YRotation:
                    perspectiveCoordinates = AthenTransformations.RotateYcoordinates(perspectiveCoordinates, AngleOfRotation);
                    break;
                case Rotation.ZRotation:
                    perspectiveCoordinates = AthenTransformations.RotateZcoordinates(perspectiveCoordinates,AngleOfRotation);
                    break;
                default:
                    break;
            }
            CoordinatesHandling.ScreenProjection(clientWidth, clientHeight, perspectiveCoordinates,
                                                out k, out dx, out dy);
                       

            List<RepresentationOf4Points> coordinatesOfPolygon = new List<RepresentationOf4Points>();
            for (int i = 0; i < N - 1; i++)
            {
                for(int j = 0; j < Count - 1; j++)
                {
                    coordinatesOfPolygon.Add(new RepresentationOf4Points
                        (
                        perspectiveCoordinates[i, j],
                        perspectiveCoordinates[i, j + 1],
                        perspectiveCoordinates[i + 1, j],
                        perspectiveCoordinates[i + 1, j + 1]
                        ));
                }
            }

            Point[] points2D = new Point[4];
            for(int i = 0; i < coordinatesOfPolygon.Count(); i++)
            {
                if (!IsScaling)
                {
                    points2D[0].X = (int)(k * coordinatesOfPolygon[i].Point1.X + dx);
                    points2D[0].Y = (int)(k * coordinatesOfPolygon[i].Point1.Y + dy);
                    points2D[1].X = (int)(k * coordinatesOfPolygon[i].Point2.X + dx);
                    points2D[1].Y = (int)(k * coordinatesOfPolygon[i].Point2.Y + dy);
                    points2D[2].X = (int)(k * coordinatesOfPolygon[i].Point4.X + dx);
                    points2D[2].Y = (int)(k * coordinatesOfPolygon[i].Point4.Y + dy);
                    points2D[3].X = (int)(k * coordinatesOfPolygon[i].Point3.X + dx);
                    points2D[3].Y = (int)(k * coordinatesOfPolygon[i].Point3.Y + dy);

                    graphics.DrawPolygon(pen, points2D);
                }
                else
                {
                    points2D[0].X = (int)(((k +KScaling )* coordinatesOfPolygon[i].Point1.X + dx));
                    points2D[0].Y = (int)(((k + KScaling) * coordinatesOfPolygon[i].Point1.Y + dy));
                    points2D[1].X = (int)(((k + KScaling) * coordinatesOfPolygon[i].Point2.X + dx));
                    points2D[1].Y = (int)(((k + KScaling) * coordinatesOfPolygon[i].Point2.Y + dy));
                    points2D[2].X = (int)(((k + KScaling) * coordinatesOfPolygon[i].Point4.X + dx));
                    points2D[2].Y = (int)(((k + KScaling) * coordinatesOfPolygon[i].Point4.Y + dy));
                    points2D[3].X = (int)(((k + KScaling) * coordinatesOfPolygon[i].Point3.X + dx));
                    points2D[3].Y = (int)(((k + KScaling) * coordinatesOfPolygon[i].Point3.Y + dy));

                    graphics.DrawPolygon(pen, points2D);
                }
            }
            return bitmapWithFigure;
        }


      
    }
}
