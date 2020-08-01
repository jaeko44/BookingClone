using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookingClone.Data;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

namespace BookingClone.Pages
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IHostingEnvironment _hostingEnvironment;


        private readonly BookingClone.Data.ApplicationDbContext _context;

        public CreateModel(BookingClone.Data.ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BookModel BookModel { get; set; }

        public async Task<IActionResult> OnPostAsync(List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }




            if (files != null && files.Count > 0)
            {
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                BookModel.ImagesUploaded = new List<Image>();
                //BookModel.ImageIds = new List<Guid>();

                foreach (IFormFile item in files)
                {
                    if (item.Length > 0)
                    {

                        string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        Guid guidValue = Guid.NewGuid();

                        var imageToAdd = new Image
                        {
                            Id = guidValue,
                            ImageName = fileName,
                            ImagePath = fullPath
                        };

                        BookModel.ImagesUploaded.Add(imageToAdd);
                        //BookModel.ImageIds.Add(guidValue);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                    }
                }
            }

            _context.BookModel.Add(BookModel);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}