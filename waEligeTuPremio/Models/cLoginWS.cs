using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using waEligeTuPremio.srLoginYanbal;

namespace waEligeTuPremio.Models
{
    public class cLoginWS
    {
        public IntegracionWSResp getUsuario(string tipo, string usr, string contrasenia)
        {
            IntegracionWSResp Login = wsGetUsurio(tipo, usr, contrasenia);
            return Login;
        }

        public IntegracionWSResp wsGetUsurio(string tipoUsr, string usr, string contrasenia)
        {
            waEligeTuPremio.srLoginYanbal.WSMantenimientoUsuariosClient wsMusr = new WSMantenimientoUsuariosClient();
            IntegracionWSReq WSReq = new IntegracionWSReq();

            Cabecera c = new Cabecera();
            c.codigoInterfaz = "CVALUSR";
            c.usuarioAplicacion = "1405365308";
            c.codigoAplicacion = "ARM";
            c.codigoPais = "PER";

            List<CodigoPaisOD> words = new List<CodigoPaisOD>();
            CodigoPaisOD paisOD = new CodigoPaisOD();
            paisOD.valor = "PER";
            words.Add(paisOD);
            c.codigosPaisOD = words.ToArray();
            WSReq.cabecera = c;

            Detalle d = new Detalle();
            Parametros pp = new Parametros();
            pp.tipoUsuario = tipoUsr;
            pp.usuario = usr;
            pp.password = contrasenia;
            d.parametros = pp;
            WSReq.detalle = d;

            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);

            return wsMusr.validaUsuariosObj(WSReq);
        }
    }
}