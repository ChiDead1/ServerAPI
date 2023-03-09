using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NovelLibaryController : ControllerBase
    {
        private static readonly string[] Genre = new[]
        {
        "Wuxia", "Xianxia", "Xuanhuan", "Action", "Horror", "Romance", "Fantasy", "Mecha", "Historical", "Drama" 
    };

        private static readonly string[] NovelName = new[]
{
        "ISSTH", "Harry Potter", "RenedgeImmortal", "Returnee", "Perfect World", "World Eater", "Hobbit", "PJATO", "A song of Ice and FIre", "Coiling Dragon" };



        private readonly ILogger<NovelLibaryController> _logger;
        

        public NovelLibaryController(ILogger<NovelLibaryController> logger)
        {
            _logger = logger;
        }
       

        [HttpGet]
        public IEnumerable<NovelLibary> Get()
        {
            return Enumerable.Range(1, 10).Select(index => new NovelLibary
            {
               // Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Chapters = Random.Shared.Next(1,2000), 
                Summary = Genre[Random.Shared.Next(Genre.Length)],
                SumName = NovelName[Random.Shared.Next(NovelName.Length)]
            })
            .ToArray();
        }
    }
}