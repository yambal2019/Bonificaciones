using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using waEligeTuPremio.Models;


namespace waEligeTuPremio.Data
{
    public partial class DAOPremio : BaseData
    {
        public DAOPremio()
        {

        }

        public static List<TBPremioModel> PremioPorIdConsultora(Int32 intCodigoConsultora)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "TBPremio_PorConsultora";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            DataTable dt = new DataTable("tb");
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
            try
            {
                command.Parameters.AddWithValue("@intCodigoConsultora", intCodigoConsultora);

                staticConnection.Open();
                sqlAdapter.Fill(dt);

                List<TBPremioModel> objLista = new List<TBPremioModel>();

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        TBPremioModel obj = new TBPremioModel();

                        obj.intPremio = Convert.ToInt32(row["intPremio"]);
                        obj.intCodigoSAP = Convert.ToInt32(row["intCodigoSAP"]);
                        obj.intCodigoCorto = Convert.ToInt32(row["intCodigoCorto"]);
                        obj.intOrden = Convert.ToInt32(row["intOrden"]);
                        obj.vchTitulo = (String)row["vchTitulo"];
                        obj.vchDescripcion = (String)row["vchDescripcion"];
                        obj.smintStock = Convert.ToInt32(row["smintStock"]);
                        obj.smintStockrReal = Convert.ToInt32(row["smintStockrReal"]);
                        obj.smintPuntos = Convert.ToInt32(row["smintPuntos"]);
                        obj.bitActivo = (Boolean)row["bitActivo"];
                        obj.dttmFecha = (DateTime)row["dttmFecha"];
                        obj.intUsr = Convert.ToInt32(row["intUsr"]);
                        obj.intNivel = Convert.ToInt32(row["intNivel"]);
              
                        obj.intCampaña = Convert.ToInt32(row["intCampaña"]);


                        obj.bitSeleccionado = Convert.ToBoolean(row["bitSeleccionado"]);

                        obj.NombreImagen = row["NombreImagen"].ToString();
                       
                        obj.Imagen = Convert.IsDBNull(row["Imagen"]) ? null : (byte[])row["Imagen"];

                        obj.intPedido = Convert.ToInt32(row["intPedido"]);
                        obj.intPedidoDetalle = Convert.ToInt32(row["intPedidoDetalle"]);

                        objLista.Add(obj);
                    }
                }
                return objLista;
            }
            catch  (Exception ex)
            {
                throw;
            }
            finally
            {
                staticConnection.Close();
                command.Dispose();
            }
        }

        public static List<NivelPremio> ListaNivelPorCampaña(int intCampaña)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "TBPremio_GetNivelesPorCampaña";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            DataTable dt = new DataTable("tb");
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
            try
            {
                command.Parameters.AddWithValue("@intCampaña", intCampaña);

                staticConnection.Open();
                sqlAdapter.Fill(dt);

                List<NivelPremio> objLista = new List<NivelPremio>();

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        NivelPremio obj = new NivelPremio();

                      
                        obj.intNivel = Convert.ToInt32(row["intNivel"]);
                        obj.cantidadNivel = Convert.ToInt32(row["cantidadNivel"]);


                        objLista.Add(obj);
                    }
                }
                return objLista;
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
        }



        public static void Update(TBPremioModel model)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "TBPremio_Update";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            try
            {
                staticConnection.Open();

                command.Parameters.AddWithValue("@intPremio", model.intPremio);
                command.Parameters.AddWithValue("@intCodigoSAP", model.intCodigoSAP);
                command.Parameters.AddWithValue("@intCodigoCorto", model.intCodigoCorto);
                command.Parameters.AddWithValue("@intOrden", model.intOrden);
                command.Parameters.AddWithValue("@vchTitulo", model.vchTitulo);
                command.Parameters.AddWithValue("@vchDescripcion", model.vchDescripcion);
                command.Parameters.AddWithValue("@smintStock", model.smintStock);
                command.Parameters.AddWithValue("@smintStockrReal", model.smintStockrReal);
                command.Parameters.AddWithValue("@smintPuntos", model.smintPuntos);
                command.Parameters.AddWithValue("@bitActivo", model.bitActivo);
                command.Parameters.AddWithValue("@dttmFecha", DateTime.Now);
                command.Parameters.AddWithValue("@intUsr", 1);
                command.Parameters.AddWithValue("@intNivel", model.intNivel);
                command.Parameters.AddWithValue("@bitInicial", model.bitInicial);
                command.Parameters.AddWithValue("@intCampaña", model.intCampaña);

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                command.Dispose();
            }
        }

        public static void Add(TBPremioModel model)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "TBPremio_Insert";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            try
            {
                staticConnection.Open();
                command.Parameters.AddWithValue("@intCodigoSAP", model.intCodigoSAP);
                command.Parameters.AddWithValue("@intCodigoCorto", model.intCodigoCorto);
                command.Parameters.AddWithValue("@intOrden", 0);
                command.Parameters.AddWithValue("@vchTitulo", model.vchTitulo);
                command.Parameters.AddWithValue("@vchDescripcion", model.vchDescripcion);
                command.Parameters.AddWithValue("@smintStock", model.smintStock);
                command.Parameters.AddWithValue("@smintStockrReal", model.smintStock);
                command.Parameters.AddWithValue("@smintPuntos", model.smintPuntos);
                command.Parameters.AddWithValue("@bitActivo", model.bitActivo);
                command.Parameters.AddWithValue("@dttmFecha", DateTime.Now);
                command.Parameters.AddWithValue("@intUsr", 1);
                command.Parameters.AddWithValue("@intNivel", model.intNivel);
                command.Parameters.AddWithValue("@bitInicial", model.bitInicial);
                command.Parameters.AddWithValue("@intCampaña", model.SelectedCampañaNuevoEditarId);
               
                command.ExecuteNonQuery();

            }
            catch ( Exception ex)
            {
                throw;
            }
            finally
            {
                staticConnection.Close();
                command.Dispose();
            }
        }

        public static void PremioEliminar(int intPremio)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "TBPremio_Delete";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            try
            {
                staticConnection.Open();
                command.Parameters.AddWithValue("@intPremio", intPremio);
               

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
        }

        public static TBPremioModel PremioPorIdPremio(int intPremio)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "TBPremio_SelectPorintPremio";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            TBPremioModel obj = new TBPremioModel();

            DataTable dt = new DataTable("tb");
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
            try
            {
                command.Parameters.AddWithValue("@intPremio", intPremio);

                staticConnection.Open();
                sqlAdapter.Fill(dt);

              

                if (dt.Rows.Count > 0)
                {
                    obj = CreateTBPremioFromDataRowShared(dt.Rows[0]);

                }



                return obj;
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
        }


        private static TBPremioModel CreateTBPremioFromDataRowShared(DataRow dr)
        {
            TBPremioModel objTBPremio = new TBPremioModel();

            objTBPremio.intPremio = (int)dr["intPremio"];
            objTBPremio.intCodigoSAP = (int)dr["intCodigoSAP"];
            objTBPremio.intCodigoCorto = (int)dr["intCodigoCorto"];
            objTBPremio.intOrden = (int)dr["intOrden"];
            objTBPremio.vchTitulo = dr["vchTitulo"].ToString();
            objTBPremio.vchDescripcion = dr["vchDescripcion"].ToString();
            objTBPremio.smintStock = (Int32)dr["smintStock"];
            objTBPremio.smintStockrReal = (Int32)dr["smintStockrReal"];
            objTBPremio.smintPuntos = (Int32)dr["smintPuntos"];
            if (dr["bitActivo"] != System.DBNull.Value)
                objTBPremio.bitActivo = (bool)dr["bitActivo"];
            else
                objTBPremio.bitActivo = false;
            objTBPremio.dttmFecha = (DateTime)dr["dttmFecha"];
            objTBPremio.intUsr = (int)dr["intUsr"];

            if (dr["intNivel"] != System.DBNull.Value)
                objTBPremio.intNivel = (int)dr["intNivel"];
            //else
            //    objTBPremio.intNivel = null;

            if (dr["bitInicial"] != System.DBNull.Value)
                objTBPremio.bitInicial = (bool)dr["bitInicial"];
            else
                objTBPremio.bitInicial = false;

            if (dr["intCampaña"] != System.DBNull.Value)
                objTBPremio.intCampaña = (int)dr["intCampaña"];
            else
                objTBPremio.intCampaña = null;

            objTBPremio.AniosNuevoEditarId =Convert.ToInt32( dr["smintAnio"]);

            return objTBPremio;
        }

        public static List<TBPremioModel> ListaPremioPorCampaña(Int32 intCampaña)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "TBPremio_PorCampaña";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            DataTable dt = new DataTable("tb");
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
            try
            {
                command.Parameters.AddWithValue("@intCampaña", intCampaña);

                staticConnection.Open();
                sqlAdapter.Fill(dt);

                List<TBPremioModel> objLista = new List<TBPremioModel>();

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        TBPremioModel obj = new TBPremioModel();

                        obj.intPremio = Convert.ToInt32(row["intPremio"]);
                        obj.intCodigoSAP = Convert.ToInt32(row["intCodigoSAP"]);
                        obj.intCodigoCorto = Convert.ToInt32(row["intCodigoCorto"]);
                        obj.intOrden = Convert.ToInt32(row["intOrden"]);
                        obj.vchTitulo = (String)row["vchTitulo"];
                        obj.vchDescripcion = (String)row["vchDescripcion"];
                        obj.smintStock = Convert.ToInt32(row["smintStock"]);
                        obj.smintStockrReal = Convert.ToInt32(row["smintStockrReal"]);
                        obj.smintPuntos = Convert.ToInt32(row["smintPuntos"]);
                        obj.bitActivo = (Boolean)row["bitActivo"];
                        obj.dttmFecha = (DateTime)row["dttmFecha"];
                        obj.intUsr = Convert.ToInt32(row["intUsr"]);
                        obj.intNivel = Convert.ToInt32(row["intNivel"]);

                        obj.intCampaña = Convert.ToInt32(row["intCampaña"]);


                        obj.bitSeleccionado = Convert.ToBoolean(row["bitSeleccionado"]);

                        obj.NombreImagen = row["NombreImagen"].ToString();

                        //if (row["Imagen"]!=null)
                        //{
                        //    obj.Imagen = (byte[])row["Imagen"];
                        //}

                        obj.Imagen = Convert.IsDBNull(row["Imagen"]) ? null : (byte[])row["Imagen"];

                        //obj.intPedido = Convert.ToInt32(row["intPedido"]);
                        //obj.intPedidoDetalle = Convert.ToInt32(row["intPedidoDetalle"]);

                        objLista.Add(obj);
                    }

                   
                }



                return objLista;
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
        }
    }
}