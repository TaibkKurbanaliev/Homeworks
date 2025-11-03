using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    public class JsonStorage : IStorage
    {
        private string _path;

        public JsonStorage(string fileName)
        {
            _path = Path.Combine(AppContext.BaseDirectory, fileName + ".json");
        }

        public T Load<T>()
        {
            using (var stream = new StreamReader(_path))
            {
                var json = stream.ReadToEnd();
                var data = JsonConvert.DeserializeObject<T>(json);
                return data;
            }
        }

        public void Save(object save)
        {
            string json = JsonConvert.SerializeObject(save);

            using (var stream = new StreamWriter(_path))
            {
                stream.Write(json);
            }
        }
    }
}
