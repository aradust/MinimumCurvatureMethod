namespace MinCurvMethod
{///<summary>
 ///класс Grid предназначен для создания структуры регулярной двумерной сетки, а также матрицы значений z(x,y) и Known(x,y)
 ///Nx - размерность сетки по x, Ny - размерность сетки по y, Xs - массив x
 ///координат, Ys - массив y координат, Z - двумерный массив значений z (x,y) 
 ///Known - булев массив, указывающий на известность/неизвестность значения z в точке (x,y)
 ///</summary>
    public class Grid
    {
        public int Nx, Ny;
        public double[] Xs, Ys;
        public double[,] Z;
        public bool[,] Known;
        public Grid(int nx, int ny)
        {
            Nx = nx; Ny = ny;
            Xs = new double[nx]; Ys = new double[ny];
            Z = new double[nx, ny]; Known = new bool[nx, ny];
        }
    }
}
