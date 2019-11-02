using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System;

namespace HttpFile.Controllers
{
	public class RootController : Controller
	{
		[HttpPost]
		public ActionResult Index()
		{
			System.Console.WriteLine("Index() was called.");
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Http2File()
		{
			System.Console.WriteLine("Http2File() was called. ---BEGIN---");
			using (StreamReader r = new StreamReader(Request.Body, Encoding.UTF8))
			{  
				string filename = DateTime.Now.ToOADate().ToString() + ".json";
				string body = await r.ReadToEndAsync();
				System.IO.File.WriteAllText(filename,body);
				Console.WriteLine(await r.ReadToEndAsync());
			}
			System.Console.WriteLine("Http2File() was called. ---END---");
			return Ok();
		}	
	}
}