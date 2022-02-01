using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Helpers
{
    public static class DataPathGenerator
    {
        public static string Create(string path)
        {
            var appDirectory = new DirectoryInfo(AppContext.BaseDirectory);            
            var pathArray = path.Split('/');

            for (int i = 0; i < pathArray.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(pathArray[i]))
                    continue;

                if (pathArray[i] == "..")
                {
                    if (appDirectory?.Parent == null)
                        return "";

                    appDirectory = appDirectory.Parent;
                }
                else
                {
                    appDirectory = new DirectoryInfo(appDirectory.FullName + "/" + pathArray[i] + "/");
                }
            }

            return appDirectory.FullName;
        }

    }
}
