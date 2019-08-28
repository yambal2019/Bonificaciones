using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Data
{
    public partial class DAOPedidoDetalle : BaseData
    {
        public DAOPedidoDetalle()
        {
        }

        public static Int32 InsertPedidoDetalle(int idPedido, Int32 idNivel, Int32 idPremio)
        {
            Int32 Error = 0;

            SqlCommand command = new SqlCommand();
            command.CommandText = "TBIngresaDetalle";
            command.CommandType = CommandType.StoredProcedure;
         
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            staticConnection.Open();
            try
            {

                command.Parameters.AddWithValue("@intPedido", idPedido);
                command.Parameters.AddWithValue("@intNivel", idNivel);
                command.Parameters.AddWithValue("@intPremio", idPremio);

                SqlParameter ret = command.Parameters.Add("@intError", SqlDbType.Int);
                ret.Direction = ParameterDirection.Output;
                ret.Value = 0;


                command.ExecuteNonQuery();

                Error = Convert.ToInt32(command.Parameters["@intError"].Value);
               // Error = 1;
             
            }
            catch
            {
                
                Error = 0;
            }
            finally
            {
                staticConnection.Close();
                command.Dispose();
            }

            return Error; 
        }

    }
}