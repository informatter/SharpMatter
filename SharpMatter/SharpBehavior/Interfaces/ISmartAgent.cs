using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpLearning.GeneticAlgorithm;
using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpBehavior.Interfaces
{
    interface ISmartAgent
    {
         DNA DNA
        {
            get;
            set;

        }

         double Fitness
        {
            get;
            set;

        }

        
    }
}
