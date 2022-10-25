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
    public class BookFlightsController : Controller
    {
        private readonly AirlineAppDBContext _context = new AirlineAppDBContext();

        //public BookFlightsController(AirlineAppDBContext context)
        //{
        //    _context = context;
        //}

        // GET: BookFlights
        public async Task<IActionResult> Index()
        {
              return View(await _context.BookFlights.ToListAsync());
        }

        // GET: BookFlights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookFlights == null)
            {
                return NotFound();
            }

            var bookFlight = await _context.BookFlights
                .FirstOrDefaultAsync(m => m.FlightNo == id);
            if (bookFlight == null)
            {
                return NotFound();
            }

            return View(bookFlight);
        }

        // GET: BookFlights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookFlights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightNo,Source,Destination,Fare,TotalSeats")] BookFlight bookFlight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookFlight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookFlight);
        }

        // GET: BookFlights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookFlights == null)
            {
                return NotFound();
            }

            var bookFlight = await _context.BookFlights.FindAsync(id);
            if (bookFlight == null)
            {
                return NotFound();
            }
            return View(bookFlight);
        }

        // POST: BookFlights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightNo,Source,Destination,Fare,TotalSeats")] BookFlight bookFlight)
        {
            if (id != bookFlight.FlightNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookFlight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookFlightExists(bookFlight.FlightNo))
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
            return View(bookFlight);
        }

        // GET: BookFlights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookFlights == null)
            {
                return NotFound();
            }

            var bookFlight = await _context.BookFlights
                .FirstOrDefaultAsync(m => m.FlightNo == id);
            if (bookFlight == null)
            {
                return NotFound();
            }

            return View(bookFlight);
        }

        // POST: BookFlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookFlights == null)
            {
                return Problem("Entity set 'AirlineAppDBContext.BookFlights'  is null.");
            }
            var bookFlight = await _context.BookFlights.FindAsync(id);
            if (bookFlight != null)
            {
                _context.BookFlights.Remove(bookFlight);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookFlightExists(int id)
        {
          return _context.BookFlights.Any(e => e.FlightNo == id);
        }
    }
}
