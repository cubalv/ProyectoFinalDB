using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.ContratosClientes;

namespace Negociacion
{
    public class contratoClienteNegocios
    {
        LeerContratoClientes LeerContratos = new LeerContratoClientes();
        GuardarContratoClientes GuardarContratos = new GuardarContratoClientes();
        public bool verificarDisponibilidadZona(int codigoZona, int codigoPlan)
        {
            return LeerContratos.verificarDisponibilidadZona(codigoZona,codigoPlan);
        }
    }
}
