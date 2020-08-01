using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookingClone.Data;
using BookingClone.Pages;
using Microsoft.AspNetCore.Authorization;

namespace BookingClone.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly BookingClone.Data.ApplicationDbContext _context;

        public DeleteModel(BookingClone.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GuestDetails = await _context.GuestDetails.FindAsync(id);

            if (GuestDetails != null)
            {
                _context.GuestDetails.Remove(GuestDetails);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
