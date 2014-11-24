using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelixService.Utility
{
    class HEnumeration
    {

    }

    /// <summary>
    /// Run SQL statements based on the state of an object.
    /// </summary>
    public enum ObjectState
    {
        /// <summary>
        /// Object has been retrieved from database but not edited yet. Nothing will run against the database. 
        /// </summary>
        Unchanged = 0,
        /// <summary>
        /// Object is new and should be added to database.
        /// </summary>
        ToBeInserted = 1,
        /// <summary>
        /// Object has been retrieved from database and at least one property of it has changed.
        /// </summary>
        ToBeUpdated = 2,
        /// <summary>
        /// Object is marked to be deleted from the database.
        /// </summary>
        ToBeDeleted = 3
    }

    /// <summary>
    /// The calendar weeks in the single month.
    /// </summary>
    public enum WeekOfMonth
    {
        /// <summary>
        /// The first week of the month.
        /// </summary>
        First = 0,
        /// <summary>
        /// The second week of the month.
        /// </summary>
        Second = 1,
        /// <summary>
        /// The third week of the month.
        /// </summary>
        Third = 2,
        /// <summary>
        /// The fourth week of the month.
        /// </summary>
        Fourth = 3,
        /// <summary>
        /// The last week of the month.
        /// </summary>
        Last = 4
    }
}
