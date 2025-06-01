namespace MinCurvMethod 
{
    public class GridExtend
    {
        public static void Extend(Grid grid, int nx, int ny, double hx, double hy)
        {
            double[] Xs = new double[nx];
            double[] Ys = new double[ny];
            double[,] Z = new double[nx, ny];
            bool[,] Known = new bool[nx, ny];
            int old_nx = grid.Nx;
            int old_ny = grid.Ny;
            for (int i = 0; i < old_nx; i++)
            {
                Xs[i] = grid.Xs[i];
            }
            for (int i=0; i<old_ny; i++)
            {
                Ys[i] = grid.Ys[i];
            }
            for (int i = 0; i < old_nx; i++)
            {
                for (int j = 0; i < old_ny; i++)
                {
                    Z[i,j]=grid.Z[i,j];
                    Known[i,j] = grid.Known[i,j];
                }
            }
            for (int i = old_nx; i < nx; i++)
            {
                Xs[i] = Xs[i - 1] + i * hx;
            }
            for (int i = old_ny; i < ny; i++)
            {
                Ys[i] = Ys[i - 1] + i * hy;
            }
            for(int i = old_nx;i < nx; i++)
            {
                for(int j = old_ny; j < ny; j++)
                {
                    Z[i, j] = 0;
                    Known[i, j] = false;
                }
            }
            grid.Nx= nx;
            grid.Ny = ny;
            grid.Xs = Xs;
            grid.Ys = Ys;
            grid.Z = Z;
            grid.Known = Known;

        }
    }
}
