using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DHCP
{
    /// <summary>
    /// A Cap lies in the 2D quadrant 1 ( +, + ). Each has a coordinate (X,Y) beginning at (0,0)
    ///     with rows going (0,0)(1,0),(2,0),(3,0),(.. ,0) and columns (0,0)(0,1),(0,2),(0, ..).
    ///     Each cap has a radius R.
    /// </summary>
    public class Cap
    {
        public double R = 0;            //radius around (x,y)
        public double X = -1, Y = -1;   //the center location (x,y) in quadrant 1.
        public Cap[] N;                 //there are at most 6 neighbors in a hex closest packing structure
        public double D                 //calculated diameter
        {
            protected set
            {
                D = value;
            }
            get
            {
                if (R != 0)
                    return (R * 2);
                else
                    throw new Exception("Radius not yet defined.");
            }
        }
        public Cap()
        {
            R = 0;                  //default, uninitialized values
            X = -1;
            Y = -1;
        }
        public Cap(double x, double y, double d)
        {
            if (d > 0)              //check that radius is valid
            {
                this.R = d / 2;     //diameter = 2 * radius

            }
            else throw new Exception("Diameter must be > 0.");
            this.X = x;
            this.Y = y;
        }
    }
    /// <summary>
    /// A table is represented by the plane in quadrant 1.
    /// </summary>
    class Table
    {
        public Cap[][] Caps;
        public double Width = 0, Length = 0;
        public int Count;
        public int xPts, yPts;
        public Table()
        {
            Caps = new Cap[1][];
            Caps[1] = new Cap[1];
            Count = 0;
        }
        /// <summary>
        /// Caps are in closest-packing configuration with radius r.
        /// </summary>
        /// <param name="L">The length of table.</param>
        /// <param name="W">The width of table.</param>
        /// <param name="r">The radius of the Caps.</param>
        public Table(double L, double W, double r)
        {
            xPts = Convert.ToInt32(W / (2 * r));
            yPts = Convert.ToInt32(L / (1.73205 * r));

            /*adjust plane size to closest discrete diameter of caps without going over*/
            Width = xPts * r * 2;
            Length = r * 2 + (yPts - 1) * 1.73205 * r;
            Caps = new Cap[yPts][];
            for (int y = 0; y < yPts; y++)              //define each cap with coordinates (x, y)
            {
                if (y % 2 == 0)
                {
                    Caps[y] = new Cap[xPts];            //even rows have x number of caps
                }
                else
                {
                    Caps[y] = new Cap[xPts - 1];          //odd rows have x-1
                }

                for (int x = 0; x < Caps[y].Length; x++)
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
                    Caps[y][x] = new Cap(dX, dY, r * 2);
                }

            }
        }
        public void AddCap(Cap C)
        {
            if (Caps != null)
            {
                //Caps[Count / xPts, Count % xPts] = C;   //integer division yields row ptr, mod yields column ptr
                Count++;
            }
            else throw new Exception("Table must be defined.");
        }
        /// <summary>
        /// Returns the center point of a particular cap in a 2D plane.
        /// Assumptions: caps are in a "closest-packing" configuration;
        ///     R is same for all Caps.
        ///     Caps are referenced by zero-based index.
        /// </summary>
        /// <returns></returns>
        public double[] GetCenterPoint(int x, int y, double r)
        {
            if (r != 0) //if first cap is defined, use its radius for calculating position
            {
                if (yPts % 2 == 0)  //if even row
                {
                    return new double[] { r + xPts * 2 * r, r + yPts * 2 * r };
                }

                else
                {
                    return new double[] { 2 * r + xPts * 2 * r, r + yPts * 2 * r };
                }
            }
            else
                throw new Exception("Cap must first be defined.");

        }
    }

}
