using Core.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Deneme.Features
{
    public class Isyeri : Entity, IEntity
    {
        public string Name { get; set; }
    }
}
