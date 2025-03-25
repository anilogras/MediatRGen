using MediatRGen.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Processes
{
    public class ConfigCreateProcess : BaseProcess
    {
        public ConfigCreateProcess()
        {
            CreateConfiguration();
        }

        public void CreateConfiguration() 
        {
            PathHelper.GetPath(DirectoryHelpers.GetAppDirectory());
        }
    }
}
