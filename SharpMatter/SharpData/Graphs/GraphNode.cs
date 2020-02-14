using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpCollections;
using Rhino.Geometry;

namespace SharpMatter.SharpData.Graphs
{
    public class GraphNode<T>: Node<T>
    {
        private List<double> m_weights;
       
        public GraphNode():base()
        {
            m_weights = new List<double>();
        }

        /// <summary>
        /// Construct a node from a generic value
        /// </summary>
        /// <param name="value"></param>
        public GraphNode(T value):base(value)
        {
            m_weights = new List<double>();
        }


        public GraphNode(T value,string name) : base(value,name)
        {
            m_weights = new List<double>();
        }


        /// <summary>
        /// Construct node from a generic value and a collection of neighbors
        /// </summary>
        /// <param name="value"></param>
        /// <param name="neighbours"></param>
        public GraphNode(T value, NodeContainer<T> neighbours) : base(value, neighbours)
        {

            m_weights = new List<double>();
        }

        /// <summary>
        /// Get a list of weights from current node
        /// to all neighbors
        /// </summary>
        public List<double> Weights
        {
            get { return m_weights; }
            set {  m_weights = value; }
        }


        /// <summary>
        /// Adds an unweighted directed edge from this node to a given node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="weight"></param>
        public void AddUnWeightedDirectedEdge(GraphNode<T> node)
        {
            this.Neighbors.Add(node);
        
        }


        
        /// <summary>
        /// Adds an a weighted directed edge from this node to a given node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="weight"></param>
        public void AddWeightedDirectedEdge(GraphNode<T> node, double weight)
        {
            this.Neighbors.Add(node);
            this.m_weights.Add(weight);
        }





    }
}
