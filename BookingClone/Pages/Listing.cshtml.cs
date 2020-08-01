using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookingClone.Pages
{
    public class ListingModel : PageModel
    {
        private readonly BookingClone.Data.ApplicationDbContext _context;
        private string any;

        public ListingModel(BookingClone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<BookModel> BookModel { get; set; }
        [BindProperty]
        public string Category { get; set; }


        [Route("Listing/{search:string}")]
        public async Task<IActionResult> OnGetAsync(string search)
        {
            any = search;
            Category = search;
            if (Category == "all" || Category == "All")
            {
                BookModel = await _context.BookModel.ToListAsync();
                foreach (var item in BookModel)
                {
                    item.ImagesUploaded = _context.Image.Where(p => p.BookModelId == item.Id).ToList<Image>();
                }
            }
            else
            {
                if (search != null && search != "undefined")
                {
                    var selected = BookModel.Where(t => search.Contains(t.Destination));

                    BookModel = selected.ToList<BookModel>();

                    BookModel = BookModel;

                    BookModel = BookModel.Where(m => m.Destination.Contains(any) ||
                                              m.SpecialComments.Contains(any) ||
                                              m.HotelAddress.Contains(any) ||
                                              m.HotelName.Contains(any)).ToList<BookModel>();
                    foreach (var item in BookModel)
                    {
                        item.ImagesUploaded = _context.Image.Where(p => p.BookModelId == item.Id).ToList<Image>();
                    }
                }

            }




            if (BookModel == null)
            {
                return NotFound();
            }
            return Page();

        }
    }
}