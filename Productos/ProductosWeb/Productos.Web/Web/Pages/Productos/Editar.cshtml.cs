using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        
        [BindProperty]
        public ProductoResponse productoResponse { get; set; }

        [BindProperty]
        public List<SelectListItem> categorias { get; set; }

        [BindProperty]
        public List<SelectListItem> subCategorias { get; set; }

        [BindProperty]
        public Guid categoriaseleccionada { get; set; }
        [BindProperty]
        public Guid subCategoriaseleccionada { get; set; }
        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id==Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints",
    "ObtenerProducto");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                await ObtenerCategorias();

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                productoResponse = JsonSerializer.Deserialize<ProductoResponse>
                    (resultado, opciones);
                if (productoResponse != null)
                {
                    categoriaseleccionada = Guid.Parse(categorias.Where(c => c.Text == productoResponse.Categoria).FirstOrDefault().Value);
                    subCategorias = (await ObtenerSubCategorias(categoriaseleccionada)).Select(c=>
                    new SelectListItem
                    { Value = c.Id.ToString(),
                        Text = c.Nombre,
                        Selected = c.Nombre == productoResponse.SubCategoria

                    }).ToList();
                    subCategoriaseleccionada = Guid.Parse(subCategorias.Where(c => c.Text == productoResponse.SubCategoria).FirstOrDefault().Value);
                }
            }
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            await ObtenerCategorias();

            if (categoriaseleccionada != Guid.Empty)
            {
                subCategorias = (await ObtenerSubCategorias(categoriaseleccionada)).Select(c =>
                    new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nombre
                    }).ToList();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarProductos");
            var cliente = new HttpClient();

            var respuesta = await cliente.PutAsJsonAsync(
                string.Format(endpoint, productoResponse.Id),
                new ProductoRequest
                {
                    Nombre = productoResponse.Nombre,
                    Descripcion = productoResponse.Descripcion,
                    Precio = productoResponse.Precio,
                    Stock = productoResponse.Stock,
                    CodigoBarras = productoResponse.CodigoBarras,
                    IdSubCategoria = subCategoriaseleccionada
                });

            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }

        private async Task ObtenerCategorias()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints",
                "ObtenerCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var resultadodeserializado = JsonSerializer.Deserialize<List<Categorias>>
                (resultado, opciones);
            categorias = resultadodeserializado.Select(c =>
            new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
            }
            ).ToList();
        }
        public async Task<List<SubCategorias>> ObtenerSubCategorias(Guid categoriaID)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints",
                "ObtenerSubCategorias");
            endpoint = string.Format(endpoint, categoriaID);
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<SubCategorias>>
                (resultado, opciones);

        }

        public async Task<JsonResult> OnGetObtenerSubCategorias(Guid categoriaID)
        {
            var subCategorias = await ObtenerSubCategorias(categoriaID);
            return new JsonResult(subCategorias);
        }
    }
}
