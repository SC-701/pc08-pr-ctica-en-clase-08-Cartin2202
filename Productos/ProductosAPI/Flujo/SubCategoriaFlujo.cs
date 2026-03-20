using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Reglas;

namespace Flujo
{
    public class SubCategoriaFlujo : ISubCategoriaFlujo
    {
        private ISubCategoriaDA _subCategoriaDA;

        public SubCategoriaFlujo(ISubCategoriaDA subCategoriaDA)
        {
            _subCategoriaDA = subCategoriaDA;
        }

        public async Task<IEnumerable<SubCategorias>> Obtener(Guid IdCategoria)
        {
            return await _subCategoriaDA.Obtener(IdCategoria);
        }
    }
}