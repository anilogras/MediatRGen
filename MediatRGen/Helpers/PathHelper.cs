using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Helpers
{
    public static class PathHelper
    {
        public static string GetPath(params string[] paths)
        {
            return string.Join('\\', paths).Replace("\\" , "/").Replace("//","/");
        }
    }
}
