using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace IASFDCI.Connection
{
    class ConProb
    {
        public static string strConnect = "Data Source=LAPTOP-7GVU7R7S\\SQLEXPRESS;Initial Catalog=DB_IASFDKI;Integrated Security=True";
        public static string sql = string.Empty;
        public static SqlConnection con = new SqlConnection(strConnect);
        public static SqlCommand cmd = default(SqlCommand);

        public static DataTable ExecuteSQL(SqlCommand com)
        {
            SqlDataAdapter adapter = default(SqlDataAdapter);
            DataTable dt = new DataTable();

            try
            {
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = com;
                adapter.Fill(dt);

                return dt;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Ошибка поключения к базе данных: " + ex.Message,
                    "Ошибка подключения SQL Server", MessageBoxButtons.OK);
                dt = null;
            }

            return dt;

        }
    }
}
