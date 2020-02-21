using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMatter.SharpExceptions
{
    public class SharpException: Exception
    {
        public SharpException(string message) : base(message)
        {
            
        }

        
    }
}
