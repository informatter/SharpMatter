using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpCollections;

namespace SharpMatter.SharpData.Graphs
{
    public class Node<T>
    {
        private T m_value;
        private NodeContainer<T> m_neigbhbours;
        private string m_name;


        public Node() { }

        /// <summary>
        /// Constructs a Node with a value
        /// </summary>
        /// <param name="val">A generic value</param>
        /// 
        public Node(T val)
        {
            m_value = val;
            m_neigbhbours = new NodeContainer<T>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public Node(T val, string name)
        {
            m_value = val;
            m_name = name;
            m_neigbhbours = new NodeContainer<T>();
        }

        /// <summary>
        /// Constructs a Node with a value and a collection of neighbors;
        /// </summary>
        /// <param name="val">A generic value</param>
        /// <param name="neighbours">A NodeContainer object </param>
        public Node(T val, NodeContainer<T> neighbours)
        {
            m_value = val;
            m_neigbhbours = neighbours;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }


        /// <summary>
        /// Get all neighbors of node
        /// </summary>
        public NodeContainer<T> Neighbors
        {
            get { return m_neigbhbours; }
            set { m_neigbhbours = value; }
        }

        /// <summary>
        /// Get value stored in node
        /// </summary>
        public T Value
        {
            get { return m_value; }
            set { m_value = value; }

        }




    }


}
