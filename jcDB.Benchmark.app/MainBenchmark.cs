using System.Collections.Generic;

using BenchmarkDotNet.Attributes;

using jcDB.lib;

namespace jcDB.Benchmark.app
{
    [CoreJob(true)]
    [RPlotExporter, RankColumn]
    public class MainBenchmark
    {
        private Database db = new Database();

        private Dictionary<string, object> _data;

        [Params(100, 1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            _data = new Dictionary<string, object>();

            for (var x = 0; x < N; x++)
            {
                _data.Add(x.ToString(), x);
            }
        }

        [Benchmark]
        public void InsertMany()
        {
            foreach (var (key, value) in _data)
            {
                db.InsertFireAndForget(key, value);
            }
        }
    }
}