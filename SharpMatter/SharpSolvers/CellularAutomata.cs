using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpField;
using SharpMatter.SharpBehavior;
using SharpMatter.SharpPopulations;
using SharpMatter.SharpSolvers;
using System.Drawing;

using SharpMatter.SharpUtilities;

namespace SharpMatter.SharpSolvers
{
    public static  class CellularAutomata

    {


        public static void Solve(SharpField2D<double> sharpField)
        {
            CreateNewGeneration(sharpField);
            ComputeStates(sharpField);
        }





        private static void CreateNewGeneration(SharpField2D<double> sharpField)
        {



            for (int i = 0; i < sharpField.Columns; i++)
            {
                for (int j = 0; j < sharpField.Rows; j++)
                {
                    if (sharpField.Field[i, j] is Automata)
                    {
                        var automata = (Automata)sharpField.Field[i, j];
                        automata.SavePreviousState();
                    }
                }
            }

        }

        private static void ComputeStates(SharpField2D<double> sharpField)
        {
            for (int i = 1; i < sharpField.Columns - 1; i++)
            {
                for (int j = 1; j < sharpField.Rows - 1; j++)
                {


                    double neighbors = AddStates(i, j, sharpField);

                    Rules(i, j, neighbors, sharpField);

                    DisplayColors(i, j, sharpField);
                }
            }

        }


        private static void DisplayColors(int x, int y, SharpField2D<double> sharpField)
        {
            if (sharpField.Field[x, y] is Automata)
            {
                var automata = (Automata)sharpField.Field[x, y];
                // m_colors[x, y] = automata.DisplayColor();

                sharpField.FieldColors[x, y] = automata.DisplayColor();
            }

            else
            {
                throw new ArgumentException("not Automata!");
            }
        }


        public static void Initdisplay(SharpField2D<double> sharpField)
        {
            for (int i = 0; i < sharpField.Columns; i++)
            {
                for (int j = 0; j < sharpField.Rows; j++)
                {
                    if (sharpField.Field[i, j] is Automata)
                    {
                        var automata = (Automata)sharpField.Field[i, j];
                        sharpField.FieldColors[i, j] = automata.DisplayColor();
                    }




                }
            }
        }

        private static void Rules(int x, int y, double neighbours, SharpField2D<double> sharpField)
        {
            if (sharpField.Field[x, y] is Automata)
            {
                var automata = (Automata)sharpField.Field[x, y];
                // death Loneliness
                if (automata.State == 1 && neighbours < 2) automata.SaveNewState(0);

                // death Overpopulation
                if (automata.State == 1 && neighbours > 3) automata.SaveNewState(0);

                // life reproduction
                if (automata.State == 0 && neighbours == 3) automata.SaveNewState(1);

                // life reproduction
                if (automata.State == 1 && (neighbours == 3 || neighbours == 2)) automata.SaveNewState(1);
            }
        }


        private static double AddStates(int x, int y, SharpField2D<double> sharpField)
        {
            double neighbours = 0;

            if (sharpField.Field[x, y + 1] is Automata)
            {
                var automata = (Automata)sharpField.Field[x, y + 1];

                neighbours += automata.PreviousState;
            }

            if (sharpField.Field[x, y - 1] is Automata)
            {
                var automata = (Automata)sharpField.Field[x, y - 1];
                neighbours += automata.PreviousState;
            }

            if (sharpField.Field[x + 1, y] is Automata)
            {
                var automata = (Automata)sharpField.Field[x + 1, y];
                neighbours += automata.PreviousState;
            }

            if (sharpField.Field[x - 1, y] is Automata)
            {
                var automata = (Automata)sharpField.Field[x - 1, y];
                neighbours += automata.PreviousState;
            }

            if (sharpField.Field[x - 1, y - 1] is Automata)
            {
                var automata = (Automata)sharpField.Field[x - 1, y - 1];
                neighbours += automata.PreviousState;
            }


            if (sharpField.Field[x + 1, y + 1] is Automata)
            {
                var automata = (Automata)sharpField.Field[x + 1, y + 1];
                neighbours += automata.PreviousState;
            }


            if (sharpField.Field[x - 1, y + 1] is Automata)
            {
                var automata = (Automata)sharpField.Field[x - 1, y + 1];
                neighbours += automata.PreviousState;
            }


            if (sharpField.Field[x + 1, y - 1] is Automata)
            {
                var automata = (Automata)sharpField.Field[x + 1, y - 1];
                neighbours += automata.PreviousState;
            }


            return neighbours;
        }



    }


