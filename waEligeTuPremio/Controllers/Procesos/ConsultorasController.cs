using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Controllers.Procesos
{
    [Authorize]
    public class ConsultorasController : Controller
    {
        // GET: Consultoras
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult ConsultorasIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            string plan = "";
            using (var db = new DBPremioEntities())
            {
                //SP_GetPremio premio = 
                plan = db.Database.SqlQuery<string>("GetPremioPlan").FirstOrDefault();
                //plan = premio.vchPlan;
            }

            if (plan == "")
                return Json("Es obligatorio cargar los premios primero antes que las Consultoras.");

            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase archivo = files[0];

                    if (archivo == null || archivo.ContentLength == 0)
                    {
                        return Json("Seleccione un archivo Excel.");
                    }
                    else
                    {

                        if (archivo.FileName.EndsWith("xls") || archivo.FileName.EndsWith("xlsx"))
                        {
                            string filename = "Ganadoras.xlsx";
                            string absolutePath = Server.MapPath("../Upload/" + filename);

                            if (System.IO.File.Exists(absolutePath))
                                System.IO.File.Delete(absolutePath);

                            archivo.SaveAs(Server.MapPath("../Upload/") + filename);

                            if (System.IO.File.Exists(absolutePath))
                            {
                                string rutaExcel = Server.MapPath("../Upload/" + filename);

                                DataTable dt = new DataTable("Ganadoras");
                                var existingFile = new FileInfo(rutaExcel);
                                using (var package = new ExcelPackage(existingFile))
                                {
                                    ExcelWorkbook workBook = package.Workbook;
                                    if (workBook != null)
                                    {
                                        if (workBook.Worksheets.Count > 0)
                                        {
                                            ExcelWorksheet worksheet = workBook.Worksheets.First();
                                            ExcelCellAddress startCell = worksheet.Dimension.Start;
                                            ExcelCellAddress endCell = worksheet.Dimension.End;

                                            for (int col = startCell.Column; col <= endCell.Column; col++)
                                            {
                                                object col1Header1 = worksheet.Cells[1, col].Value; //1
                                                dt.Columns.Add("" + col1Header1 + "");
                                            }

                                            for (int row = 2; row <= endCell.Row; row++)
                                            {
                                                DataRow dr = dt.NewRow();
                                                int x = 0;
                                                for (int col = startCell.Column; col <= endCell.Column; col++)
                                                {
                                                    if (worksheet.Cells[row, col].Value != null)
                                                        dr[x++] = worksheet.Cells[row, col].Value;
                                                    else
                                                        return Json("Campo vacío en la fila " + row + " columna " + col);
                                                    if (col == 4)
                                                    {
                                                        if (worksheet.Cells[row, col].Value.ToString() != plan)
                                                            return Json("El Plan cargado no Existe. Revise la fila " + row + " columna " + col);
                                                    }
                                                    if (col == 6)
                                                    {
                                                        if (worksheet.Cells[row, col].Value.ToString() != plan)
                                                            return Json("La Regla cargada no coincide con el Plan en la fila " + row + " columna " + col);
                                                    }

                                                    List<int> columnas = new List<int> {1, 2, 3, 7, 8};
                                                    if (columnas.Contains(col))
                                                    {
                                                        int entero = 0;
                                                        bool sw = int.TryParse(worksheet.Cells[row, col].Value.ToString(), out entero);
                                                        if (!sw)
                                                            return Json("Debe ser un número en la fila " + row + " columna " + col);

                                                    }
                                                    // validamos que no se repita la consultora en la Tabla TConsultora
                                                    //if (col == 1)
                                                    //{
                                                    //    using (var db = new DBPremioEntities())
                                                    //    {
                                                    //        var sql = "SELECT COUNT(*) FROM dbo.TGanadoras where intCodigoCNS = " + worksheet.Cells[row, col].Value.ToString();
                                                    //        int total = db.Database.SqlQuery<int>(sql).First();

                                                    //        if (total != 0)
                                                    //            return Json("La consultora con Código " + worksheet.Cells[row, col].Value.ToString() + " ya existe.");

                                                    //        sql = "SELECT COUNT(*) FROM dbo.TConsultora where intCodigoCNS = " + worksheet.Cells[row, col].Value.ToString();
                                                    //        total = db.Database.SqlQuery<int>(sql).First();
                                                    //        if (total == 0)
                                                    //            return Json("La consultora con Código " + worksheet.Cells[row, col].Value.ToString() + " No existe en la Tabla Consultoras. Suba el Archivo excel de las Consultoras.");
                                                    //    }
                                                    //}
                                                }
                                                dt.Rows.Add(dr);
                                            }
                                        }

                                    }
                                }

                                return Json("Status de Importacion: Se subio correctamente el archivo de las Ganadoras.");

                                //string errorbulkcopy;
                                //bool resultadobulkcopy;
                                //resultadobulkcopy = CBulk.CopiarDatosBulkGanadoras(dt, "TGanadorasTemp", out errorbulkcopy);
                                //if (resultadobulkcopy)
                                //{
                                //    return Json("Status de Importacion: Se subio correctamente el archivo de las Ganadoras.");
                                //}
                                //else
                                //{
                                //    return Json("Status de Importacion: Hubo un error al Importar! Verifique archivo de Excel." + errorbulkcopy);
                                //}
                            }
                            else
                            {
                                return Json("El Archivo no Existe.");
                            }
                        }
                        else
                        {
                            return Json("Tipo incorrecto de Archivo.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("Seleccione un Archivo Excel.");
            }
        }

        [HttpPost]
        public ActionResult AgregarGanadoras()
        {
            try
            {
                using (var db = new DBPremioEntities())
                {
                    var sql = "SELECT COUNT(*) FROM dbo.TGanadorasTemp";
                    int total = db.Database.SqlQuery<int>(sql).First();

                    if (total == 0)
                        return Json("Debe subir primero el listado de las Ganadoras con el archivo Excel.");

                    db.Database.ExecuteSqlCommand("InsertGanadoras @intCodigo",
                    new SqlParameter("intCodigo ", int.Parse(HttpContext.User.Identity.Name)));

                    return Json("Se actualizó los datos de las Ganadoras correctamente.");

                }
            }
            catch (Exception)
            {
                return Json("No se puedo Insertar a las Consultoras.");
                throw;
            }

        }

        [HttpPost]
        public ActionResult EliminarGanadoras()
        {
            try
            {
                using (var db = new DBPremioEntities())
                {
                    db.Database.ExecuteSqlCommand("DeleteGanadoras");
                    return Json("Se eliminó correctamente las Ganadoras.");
                }
            }
            catch (Exception)
            {
                return Json("No se pudo eliminar a las Ganadoras.");
                throw;
            }

        }

        [HttpPost]
        public ActionResult UploadFilesCNS()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase archivo = files[0];

                    if (archivo == null || archivo.ContentLength == 0)
                    {
                        return Json("Seleccione un archivo Excel.");
                    }
                    else
                    {
                        if (archivo.FileName.EndsWith("xls") || archivo.FileName.EndsWith("xlsx"))
                        {
                            string filename = "Consultora.xlsx";
                            string absolutePath = Server.MapPath("../Upload/" + filename);

                            if (System.IO.File.Exists(absolutePath))
                                System.IO.File.Delete(absolutePath);

                            archivo.SaveAs(Server.MapPath("../Upload/") + filename);

                            if (System.IO.File.Exists(absolutePath))
                            {
                                string rutaExcel = Server.MapPath("../Upload/" + filename);

                                //DataTable dtCNS = new DataTable("CNS");
                                //var existingFile = new FileInfo(rutaExcel);
                                //using (var package = new ExcelPackage(existingFile))
                                //{
                                //    ExcelWorkbook workBook = package.Workbook;
                                //    if (workBook != null)
                                //    {
                                //        if (workBook.Worksheets.Count > 0)
                                //        {
                                //            ExcelWorksheet worksheet = workBook.Worksheets.First();
                                //            ExcelCellAddress startCell = worksheet.Dimension.Start;
                                //            ExcelCellAddress endCell = worksheet.Dimension.End;

                                //            for (int col = startCell.Column; col <= endCell.Column; col++)
                                //            {
                                //                object col1Header1 = worksheet.Cells[3, col].Value; //1
                                //                dtCNS.Columns.Add("" + col1Header1 + "");
                                //            }

                                //            for (int row = 4; row <= endCell.Row; row++)
                                //            {
                                //                DataRow dr = dtCNS.NewRow();
                                //                int x = 0;
                                //                for (int col = startCell.Column; col <= endCell.Column; col++)
                                //                {
                                //                    dr[x++] = worksheet.Cells[row, col].Value;
                                //                }
                                //                dtCNS.Rows.Add(dr);
                                //            }
                                //        }

                                //    }
                                //}


                                return Json("Status de Importacion: Se subio correctamente el archivo de Consultoras.");

                                //string errorbulkcopy;
                                //bool resultadobulkcopy;
                                //resultadobulkcopy = CBulk.CopiarDatosBulkConsultoras(dt, "TConsultoraTemp", out errorbulkcopy);
                                //if (resultadobulkcopy)
                                //{
                                //    return Json("Status de Importacion: Se subio correctamente el archivo de Consultoras.");
                                //}
                                //else
                                //{
                                //    return Json("Status de Importacion: Hubo un error al Importar! Verifique archivo de Excel." + errorbulkcopy);
                                //}
                            }
                            else
                            {
                                return Json("El Archivo no Existe.");
                            }
                        }
                        else
                        {
                            return Json("Tipo incorrecto de Archivo.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("Seleccione un Archivo Excel.");
            }
        }

        [HttpPost]
        public ActionResult AgregarConsultoras()
        {
            try
            {
                using (var db = new DBPremioEntities())
                {
                    var sql = "SELECT COUNT(*) FROM dbo.TConsultoraTemp";
                    int total = db.Database.SqlQuery<int>(sql).First();

                    if (total == 0)
                        return Json("Debe subir primero el listado de las Consultoras con el archivo Excel.");

                    db.Database.ExecuteSqlCommand("InsertConsultoras @intCodigo",
                    new SqlParameter("intCodigo ", int.Parse(HttpContext.User.Identity.Name)));

                    return Json("Se actualizó los datos de las Consultoras correctamente.");

                }
            }
            catch (Exception ex)
            {
                return Json("No se puedo Insertar a las Consultoras.");
                throw;
            }

        }

        [HttpPost]
        public ActionResult AltaConsultoraPremio()
        {
            //creamos las tablas donde vamos cargas los datos de las consultoras y ganadoras
            DataTable dtCNS = new DataTable("CNS");
            DataTable dtGanadoras = new DataTable("Ganadoras");

            //buscamo el archivo del universo de las consultoras
            string filename = "Consultora.xlsx";
            string rutaExcel = Server.MapPath("../Upload/" + filename);
            if (System.IO.File.Exists(rutaExcel))
            {
                var existingFile = new FileInfo(rutaExcel);
                using (var package = new ExcelPackage(existingFile))
                {
                    ExcelWorkbook workBook = package.Workbook;
                    if (workBook != null)
                    {
                        if (workBook.Worksheets.Count > 0)
                        {
                            ExcelWorksheet worksheet = workBook.Worksheets.First();
                            ExcelCellAddress startCell = worksheet.Dimension.Start;
                            ExcelCellAddress endCell = worksheet.Dimension.End;

                            for (int col = startCell.Column; col <= endCell.Column; col++)
                            {
                                object col1Header1 = worksheet.Cells[3, col].Value; //1
                                dtCNS.Columns.Add("" + col1Header1 + "");
                            }

                            for (int row = 4; row <= endCell.Row; row++)
                            {
                                DataRow dr = dtCNS.NewRow();
                                int x = 0;
                                for (int col = startCell.Column; col <= endCell.Column; col++)
                                {
                                    dr[x++] = worksheet.Cells[row, col].Value;
                                }
                                dtCNS.Rows.Add(dr);
                            }
                        }

                    }
                }
            }
            else
            {
                return Json("El Archivo de las Consultoras no Existe.");
            }

            //buscamo el archivo del universo de las Ganadoras
            filename = "Ganadoras.xlsx";
            rutaExcel = Server.MapPath("../Upload/" + filename);
            if (System.IO.File.Exists(rutaExcel))
            {
                var existingFile = new FileInfo(rutaExcel);
                using (var package = new ExcelPackage(existingFile))
                {
                    ExcelWorkbook workBook = package.Workbook;
                    if (workBook != null)
                    {
                        if (workBook.Worksheets.Count > 0)
                        {
                            ExcelWorksheet worksheet = workBook.Worksheets.First();
                            ExcelCellAddress startCell = worksheet.Dimension.Start;
                            ExcelCellAddress endCell = worksheet.Dimension.End;

                            for (int col = startCell.Column; col <= endCell.Column; col++)
                            {
                                object col1Header1 = worksheet.Cells[1, col].Value; //1
                                dtGanadoras.Columns.Add("" + col1Header1 + "");
                            }

                            for (int row = 2; row <= endCell.Row; row++)
                            {
                                DataRow dr = dtGanadoras.NewRow();
                                int x = 0;
                                for (int col = startCell.Column; col <= endCell.Column; col++)
                                {
                                    dr[x++] = worksheet.Cells[row, col].Value;
                                }
                                dtGanadoras.Rows.Add(dr);
                            }
                        }

                    }
                }
            }
            else
            {
                return Json("El Archivo de las Consultoras no Existe.");
            }

            dtGanadoras.Columns.Add("Cedula");
            dtGanadoras.Columns.Add("PtoEmision");
            dtGanadoras.Columns.Add("NombreCompleto");
            dtGanadoras.Columns.Add("Apellido");
            dtGanadoras.Columns.Add("Nombre");
            dtGanadoras.Columns.Add("SegundoNombre");
            dtGanadoras.Columns.Add("Titulo");
            dtGanadoras.Columns.Add("Email");
            dtGanadoras.Columns.Add("TelCasa");
            dtGanadoras.Columns.Add("Celular");
            dtGanadoras.Columns.Add("TelTrabajo");
            dtGanadoras.Columns.Add("CodDir");
            dtGanadoras.Columns.Add("NombreDir");
            dtGanadoras.Columns.Add("CelDir");
            dtGanadoras.Columns.Add("CodRecomendante");
            dtGanadoras.Columns.Add("NombreRecomendante");
            dtGanadoras.Columns.Add("CelularRecomendante");
            dtGanadoras.Columns.Add("Zona");
            dtGanadoras.Columns.Add("Departamento");
            dtGanadoras.Columns.Add("Ciudad");
            dtGanadoras.Columns.Add("Distrito");
            dtGanadoras.Columns.Add("DireccionL1");
            dtGanadoras.Columns.Add("DireccionL2");
            dtGanadoras.Columns.Add("DireccionL3");
            dtGanadoras.Columns.Add("DireccionL4");
            dtGanadoras.Columns.Add("DireccionL5");
            dtGanadoras.Columns.Add("NIT");
            dtGanadoras.Columns.Add("Regional");
            dtGanadoras.Columns.Add("Divisional");
            dtGanadoras.Columns.Add("CodCoordinador");
            dtGanadoras.Columns.Add("NombreCoordinador");
            dtGanadoras.Columns.Add("CodBanco");
            dtGanadoras.Columns.Add("NombreBanco");
            dtGanadoras.Columns.Add("NroCuenta");
            dtGanadoras.Columns.Add("FechaAlta");


            string codigo;
            foreach (DataRow drG in dtGanadoras.Rows)
            {
                codigo = drG[0].ToString();

                var ResultadoRows = dtCNS.AsEnumerable().Where(r => r.Field<String>("Código").Equals(codigo));

                if (ResultadoRows.Count() == 0)
                {
                    return Json("No existe el Codigo " + codigo + " en el archivo del universo de las Consultaras. ");
                }

                DataRow Resultado = ResultadoRows.FirstOrDefault();

                drG["Cedula"] = Resultado[1];
                drG["PtoEmision"] = Resultado[2];
                drG["NombreCompleto"] = Resultado[3];
                drG["Apellido"] = Resultado[4];
                drG["Nombre"] = Resultado[5];
                drG["SegundoNombre"] = Resultado[6];
                drG["Titulo"] = Resultado[7];
                drG["Email"] = Resultado[8];
                drG["TelCasa"] = Resultado[9];
                drG["Celular"] = Resultado[10];
                drG["TelTrabajo"] = Resultado[11];
                drG["CodDir"] = Resultado[12];
                drG["NombreDir"] = Resultado[13];
                drG["CelDir"] = Resultado[14];
                drG["CodRecomendante"] = Resultado[15];
                drG["NombreRecomendante"] = Resultado[16];
                drG["CelularRecomendante"] = Resultado[17];
                drG["Zona"] = Resultado[18];
                drG["Departamento"] = Resultado[19];
                drG["Ciudad"] = Resultado[20];
                drG["Distrito"] = Resultado[21];
                drG["DireccionL1"] = Resultado[22];
                drG["DireccionL2"] = Resultado[23];
                drG["DireccionL3"] = Resultado[24];
                drG["DireccionL4"] = Resultado[25];
                drG["DireccionL5"] = Resultado[26];
                drG["NIT"] = Resultado[27];
                drG["Regional"] = Resultado[28];
                drG["Divisional"] = Resultado[29];
                drG["CodCoordinador"] = Resultado[30];
                drG["NombreCoordinador"] = Resultado[31];
                drG["CodBanco"] = Resultado[32];
                drG["NombreBanco"] = Resultado[33];
                drG["NroCuenta"] = Resultado[34];
                drG["FechaAlta"] = Resultado[35];
            }


            string errorbulkcopy;
            bool resultadobulkcopy;
            resultadobulkcopy = cBulk.CopiarDatosBulkGanadoras(dtGanadoras, "TGanadorasTemp", out errorbulkcopy);
            if (resultadobulkcopy)
            {
                filename = "Ganadoras.xlsx";
                rutaExcel = Server.MapPath("../Upload/" + filename);
                if (System.IO.File.Exists(rutaExcel))
                    System.IO.File.Delete(rutaExcel);

                filename = "Consultora.xlsx";
                rutaExcel = Server.MapPath("../Upload/" + filename);
                if (System.IO.File.Exists(rutaExcel))
                    System.IO.File.Delete(rutaExcel);

                return Json("Status de Importacion: Se Consolido la información correctamente en la Base de Datos.");
            }
            else
            {
                return Json("Status de Importacion: Hubo un error al Consolidar! Verifique archivo de Excel." + errorbulkcopy);
            }

        }
    }
}