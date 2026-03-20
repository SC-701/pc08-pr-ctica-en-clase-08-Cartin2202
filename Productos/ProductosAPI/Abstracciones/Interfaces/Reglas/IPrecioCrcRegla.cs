using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Reglas
{
    public interface IPrecioCrcRegla
    {
        Task<decimal> ObtenerTipoCambio(ProductoResponse producto);
    }
}
