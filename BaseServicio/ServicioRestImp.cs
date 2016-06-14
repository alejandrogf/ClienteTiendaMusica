using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BaseServicio
{
    public class ServicioRestImpl<TModelo> : IServiciosRest<TModelo>
    {
        private String url;
        private bool auth;
        private String user;
        private String pass;

        public ServicioRestImpl(String url, bool auth = false, String user = null, String pass = null)
        {
            this.url = url;
            this.auth = auth;
            this.user = user;
            this.pass = pass;
        }

        public async Task<TModelo> Add(TModelo model)
        {
            var datos = Serializacion<TModelo>.Serializar(model);
            using (var handler = new HttpClientHandler())
            {
                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }
                using (var client = new HttpClient(handler))
                {
                    var contenido = new StringContent(datos);
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var r = await client.PostAsync(new Uri(url), contenido);
                    if (!r.IsSuccessStatusCode)
                        throw new Exception("Fallo Add - Cliente");
                    var objSerializado = await r.Content.ReadAsStringAsync();
                    return Serializacion<TModelo>.Deserializar(objSerializado);
                }
            }
        }

        public async Task Update(TModelo model)
        {
            var datos = Serializacion<TModelo>.Serializar(model);
            using (var handler = new HttpClientHandler())
            {
                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }
                using (var client = new HttpClient(handler))
                {
                    var contenido = new StringContent(datos);
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var r = await client.PutAsync(new Uri(url), contenido);
                    if (!r.IsSuccessStatusCode)
                        throw new Exception("Fallo Update - Cliente");
                }
            }
        }
        public async Task Delete(TModelo model)
        {
            var datos = Serializacion<TModelo>.Serializar(model);
            using (var handler = new HttpClientHandler())
            {
                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }
                using (var client = new HttpClient(handler))
                {
                    var contenido = new StringContent(datos);
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var urlaux = url + "/" + datos;
                    var urlD = new Uri(urlaux);

                    
                    var r = await client.DeleteAsync(urlD);
                    if (!r.IsSuccessStatusCode)
                        throw new Exception("Fallo Delete(model) - Cliente");
                }
            }
        }
        public async Task Delete(int id)
        {
            using (var handler = new HttpClientHandler())
            {
                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }
                using (var client = new HttpClient(handler))
                {
                    var r = await client.DeleteAsync(new Uri(url + "/" + id));
                    if (!r.IsSuccessStatusCode)
                        throw new Exception("Fallo Delete(id) - Cliente");
                }
            }
        }

        public List<TModelo> Get(String paramUrl = null)
        {
            List<TModelo> lista;
            var urlDest = url;
            if (paramUrl != null)
                urlDest += paramUrl;

            var request = WebRequest.Create(urlDest);
            if (auth)
            {
                request.Credentials = new NetworkCredential(user, pass);
            }
            request.Method = "GET";
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var serializado = reader.ReadToEnd();
                    lista = Serializacion<List<TModelo>>.Deserializar(serializado);
                }
            }
            return lista;
        }

        public List<TModelo> Get(Dictionary<string, string> param)
        {
            var paramsurl = "";
            var primero = true;
            foreach (var key in param.Keys)
            {
                if (primero)
                {
                    paramsurl += "?";
                    primero = false;
                }
                else
                    paramsurl += "&";
                paramsurl += key + "=" + param[key];
            }

            return Get(paramsurl);
        }

        public TModelo Get(int id)
        {
            TModelo objeto;

            var request = WebRequest.Create(url + "/" + id);
            if (auth)
            {
                request.Credentials = new NetworkCredential(user, pass);
            }
            request.Method = "GET";
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var serializado = reader.ReadToEnd();
                    objeto = Serializacion<TModelo>.Deserializar(serializado);
                }
            }
            return objeto;
        }
    }
}
