using System;
using System.Collections.Generic;

namespace MinCurvMethod
{
    public static class Interpolation
    {
        ///<summary>
        ///метод Interpolate интерполирует данные либо методом ближайшего соседа, либо методом усреднения точек.
        ///</summary>
        /// <param name="stepX">шаг сетки по x</param>
        /// <param name="stepY">шаг сетки по y</param>
        ///  <param name="grid">регулярная сетка</param>
        /// <param name="points">трёхмерные точки, построенные по входным данным</param>
        /// <param name="rtype">радиус влияния</param>
        /// <param name="imethod">интерполяционный метод</param>
        /// <param name="wfunc">весовая функция, используемая в методе усреднения точек</param>
        public static void Interpolate(int stepX, int stepY, Grid grid, List<Point3D> points,
                                       RadiusType rtype,
                                       InterpolationMethod imethod,
                                       WeightFunction wfunc)
        {
            //перезаписываем размерность сетки (для удобства)
            int nx = grid.Nx, ny = grid.Ny;
            //перезаписываем шаг сетки (для удобства)
            double hx = stepX;
            double hy = stepY;

            //Определение радиуса влияния, в зависимости от параметра.
             double rx = rtype == RadiusType.Local ? 0.5 : 100;  
            Console.WriteLine($"Используется радиус влияния: {rx}");

           /* // Выводим точки для отладки
            Console.WriteLine("Точки:");
            foreach (var p in points)
            {
                Console.WriteLine($"{p.X:F2} {p.Y:F2} {p.Z:F2}");
            }*/

            // Начинаем интерполяцию. Перебираем точку на сетке и ищем лежащие в радиусе влияния входные точки
            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    var x0 = grid.Xs[i];
                    var y0 = grid.Ys[j];

                    var neighbors = new List<Point3D>();

                    foreach (var p in points)
                    {
                        double normDx = Math.Abs(p.X - x0) / hx;
                        double normDy = Math.Abs(p.Y - y0) / hy;
                        //находим точки в радиусе влияния
                        if (Math.Max(normDx, normDy) <= rx)
                            neighbors.Add(p);
                    }
                    
                    if (neighbors.Count == 0)

                    {
                        // не находим точки, лежащие в радиусе влияния
                        grid.Known[i, j] = false;
                        continue;
                    }
                    //реализация метода ближайшего соседа
                    if (imethod == InterpolationMethod.Nearest)
                    {
                        double minDist = double.MaxValue;
                        Point3D best = null;

                      
                        foreach (var p in neighbors)
                        {
                            double d = Distance(p.X, p.Y, x0, y0);
                            if (d < minDist)
                            {
                                minDist = d;
                                best = p;
                            }
                        }

                        if (best != null)
                        {
                          //записываем интерполированное значение и помечаем это в матрице
                            grid.Z[i, j] = best.Z;
                            grid.Known[i, j] = true;
                        }
                        else
                        {
                            grid.Known[i, j] = false;
                        }
                    }
                    else // Реализация метода усреднения точек
                    {
                        double sumW = 0, sumZW = 0;

                        foreach (var p in neighbors)
                        {
                            double r = Distance(p.X, p.Y, x0, y0);
                            if (r < 1e-12)
                            {
                                grid.Z[i, j] = p.Z;
                                grid.Known[i, j] = true;
                                goto NextPoint;  // Переходим к следующей точке
                            }

                            double w = Rfunc(r, wfunc);
                            if (double.IsInfinity(w) || double.IsNaN(w)) continue;

                            sumW += w;
                            sumZW += p.Z * w;
                        }

                        if (sumW > 0)
                        {
                            grid.Z[i, j] = sumZW / sumW;
                            grid.Known[i, j] = true;
                        }
                        else
                        {
                            grid.Known[i, j] = false;
                        }

                    NextPoint:;
                    }
                }
            }

        }
        ///<summary>
        ///метод rfunc реализует весовую функцию для метода усреднения точек
        ///</summary>
        /// <param name="r">расстояние между точками</param>
        /// <param name="wf">выбранный тип весовой функции</param>
        /// <returns>значение весовой функции.</returns>
        static double Rfunc(double r, WeightFunction wf)
        {
            switch (wf)
            {
                case WeightFunction.Linear: return r;
                case WeightFunction.Inverse: return r < 1e-12 ? double.MaxValue : 1.0 / r;
                case WeightFunction.Inverse2: return r < 1e-12 ? double.MaxValue : 1.0 / (r * r);
                case WeightFunction.Inverse4: return r < 1e-12 ? double.MaxValue : 1.0 / Math.Pow(r, 4);
                default: return 1.0;
            }
        }
        ///<summary>
        ///метод Distance считает расстояния между двумя точками (двумерный случай)
        ///</summary>
        /// <param name="x1">первая точка x</param>
        /// <param name="x2">вторая точка x</param>
        /// <param name="y1">первая точка y</param>
        /// <param name="y2">вторая точка y</param>
        /// <returns>расстояние между точками.</returns>
        static double Distance(double x1, double y1, double x2, double y2)
        {
            double dx = x1 - x2, dy = y1 - y2;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}