using System;

namespace MinCurvMethod
{
    public static class Extrapolator
    {///<summary>
     ///Метод Extrapolate производит экстраполяцию данных классическим алгоритмом минимальной кривизны. Для приближения
     ///би-лапласиана строится 13-точечная конечно-разностная схема 4-порядка точности, итерационно находятся приближения компоненты z
     ///</summary>
     ///<param name="stepX">шаг сетки по x</param>
     ///<param name="stepY">шаг сетки по y</param>
     ///<param name="grid">регулярная сетка</param>
     ///<param name="lambda">параметр регуляризации</param>
     ///<param name="tolerance">точность итерационного метода</param>
     ///<param name="maxIter">ограничитель итераций</param>
     ///<param name="omega">параметр релаксации</param>
        public static void Extrapolate(int stepX, int stepY, Grid grid, double lambda = 0, double tolerance = 1e-6, int maxIter = 1000, double omega = 0.1)
        {
            //перезаписываем размерность сетки (для удобства)
            int nx = grid.Nx;
            int ny = grid.Ny;

            //перезаписываем шаги сетки и считаем квадрат и 4-ую степень
            double hx = stepX;
            double hy = stepY;
            double hx2 = hx * hx, hy2 = hy * hy;
            double hx4 = hx2 * hx2, hy4 = hy2 * hy2;

            // Буферы для итераций
            double[,] Z_old = new double[nx, ny];
            double[,] Z_new = new double[nx, ny];

            // Копируем начальные значения из восстановленных ранее интерполяцией
            for (int i = 0; i < nx; i++)
                for (int j = 0; j < ny; j++)
                    Z_old[i, j] = grid.Z[i, j];

            Console.WriteLine("Начало экстраполяции методом минимальной кривизны с релаксацией");

            for (int iter = 0; iter < maxIter; iter++)
            {
                double maxDiff = 0.0;

                for (int i = 0; i < nx; i++)
                {
                    for (int j = 0; j < ny; j++)
                    {
                        if (grid.Known[i, j])
                        {
                            Z_new[i, j] = Z_old[i, j]; // известные точки не трогаем
                            continue;
                        }

                        double sum = 0.0;
                        double diag = (20.0 / hx4 + 20.0 / hy4) + lambda;

                        // Центральные окрестности
                        sum += Z_old[ i - 1, j] * (-8.0 / hx4);
                        sum += Z_old[ i + 1, j] * (-8.0 / hx4);
                        sum += Z_old[ i, j - 1] * (-8.0 / hy4);
                        sum += Z_old[ i, j + 1] * (-8.0 / hy4);

                        // Краевые точки на 2
                        sum += Z_old[ i - 2, j] * (2.0 / hx4);
                        sum += Z_old[ i + 2, j] * (2.0 / hx4);
                        sum += Z_old[ i, j - 2] * (2.0 / hy4);
                        sum += Z_old[ i, j + 2] * (2.0 / hy4);

                        // Диагонали (смешанные производные)
                        double mix = 4.0 / (hx2 * hy2);
                        sum += Z_old[ i - 1, j - 1] * mix;
                        sum += Z_old[i - 1, j + 1] * mix;
                        sum += Z_old[ i + 1, j - 1] * mix;
                        sum += Z_old[ i + 1, j + 1] * mix;

                        // Новое значение с релаксацией
                        double newZ = -sum / diag;
                        double relaxedZ = (1 - omega) * Z_old[i, j] + omega * newZ;
                        Z_new[i, j] = relaxedZ;

                        maxDiff = Math.Max(maxDiff, Math.Abs(Z_old[i, j] - relaxedZ));
                    }
                }

                Console.WriteLine($" Итерация {iter + 1}: макс. изменение Z = {maxDiff:E}");

                if (maxDiff < tolerance)
                {
                    Console.WriteLine("Сходимость достигнута.");
                    break;
                }

                // Смена буфера
                var tmp = Z_old;
                Z_old = Z_new;
                Z_new = tmp;
            }

            // Переносим результат в матрицу Z
            for (int i = 0; i < nx; i++)
                for (int j = 0; j < ny; j++)
                    grid.Z[i, j] = Z_old[i, j];
        }

       

    }
}