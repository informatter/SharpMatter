using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMatter.SharpForces
{
    public class Springs

    {
        private double restLenght;
        private double force;

        public Springs(double restLength, double force)
        {
            this.restLenght = restLength;
            this.force = force;
        }
        public Springs()
        {
           
        }
        public double RestLength
        {
            get { return restLenght; }

            set
            {

                restLenght = value;
            }
        }


        public double Force
        {
            get { return force; }

            set
            {

                force = value;
            }
        }



    }
}
