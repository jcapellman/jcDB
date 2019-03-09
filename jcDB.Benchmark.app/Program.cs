using BenchmarkDotNet.Running;

namespace jcDB.Benchmark.app
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MainBenchmark>();
        }
    }
}