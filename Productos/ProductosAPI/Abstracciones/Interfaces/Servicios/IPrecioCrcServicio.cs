using Abstracciones.Modelos.Servicios.TipoCambio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Servicios
{
    public interface IPrecioCrcServicio
    {
        Task<decimal> ObtenerTipoCambio(); //el servicio no recibe el tipo de cambio, ya que el mismo lo consulta a unaa api
    }
}
