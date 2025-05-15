namespace MinCurvMethod
{///<summary>
 ///класс Point3D предназначен для создания трёхмерных точек из чисел типа double
 ///</summary>
    public class Point3D
    {
        public double X, Y, Z;
        public Point3D(double x, double y, double z) => (X, Y, Z) = (x, y, z);
    }
}