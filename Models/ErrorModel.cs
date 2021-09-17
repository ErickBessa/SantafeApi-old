using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Models
{
	public class ErrorModel
	{
		public string FieldName { get; set; }

		public string Message { get; set; }

		public ErrorModel(){}
		public ErrorModel(string message)
		{
			this.Message = message;
		}
	}
}
