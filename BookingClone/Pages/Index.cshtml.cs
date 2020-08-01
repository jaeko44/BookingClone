using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookingClone.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly BookingClone.Data.ApplicationDbContext _context;

        [BindProperty]
        public ContactFormModel Contact { get; set; }

        public IndexModel(BookingClone.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public IList<BookModel> BookModel { get; set; }
        public string search { get; set; }

        public async Task OnGetAsync()
        {
            BookModel = await _context.BookModel.ToListAsync();
            foreach (var item in BookModel)
            {
                item.ImagesUploaded = _context.Image.Where(p => p.BookModelId == item.Id).ToList<Image>();
            }
        }

    }
}

