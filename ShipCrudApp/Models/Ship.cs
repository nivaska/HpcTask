using ShipCrudApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipCrudApp.Models
{
    public class Ship
    {
        private int id;
        private string name;
        private double length;
        private double width;
        private string code;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Length
        {
            get { return length; }
            set
            { length = value; }
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
    }
}
