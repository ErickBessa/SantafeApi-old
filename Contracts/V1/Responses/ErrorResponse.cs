using SantafeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Contracts.Responses
{
	public class ErrorResponse
	{

		public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
	}
}
