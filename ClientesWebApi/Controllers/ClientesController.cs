using ClientesWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClientesWebApi.Controllers
{
    public class ClientesController : ApiController
    {

        private ClienteDBContext context;

        public ClientesController()
        {
            this.context = new ClienteDBContext();
        }


        List<Cliente> clientes = new List<Cliente>()
        {
            new Cliente() { Nombre="Juan", Apellido="suazo", Edad=88, ID=1 },
            new Cliente() { Nombre="marcelo", Apellido="vergara", Edad=37, ID=2 },
            new Cliente() { Nombre="rodrigo", Apellido="ruiz", Edad=29, ID=3 }
        };


        public IEnumerable<Object> get()
        {
            return context.Clientes.Include("Categoria").Select(c => new
            {
                Id = c.ID,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Edad = c.Edad,
                Categoria = new {
                     Nombre = c.Categoria.Nombre
                },
                Gustos = c.Gustos.Select(g=> new
                {
                    ID = g.ID,
                    Nombre  = g.Nombre
                })
            });
        }
        //api/clientes/{id}
        public IHttpActionResult get(int id)
        {
            Cliente cliente = context.Clientes.Find(id);

            if(cliente == null)//404 notfound
            {
                return NotFound();
            }


            return Ok(cliente);//retornamos codigo 200 junto con el cliente buscado
        }

        //api/clientes
        public IHttpActionResult post(Cliente cliente)
        {

            context.Clientes.Add(cliente);
            int filasAfectadas = context.SaveChanges();

            if(filasAfectadas == 0)
            {
                return InternalServerError();//500
            }

            return Ok(new { mensaje ="Agregado correctamente" });

        }


        //api/clientes/{id}
        public  IHttpActionResult delete(int id)
        {
            //buscamos el cliente a eliminar
            Cliente cliente = context.Clientes.Find(id);

            if (cliente == null) return NotFound();//404

            context.Clientes.Remove(cliente);

            if(context.SaveChanges() > 0)
            {
                //retornamos codigo 200
                return Ok(new { Mensaje = "Eliminado correctamente" });
            }

            return InternalServerError();//500
            
        }

        public IHttpActionResult put(Cliente cliente)
        {
            context.Entry(cliente).State = System.Data.Entity.EntityState.Modified;

            if (context.SaveChanges() > 0)
            {
                return Ok(new { Mensaje = "Modificado correctamente" });
            }

            return InternalServerError();



        }

    }
}
