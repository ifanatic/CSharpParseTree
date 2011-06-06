using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace CSharpParseTreeC
{
    class Utils
    {
        public static string GetDmcsAssemblyPath()
        {
            string appPath = Assembly.GetExecutingAssembly().Location;
            appPath = Path.GetDirectoryName(appPath);

            return Path.Combine(appPath, "dmcs.exe");
        }

    }
}
