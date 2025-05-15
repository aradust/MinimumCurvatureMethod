using System;
using System.IO;

namespace MinCurvMethod
{ 
    public class DataWriter
    {
        ///<summary>
        ///метод writeData предназначен для записи точек сетки в выходной файл.
        ///После записи выходной файл содержит числа типа double с шестью знаками после запятой x y z с разделителями в виде пробелов.
        ///</summary>
        /// <param name="path">относительный путь к файлу.</param>
        ///<exception cref="FileNotFoundException">Когда не удается открыть файл.</exception>
        public static void WriteData(string path, Grid grid)
        { string prePath = "C:\\Users\\alexa\\source\\repos\\Interpolation\\Interpolation\\outputData\\";
            path = prePath + path;
            try
            {
                var sw = new StreamWriter(path);
                for (int i = 0; i < grid.Ny; i++)
                    for (int j = 0; j < grid.Nx; j++)
                        
                            sw.WriteLine($"{grid.Xs[j]:F6} {grid.Ys[i]:F6} {grid.Z[j, i]:F6}");
                        
            }
            catch
            {
                Console.WriteLine($"Не удалось открыть файл, имеющий полный путь: {path}");
            }
        }
    }
}
