using GearShopAPI.Data;
using GearShopAPI.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GearShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadPhoneController : ControllerBase
    {
        GearContext _gearContext;
        private static IHostingEnvironment _environment;
        public HeadPhoneController(GearContext gearContext, IHostingEnvironment environment)
        {
            _gearContext = gearContext;
            _environment = environment;
        }
        [HttpGet("Brand")]
        public ActionResult Get()
        {
            var brand = _gearContext.Brands;
            return Ok(brand);
        }
        [HttpPost("addheadphone")]
        public void Post([FromBody] HeadPhone headPhone)
        {
            headPhone.Id = Guid.NewGuid();
            headPhone.Brands = null;

            _gearContext.HeadPhones.Add(headPhone);
            _gearContext.SaveChanges();
        }
        [HttpGet("getheadphone")]
        public ActionResult GetCPU([FromQuery]int page = 1, Guid? guid = null, string keyword = null)
        {
            int propage = 5;
            var skip = (page - 1) * propage;
            if (keyword == null && guid == null)
            {
                var headphone = _gearContext.HeadPhones.Include(x => x.Brands);
                return Ok(headphone.Skip(skip).Take(propage).ToList());
            }
            if (guid != null)
            {
                var headphone = _gearContext.HeadPhones.Where(x => x.Brands.Id == guid).Include(x => x.Brands);
                return Ok(headphone.Skip(skip).Take(propage).ToList());
            }
            if (keyword != null)
            {
                var headphone = _gearContext.HeadPhones.Where(x => x.Name.ToLower().Contains(keyword)).Include(x => x.Brands);
                return Ok(headphone.Skip(skip).Take(propage).ToList());
            }
            else
            {
                var headphone = _gearContext.HeadPhones.Where(x => x.Name.ToLower().Contains(keyword) && x.Brands.Id == guid).Include(x => x.Brands);
                return Ok(headphone.Skip(skip).Take(propage).ToList());
            }
        }
        [HttpPost("image/upload")]
        public async Task<ActionResult> UploadImages()
        {
            string link = "";
            try
            {
                var files = Request.Form.Files;
                if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\Post\\Images\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\Post\\Images\\");
                }
                foreach (var file in files)
                {
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + $"\\Upload\\Post\\Images\\{file.FileName}"))
                    {
                        file.CopyTo(filestream);
                        filestream.Flush();
                        link = $"/Upload/Post/Images/{file.FileName}";
                    }
                }
                return Ok(new ApiResponse(true, link, null));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(false, null, Message: ex.Message));
            }
        }
    }
}
