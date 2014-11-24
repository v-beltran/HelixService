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
    /// Named actions to easily indicate what type
    /// of transaction will be committed to an object.
    /// </summary>
    public enum ObjectState
    {
        Unchanged = 0,
        ToBeInserted = 1,
        ToBeUpdated = 2,
        ToBeDeleted = 3
    }

    public enum WeekOfMonth
    {
        First = 0,
        Second = 1,
        Third = 2,
        Fourth = 3,
        Last = 4
    }
}
