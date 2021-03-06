﻿using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace jcDB.lib
{
    public class Database
    {
        private readonly string _flushFileName;
        private readonly int _intervalToPurgeSeconds;

        private static Dictionary<string, object> _db = new Dictionary<string, object>();

        private readonly BackgroundWorker _flushWorker;

        public Database(bool persist = false, string flushFileName = null, int intervalToPurgeSeconds = Common.Constants.INTERVAL_TO_PURGE_SECONDS)
        {
            _flushFileName = flushFileName;
            _intervalToPurgeSeconds = intervalToPurgeSeconds;

            if (!persist)
            {
                return;
            }

            _flushWorker = new BackgroundWorker();
            _flushWorker.DoWork += _flushWorker_DoWork;
            _flushWorker.RunWorkerCompleted += _flushWorker_RunWorkerCompleted;
            _flushWorker.RunWorkerAsync();
        }

        private void _flushWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Thread.Sleep(_intervalToPurgeSeconds * 1000);
            
            _flushWorker.RunWorkerAsync();
        }

        private void _flushWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Flush();
        }

        public void InsertFireAndForget(string key, object value)
        {
            Task.Run(() => Insert(key, value));
        }

        private static void Insert(string key, object value)
        {
            _db[key] = value;
        }

        public async Task<bool> InsertAsync(string key, object value)
        {
            await Task.Run(delegate { Insert(key, value); });

            return true;
        }

        public object Get(string key) => _db.ContainsKey(key) ? _db[key] : null;

        public Dictionary<string, object> GetAll() => _db;

        public Dictionary<string, object> Get(List<string> keys) => new Dictionary<string, object>(_db.Where(a => keys.Contains(a.Key)));

        public void Clear()
        {
            _db.Clear();
        }

        public void Flush()
        {
            File.WriteAllText(_flushFileName, Newtonsoft.Json.JsonConvert.SerializeObject(_db));
        }
    }
}