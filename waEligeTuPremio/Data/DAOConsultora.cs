using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Data
{
    public partial class DAOConsultora:BaseData
    {
        public DAOConsultora()
        {

        }

        public static Int32 InsertConsultoraNueva(TConsultora objTConsultora)
        {
            Int32 Error = 0;

       
            SqlCommand command = new SqlCommand();
            command.CommandText = "TConsultora_InsertNoExiste";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection objConnection = new SqlConnection(StaticConnectionString);
            command.Connection = objConnection;

            objConnection.Open();

            try
            {
                command.Parameters.AddWithValue("@vchNombreCompleto", objTConsultora.vchNombreCompleto);
                command.Parameters.AddWithValue("@intCodigoCNS", objTConsultora.intCodigoCNS);
                command.Parameters.AddWithValue("@vchEmail", objTConsultora.vchEmail);
                command.Parameters.AddWithValue("@vchApellido", objTConsultora.vchApellido);
                command.Parameters.AddWithValue("@vchNombre", objTConsultora.vchNombre);


                SqlParameter ret = command.Parameters.Add("@ReturnValue", SqlDbType.Int);
                ret.Direction = ParameterDirection.Output;
                ret.Value = 0;

                command.ExecuteNonQuery();

                Error = Convert.ToInt32(command.Parameters["@ReturnValue"].Value);

            }
            catch (Exception ex)
            {
                Error = 0;
            }
            finally
            {
                command.Dispose();
            }


            return Error;
        }

    }
}
