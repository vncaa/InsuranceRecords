using Insurance.Data;
using Insurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index(int? id)
        {
            var user = from u in _context.Users
                           select u;
            if (id == null)
            {
                user = user.Where(u => u.UserId == id);
            }

            return View(await _context.Users.ToListAsync());
            
        }
        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Include(i => i.UserInsuranceRecords).ThenInclude(i => i.InsuranceRecord).AsNoTracking().FirstOrDefaultAsync(i => i.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            ViewData["InsuranceRecords"] = _context.InsuranceRecords.ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, int[] selectedInsuranceRecords)
        {
            if (ModelState.IsValid)
            {
                user = _context.Add(user).Entity;

                await _context.SaveChangesAsync();


            }
            else
            {
                ViewData["InsuranceRecords"] = _context.InsuranceRecords.ToList();
                return View(user);
            }

            var userUpdate = await _context.Users.Include(c => c.UserInsuranceRecords).ThenInclude(i => i.InsuranceRecord).AsNoTracking().FirstOrDefaultAsync(i => i.UserId == user.UserId);
            
            UpdateUserInsuranceRecords(selectedInsuranceRecords, userUpdate);
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Include(i => i.UserInsuranceRecords).ThenInclude(ci => ci.InsuranceRecord).AsNoTracking().FirstOrDefaultAsync(c => c.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["InsuranceRecords"] = _context.InsuranceRecords.ToList();
            ViewData["UserInsuranceRecords"] = user.UserInsuranceRecords.Select(ci => ci.InsuranceId).ToList();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, int[] selectedInsuranceRecords)
        {
            var userUpdate = await _context.Users.Include(i => i.UserInsuranceRecords).ThenInclude(i => i.InsuranceRecord).FirstOrDefaultAsync(c => c.UserId == id);


            if (await TryUpdateModelAsync<User>(userUpdate, "", i => i.UserId, i => i.FirstName, i => i.LastName, i => i.Phone, i => i.Email, i => i.Street, i => i.City, i => i.PostalCode, i => i.Country, i => i.UserInsuranceRecords))
            {
                try
                {
                    UpdateUserInsuranceRecords(selectedInsuranceRecords, userUpdate);
                   
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userUpdate.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            
            return View(userUpdate);
        }



        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Include(i => i.UserInsuranceRecords).ThenInclude(i => i.InsuranceRecord).AsNoTracking().FirstOrDefaultAsync(c => c.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(c => c.UserId == id);
        }

        private void UpdateUserInsuranceRecords(int[] selectedInsuranceRecords, User user)
        {
            if (selectedInsuranceRecords == null)
            {
                user.UserInsuranceRecords = new List<UserInsurance>();
                return;
            }

            var selectedInsuranceRecordsHashSet = new HashSet<int>(selectedInsuranceRecords);
            var userInsuranceRecords = new HashSet<int>(user.UserInsuranceRecords.Select(i => i.InsuranceRecord.InsuranceId));

            foreach (var insuranceRecord in _context.InsuranceRecords)
            {
                if (selectedInsuranceRecordsHashSet.Contains(insuranceRecord.InsuranceId))
                {
                    if (!userInsuranceRecords.Contains(insuranceRecord.InsuranceId))
                    {
                        user.UserInsuranceRecords.Add(new UserInsurance { UserId = user.UserId, InsuranceId = insuranceRecord.InsuranceId });
                    }
                }
                else
                {
                    if (userInsuranceRecords.Contains(insuranceRecord.InsuranceId))
                    {
                        UserInsurance insuranceRecordRemove = user.UserInsuranceRecords.FirstOrDefault(i => i.InsuranceId == insuranceRecord.InsuranceId);
                        _context.Remove(insuranceRecordRemove);
                    }
                }
            }
        }
    }
}
