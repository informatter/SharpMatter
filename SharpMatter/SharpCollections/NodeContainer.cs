using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpData.Graphs;

namespace SharpMatter.SharpCollections
{
    public class NodeContainer<T>: Collection<Node<T>>
    {
        public NodeContainer() : base()
        { }

        public NodeContainer(int numOfItems)
        {
            for (int i = 0; i < numOfItems; i++)
            {
                base.Items.Add(default( Node<T>));
            }
        }


        /// <summary>
        /// Tries to find a node in the collection by a unique value
        /// </summary>
        /// <param name="value">Value to search for</param>
        /// <param name="node">the returned Node on success,</param>
        /// <returns>true on success false on failure</returns>
        public bool TryFindByValue(T value, out Node<T> node)
        {
            bool result = false;
            node = null;
            foreach (Node<T> n in base.Items)
            {
                if (n.Value.Equals(value))
                {
                    result = true;
                    node = n;
                    break;
                }
            }
                                
            return result;
        }
    }
}
