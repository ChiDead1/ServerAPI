using System.Diagnostics.Metrics;
using System.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ClassLibrary584;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly MasterContext _context;
        private readonly IWebHostEnvironment _env;

        public SeedController(
            MasterContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult> Import()
        {
            // prevents non-development environments from running this method
            if (!_env.IsDevelopment())
                throw new SecurityException("Not allowed");

            var path = System.IO.Path.Combine(
                _env.ContentRootPath,
                "NovelList.xlsx");

            using var stream = System.IO.File.OpenRead(path);
            using var excelPackage = new ExcelPackage(stream);

            // get the first worksheet 
            var worksheet = excelPackage.Workbook.Worksheets[0];

            // define how many rows we want to process 
            var nEndRow = worksheet.Dimension.End.Row;

            // initialize the record counters 
            var numberOfCountriesAdded = 0;
            var numberOfCitiesAdded = 0;

            // create a lookup dictionary 
            // containing all the countries already existing 
            // into the Database (it will be empty on first run).
            var countriesByName = _context.NovelL
                .AsNoTracking()
                .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            // iterates through all rows, skipping the first one 
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var row = worksheet.Cells[
                    nRow, 1, nRow, worksheet.Dimension.End.Column];

                var NovelName = row[nRow, 5].GetValue<string>();
                var iso2 = row[nRow, 6].GetValue<string>();
                var iso3 = row[nRow, 7].GetValue<string>();

                // skip this Novel if it already exists in the database
                if (countriesByName.ContainsKey(NovelName))
                    continue;

                // create the Novel entity and fill it with xlsx data 
                var Novel = new NovelLibary
                {
                    Name = NovelName,
                    ISO2 = iso2,
                    ISO3 = iso3
                };

                // add the new Novel to the DB context 
                await _context.NovelL.AddAsync(Novel);

                // store the Novel in our lookup to retrieve its Id Chapterer on
                countriesByName.Add(NovelName, Novel);

                // increment the counter 
                numberOfCountriesAdded++;
            }

            // save all the countries into the Database 
            if (numberOfCountriesAdded > 0)
                await _context.SaveChangesAsync();

            // create a lookup dictionary
            // containing all the cities already existing 
            // into the Database (it will be empty on first run). 
            var EasternNovelLibarys = _context.EasternNovelLibary
                .AsNoTracking()
                .ToDictionary(x => (
                    Name: x.Name,
                    Chapter: x.Chapter,
                    Author: x.Author,
                    CountryId: x.CountryId));

            // iterates through all rows, skipping the first one 
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var row = worksheet.Cells[
                    nRow, 1, nRow, worksheet.Dimension.End.Column];

                var name = row[nRow, 1].GetValue<string>();
                var Chapter = row[nRow, 3].GetValue<decimal>();
                var Author = row[nRow, 4].GetValue<decimal>();
                var NovelName = row[nRow, 5].GetValue<string>();

                // retrieve Novel Id by NovelName
                var CountryId = countriesByName[NovelName].Id;

                // skip this city if it already exists in the database
                if (EasternNovelLibarys.ContainsKey((
                    Name: name,
                    Chapter: Chapter,
                    Author: Author,
                    CountryId: CountryId)))
                    continue;

                // create the City entity and fill it with xlsx data 
                var EasternNovelLibary = new EasternNovelLibary
                {
                    Name = name,
                    Chapter = Chapter,
                    Author = Author,
                    CountryId = CountryId
                };

                // add the new city to the DB context 
                _context.EasternNovelLibary.Add(EasternNovelLibary);

                // increment the counter 
                numberOfCitiesAdded++;
            }

            // save all the cities into the Database 
            if (numberOfCitiesAdded > 0)
                await _context.SaveChangesAsync();

            return new JsonResult(new
            {
                EasternNovelLibary = numberOfCitiesAdded,
                novel = numberOfCountriesAdded
            });
        }
    }
}