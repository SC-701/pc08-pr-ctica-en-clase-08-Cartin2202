using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reglas
{
    public class PrecioCrcRegla : IPrecioCrcRegla
    {
        private readonly IPrecioCrcServicio _precioCrcServicio;

        public PrecioCrcRegla(IPrecioCrcServicio precioCrcServicio)
        {
            _precioCrcServicio = precioCrcServicio;
        }

        public async Task<decimal> ObtenerTipoCambio(ProductoResponse producto)
        {
            // 1️. Obtener tipo de cambio desde servicio
            var tipoCambio = await _precioCrcServicio.ObtenerTipoCambio();

            // 2️. Convertir de dolares a colones 
            var precioCrc = producto.Precio * tipoCambio;

            // 3️. Redondear a 2 decimales
            return Math.Round(precioCrc, 2);
        }
    }
}