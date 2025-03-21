using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.States
{
    public class GlobalState
    {
        public GlobalState()
        {
            Lang = "en";
        }

        private static GlobalState _instance;

        public static GlobalState instance
        {
            get { 
            
                if(_instance == null)
                    _instance = new GlobalState();

                return _instance;
            }
        }

        public string Lang { get; set; }
    }
}
