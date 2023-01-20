using Error.Helpers;
using Error.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Error.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="SuperAdmin")]
    public class SliderController : Controller
    {
        private readonly PustokContext _pustokContext;
        private readonly IWebHostEnvironment _env;

        public SliderController(PustokContext pustokContext ,IWebHostEnvironment env)
        {
            _pustokContext = pustokContext;
            _env = env;
        }
            

        

        public IActionResult Index()
        {
           List<Slider>sliders= _pustokContext.Sliders.ToList();

            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
     
            return View();
        }


        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (slider.ImageFile.ContentType !="image/png" && slider.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq png ve jpg ola biler");
                return View();
            }
            /*Guid.NewGuid().ToString()+*/
            if (slider.ImageFile.Length>3145728)
            {
                ModelState.AddModelError("ImageFile", "Sekilin olcusu max 3 mb olmalidir");
                return View();
            }

            //string name = slider.ImageFile.FileName;
            //if (name.Length >64)
            //{
            //    name = name.Substring(name.Length - 64, 64);
            //}
            //name = Guid.NewGuid().ToString() + name;

            ////string path = "C:\\Users\\Dell\\Desktop\\Yeni klasör (1)\\Yeni klasör\\Error\\Error\\wwwroot\\uploads\\sliders\\" + name;
            ////string path = _env.WebRootPath + "/uploads/sliders" + name;

            //string path = Path.Combine(_env.WebRootPath, "uploads\\sliders", name);

            //using (FileStream fileStream = new FileStream(path, FileMode.Create))
            //{
            //    slider.ImageFile.CopyTo(fileStream);
            //}
                    

            slider.Image = FileManager.SaveFile(_env.WebRootPath, "uploads\\sliders", slider.ImageFile); ;
            _pustokContext.Sliders.Add(slider);
            _pustokContext.SaveChanges();
            return RedirectToAction("index"); 
        }

        public IActionResult Update(int id)
        {
            Slider slider = _pustokContext.Sliders.Find(id);
            if (slider == null) return View("Error");
            return View(slider);
        }

        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            Slider existSlider = _pustokContext.Sliders.Find(slider.Id);
            if (existSlider == null) return View("error");

            if (slider.ImageFile !=null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Ancaq png ve jpg ola biler");
                    return View();
                }
                /*Guid.NewGuid().ToString()+*/
                if (slider.ImageFile.Length > 3145728)
                {
                    ModelState.AddModelError("ImageFile", "Sekilin olcusu max 3 mb olmalidir");
                    return View();
                }

                string name = FileManager.SaveFile(_env.WebRootPath, "uploads\\sliders", slider.ImageFile);

                //string deletePath = Path.Combine(_env.WebRootPath, "uploads\\sliders", existSlider.Image);
                //if (System.IO.File.Exists(deletePath))
                //{

                //    System.IO.File.Delete(deletePath);
                //}


                FileManager.DeleteFile(_env.WebRootPath, "uploads\\sliders", existSlider.Image);

                existSlider.Image = name;


            }

            existSlider.Title1 = slider.Title1;
            existSlider.Title2 = slider.Title2;
            existSlider.RedirectUrlText = slider.RedirectUrlText; 
            existSlider.RedirectUrl1 = slider.RedirectUrl1;
       

            _pustokContext.SaveChanges();
            return RedirectToAction("index");
        }

  

        public IActionResult Delete(int id)
        {
            Slider slider = _pustokContext.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider == null) return NotFound();

            if (slider.Image != null)
            {
                FileManager.DeleteFile(_env.WebRootPath, "uploads\\sliders", slider.Image);
            }

            _pustokContext.Sliders.Remove(slider);
            _pustokContext.SaveChanges();
            return Ok();
        }
     
    }
}
