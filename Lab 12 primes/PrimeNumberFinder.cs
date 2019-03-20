/* PrimeNumberFinder.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.PrimeNumbers
{
    /// <summary>
    /// A collection of methods for finding prime numbers.
    /// </summary>
    public static class PrimeNumberFinder
    {
        /// <summary>
        /// Gets a linked list of the integers strictly between 1 and n.
        /// </summary>
        /// <param name="n">The upper limit.</param>
        /// <returns>A list of the integers strictly between 1 and n.</returns>
        public static LinkedListCell<int> GetNumbersLessThan(int n)
        {
            LinkedListCell<int> list = null;
            for (int i = n - 1; i > 1; i--)
            {
                LinkedListCell<int> cell = new LinkedListCell<int>();
                cell.Data = i;
                cell.Next = list;
                list = cell;
            }
            return list;
        }

        /// <summary>
        /// Removes all multiples of the given int from the linked list beginning after the given cell.
        /// </summary>
        /// <param name="k">The value whose multiples are to be removed.</param>
        /// <param name="list">The cell preceding the first cell to examine.</param>
        public static void RemoveMultiples(int k, LinkedListCell<int> list)
        {
            while (list.Next != null)
            {
                if (list.Next.Data % k == 0)
                {
                    list.Next = list.Next.Next;
                }
                else
                {
                    list = list.Next;
                }
            }
        }

        /// <summary>
        /// Gets a linked list of the prime numbers less than the given value.
        /// </summary>
        /// <param name="n">The upper limit.</param>
        /// <returns>A list of the prime numbers less than n.</returns>
        public static LinkedListCell<int> GetPrimesLessThan(int n)
        {
            LinkedListCell<int> list = GetNumbersLessThan(n);
            LinkedListCell<int> p = list;
            while (p != null && p.Data * p.Data < n)
            {
                RemoveMultiples(p.Data, p);
                p = p.Next;
            }
            return list;
        }
    }
}
