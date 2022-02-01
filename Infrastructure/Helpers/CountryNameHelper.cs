using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Helpers
{
    public class CountryNameHelper
    {
        private WorldMapIndex? worldMapIndex;

        public CountryNameHelper()
        {
            var path = AppContext.BaseDirectory + @"Helpers\world-map-index.json";
            using (StreamReader reader = new StreamReader(path))
            {
                var jsonString = reader.ReadToEnd();
                worldMapIndex = JsonConvert.DeserializeObject<WorldMapIndex>(jsonString);
            };
        }

        public string GetShortName(string name)
        {
            if (worldMapIndex == null)
                return "";

            return worldMapIndex.Lands.FirstOrDefault(i => i.Name == name)?.ShortName ?? "";
        }
    }

    public class WorldMapIndex
    {
        public string Version { get; set; }
        public List<Country> Lands { get; set; }
    }

    public class Country
    {
        public string ShortName { get; set; }
        public string Name { get; set; }
    }
}
