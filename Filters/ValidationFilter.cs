using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SantafeApi.Contracts.Responses;
using SantafeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Filters
{
	public class ValidationFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (!context.ModelState.IsValid)
			{
				var errorsInModelState = context.ModelState
					.Where(x => x.Value.Errors.Count > 0)
					.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage))
					.ToArray();

				var errorResponse = new ErrorResponse();

				foreach( var error in errorsInModelState)
				{
					foreach(var message in error.Value)
					{
						var errorModel = new ErrorModel
						{
							FieldName = error.Key,
							Message = message
						};

						errorResponse.Errors.Add(errorModel);
					}
				}

				context.Result = new BadRequestObjectResult(errorResponse);
				return;
			}

			await next();


		}
	}
}
