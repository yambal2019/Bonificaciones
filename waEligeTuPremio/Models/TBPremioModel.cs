using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace waEligeTuPremio.Models
{
    public class TBPremioModel
    {
        public TBPremioModel()
        {

            ListaNivelPremio = new List<NivelPremio>();
            ListaPremio = new List<TBPremioModel>();
        }

        public Int32 intPremio { get; set; }
        public Int32? intCodigoSAP { get; set; }
        public Int32? intCodigoCorto { get; set; }
        public Int32? intOrden { get; set; }
        public String vchTitulo { get; set; }
        public String vchDescripcion { get; set; }
        public Int32? smintStock { get; set; }
        public Int32? smintStockrReal { get; set; }
        public Int32? smintPuntos { get; set; }
        public Boolean bitActivo { get; set; }
        public DateTime? dttmFecha { get; set; }
        public Int32? intUsr { get; set; }
        public Int32 intNivel { get; set; }
        public Boolean bitInicial { get; set; }
        public Int32? intImage { get; set; }
        public Int32? intCampaña { get; set; }

        //complemento
        public Boolean bitSeleccionado { get;  set; }
        public String NombreImagen { get;  set; }
        public byte[] Imagen { get;  set; }

        public List<NivelPremio> ListaNivelPremio { get; set; }

        public Int32 intPedido { get; set; }
        public Int32 intPedidoDetalle { get; set; }

        public int SelectedCampañaId { get; set; }
        public SelectList ListaCampaña { get; set; }
        public int AniosId { get; set; }
        public SelectList ListaAnios { get; set; }
        public List<TBPremioModel> ListaPremio { get; set; }

    }

    public class NivelPremio
    {
        public int intNivel { get; set; }
        public int cantidadNivel { get; set; }
    }

    public class Añio
    {
        public String id { get; set; }
        public String Anio { get; set; }
    }

}