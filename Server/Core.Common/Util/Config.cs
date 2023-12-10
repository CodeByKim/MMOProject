using System;
using Newtonsoft.Json;

namespace Core.Common
{
    public abstract class Config<T> where T : new()
    {
        public int PortNumber { get; set; } = 10000;

        public int ReceiveBufferSize { get; set; } = 1024 * 4;

        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance is null)
                    _instance = new T();

                return _instance;
            }
        }

        public void Load(string path)
        {
            try
            {
                var text = File.ReadAllText(path);
                _instance = JsonConvert.DeserializeObject<T>(text);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fail Loading Config... Message: {e.Message}, Path: {Path.GetFullPath(path)}");
                Environment.Exit(0);
            }
        }
    }
}
