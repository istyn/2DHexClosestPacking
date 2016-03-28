using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DHCP
{
    class Program
    {
        static void Main(string[] args)
        {
            double r = .5625;
            const double border = .5;
            double length = 96;//inches
            double width = 34;





            //double[,,] dist = new double[t.xPts, t.yPts,2];

            //Cap[,] coords = new Cap[t.xPts,t.yPts];

            Console.WriteLine("radius?");
            r = Double.Parse(Console.ReadLine());

            Table t = new Table(length, width, r);
            for (int y = 0; y < t.yPts; y++)
            {
                for (int x = 0; x < t.xPts; x++)      //determine the distance of each point from the origin recursively
                {
                    double dX = r, dY = r;                //incremented distance from origin
                    if (y % 2 == 0)                    //if even row, no offset
                    {
                        dX += x * 2 * r;                //increment dX distance as you fill the rows
                        dY += y * 2 * r;                //increment dY distance when the row changes
                    }
                    else
                    {
                        dX += r + x * 2 * r;            //odd rows are offset by a radius
                        dY += y * 2 * r;
                    }
                    t.AddCap(new Cap());
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(Math.Round(t.Caps[i][j].X, 4).ToString().PadLeft(5, ' ') + ", " + Math.Round(t.Caps[i][j].Y, 4).ToString().PadRight(5, ' ') + "  ");
                }
                Console.Write("\n");
            }
            Console.Write("    Table Stats:\n\tLength: {0}in.  {1} caps\n\t Width: {2}in.  {3} caps\n\t Total: {4}\n", Math.Round(t.Length, 4), t.yPts, Math.Round(t.Width, 4), t.xPts, t.Count);

            Console.ReadKey();
            
            

        }
    }
}
