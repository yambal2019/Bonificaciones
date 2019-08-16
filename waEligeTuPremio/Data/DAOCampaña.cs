using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Data
{
    public partial class DAOCampaña : BaseData
    {
        public DAOCampaña()
        {

        }


        public static DataTable PremioPorCampaña(Int32 intCampaña)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "[rptPedidoPorCampaña]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@intCampaña", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, intCampaña));


            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            DataTable dt = new DataTable("TBCampaña");
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
            try
            {

                staticConnection.Open();
                sqlAdapter.Fill(dt);

             
                return dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                staticConnection.Close();
                command.Dispose();
            }
        }


        public static List<TBCampañaModel> SelectAll()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "TBCampaña_SelectAll";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            DataTable dt = new DataTable("TBCampaña");
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
            try
            {

                staticConnection.Open();
                sqlAdapter.Fill(dt);

                List<TBCampañaModel> objList = new List<TBCampañaModel>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        TBCampañaModel retObj = new TBCampañaModel();
                        retObj.intCampaña = Convert.IsDBNull(row["intCampaña"]) ? (Int32?)null : (Int32?)row["intCampaña"];
                        retObj.vchDescripcion = Convert.IsDBNull(row["vchDescripcion"]) ? null : (string)row["vchDescripcion"];
                        retObj.smintAnio = Convert.IsDBNull(row["smintAnio"]) ? (Int16?)null : (Int16?)row["smintAnio"];
                        retObj.smintCampania = Convert.IsDBNull(row["smintCampania"]) ? (Int16?)null : (Int16?)row["smintCampania"];
               
                        retObj.dttmFechaInicio = Convert.IsDBNull(row["dttmFechaInicio"]) ? (DateTime?)null : (DateTime?)row["dttmFechaInicio"];
                        retObj.dttmFechaFin = Convert.IsDBNull(row["dttmFechaFin"]) ? (DateTime?)null : (DateTime?)row["dttmFechaFin"];
                        retObj.bitEstado = Convert.IsDBNull(row["bitEstado"]) ? (bool?)null : (bool?)row["bitEstado"];
                        objList.Add(retObj);
                    }
                }
                return objList;
            }
            catch
            {
                throw;
            }
            finally
            {
                staticConnection.Close();
                command.Dispose();
            }
        }
    }
}