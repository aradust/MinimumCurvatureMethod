using System;
using System.Collections.Generic;
namespace MinCurvMethod
{
    public static class GridBuilder
    {///<summary>
     ///метод BuildGrid создаёт сетку по входным данным (трёхмерным точкам). 
     ///Вначале строится BoundingBox, после чего создаётся сетка Grid, каждому значению булевой матрицы известных z-узлов
     ///присваивается false (не меняется)
     ///</summary>
     ///<param name="points">трёхмерные точки, построенные по входным данным</param>
     ///<param name="stepX">шаг сетки по x</param>
     ///<param name="stepY">шаг сетки по y</param>
     /// <returns>двумерная регулярная сетка, а также массив значений z(x,y) и Known(x,y)</returns>
        public static Grid BuildGrid(List<Point3D> points,
                                     double stepX, double stepY)
        {
            // Определение BoundingBox
            double minX = double.MaxValue, maxX = double.MinValue;
            double minY = double.MaxValue, maxY = double.MinValue;
            foreach (var p in points)
            {
                if (p.X < minX) minX = p.X;
                if (p.X > maxX) maxX = p.X;
                if (p.Y < minY) minY = p.Y;
                if (p.Y > maxY) maxY = p.Y;
            }
            
            // Создание сетки 
            int nx = (int)Math.Ceiling((maxX - minX) / stepX) + 1;
            int ny = (int)Math.Ceiling((maxY - minY) / stepY) + 1;

            var grid = new Grid(nx, ny);
            for (int i = 0; i < nx; i++) grid.Xs[i] = minX + i * stepX;
            for (int j = 0; j < ny; j++) grid.Ys[j] = minY + j * stepY;

            // Инициализация Known=false
            return grid;
        }
    }
}
