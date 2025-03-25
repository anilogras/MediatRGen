using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Helpers
{
    public class FileHelpers
    {
        public static bool Create() { return true; }

        public static string Read() { return ""; }

        public static bool Delete() { return true; }

        public static bool Update() { return true; }

        public static bool CheckFile(string path, string fileName)
        {
            string _combinedPathWithFile = PathHelper.GetPath(path , fileName);

            if (File.Exists(_combinedPathWithFile))
                return true;

            return false;
        }

    }
}
