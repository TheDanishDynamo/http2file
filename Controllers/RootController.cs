using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;

namespace Http2File.Controllers
{
	public class RootController : Controller
	{
		private readonly IConfiguration _config;

		public RootController(IConfiguration config)
		{
			_config = config;
		}

		[HttpPost]
		public ActionResult Index()
		{
			System.Console.WriteLine("Index() was called.");
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Http2File()
		{
			using (StreamReader r = new StreamReader(Request.Body, Encoding.UTF8))
			{  
				string filename = DateTime.Now.ToOADate().ToString() + ".json";
				string folder = _config.GetValue<string>("Output.Folder");
				string fullpath = Path.Combine(folder,filename);
				string body = await r.ReadToEndAsync();
				System.IO.File.WriteAllText(fullpath,body);
			}
			return Ok();
		}	
	}
}