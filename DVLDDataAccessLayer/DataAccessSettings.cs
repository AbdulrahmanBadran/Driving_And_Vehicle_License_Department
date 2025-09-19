using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    internal class DataAccessSettings
    {
        public static string connectionString()
            {
         string connectionString = "server=.;Database=DVLD;user ID=sa;password=sa123456";
        return connectionString;
        }

}

    }