    //public class CellularAutomataSolver
    //{

    //    public Automata[,] population;
    //    public Color[,] colors;

    //    int m_columns;
    //    int m_rows;
    //    public CellularAutomataSolver(int columns, int rows, List<double> states)
    //    {
    //         m_columns = columns;
    //         m_rows = rows;
    //        population = new Automata[m_columns, m_rows];
    //        colors = new Color[m_columns, m_rows];


    //        double[] tempValueA = states.ToArray();      
    //        double[,] states2D = Utilities.Make2DArray(tempValueA, m_columns, m_rows);

    //        for (int i = 0; i < columns; i++)
    //        {
    //            for (int j = 0; j < rows; j++)
    //            {
    //                population[i, j] = new Automata(new SharpGeometry.Vec3(i, j, 0), states2D[i, j], 0);
    //            }
    //        }


    //    }


    //    public  void Solve()
    //    {
    //        CreateNewGeneration();
    //        ComputeStates();
    //    }





    //    private  void CreateNewGeneration()
    //    {

    //        for (int i = 0; i < m_columns; i++)
    //        {
    //            for (int j = 0; j < m_rows; j++)
    //            {                   
    //                population[i, j].SavePreviousState();
    //            }
    //        }

    //    }

    //    private  void ComputeStates()
    //    {
    //        for (int i = 1; i < m_columns - 1; i++)
    //        {
    //            for (int j = 1; j < m_rows - 1; j++)
    //            {


    //                double neighbors = AddStates(i, j);

    //                Rules(i, j, neighbors);

    //                DisplayColors(i, j);
    //            }
    //        }

    //    }


    //    private  void DisplayColors(int x, int y)
    //    {
    //        colors[x, y] = population[x,y].DisplayColor();

    //    }


    //    public  void Initdisplay()
    //    {
    //        for (int i = 0; i < m_columns; i++)
    //        {
    //            for (int j = 0; j < m_rows; j++)
    //            {
    //                colors[i, j] = population[i, j].DisplayColor();




    //            }
    //        }
    //    }

    //    private  void Rules(int x, int y, double neighbours)
    //    {
           
              
    //            // death Loneliness
    //            if (population[x,y].State == 1 && neighbours < 2) population[x, y].SaveNewState(0);

    //            // death Overpopulation
    //            else if (population[x, y].State == 1 && neighbours > 3) population[x, y].SaveNewState(0);

    //            // life reproduction
    //            else if (population[x, y].State == 0 && neighbours == 3) population[x, y].SaveNewState(1);

    //            // life reproduction
    //            else if (population[x, y].State == 1 && neighbours == 3 || neighbours == 2) population[x, y].SaveNewState(1);
            
    //    }


    //    private  double AddStates(int x, int y)
    //    {
    //        double neighbours = 0;

    //       neighbours += population[x, y + 1].PreviousState;
    //       neighbours += population[x, y - 1].PreviousState;
    //       neighbours += population[x+1, y].PreviousState;
    //       neighbours += population[x - 1, y].PreviousState;

    //       neighbours += population[x - 1, y-1].PreviousState;
    //       neighbours += population[x + 1, y+1].PreviousState;
    //       neighbours += population[x - 1, y + 1].PreviousState;
    //       neighbours += population[x + 1, y - 1].PreviousState;

    //        return neighbours;
    //    }


    //}
}
