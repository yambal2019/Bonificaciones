using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using waEligeTuPremio.Models;


namespace waEligeTuPremio.Data
{
    public partial class DAOImage : BaseData
    {
        public DAOImage()
        {

        }

        public static void Update(TBImage objTImagen)
        {
            string storedProcName = "TImagen_Update";
            InsertUpdate(objTImagen, true, storedProcName);
        }

        private static int InsertUpdate(TBImage objTImagen, bool isUpdate, string storedProcName)
        {
            int newlyCreatedIntImagen = objTImagen.intImagen;
            SqlCommand command = new SqlCommand();
            command.CommandText = "TImagen_Update";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;
            try
            {
                staticConnection.Open();
                // parameters
                //if (isUpdate)
                //{
                //    // for update only
                //    command.Parameters.AddWithValue("@intImagen", objTImagen.intImagen);
                //}

                command.Parameters.AddWithValue("@vchNombre", objTImagen.vchNombre);
                command.Parameters.AddWithValue("@vchExtencion", objTImagen.vchExtencion);
                command.Parameters.AddWithValue("@vchImagen", objTImagen.vchImagen);
                command.Parameters.AddWithValue("@intPremio", objTImagen.intPremio);

                if (isUpdate)
                    command.ExecuteNonQuery();
               

            }
            catch (Exception ex)
            {

                throw;
            }

            finally
            {
                staticConnection.Close();
                command.Dispose();
            }


            return newlyCreatedIntImagen;
        }
    }
}