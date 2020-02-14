using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;
using Grasshopper.Kernel.Data;
using SharpMatter.SharpCollections;


namespace SharpMatter.SharpData.Graphs
{
    public class Graph<T>
    {
        private readonly NodeContainer<T> m_nodes;


        /// <summary>
        /// default constructor
        /// </summary>
        public Graph()
        {
            m_nodes = new NodeContainer<T>();
        }

        /// <summary>
        /// Initialize a graph from an existing node collection
        /// </summary>
        /// <param name="nodeCollection"></param>
        public Graph(NodeContainer<T> nodeCollection)
        {
            m_nodes = nodeCollection;
        }


        /// <summary>
        /// Get node Collection
        /// </summary>
        public NodeContainer<T> Nodes
        {
            get { return m_nodes; }
        }


        /// <summary>
        /// Get total number of nodes in Graph
        /// </summary>
        public int TotalNodes
        {
            get { return m_nodes.Count; }
        }



        /// <summary>
        /// Display Adjacency information of a graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static DataTree<T> DisplayAdjacencyList(Graph<T> graph)
        {
            DataTree<T> d = new DataTree<T>();
            for (int i = 0; i < graph.Nodes.Count; i++)
            {
                GH_Path p = new GH_Path(i);
                var neighbours = graph.Nodes[i].Neighbors;

                for (int j = 0; j < neighbours.Count; j++)
                {
                    d.Add(neighbours[j].Value, p);
                }
            }

            return d;
        }


        /// <summary>
        /// Display Adjacency weights of a graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static DataTree<double> AdjacencyListWeights(Graph<T> graph)
        {
            DataTree<double> d = new DataTree<double>();
            for (int i = 0; i < graph.Nodes.Count; i++)
            {

                GraphNode<T> node = graph.Nodes[i] as GraphNode<T>;
                List<double> weights = node.Weights;

                for (int j = 0; j < weights.Count; j++)
                {
                    GH_Path p = new GH_Path(i);
                    d.Add(weights[j], p);
                }


            }

            return d;
        }






        /// <summary>
        /// Adds a Node to the graph
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(GraphNode<T> node)
        {
            if (!m_nodes.Contains(node))
            {
                m_nodes.Add(node);
            }

        }


        /// <summary>
        /// Check if graph contains a node by specifying a unique value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Contains(T value, out GraphNode<T> node)
        {
            bool result = false;
            Node<T> _node = null;

            if (m_nodes.TryFindByValue(value, out _node))
            {

                result = true;
            }

            node = _node as GraphNode<T>;
            return result;

        }


  


        public void Remove(T value)
        {


            //nodeToRemove = null;
            if (!Contains(value, out GraphNode<T> nodeToRemove))
            {
                throw new ArgumentNullException("The node you are trying to remove does not exist!");
            }

            else
            {
                // Remove node form node collection
                m_nodes.Remove(nodeToRemove);


                // loop through all nodes an remove the desired node from all the other nodes
                // neighbors and corresponding weights 
                for (int i = 0; i < m_nodes.Count; i++)
                {
                    GraphNode<T> graphNode = m_nodes[i] as GraphNode<T>;

                    // Get index of node to remove
                    int index = graphNode.Neighbors.IndexOf(nodeToRemove);


                    graphNode.Neighbors.RemoveAt(index);
                    graphNode.Weights.RemoveAt(index);
                }
            }



        }

    
    }
}
