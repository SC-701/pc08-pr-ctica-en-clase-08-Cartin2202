using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.TipoCambio;
using Google.Api.Ads.Common.Lib;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Servicios
{
    public class PrecioCrcServicio : IPrecioCrcServicio
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _appConfig;

        public PrecioCrcServicio(IConfiguracion configuracion, IHttpClientFactory httpClient, IConfiguration appConfig)
        {
            _configuracion = configuracion;
            _httpClient = httpClient;
            _appConfig = appConfig;
        }

        public async Task<decimal> ObtenerTipoCambio()
        {
            // 1. se lee la Url base desde appsettings
            var endPoint = _configuracion.ObtenerMetodo("ApiEndPointsPrecioCrc", "ObtenerTipoCambio");

            // 2. se lee el Bearer Token desde appsettings
            var token = _appConfig["ApiEndPointsPrecioCrc:BearerToken"];
            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("No se encontró BearerToken en appsettings (ApiEndPointsPrecioCrc:BearerToken).");

            // 3. se construye la URL con fecha de hoy
            var hoy = DateTime.Now.ToString("yyyy/MM/dd");
            var url = $"{endPoint}?fechaInicio={hoy}&fechaFin={hoy}&idioma=ES";

            // 4. se crea el cliente
            var cliente = _httpClient.CreateClient("ServicioPrecioCrc");

            // 5. Agregar header con la autorizacion del bearer
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // 6. se usa el api
            var respuesta = await cliente.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();

            // 7) deserializar
            var json = await respuesta.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<TipoDeCambio>(json, opciones);

            // 8) extraer el valor del tipo de cambio
            var tipoCambio = data?.datos?
                .FirstOrDefault()?
                .indicadores?.FirstOrDefault()?
                .series?.FirstOrDefault()?
                .valorDatoPorPeriodo;

            if (tipoCambio == null || tipoCambio <= 0)
                throw new Exception("No se pudo extraer el tipo de cambio del JSON del BCCR.");

            return Convert.ToDecimal(tipoCambio.Value);
        }
    }
}