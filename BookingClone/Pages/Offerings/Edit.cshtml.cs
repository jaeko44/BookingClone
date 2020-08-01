using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingClone.Data;
using Microsoft.AspNetCore.Authorization;

namespace BookingClone.Pages
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
        public BookModel BookModel { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BookModel = await _context.BookModel.SingleOrDefaultAsync(m => m.Id == id);

            if (BookModel == null)
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

            _context.Attach(BookModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookModelExists(BookModel.Id))
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

        private bool BookModelExists(Guid id)
        {
            return _context.BookModel.Any(e => e.Id == id);
        }
    }
}
