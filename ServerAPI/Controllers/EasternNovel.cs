using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EasternNovel : ControllerBase
    {
        private static readonly string[] Genre = new[]
        {
        "Wuxia", "Xianxia", "Xuanhuan", "Action", "Horror", "Romance", "Fantasy", "Mecha", "Historical", "Drama"
    };
        private static readonly string[] NovelName = new[]
{
        "ISSTH", "Harry Potter", "RenedgeImmortal", "Returnee", "Perfect World", "World Eater", "Hobbit", "PJATO", "A song of Ice and FIre", "Coiling Dragon" };



        // GET: api/<EasternNovel>
        [HttpGet]
        public IEnumerable<NovelLibary> Get()
        {
            return Enumerable.Range(1, 10).Select(index => new NovelLibary
            {
                // Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Chapters = Random.Shared.Next(1, 2000),
                Summary = Genre[Random.Shared.Next(Genre.Length)],
                SumName = NovelName[Random.Shared.Next(NovelName.Length)]
            })
           .ToArray();
        }

        // GET api/<EasternNovel>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EasternNovel>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EasternNovel>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EasternNovel>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
