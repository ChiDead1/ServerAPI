using System.Diagnostics.Metrics;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Microsoft.AspNetCore.Identity;
using ClassLibrary584;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class SeedController : ControllerBase
    {
        private readonly MasterContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public SeedController(
            MasterContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment env,
            IConfiguration configuration)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _env = env;
            _configuration = configuration;
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

                // skip this easternNovelLibary if it already exists in the database
                if (EasternNovelLibarys.ContainsKey((
                    Name: name,
                    Chapter: Chapter,
                    Author: Author,
                    CountryId: CountryId)))
                    continue;

                // create the easternNovelLibary entity and fill it with xlsx data 
                var easternNovelLibary = new EasternNovelLibary
                {
                    Name = name,
                    Chapter = Chapter,
                    Author = Author,
                    CountryId = CountryId
                };

                // add the new easternNovelLibary to the DB context 
                _context.EasternNovelLibary.Add(easternNovelLibary);

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
        [HttpGet]
        public async Task<ActionResult> CreateDefaultUsers()
        {
            // setup the default role names
            string role_RegisteredUser = "RegisteredUser";
            string role_Administrator = "Administrator";

            // create the default roles (if they don't exist yet)
            if (await _roleManager.FindByNameAsync(role_RegisteredUser) ==
             null)
                await _roleManager.CreateAsync(new
                 IdentityRole(role_RegisteredUser));

            if (await _roleManager.FindByNameAsync(role_Administrator) ==
             null)
                await _roleManager.CreateAsync(new
                 IdentityRole(role_Administrator));

            // create a list to track the newly added users
            var addedUserList = new List<ApplicationUser>();

            // check if the admin user already exists
            var email_Admin = "admin@email.com";
            if (await _userManager.FindByNameAsync(email_Admin) == null)
            {
                // create a new admin ApplicationUser account
                var user_Admin = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email_Admin,
                    Email = email_Admin,
                };

                string defaultPassword_Admin = _configuration["DefaultPasswords:Administrator"] ?? "Admin@#123"; // provide a default password value if configuration value is null or empty

                // insert the admin user into the DB
                var result = await _userManager.CreateAsync(user_Admin, defaultPassword_Admin);

                if (!result.Succeeded) // check if the operation was successful
                {
                    return BadRequest(result.Errors.FirstOrDefault()?.Description); // return an error response
                }

                await _context.SaveChangesAsync(); // save changes here

                // assign the "RegisteredUser" and "Administrator" roles
                await _userManager.AddToRoleAsync(user_Admin, role_RegisteredUser);
                await _userManager.AddToRoleAsync(user_Admin, role_Administrator);

                // confirm the e-mail and remove lockout
                user_Admin.EmailConfirmed = true;
                user_Admin.LockoutEnabled = false;

                // add the admin user to the added users list
                addedUserList.Add(user_Admin);
            }

            // check if the standard user already exists
            var email_User = "user";
            if (await _userManager.FindByNameAsync(email_User) == null)
            {
                // create a new standard ApplicationUser account
                var user_User = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email_User,
                    Email = email_User
                };

                string defaultPassword_User = _configuration["DefaultPasswords:RegisteredUser"] ?? "User@#123"; // provide a default password value if configuration value is null or empty

                // insert the standard user into the DB
                var result = await _userManager.CreateAsync(user_User, defaultPassword_User);

                if (!result.Succeeded) // check if the operation was successful
                {
                    return BadRequest(result.Errors.FirstOrDefault()?.Description); // return an error response
                }

                await _context.SaveChangesAsync(); // save changes here

                // assign the "RegisteredUser" role
                await _userManager.AddToRoleAsync(user_User, role_RegisteredUser);

                // confirm the e-mail and remove lockout
                user_User.EmailConfirmed = true;
                user_User.LockoutEnabled = false;

                // add the standard user to the added users list
                addedUserList.Add(user_User);
            }

            // if we added at least one user, persist the changes into the DB
            if (addedUserList.Count > 0)
                await _context.SaveChangesAsync();

            return new JsonResult(new
            {
                Count = addedUserList.Count,
                Users = addedUserList
            });
        }
    }
}