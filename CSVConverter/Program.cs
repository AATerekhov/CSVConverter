using CSVElements;
using CSVloader;
using System.Diagnostics;
using System.Dynamic;
using System.Text.Json;
using System.Xml.Linq;

namespace CSVConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var element = new CSVElements.Sequence() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };
            string pathFoulder = "C:\\Users\\User\\Desktop\\test";
            var nameFile = "test1";
            ILoader loader = new CSVLoader(pathFoulder);

            MeasuringTimeForSaving(element, loader, nameFile);
            MeasuringTimeForLoading<CSVElements.Sequence>(loader, nameFile);
            //SaveCountingTimeWithOutConcoleEach(element, loader, nameFile);
            MeasuringTimeForSavingJson(element,Path.Combine(pathFoulder, nameFile));
            MeasuringTimeForLoadingJson<CSVElements.Sequence>(Path.Combine(pathFoulder, nameFile));

        }

        static void MeasuringTimeForLoadingJson<T>(string pathFile, int numberIterations = 1000)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < numberIterations; i++)
            {
                var s = File.ReadAllText(pathFile + ".json");
                var json = JsonSerializer.Deserialize<T>(s);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch);
        }
        static void MeasuringTimeForSavingJson<T>(T element, string pathFile ,int numberIterations = 1000)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < numberIterations; i++)
            {
                var s = JsonSerializer.Serialize(element);
                File.WriteAllText(pathFile+".json", s);
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch);
        }

        static void MeasuringTimeForLoading<T>(ILoader loader, string nameFile, int numberIterations = 1000) 
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < numberIterations; i++)
            {
                var newElement = loader.Load<T>(nameFile);
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch);
        }

        static void SaveCountingTimeWithOutConcoleEach<T>(T element, ILoader loader, string nameFile, int numberIterations = 1000)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < numberIterations; i++)
            {
                loader.Save(element, nameFile);
                Console.WriteLine(loader.GetFileContent(nameFile));
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch);
        }

        static void MeasuringTimeForSaving<T>(T element, ILoader loader, string nameFile, int numberIterations = 1000)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < numberIterations; i++)
            {
                loader.Save(element, nameFile);
            }

            stopwatch.Stop();

            Console.WriteLine(loader.GetFileContent(nameFile));
            Console.WriteLine(stopwatch);
        }
    }
}
