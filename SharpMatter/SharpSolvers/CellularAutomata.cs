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



}
