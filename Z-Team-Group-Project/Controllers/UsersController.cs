using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Z_Team_Group_Project.Data;
using Z_Team_Group_Project.Models;

namespace Z_Team_Group_Project.Controllers
{
    public class UsersController : Controller
    {
        private readonly Z_Team_Group_ProjectContext _context;

        public UsersController(Z_Team_Group_ProjectContext context)
        {
            _context = context;
        }

        // POST: login user

        [HttpPost]
        public IActionResult LogIn(User user)
        {
            if (ModelState.IsValid)
            {
                // Perform login logic here (e.g., authenticate the user)
                // Redirect to appropriate page if login is successful
            }
            return View(user); // Return the view with validation errors if login failed
        }


        // GET: Users 
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var users =  _context.User
                .Include(u => u.Account)
                .OrderBy(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View(users);
        }

        // GET: Users/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user =  _context.User
                .FirstOrDefault(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                //add a new account when we create a user
                var account = new Account { Amount = 0 };
                _context.Accounts.Add(account);

                _context.SaveChanges();

                //set the new accounts id to the users account id
                user.AccountId = account.AccountId;

                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Password")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                //need to delete the users bound account when they leave the site
                var account = _context.Find<Account>(user.Id);
                if (account != null)
                {
                    //if the account is found for the user we remove it from the table.
                    _context.Accounts.Remove(account);
                    //db can only save one operation at a time so we save before removing the user.
                    await _context.SaveChangesAsync();
                }
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
