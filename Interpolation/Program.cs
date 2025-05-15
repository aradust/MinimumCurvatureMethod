using System;

namespace MinCurvMethod
{ ///<summary>
  ///точка входа в программу. Здесь можно задавать входные файлы, шаги сетки, выходные файлы
  ///</summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Чтение входного файла с точками");
            var points = DataReader.ReadPointsFromFile("data_1.txt");

            // Параметры сетки
            int stepX = 50, stepY = 50;
            var grid = GridBuilder.BuildGrid(points, stepX, stepY);

            Console.WriteLine("Интерполяция...");
            Interpolation.Interpolate(stepX, stepY, grid, points,
                RadiusType.Cell, InterpolationMethod.Mean, WeightFunction.Inverse2);
            int knownCount = 0;
            for (int i = 0; i < grid.Nx; i++)
                for (int j = 0; j < grid.Ny; j++)
                    if (grid.Known[i, j]) knownCount++;
           
           Console.WriteLine($"После интерполяции известно: {knownCount} из {grid.Nx * grid.Ny}");
           
            Console.WriteLine("Экстраполяция методом минимальной кривизны...");
            Extrapolator.Extrapolate(stepX,stepY,grid, lambda: 0);

            // Вывод результата
            DataWriter.WriteData("output_grid.txt",grid);
           
            Console.WriteLine("Готово. Результат сохранён в output_grid.txt");
        }
    }
}
