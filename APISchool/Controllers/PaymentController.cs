using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;//se agrego
using APISchool.Models;//se agrego

namespace APISchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public readonly DB_API_SCHOOLContext _dbcontext;

        public PaymentController (DB_API_SCHOOLContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //Metodo que muestra el listado de pagos
        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {
            List<Payment> paymentlist = new List<Payment>();

            try
            {
                //paymentlist = _dbcontext.Payments.ToList();
                paymentlist = _dbcontext.Payments.Include(c=> c.oPayStudent).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = paymentlist });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = paymentlist });
            }
        }

        //Metodo busqueda por Id 
        [HttpGet]
        [Route("{idpay:int}")]
        public IActionResult GetById(int idPay)
        {
            Payment oPayment = _dbcontext.Payments.Find(idPay);

            if (oPayment == null)
            {
                return BadRequest("Pago no encontrado");
            }

            try
            {
                oPayment = _dbcontext.Payments.Where(a => a.IdPayment == idPay).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oPayment });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oPayment });
            }
        }


        //Metodo para insertar un nuevo pago
        [HttpPost]
        [Route("NewPayment")]
        public IActionResult Add([FromBody] Payment objeto)
        {
            try
            {
                _dbcontext.Payments.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        //Metodo para editar un pago
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit([FromBody] Payment objeto)
        {
            Payment oPayment = _dbcontext.Payments.Find(objeto.IdPayment);

            if (oPayment == null)
            {
                return BadRequest("Pago no encontrado");
            }

            try
            {
                oPayment.IdStudent = objeto.IdStudent is null ? oPayment.IdStudent : objeto.IdStudent;
                oPayment.Name = objeto.Name is null ? oPayment.Name : objeto.Name;
                oPayment.LastName = objeto.LastName is null ? oPayment.LastName : objeto.LastName;//modificar o quitar
                oPayment.SecLastname = objeto.SecLastname is null ? oPayment.SecLastname : objeto.SecLastname;//modificar o quitar
                oPayment.Amount = objeto.Amount is null ? oPayment.Amount : objeto.Amount;//modificar o quitar
                oPayment.PaymentDate = objeto.PaymentDate is null ? oPayment.PaymentDate : objeto.PaymentDate;//modificar o quitar
                oPayment.RegistratedDate = objeto.RegistratedDate is null ? oPayment.RegistratedDate : objeto.RegistratedDate;//modificar o quitar
                oPayment.Document = objeto.Document is null ? oPayment.Document : objeto.Document;//modificar o quitar

                _dbcontext.Payments.Update(oPayment);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos guardados exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        //Metodo para eliminar pago
        [HttpDelete]
        [Route("Delete/{idpay:int}")]
        public IActionResult Delete(int idPay)
        {
            Payment oPayment = _dbcontext.Payments.Find(idPay);

            if (oPayment == null)
            {
                return BadRequest("Pago no encontrado");
            }

            try
            {

                _dbcontext.Payments.Remove(oPayment);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Pago eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

    }
}
