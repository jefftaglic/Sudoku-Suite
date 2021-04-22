using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sudokube
{
    /*
     * Class: Cell
     * Description: Represents an individual cube in the Sudokube.
     *
     * Variables:
     * public char FillNumber      - holds the number for the cell
     * public List<char> TakenNum      - list of numbers the current number cannot be
     * private bool LockedNum      - flag to deny user to change the number
     *                             - set to "True" if number was generated
     * private char GenNum         - generated number, hidden from the user
     *
     */
    class Cell
    {
        public char FillNumber { get; set; }
        public List<char> TakenNum { get; set; } = new List<char>();
        public bool LockedNum { get; set; }
        public char GenNum { get; set; }

        // Default constructor
        public Cell()
        {
            FillNumber = '0';
            LockedNum = false;
        }

        // Specified number constructor
        public Cell(char fill, bool lockup)
        {
            FillNumber = fill;
            LockedNum = lockup;
            GenNum = fill;
        }

        public void UpdateTaken(char num)
        {
            if (!(this.TakenNum.Contains(num)))
            {
                TakenNum.Add(num);
            }
        }

    }
}
