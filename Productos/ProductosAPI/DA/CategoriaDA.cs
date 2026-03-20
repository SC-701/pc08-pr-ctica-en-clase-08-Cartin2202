using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class CategoriaDA : ICategoriaDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        #region Constructor
        public CategoriaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        #endregion

        #region Operaciones
        public async Task<IEnumerable<Categorias>> Obtener()
        {
            string query = @"ObtenerCategorias";
            var resultadoConsulta = await _sqlConnection.QueryAsync<Categorias>(query);
            return resultadoConsulta;
        }
        #endregion
    }
}

