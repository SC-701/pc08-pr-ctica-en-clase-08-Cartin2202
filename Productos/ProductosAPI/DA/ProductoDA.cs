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
    public class ProductoDA : IProductoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        #region Contructor

        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        #endregion

        #region Operaciones

        public async Task<Guid> Agregar(ProductoRequest producto)
        {
            string query = @"AgregarProductos";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras

            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            await VerificarProductoExiste(Id);
            string query = @"EditarProductos";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras

            });
            return resultadoConsulta;
        }

        

        public async Task<Guid> Eliminar(Guid Id)
        {
            await VerificarProductoExiste(Id);
            string query = @"EliminarProductos";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            string query = @"ObtenerProductos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<ProductoDetalle> Obtener(Guid Id)
        {
            string query = @"ObtenerProducto";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoDetalle>(query, 
                new {Id=Id});
            return resultadoConsulta.FirstOrDefault();
        }
        #endregion
        private async Task VerificarProductoExiste(Guid Id)
        {
            ProductoResponse? resultadoConsultaProducto = await Obtener(Id);
            if (resultadoConsultaProducto == null)
                throw new Exception("El producto no existe");
        }
    }
}

