using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.ADO
{
    class DBWork
    {
        public SqlConnection connection;

        public DBWork()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EcologyConnectionString"].ConnectionString);
        }


    }
}
