using ESESMT.Infra.Shared.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace ESESMT.Application.Filters
{
    public class NotificationFilter : IAsyncResultFilter
	{
		private readonly NotificationContext _notificationContext;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public NotificationFilter(NotificationContext notificationContext,
            IOptions<MvcNewtonsoftJsonOptions> mvcJsonOptions)
		{
			_notificationContext = notificationContext;
            _jsonSerializerSettings = mvcJsonOptions.Value.SerializerSettings;
        }

		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			if (_notificationContext.HasNotifications)
			{
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				context.HttpContext.Response.ContentType = "application/json";

				var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications, _jsonSerializerSettings);
				await context.HttpContext.Response.WriteAsync(notifications);

				return;
			}

			await next();
		}
	}
}
