using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


namespace Lesson3
{
    public class PointClassFloat
    {
        public float X;
        public float Y;
    }

    public struct PointStructFloat
    {
        public float X;
        public float Y;
    }

    public struct PointStructDouble
    {
        public double X;
        public double Y;
    }

    public class Distance
    {
        //Обычный метод расчёта дистанции со ссылочным типом(PointClass — координаты типа float).
        public double PointDistanceClassFloat(PointClassFloat pointOne, PointClassFloat pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));
        }
        //Обычный метод расчёта дистанции со значимым типом(PointStruct — координаты типа float).
        public double PointDistanceStructFloat(PointStructFloat pointOne, PointStructFloat pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));
        }
        //Обычный метод расчёта дистанции со значимым типом(PointStruct — координаты типа double).
        public double PointDistanceStructDouble(PointStructDouble pointOne, PointStructDouble pointTwo)
        {
            double x = pointOne.X - pointTwo.X;
            double y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));
        }
        //Метод расчёта дистанции без квадратного корня со значимым типом(PointStruct — координаты типа float).
        public float PointDistanceShort(PointStructFloat pointOne, PointStructFloat pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return (x * x) + (y * y);
        }
    }

    [MemoryDiagnoser]
    [RankColumn]
    public class DistBenchmark
    {
        private PointClassFloat point1ClassFlt = new PointClassFloat() { X = 1.5f, Y = 2.345f };
        private PointClassFloat point2ClassFlt = new PointClassFloat() { X = 10.273f, Y = 20.231f };

        private PointStructFloat point1StructFlt = new PointStructFloat() { X = 14.5342f, Y = 26.23423f };
        private PointStructFloat point2StructFlt = new PointStructFloat() { X = 100.464f, Y = 200.6f };

        private PointStructDouble point1StructDbl = new PointStructDouble() { X = 5.234, Y = 5.333 };
        private PointStructDouble point2StructDbl = new PointStructDouble() { X = 120.55, Y = 122.5454 };

        private Distance distance = new Distance();

        [Benchmark]
        //Обычный метод расчёта дистанции со ссылочным типом(PointClass — координаты типа float).
        public void pPointDistanceClassFloat()
        {
            double res = distance.PointDistanceClassFloat(point1ClassFlt, point2ClassFlt);
        }

        [Benchmark]
        //Обычный метод расчёта дистанции со значимым типом(PointStruct — координаты типа float).
        public double PointDistanceStructFloat()
        {
            return distance.PointDistanceStructFloat(point1StructFlt, point2StructFlt);
        }

        [Benchmark]
        //Обычный метод расчёта дистанции со значимым типом(PointStruct — координаты типа double).
        public double PointDistanceStructDouble()
        {
            return distance.PointDistanceStructDouble(point1StructDbl, point2StructDbl);
        }

        [Benchmark]
        //Метод расчёта дистанции без квадратного корня со значимым типом(PointStruct — координаты типа float).
        public float PointDistanceShort()
        {
            return distance.PointDistanceShort(point1StructFlt, point2StructFlt);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            BenchmarkRunner.Run<DistBenchmark>();
        }
    }
}
