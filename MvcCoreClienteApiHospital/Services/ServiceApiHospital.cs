using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using MvcCoreClienteApiHospital.Models;
using System.Net.Http;

namespace MvcCoreClienteApiHospital.Services
{
    public class ServiceApiHospital
    {
        private string url;
        //UN OBJETO EN EL HEADER DE LA PETICION AL API
        //PARA INDICAR QUE ES JSON
        private MediaTypeWithQualityHeaderValue header;

        //OPCION CON LA URL DEL SERVICIO DENTRO DE LA CLASE
        public ServiceApiHospital()
        {
            //LA URL SOLO ES LA DIRECCION WEB DEL ALOJAMIENTO
            this.url = "https://apihospitalesazureandalucia.azurewebsites.net/";
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        //CONSTRUCTOR RECIBIENDO LA URL DEL SERVICIO DESDE Startup
        public ServiceApiHospital(string url)
        {
            this.url = url;
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        //METODOS ASINCRONOS
        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            //UTILIZAMOS UN OBJETO HttpClient PARA LA PETICION
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/hospitales";
                //AÑADIMOS AL OBJETO client LA DIRECCION URL DEL API
                client.BaseAddress = new Uri(this.url);
                //LIMPIAMOS LAS CABECERAS DE LA PETICION
                client.DefaultRequestHeaders.Clear();
                //UTILIZAMOS EL HEADER PARA INDICAR QUE CONSUMIMOS JSON
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //REALIZAMOS LA PETICION CON EL METODO GET
                //Y NOS DEVOLVERA UNA RESPUESTA HttpResponseMessage
                HttpResponseMessage response =
                    await client.GetAsync(request);
                //SI LA RESPUESTA ES CORRECTA, DEVOLVEMOS LOS DATOS
                if (response.IsSuccessStatusCode)
                {
                    //EN LA PROPIEDAD Content DE LA RESPUESTA
                    //VIENEN LOS DATOS
                    //MEDIANTE UN METODO LLAMADO ReadAsAsync
                    //RECUPERAMOS LOS DATOS Y HACE AUTOMATICAMENTE
                    //EL CASTING AL OBJETO
                    List<Hospital> hospitales =
                        await response.Content.ReadAsAsync<List<Hospital>>();
                    return hospitales;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
