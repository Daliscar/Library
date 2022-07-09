using Library.Models;

namespace Library.Controllers
{
    /// <summary>
    /// Controls interaction with the user resources.
    /// </summary>
    internal static class ResourcesController
    {
        #region Pass Day
        /// <summary>
        /// Pass the day.
        /// </summary>
        /// <param name="currentDay">Current day, passed by value.</param>
        /// <returns>The new day count.</returns>
        internal static int PassDay(int currentDay)
        {
            return currentDay + 1;
        }
        #endregion

        #region Pay Fee
        /// <summary>
        /// Pay the fee for the book rental.
        /// </summary>
        /// <param name="resources">The resources object, passed by refference.</param>
        /// <param name="fee">The calculated fee, to be subtracted from the current money.</param>
        internal static void PayFee(ref ResourcesModel resources, double fee)
        {
            resources.Money -= fee;
        } 
        #endregion
    }
}
