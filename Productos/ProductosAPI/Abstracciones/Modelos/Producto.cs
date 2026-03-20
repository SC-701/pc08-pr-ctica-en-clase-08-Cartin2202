using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required(ErrorMessage = "La propiedad de Nombre es requerida")]
        [StringLength(40, ErrorMessage = "La propiedad del nombre debe ser mayor a 4 caracteres y menor a 40", MinimumLength = 1)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La propiedad de Descripción es requerida")]
        [StringLength(40, ErrorMessage = "La propiedad del nombre debe ser mayor a 4 caracteres y menor a 40", MinimumLength = 4)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La propiedad de Precio es requerida")]
        [RegularExpression("^\\d+(,\\d{1,2})?$", ErrorMessage = "El formato del precio no es valido")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La propiedad del Stock es requerida")]
        [RegularExpression("^\\d+$", ErrorMessage = "El formato del precio no es valido")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La propiedad del codigo de barras es requerida")]
        [StringLength(40, ErrorMessage = "La propiedad del nombre debe ser mayor a 4 caracteres y menor a 40", MinimumLength = 5)]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public string SubCategoria { get; set; }
        public string Categoria { get; set; }
    }

    public class ProductoDetalle : ProductoResponse
    {
        public decimal PrecioCrc { get; set; }
    }
}