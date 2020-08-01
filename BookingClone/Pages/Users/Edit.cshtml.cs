using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingClone.Data;
using BookingClone.Pages;
using Microsoft.AspNetCore.Authorization;

namespace BookingClone.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly BookingClone.Data.ApplicationDbContext _context;

        public EditModel(BookingClone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GuestDetails GuestDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GuestDetails = await _context.GuestDetails.SingleOrDefaultAsync(m => m.Id == id);

            if (GuestDetails == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(GuestDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestDetailsExists(GuestDetails.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GuestDetailsExists(Guid id)
        {
            return _context.GuestDetails.Any(e => e.Id == id);
        }
    }
}
