using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;//se agrego
using APISchool.Models;//se agrego


namespace APISchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        public readonly DB_API_SCHOOLContext _dbcontext;

        public ContactController(DB_API_SCHOOLContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //Metodo que muestra el listado de contactos
        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {
            List<Contact> contactlist = new List<Contact>();

            try
            {
                contactlist = _dbcontext.Contacts.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = contactlist });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = contactlist });
            }
        }

        //Metodo busqueda por Id estudiante 
        [HttpGet]
        [Route("{idStudent:int}")]
        public IActionResult GetById(int idStudent)
        {
            List<Contact> ListContactsStudnt = new List<Contact>();
            

            try
            {
                    ListContactsStudnt = _dbcontext.Contacts.Where(c => c.IdStudent == idStudent).ToList();
                //lista = _dbcontext.Alumnos.Include(c => c.Contactos).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = ListContactsStudnt });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = ListContactsStudnt });
            }
        }

        //Metodo busqueda estudiante por texto 
        [HttpGet]
        [Route("Search/{name}")]
        public IActionResult GetByName(string? name)
        {

            List<Contact> ResContacts= new List<Contact>();

            //Buscar en multiples columnas
            ResContacts = _dbcontext.Contacts.Where(a => a.Name.Contains(name) || a.LastName.Contains(name) || a.SecLastname.Contains(name)).ToList();

            //Tambien funciona
            /*ResAlumnos = (from a in _dbcontext.Alumnos
                              where a.Nombre.Contains(nombre) || 
                              a.ApPat.Contains(nombre) ||
                              a.ApMat.Contains(nombre)
                              select a).ToList();
            */

            if (ResContacts == null)
            {
                return BadRequest("No se encontraron registros");
            }

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = ResContacts });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = ResContacts });
            }
        }


        //Metodo para insertar un nuevo contacto
        [HttpPost]
        [Route("NewContact")]
        public IActionResult Add([FromBody] Contact objeto)
        {
            try
            {

                _dbcontext.Contacts.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Contacto guardado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        //Metodo para editar un contacto
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit([FromBody] Contact objeto)
        {
            Contact oContact = _dbcontext.Contacts.Find(objeto.IdContact);

            if (oContact == null)
            {
                return BadRequest("Estudiante no encontrado");
            }

            try
            {
                oContact.Name = objeto.Name is null ? oContact.Name : objeto.Name;
                oContact.LastName = objeto.LastName is null ? oContact.LastName : objeto.LastName;
                oContact.SecLastname = objeto.SecLastname is null ? oContact.SecLastname : objeto.SecLastname;              
                oContact.Gender = objeto.Gender is null ? oContact.Gender : objeto.Gender;
                oContact.Relationship = objeto.Relationship is null ? oContact.Relationship : objeto.Relationship;
                oContact.Email = objeto.Email is null ? oContact.Email : objeto.Email;
                oContact.HomePhone = objeto.HomePhone is null ? oContact.HomePhone : objeto.HomePhone;
                oContact.CellPhone = objeto.CellPhone is null ? oContact.CellPhone : objeto.CellPhone;


                _dbcontext.Contacts.Update(oContact);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos actualizados exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        //Metodo para eliminar un contacto
        [HttpDelete]
        [Route("Delete/{idContact:int}")]
        public IActionResult Delete(int idContact)
        {
            Contact oContact = _dbcontext.Contacts.Find(idContact);

            if (oContact == null)
            {
                return BadRequest("Contacto no encontrado");
            }

            try
            {

                _dbcontext.Contacts.Remove(oContact);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos eliminados exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
