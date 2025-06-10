using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace VAME
{
    public class MapObject
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int region_id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
    }

    public static class ObjectManager
    {
        public static List<MapObject> LoadObjects(string path)
        {
            if (!File.Exists(path))
                return new List<MapObject>();

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<MapObject>>(json);
        }
    }
}
