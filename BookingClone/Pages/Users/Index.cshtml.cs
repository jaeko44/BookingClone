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
    public class IndexModel : PageModel
    {
        private readonly BookingClone.Data.ApplicationDbContext _context;

        public IndexModel(BookingClone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<GuestDetails> GuestDetails { get;set; }

        public async Task OnGetAsync()
        {
            GuestDetails = await _context.GuestDetails.ToListAsync();
        }
    }
}
