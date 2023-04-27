using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WebAPI_example.Models;

namespace WebAPI_example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IRepository repository;
        public ReservationController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<Reservation> Get() => repository.Reservations;

        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value mast be passed in the requests body");
            }
            else return Ok(repository[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Reservation res)
        {
            if(!Authenticate()) { return Unauthorized(); }
            return Ok(repository.AddReservation(new Reservation { Name = res.Name, StartLocation = res.StartLocation, EndLocation = res.EndLocation }));
        }

        bool Authenticate()
        {
            var allowKeys = new[] { "secret@123", "VictoriasSecret" };
            StringValues key = Request.Headers["Key"];
            int count = (from t in allowKeys where t == key select t).Count();
            return count == 0 ? false : true;
        }

        [HttpPut]
        public Reservation Put([FromForm] Reservation res) => repository.UpdateReservation(res);

        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReservation(id);
    }
}
