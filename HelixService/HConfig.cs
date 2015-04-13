using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelixService.Utility
{
    public class HConfig
    {

        #region " Properties "

        /// <summary>
        /// Backs public property.
        /// </summary>
        private static String _db_connection_string;

        /// <summary>
        /// String to inform application how to connect to a database.
        /// </summary>
        public static String DBConnectionString
        {
            get
            {
                return _db_connection_string;
            }
            set
            {
                _db_connection_string = value;
            }
        }

        #endregion

        #region " Constructors "

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public HConfig()
        {
            
        }

        #endregion

        #region " Load Methods "

        /// <summary>
        /// Initialize app configuration.
        /// </summary>
        /// <param name="cn">Database connection string.</param>
        public static void Load(String cn)
        {
            _db_connection_string = cn;
        }

        #endregion
    }
}
