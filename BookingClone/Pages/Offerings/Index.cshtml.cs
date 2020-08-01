using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookingClone.Data;
using Microsoft.AspNetCore.Authorization;

namespace BookingClone.Pages
{
    [Authorize(Roles = "Admin")]
    public class OfferingIndexModel : PageModel
    {
        private readonly BookingClone.Data.ApplicationDbContext _context;

        public OfferingIndexModel(BookingClone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<BookModel> BookModel { get;set; }

        public async Task OnGetAsync()
        {
            BookModel = await _context.BookModel.ToListAsync();
            //
            foreach(var item in BookModel)
            {
                item.ImagesUploaded = _context.Image.Where(p => p.BookModelId == item.Id).ToList<Image>();
            }

        }
    }
}
