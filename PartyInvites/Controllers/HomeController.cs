using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PartyInvites.Models;
using PartyInvites.Services;

namespace PartyInvites.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			int hour = DateTime.Now.Hour;
			if (hour < 12)
			{
				ViewBag.Greeting = "Good Morning";

			}
			else if (hour < 18)
			{
				ViewBag.Greeting = "Good Afternoon";
			}
			else
			{
				ViewBag.Greeting = "Good Evening";
			}
			return View();
		}

		[HttpGet]
		public IActionResult RsvpForm()
		{
			return View();
		}

		[HttpPost]
		public ViewResult RsvpForm(GuestResponseModel guestResponse)
		{
			string c = guestResponse.RSVpType;
			if (c==("none") | c==("null"))
			{

				 ViewBag.Error = "Please select your room type";
				return View();
			}
			else
			{
				ViewBag.Greeting = "Good Evening, " + guestResponse.Name+
					"\n Your Reservation was completed We will call you to confirm. \nThank you";
				int res = DbActions.CustomerRVSP(guestResponse);
				if (res > 0)
				{
					ViewData["Message"] = "Reservation Successfully";
					
				}
				else
				{
					ViewData["Message"] = "Something Went Wrong";
				}
				return View("Index");
			}

			//TODO: SubscribeUser(model.Email);

			// TODO: Email response to the party organizer
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Call()
		{
			//return "Calling....";

			//Task.Delay(10000).Wait();

			return Redirect("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
