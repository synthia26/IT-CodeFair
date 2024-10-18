using HolidayHomeProject.DataAccessLayer.Context;
using HolidayHomeProject.DataAccessLayer.Dto;
using HolidayHomeProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolidayHomeProject.Controllers
{
    public class HostController : Controller
    {
        private readonly AppDbContext _context;

        public HostController(AppDbContext context)
        {
            _context = context;
        }

        // Renders the HostDetails view
        [HttpGet]
        public IActionResult HostDetails()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Step1()
        {
            return View(new HostViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Step1(HostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim != null)
                {
                    int userId = int.Parse(userIdClaim.Value);

                    // Store data in TempData
                    TempData["Bio"] = model.Bio;
                    TempData["UserId"] = userId;
                    TempData["Address"] = model.Address;
                    TempData["Description"] = model.Description;
                    TempData["MaxPeopleNo"] = model.MaxPeopleNo;
                    TempData["RentPricePerDay"] = model.RentPricePerDay.ToString();

                    // Handle file uploads and store paths in TempData
                    string thumbnailImagePath = null;
                    if (model.ThumbnailImage != null)
                    {
                        thumbnailImagePath = await SaveFileAsync(model.ThumbnailImage); // Save the file and get the path
                    }

                    TempData["ThumbnailImage"] = thumbnailImagePath;

                    List<string> houseImagesPaths = new List<string>();
                    if (model.HouseImages != null && model.HouseImages.Count > 0)
                    {
                        foreach (var file in model.HouseImages)
                        {
                            houseImagesPaths.Add(await SaveFileAsync(file)); // Save each image and get the path
                        }
                    }

                    TempData["HouseImages"] = houseImagesPaths;

                    // Retrieve TempData
                    var thumbnailImage = TempData["ThumbnailImage"] as string;
                    var houseImages = TempData["HouseImages"] as List<string>;

                    // Create and save the Host entity
                    var host = new HostBio
                    {
                        Bio = TempData["Bio"].ToString(),
                        UserId = userId
                    };
                    _context.Hosts.Add(host);
                    await _context.SaveChangesAsync(); // Save to get HostId

                    // Create and save the House entity
                    var house = new House
                    {
                        Address = TempData["Address"].ToString(),
                        Description = TempData["Description"].ToString(),
                        MaxPeopleNo = int.Parse(TempData["MaxPeopleNo"].ToString()),
                        RentPricePerDay = decimal.Parse(TempData["RentPricePerDay"].ToString()),
                        HostId = host.HostId,
                        ThumbnailImagePath = thumbnailImage, // Store the image path
                        HouseImagesPaths = houseImages // Store the list of image paths
                    };

                    _context.Houses.Add(house);
                    await _context.SaveChangesAsync();

                    // Clear TempData
                    TempData.Clear();

                    return RedirectToAction("Success");
                }
                else
                {
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Step2()
        {
            var model = new HostViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Step2(HostViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Store in TempData
                TempData["Address"] = model.Address;
                TempData["Description"] = model.Description;
                TempData["MaxPeopleNo"] = model.MaxPeopleNo;
                TempData["RentPricePerDay"] = model.RentPricePerDay;

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Step3()
        {
            return View(new HostViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Step3(HostViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle file uploads and store paths in TempData
                string thumbnailImagePath = null;
                if (model.ThumbnailImage != null)
                {
                    thumbnailImagePath = await SaveFileAsync(model.ThumbnailImage); // Save the file and get the path
                }

                TempData["ThumbnailImage"] = thumbnailImagePath;

                List<string> houseImagesPaths = new List<string>();
                if (model.HouseImages != null && model.HouseImages.Count > 0)
                {
                    foreach (var file in model.HouseImages)
                    {
                        houseImagesPaths.Add(await SaveFileAsync(file)); // Save each image and get the path
                    }
                }

                TempData["HouseImages"] = houseImagesPaths;


                // Retrieve TempData
                var thumbnailImage = TempData["ThumbnailImage"] as string;
                var houseImages = TempData["HouseImages"] as List<string>;

                // Create and save the Host entity
                var host = new HostBio
                {
                    Bio = TempData["Bio"].ToString(),
                    UserId = int.Parse(TempData["UserId"].ToString())
                };
                _context.Hosts.Add(host);
                await _context.SaveChangesAsync(); // Save to get HostId

                // Create and save the House entity
                var house = new House
                {
                    Address = TempData["Address"].ToString(),
                    Description = TempData["Description"].ToString(),
                    MaxPeopleNo = int.Parse(TempData["MaxPeopleNo"].ToString()),
                    RentPricePerDay = decimal.Parse(TempData["RentPricePerDay"].ToString()),
                    HostId = host.HostId,
                    ThumbnailImagePath = thumbnailImage, // Store the image path
                    HouseImagesPaths = houseImages // Store the list of image paths
                };

                _context.Houses.Add(house);
                await _context.SaveChangesAsync();

                // Clear TempData
                TempData.Clear();

                return RedirectToAction("Success");

                // return RedirectToAction("Step4", "Host");
            }
            else
            {

                // Iterate over the validation errors
                foreach (var state in ModelState)
                {
                    var key = state.Key; // Field name
                    var errors = state.Value.Errors; // Validation errors for this field

                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Step4()
        {
            return View(new HostViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Step4(HostViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve TempData
                var thumbnailImage = TempData["ThumbnailImage"] as string;
                var houseImages = TempData["HouseImages"] as List<string>;

                // Create and save the Host entity
                var host = new HostBio
                {
                    Bio = TempData["Bio"].ToString(),
                    UserId = int.Parse(TempData["UserId"].ToString())
                };
                _context.Hosts.Add(host);
                await _context.SaveChangesAsync(); // Save to get HostId

                // Create and save the House entity
                var house = new House
                {
                    Address = TempData["Address"].ToString(),
                    Description = TempData["Description"].ToString(),
                    MaxPeopleNo = int.Parse(TempData["MaxPeopleNo"].ToString()),
                    RentPricePerDay = decimal.Parse(TempData["RentPricePerDay"].ToString()),
                    HostId = host.HostId,
                    ThumbnailImagePath = thumbnailImage, // Store the image path
                    HouseImagesPaths = houseImages // Store the list of image paths
                };

                _context.Houses.Add(house);
                await _context.SaveChangesAsync();

                // Clear TempData
                TempData.Clear();

                return RedirectToAction("Success");
            }

            return View(model);
        }


        private async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/uploads");

            // Ensure the folder exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/images/uploads/{file.FileName}";
        }
        // Handles form submission using HostViewModel
        /*    [HttpPost]
            public async Task<IActionResult> HostDetails(HostViewModel model)
            {
                if (ModelState.IsValid)
                {
                    // Map HostViewModel to Host, UserAccount, and House
                    var host = new DataAccessLayer.Dto.Host
                    {
                        Bio = model.Bio,
                        UserAccount = new UserAccount
                        {
                            Id = model.UserId,
                            Email = model.Email
                        },
                        House = new House
                        {
                            HouseAddress = model.HouseAddress,
                            NumberOfPeople = model.NumberOfPeople,
                            RentPricePerDay = model.RentPricePerDay,
                            HouseImages = new List<string>()
                        }
                    };

                    // Handle file saving logic for images
                    if (model.Thumbnail != null)
                    {
                        var thumbnailPath = Path.Combine("wwwroot/images", model.Thumbnail.FileName);
                        using (var stream = new FileStream(thumbnailPath, FileMode.Create))
                        {
                            await model.Thumbnail.CopyToAsync(stream);
                        }
                        host.House.ThumbnailImage = model.Thumbnail.FileName; // Add thumbnail to HouseDTO images
                    }

                    if (model.AdditionalImages != null && model.AdditionalImages.Count > 0)
                    {
                        foreach (var image in model.AdditionalImages)
                        {
                            var imagePath = Path.Combine("wwwroot/images", image.FileName);
                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }
                            host.House.HouseImages.Add(image.FileName); // Add each additional image
                        }
                    }

                    // At this point, you would typically save the hostDto data to a database
                    // Save hostDto to a database or pass it to a service

                    // Redirect to success page
                    return RedirectToAction("Success");
                }

                // If validation fails, return the form with validation errors
                return View(model);
            }*/

        public IActionResult Success()
        {
            return View();
        }
    }
}
