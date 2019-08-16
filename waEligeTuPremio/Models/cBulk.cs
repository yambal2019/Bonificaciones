using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class cBulk
    {
        // Crea y prepara un nuevo objeto DbCommand sobre una nueva conexion
        public static DbCommand CreateCommand()
        {
            // Obtain the database provider name
            string dataProviderName = ConfigurationManager.ConnectionStrings["DBPremioConnectionString"].ProviderName;
            // Obtain the database connection string
            string connectionString = ConfigurationManager.ConnectionStrings["DBPremioConnectionString"].ConnectionString;
            // Create a new data provider factory
            DbProviderFactory factory = DbProviderFactories.
            GetFactory(dataProviderName);
            // Obtain a database-specific connection object
            DbConnection conn = factory.CreateConnection();
            // Set the connection string
            conn.ConnectionString = connectionString;
            // Create a database-specific command object
            DbCommand comm = conn.CreateCommand();
            // Set the command type to stored procedure
            comm.CommandType = CommandType.StoredProcedure;
            // Return the initialized command object
            return comm;
        }

        // Ejecuta un update, delete, o insert
        // y retorna el numero de filas afectadas
        public static int ExecuteNonQuery(DbCommand command)
        {
            // El numero de filas afectadas
            int affectedRows = -1;
            // Ejecuta el comando asegurandose que la conexion se cierre al final
            try
            {
                // Abre la conexion del comando
                command.Connection.Open();

                // Ejecuta el comando y obtiene el numero de filas afectadas
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Si hay errores me los envia por correo
                //Utilities.LogError(ex);
                throw;
            }
            finally
            {
                // Cierra la conexion
                command.Connection.Close();
            }
            // Devuelve el numero de filas afectadas
            return affectedRows;
        }

        public static bool CopiarDatosBulkCalendario(
        DataTable dsorigen,
        string nombretabla,
        out string errorcopiar)
        {
            bool result = false;
            errorcopiar = "";
            string connectionString = ConfigurationManager.ConnectionStrings["DBPremioConnectionString"].ConnectionString;
            // Open a connection to the AdventureWorks database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DbCommand comm = CreateCommand();
                // Colocamos el nombre del procedimiento almacenado
                comm.CommandText = "DeleteCalendarioTemp";
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = nombretabla;
                    try
                    {
                        // Write from the source to the destination.
                        ExecuteNonQuery(comm);
                        bulkCopy.WriteToServer(dsorigen);
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        errorcopiar = ex.Message.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }

        public static bool CopiarDatosBulkPremios(
        DataTable dsorigen,
        string nombretabla,
        out string errorcopiar)
        {
            bool result = false;
            errorcopiar = "";
            string connectionString = ConfigurationManager.ConnectionStrings["DBPremioConnectionString"].ConnectionString;
            // Open a connection to the AdventureWorks database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DbCommand comm = CreateCommand();
                // Colocamos el nombre del procedimiento almacenado
                comm.CommandText = "DeletePremioTemp";
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = nombretabla;
                    try
                    {
                        // Write from the source to the destination.
                        ExecuteNonQuery(comm);
                        bulkCopy.WriteToServer(dsorigen);
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        errorcopiar = ex.Message.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }


        public static bool CopiarDatosBulkConsultoras(
            DataTable dsorigen,
            string nombretabla,
            out string errorcopiar)
        {
            bool result = false;
            errorcopiar = "";
            string connectionString = ConfigurationManager.ConnectionStrings["DBPremioConnectionString"].ConnectionString;
            // Open a connection to the AdventureWorks database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DbCommand comm = CreateCommand();
                // Colocamos el nombre del procedimiento almacenado
                comm.CommandText = "DeleteConsultoraTemp";
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = nombretabla;
                    try
                    {
                        // Write from the source to the destination.
                        ExecuteNonQuery(comm);
                        bulkCopy.WriteToServer(dsorigen);
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        errorcopiar = ex.Message.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }

        public static bool CopiarDatosBulkGanadoras(
        DataTable dsorigen,
        string nombretabla,
        out string errorcopiar)
        {
            bool result = false;
            errorcopiar = "";
            string connectionString = ConfigurationManager.ConnectionStrings["DBPremioConnectionString"].ConnectionString;
            // Open a connection to the AdventureWorks database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DbCommand comm = CreateCommand();
                // Colocamos el nombre del procedimiento almacenado
                comm.CommandText = "DeleteGanadorasTemp";
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = nombretabla;
                    try
                    {
                        // Write from the source to the destination.
                        ExecuteNonQuery(comm);
                        bulkCopy.WriteToServer(dsorigen);
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        errorcopiar = ex.Message.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }
    }
}