
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

namespace MinCurvMethod
{

        public static class DataReader
        {
            ///<summary>
            ///метод ReadPointsFromFile предназначен для чтения входных текстовых файлов,
            ///содержащих числа типа double с шестью знаками после запятой x y z с разделителями в виде пробелов.
            ///</summary>/// <param name="path">относительный путь к файлу.</param>
            /// <returns>список точек Point3D.</returns>
            /// <exception cref="FormatException">Когда происходит ошибка парсинга строки.</exception>
            ///   <exception cref="FileNotFoundException">Когда не удается открыть файл.</exception>
            public static List<Point3D> ReadPointsFromFile(string path)
            {
                string prepath = "C:\\Users\\alexa\\source\\repos\\Interpolation\\Interpolation\\inputData\\";
                path=prepath + path;
                var points = new List<Point3D>();
                try
                {

                    foreach (var line in File.ReadLines(path))
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        var p = line.Split();
                        if (p.Length < 3) continue;

                        try
                        {
                            double x = double.Parse(p[0], CultureInfo.InvariantCulture);
                            double y = double.Parse(p[1], CultureInfo.InvariantCulture);
                            double z = double.Parse(p[2], CultureInfo.InvariantCulture);

                            points.Add(new Point3D(x, y, z));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Ошибка парсинга строки: {line}");
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"Не удалось открыть файл, имеющий полный путь: {path}");
                }

                return points;
            }
        }
    }
