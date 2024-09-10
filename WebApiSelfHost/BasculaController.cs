using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiSelfHost.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class BasculaController : ApiController
    {

        // GET: api/Bascula
        public double GetLeerBascula()
        {

            double Peso = 0;

            BasculasLibrary.LeerPuerto leer = new BasculasLibrary.LeerPuerto();

            leer.AbrirPuerto();
            do
            {
                Peso = leer.leerDatos();
            } while (Peso == 0);


            leer.CerrarPuerto();

            //return 24;

            return Peso;

        }

        // GET: api/Bascula/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Bascula
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Bascula/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bascula/5
        public void Delete(int id)
        {
        }
    }
}
