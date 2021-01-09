﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carworld.Models;
using Logic;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Carworld.Controllers
{
    public class AddCarController : Controller
    {
        private readonly IWebHostEnvironment envi;
        //Methods 
        private List<BrandModel> GetBrands()
        {
            List<BrandModel> brands = new List<BrandModel>();

            foreach (var brand in new BrandCollection().GetAll())
            {
                brands.Add(new BrandModel { Id = brand.Id, Name = brand.Name, Origin = brand.Origin });
            }
            return brands;
        }

        private List<CarClassModel> GetCarClasses()
        {
            List<CarClassModel> carClasses = new List<CarClassModel>();

            foreach (var carClass in new CarClassCollection().GetAll())
            {
                carClasses.Add(new CarClassModel { Id = carClass.Id, Name = carClass.Name });
            }
            return carClasses;
        }

        private List<FuelModel> GetFuels()
        {
            List<FuelModel> fuels = new List<FuelModel>();

            foreach (var fuel in new FuelCollection().GetAll())
            {
                fuels.Add(new FuelModel { Id = fuel.Id, Name = fuel.Name });
            }
            return fuels;
        }

        private bool createCar(CarModel car)
        {
            Car newCar = new Car()
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Price = car.Price,
                Horsepower = car.Horsepower,
                Torque = car.Torque,
                Acceleration = car.Acceleration,
                Topspeed = car.Topspeed,
                CarClass = car.CarClass,
                Fuel = car.Fuel,
                FuelConsumption = car.FuelConsumption,
                MadeByUser = car.MadeByUser,
            };

            if(!new CarCollection().Create(newCar))
            {
                return false;
            }
            return true;
        }

        public AddCarController(IWebHostEnvironment _environment)
        {
            envi = _environment;
        }

        //End methods
        [Route("/[action]")]
        public IActionResult AddCar()
        {
            //Dropdown menus
            ViewBag.Brands = GetBrands();
            ViewBag.Fuels = GetFuels();
            ViewBag.CarClasses = GetCarClasses();

            return View();
        }

        [Route("/[action]")]
        [HttpPost]
        public IActionResult AddCar(CarModel car)
        {
            var brands = GetBrands();
            var fuels = GetFuels();
            var carClasses = GetCarClasses();

            //Dropdown menus
            ViewBag.Brands = brands;
            ViewBag.Fuels = fuels;
            ViewBag.CarClasses = carClasses;

            CarModel createdCar = new CarModel()
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Price = car.Price,
                Horsepower = car.Horsepower,
                Torque = car.Torque,
                Acceleration = car.Acceleration,
                Topspeed = car.Topspeed,
                CarClass = car.CarClass,
                Fuel = car.Fuel,
                FuelConsumption = car.FuelConsumption,
                MadeByUser = car.MadeByUser,
            };

            //Image Upload
            string uploadsFolder = Path.Combine(envi.WebRootPath, "Images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + car.Image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var filestream = new FileStream(filePath, FileMode.Create))
            {
                car.Image.CopyTo(filestream);
            }

            if (createCar(createdCar))
            {
                TempData.Add("Success", "Car has succesfully been uploaded to database");
            }
            else
            {
                TempData.Add("Alert", "Something went wrong uploading data to the server");
            }
            
            return View();
        }


    }
}
