using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServiceNotas
{
    /// <summary>
    /// Descripción breve de LogicaNotas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class LogicaNotas : System.Web.Services.WebService
    {
        NotasXamarinEntities dataContext;
        public LogicaNotas()
        {
            dataContext = new NotasXamarinEntities();
        }

        [WebMethod]
        public List<Nota> AddNote(Nota _nuevaNota)
        {
            try
            {
                dataContext.Nota.Add(_nuevaNota);
                dataContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return GetNotas();
        }
        [WebMethod]
        public List<Nota> GetNotas()
        {
            return dataContext.Nota.ToList();
        }

        [WebMethod]
        public List<Nota> UpdateNota(Nota _nota)
        {
            var element = dataContext.Nota.FirstOrDefault(nota => nota.Id == _nota.Id);
            element.Titulo = _nota.Titulo;
            element.Contenido = _nota.Contenido;
            element.Fecha = _nota.Fecha;

           dataContext.SaveChanges();
            return GetNotas();
        }

        [WebMethod]
        public List<Nota> DeleteNota(int _notaId)
        {
            var element = dataContext.Nota.FirstOrDefault(nota => nota.Id == _notaId);
            dataContext.Nota.Remove(element);
            dataContext.SaveChanges();
            return GetNotas();
        }
    }
}
