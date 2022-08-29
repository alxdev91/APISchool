using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;//se agrego
using APISchool.Models;//se agrego
using Microsoft.AspNetCore.Authorization;//se agrega para uso de token

namespace APISchool.Controllers
{
    [Route("api/[controller]")]
    [Authorize]//se agrega para uso de token
    [ApiController]
    public class StudentController : ControllerBase
    {
        public readonly DB_API_SCHOOLContext _dbcontext;

        public StudentController(DB_API_SCHOOLContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //Metodo que muestra el listado de estudiantes
        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {
            List<Student> studentlist = new List<Student>();

            try
            {
                studentlist = _dbcontext.Students.ToList();
                //lista = _dbcontext.Alumnos.Include(c => c.Contactos).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = studentlist });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = studentlist });
            }
        }

        //Metodo busqueda por Id 
        [HttpGet]
        [Route("{idStudent:int}")]
        public IActionResult GetById(int idStudent)
        {
            Student oStudent = _dbcontext.Students.Find(idStudent);

            if (oStudent == null)
            {
                return BadRequest("Estudiante no encontrado");
            }

            try
            {
                oStudent = _dbcontext.Students.Where(a => a.IdStudent == idStudent).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oStudent });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oStudent });
            }
        }

        //Metodo busqueda estudiante por texto 
        [HttpGet]
        [Route("Search/{name}")]
        public IActionResult GetByName(string? name)
        {

            List<Student> ResStudents = new List<Student>();

            //Buscar en multiples columnas
            ResStudents = _dbcontext.Students.Where(a => a.Name.Contains(name) || a.LastName.Contains(name) || a.SecLastname.Contains(name)).ToList();

            //Tambien funciona
            /*ResAlumnos = (from a in _dbcontext.Alumnos
                              where a.Nombre.Contains(nombre) || 
                              a.ApPat.Contains(nombre) ||
                              a.ApMat.Contains(nombre)
                              select a).ToList();
            */

            if (ResStudents == null)
            {
                return BadRequest("No se encontraron registros");
            }

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = ResStudents });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = ResStudents });
            }
        }

        //Metodo para insertar un nuevo estudiante
        [HttpPost]
        [Route("NewStudent")]
        public IActionResult Add([FromBody] Student objeto)
        {
            try
            {

                objeto.EnrolDate = DateTime.Now;
                _dbcontext.Students.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Estudiante guardado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        //Metodo para editar un estudiante
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit([FromBody] Student objeto)
        {
            Student oStudent = _dbcontext.Students.Find(objeto.IdStudent);

            if (oStudent == null)
            {
                return BadRequest("Estudiante no encontrado");
            }

            try
            {
                oStudent.Name = objeto.Name is null ? oStudent.Name : objeto.Name;
                oStudent.LastName = objeto.LastName is null ? oStudent.LastName : objeto.LastName;
                oStudent.SecLastname = objeto.SecLastname is null ? oStudent.SecLastname : objeto.SecLastname;
                oStudent.Age = objeto.Age is null ? oStudent.Age : objeto.Age;
                oStudent.Gender = objeto.Gender is null ? oStudent.Gender : objeto.Gender;
                oStudent.Status = objeto.Status is null ? oStudent.Status : objeto.Status;
                //oStudent.EnrolDate = objeto.EnrolDate is null ? oStudent.EnrolDate : objeto.EnrolDate;//modificar o quitar
                //oStudent.FechaBaja = objeto.FechaBaja is null ? oStudent.FechaBaja : objeto.FechaBaja;//modificar o quitar
                //oStudent.UsuAlta = objeto.UsuAlta is null ? oStudent.UsuAlta : objeto.UsuAlta;//modificar o quitar
                //oStudent.UsuBaja = objeto.UsuBaja is null ? oStudent.UsuBaja : objeto.UsuBaja;//modificar o quitar

                _dbcontext.Students.Update(oStudent);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos actualizados exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Delete/{idStudent:int}")]
        public IActionResult Delete(int idStudent)
        {
            Student oStudent = _dbcontext.Students.Find(idStudent);

            if (oStudent == null)
            {
                return BadRequest("Alumno no encontrado");
            }

            try
            {

                _dbcontext.Students.Remove(oStudent);
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
