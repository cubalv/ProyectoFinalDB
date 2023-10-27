using Datos.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Comun;
using Comun.Cache;

namespace Datos.ContratosClientes
{
    public class GuardarContratoClientes
    {
        ConexionLinqSqlDataContext linqConect = new ConexionLinqSqlDataContext(ConexionSql.route);
        public bool guardarContratoNuevo(string dpi, int codigoPlan, int codigoZona,string direccionInstalacion, string direccionCobro, string nit,string detallesContrato,decimal precioMensual,decimal precioInstalacion)
        {
            using(TransactionScope transa = new TransactionScope())
            {
                try
                {
                    contrato_cliente_plan nuevoContrato = new contrato_cliente_plan()
                    {
                        dpi_cliente = dpi,
                        id_plan = codigoPlan,
                        fecha_contratacion = DateTime.Now,
                        direccion_contrato = direccionInstalacion,
                        direccion_de_cobro = direccionCobro,
                        estado_contrato = true,
                        nit_facturacion = nit,
                        id_zona = codigoZona,
                        detalles_contrato = detallesContrato,
                        dpi_empleado = CacheLogin.dpi_usuario,
                        precio_plan = precioMensual
                    };
                    linqConect.contrato_cliente_plan.InsertOnSubmit(nuevoContrato);

                    solicitudes_de_contrato NuevoSolicitud = new solicitudes_de_contrato()
                    {
                        fecha_solicitud=DateTime.Now,
                        id_contrato_cliente_plan=nuevoContrato.id_contrato_cliente_plan,
                        total_pago=precioInstalacion,
                        estado_solicitud=true,
                        concepto_solicitud="Instalacion"
                    };


                    transa.Complete();
                    return true;
                }
                catch
                {
                    transa.Dispose();
                    return false;
                }
            }
        }
    }
}
