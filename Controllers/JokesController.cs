using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyJokesWeb.Data;
using MyJokesWeb.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyJokesWeb.Controllers
{
    public class JokesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JokesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SearchResults(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View("Index", await _context.Jokes.ToListAsync());
            }

            var results = await _context.Jokes
                .Where(j => j.JokesQuestion.Contains(query) ||
                            j.JokesAnswer.Contains(query))
                .ToListAsync();

            return View("Index", results);
        }

        // ================= INDEX =================
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jokes.ToListAsync());
        }

        // ================= DETAILS =================
        public async Task<IActionResult> Details(int id)
        {
            var joke = await _context.Jokes
                .Include(j => j.Comments)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (joke == null)
                return NotFound();

            return View(joke);
        }

        // ================= LIKE =================
        [HttpPost]
        public async Task<IActionResult> Like(int id)
        {
            var joke = await _context.Jokes.FindAsync(id);

            if (joke == null)
                return NotFound();

            joke.Likes++;

            _context.Update(joke);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Search()
        {
            return View();
        }

        // ================= ADD COMMENT (FIXED TYPE ISSUE) =================
        [HttpPost]
        public async Task<IActionResult> AddComment(int jokeId, string content)
        {
            var joke = await _context.Jokes.FindAsync(jokeId);

            if (joke == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(content))
            {
                var comment = new Comment
                {
                    Content = content,
                    CreatedAt = DateTime.Now,
                    JokeId = jokeId // ✅ FIXED
                }; 

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = jokeId });
        }
        // ================= CREATE =================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JokesQuestion,JokesAnswer")] Jokes jokes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jokes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jokes);
        }

        // ================= EDIT =================
        public async Task<IActionResult> Edit(int id)
        {
            var joke = await _context.Jokes.FindAsync(id);

            if (joke == null)
                return NotFound();

            return View(joke);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Jokes jokes)
        {
            _context.Update(jokes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ================= DELETE =================
        public async Task<IActionResult> Delete(int id)
        {
            var joke = await _context.Jokes.FindAsync(id);

            if (joke == null)
                return NotFound();

            _context.Jokes.Remove(joke);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ================= PROFILE =================
        [Authorize]
        public IActionResult UserProfile()
        {
            var email = User.Identity?.Name;
            ViewBag.Email = email;
            return View();
        }
        public IActionResult Profile()
        {
            var email = User.Identity?.Name;
            ViewBag.Email = email;
            return View();
        }
    }
}