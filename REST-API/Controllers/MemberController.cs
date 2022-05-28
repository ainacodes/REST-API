using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using REST_API.Services;

namespace REST_API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class MemberController : Controller
    {
       
        public static List<Member> memberList = new List<Member>();

        private DatabaseService dbService { get; }

        // Constructor
        public MemberController(DatabaseService service)
        {
            dbService = service;
        }

        // GET: api/<MemberController>
        [HttpGet]
        public IEnumerable<Member> Get()
        {
            return dbService.GetMemberList(); //new string[] { "value1", "value2" };
        }

        // GET api/<MemberController>/5

        // Get by name
        [HttpGet("{name}")]
        public Member Get(string name)
        {
            return dbService.GetMemberByName(name);
        }

        // Get by email
        [HttpGet]
        [Route("email/{email}")]
        public Member GetbyEMAIL(string email)
        {
            return dbService.GetMemberByEmail(email);

        }

        // Get by id
        [HttpGet]
        [Route("id/{id}")]
        public Member GetbyId(string id)
        {
            return dbService.GetMemberById(id);

        }


        // POST api/<MemberController>
        [HttpPost]
        public void Post([FromBody] Member value)
        {
            dbService.InsertNewMember(value);
        }

        // PUT api/<MemberController>/5
        [HttpPut("{Name}")]
        public void Put(string Name, [FromBody] Member editMember)
        {
            dbService.UpdateMember(editMember);
        }

        // DELETE api/<MemberController>/5
        [HttpDelete("{MemberID}")]
        public void Delete(int MemberID)
        {
            dbService.DeleteMember(MemberID);
        }
    }
}
