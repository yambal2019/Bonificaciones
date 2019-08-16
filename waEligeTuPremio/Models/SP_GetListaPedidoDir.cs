using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_GetListaPedidoDir
    {
        public int intCodigoCNS { get; set; }
        public string vchNombreCompleto { get; set; }
        public string vchTelCasa { get; set; }
        public string vchCelular { get; set; }
        public string vchTelTrabajo { get; set; }
        public string vchDireccionL1 { get; set; }
        public string Pedido { get; set; }
        public int intCodigo { get; set; }

    }

    public class SP_GetListaPedidoDirStaff
    {
        public int Codigo_CNS { get; set; }
        public string Nombre { get; set; }
        public string Realizo_Pedido { get; set; }
        public string TelfCasa { get; set; }
        public string Celular { get; set; }
        public string TeflTrabajo { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public int CodigoDir { get; set; }
        public string NombreDir { get; set; }
        public string CelularDir { get; set; }
        public string Zona { get; set; }
        public string Departamento { get; set; }
        public string Ciudad { get; set; }
        public string Distrito { get; set; }
        public string Regional { get; set; }
        public string Divisional { get; set; }
        public string Cod_Coordinador { get; set; }
        public string Nombre_Coordinador { get; set; }

    }
}