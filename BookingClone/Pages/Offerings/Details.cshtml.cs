using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookingClone.Data;

namespace BookingClone.Pages
{
    public class DetailsModel : PageModel
    {
        public readonly BookingClone.Data.ApplicationDbContext _context;

        public DetailsModel(BookingClone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public BookModel BookModel { get; set; }

        [BindProperty]
        public GuestDetails GuestDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BookModel = await _context.BookModel.SingleOrDefaultAsync(m => m.Id == id);
            BookModel.ImagesUploaded = _context.Image.Where(p => p.BookModelId == BookModel.Id).ToList<Image>();

            if (BookModel == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            BookModel = await _context.BookModel.SingleOrDefaultAsync(m => m.Id == id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            GuestDetails.BookingOfferId = BookModel.Id;

            _context.GuestDetails.Add(GuestDetails);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Success");
        }
    }
}
