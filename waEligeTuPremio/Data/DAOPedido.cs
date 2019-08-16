using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Data
{
    public partial class DAOPedido : BaseData
    {
        public DAOPedido()
        {

        }

        public static IList<TBPedidoModel> SelectPorIdConsultora(Int32 intCodigoConsultora)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "[dbo].[TBPedido_SelectPorIdConsultora]";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            DataTable dt = new DataTable("tb");
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
            try
            {
                command.Parameters.AddWithValue("@intCodigoConsultora", intCodigoConsultora);
                //command.Parameters.Add(new SqlParameter("@intCodigoConsultora", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, (object)idEntidad ?? (object)DBNull.Value));

                staticConnection.Open();
                sqlAdapter.Fill(dt);

                List<TBPedidoModel> objList = new List<TBPedidoModel>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        TBPedidoModel obj = new TBPedidoModel();

                        obj.intPedido = (Int32)row["intPedido"];
                        obj.intCodigoConsultora = Convert.IsDBNull(row["intCodigoConsultora"]) ? (Int32?)null : (Int32?)row["intCodigoConsultora"];
                        //obj.smintNano = Convert.IsDBNull(row["smintNano"]) ? (Int32?)null : (Int32?)row["smintNano"];
                        //obj.smintCampana = Convert.IsDBNull(row["smintCampana"]) ? (Int32?)null : (Int32?)row["smintCampana"];
                        //obj.smintSemana = Convert.IsDBNull(row["smintSemana"]) ? (Int32?)null : (Int32?)row["smintSemana"];
                       // obj.smintPuntosPedido = Convert.IsDBNull(row["smintPuntosPedido"]) ? (Int32?)null : (Int32?)row["smintPuntosPedido"];
                        if (!Convert.IsDBNull(row["smintPuntosPedido"]))
                        {
                            obj.smintPuntosPedido = Convert.ToInt32( row["smintPuntosPedido"]);
                        }

                        obj.dttmFecha= Convert.IsDBNull(row["dttmFecha"]) ? (DateTime?)null : (DateTime?)row["dttmFecha"];
                        obj.vchEstadoDescarga = Convert.IsDBNull(row["vchEstadoDescarga"]) ? null : (string)row["vchEstadoDescarga"];
                        obj.intCampaña= Convert.ToInt32(row["intCampaña"]);


                        //retObj.IdAplicacion = Convert.IsDBNull(row["IdAplicacion"]) ? (Int32?)null : (Int32?)row["IdAplicacion"];
                        //retObj.Aplicacion = Convert.IsDBNull(row["Aplicacion"]) ? null : (string)row["Aplicacion"];
                        //retObj.IdEntidad = Convert.IsDBNull(row["IdEntidad"]) ? (Int32?)null : (Int32?)row["IdEntidad"];
                        //retObj.Entidad = Convert.IsDBNull(row["Entidad"]) ? null : (string)row["Entidad"];

                        //retObj.IdCriticidad = Convert.IsDBNull(row["IdCriticidad"]) ? (Int32?)null : (Int32?)row["IdCriticidad"];
                        //retObj.ITOwner = Convert.IsDBNull(row["ITOwner"]) ? (Int32?)null : (Int32?)row["ITOwner"];
                        //retObj.Estado = Convert.IsDBNull(row["Estado"]) ? (bool?)null : (bool?)row["Estado"];
                        //retObj.FechaCreacion = Convert.IsDBNull(row["FechaCreacion"]) ? (DateTime?)null : (DateTime?)row["FechaCreacion"];
                        
                        objList.Add(obj);
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

        public static Int32 InsertPedido_DetallePedido(Int32 intCodigoConsultora)
        {
            Int32 Error = 0;

            //TBPedido_InsertPedido_PedidoDetalle


            SqlCommand command = new SqlCommand();
            command.CommandText = "TBPedido_InsertPedido_PedidoDetalle";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection objConnection = new SqlConnection(StaticConnectionString);
            command.Connection = objConnection;

            objConnection.Open();

            try
            {
                command.Parameters.AddWithValue("@intCodigoConsultora", intCodigoConsultora);
                SqlParameter ret = command.Parameters.Add("@intError", SqlDbType.Int);
                ret.Direction = ParameterDirection.Output;
                ret.Value = 0;

                command.ExecuteNonQuery();

                Error = Convert.ToInt32(command.Parameters["@intError"].Value) ;

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