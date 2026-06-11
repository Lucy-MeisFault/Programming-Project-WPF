using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{


    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Platform(int id, string name)
        {
            Id = id;
            Name = name;
        }


    }

}
