using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ESESMT.Infra.Shared.Interfaces;

namespace ESESMT.Infra.Shared.Common
{
    public class HttpRequests : IHttpRequests
	{
		public IApplicationLogger ApplicationLogger { get; }

		public HttpRequests()
		{

		}

		public HttpRequests(IApplicationLogger applicationLogger)
		{
			ApplicationLogger = applicationLogger ??
				throw new ArgumentNullException(nameof(applicationLogger));
		}

		public async Task<string> GetAsync(string endpoint)
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					httpClient.Timeout = TimeSpan.FromMinutes(2);
					var response = await httpClient.GetStringAsync(endpoint);
					if (response != null)
					{
						return response;
					}

					return null;
				}
			}
			catch (Exception ex)
			{
				ApplicationLogger.LogError(ex, ex.Message);
				throw ex;
			}
		}

		public async Task<string> DeleteAsync(string endpoint)
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					httpClient.Timeout = TimeSpan.FromMinutes(2);
					var response = await httpClient.DeleteAsync(endpoint);

					return response.ToString();
				}
			}
			catch (Exception ex)
			{
				ApplicationLogger.LogError(ex, ex.Message);
				throw ex;
			}
		}

		public async Task<string> PostAsync(string endpoint, object objectToSend)
		{
			try
			{
				if (objectToSend == null)
					throw new ArgumentNullException("Request Content", "Post Request without Content is not allowed.");

				var httpContent = new StringContent(
						JsonConvert.SerializeObject(objectToSend),
						Encoding.UTF8, "application/json");

				using (var httpClient = new HttpClient())
				{
					httpClient.Timeout = TimeSpan.FromMinutes(2);
					var response = await httpClient.PostAsync(endpoint, httpContent);

					string result = await response.Content.ReadAsStringAsync();
					if (!response.IsSuccessStatusCode)
					{
						var messageException = $"GetAsync End, url:{endpoint}, HttpStatusCode:{response.StatusCode}, result:{result}";
						ApplicationLogger.LogError(new ArgumentException(messageException), messageException);
					}

					return result;
				}
			}
			catch (Exception ex)
			{
				ApplicationLogger.LogError(ex, ex.Message);
				throw ex;
			}
		}
	}
}
