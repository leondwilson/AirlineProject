using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.EF;

namespace WebApplication1.Controllers
{
    public class BookTicketsController : Controller
    {
        private readonly AirlineAppDBContext _context = new AirlineAppDBContext();

        //public BookTicketsController(AirlineAppDBContext context)
        //{
        //    _context = context;
        //}

        // GET: BookTickets
        public async Task<IActionResult> Index()
        {
            var airlineAppDBContext = _context.BookTickets.Include(b => b.FlightNoNavigation);
            return View(await airlineAppDBContext.ToListAsync());
        }

        // GET: BookTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookTickets == null)
            {
                return NotFound();
            }

            var bookTicket = await _context.BookTickets
                .Include(b => b.FlightNoNavigation)
                .FirstOrDefaultAsync(m => m.PassengerId == id);
            if (bookTicket == null)
            {
                return NotFound();
            }

            return View(bookTicket);
        }

        // GET: BookTickets/Create
        public IActionResult Create()
        {
            ViewData["FlightNo"] = new SelectList(_context.BookFlights, "FlightNo", "FlightNo");
            return View();
        }

        // POST: BookTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PassengerId,FlightNo,PassengerFistName,PassengerLastName,City,Age")] BookTicket bookTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightNo"] = new SelectList(_context.BookFlights, "FlightNo", "FlightNo", bookTicket.FlightNo);
            return View(bookTicket);
        }

        // GET: BookTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookTickets == null)
            {
                return NotFound();
            }

            var bookTicket = await _context.BookTickets.FindAsync(id);
            if (bookTicket == null)
            {
                return NotFound();
            }
            ViewData["FlightNo"] = new SelectList(_context.BookFlights, "FlightNo", "FlightNo", bookTicket.FlightNo);
            return View(bookTicket);
        }

        // POST: BookTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PassengerId,FlightNo,PassengerFistName,PassengerLastName,City,Age")] BookTicket bookTicket)
        {
            if (id != bookTicket.PassengerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookTicketExists(bookTicket.PassengerId))
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
            ViewData["FlightNo"] = new SelectList(_context.BookFlights, "FlightNo", "FlightNo", bookTicket.FlightNo);
            return View(bookTicket);
        }

        // GET: BookTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookTickets == null)
            {
                return NotFound();
            }

            var bookTicket = await _context.BookTickets
                .Include(b => b.FlightNoNavigation)
                .FirstOrDefaultAsync(m => m.PassengerId == id);
            if (bookTicket == null)
            {
                return NotFound();
            }

            return View(bookTicket);
        }

        // POST: BookTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookTickets == null)
            {
                return Problem("Entity set 'AirlineAppDBContext.BookTickets'  is null.");
            }
            var bookTicket = await _context.BookTickets.FindAsync(id);
            if (bookTicket != null)
            {
                _context.BookTickets.Remove(bookTicket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookTicketExists(int id)
        {
          return _context.BookTickets.Any(e => e.PassengerId == id);
        }
    }
}
