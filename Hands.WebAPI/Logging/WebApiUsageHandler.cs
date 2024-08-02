using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Hands.WebAPI.Logging
{
    public class WebApiUsageHandler : DelegatingHandler
    {
        private static readonly IApiUsageRepository _repo = new ApiUsageRepositoryDb();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string apikey = HttpUtility.ParseQueryString(request.RequestUri.Query).Get("apikey");

            var apiRequest = new WebApiUsageRequest(request, apikey);

            apiRequest.Content = request.Content.ReadAsStringAsync().Result;
            _repo.Add(apiRequest);

            return base.SendAsync(request, cancellationToken).ContinueWith(
                task =>
                {
                    var apiResponse = new WebApiUsageResponse(task.Result, apikey);
                    apiResponse.Content = task.Result.Content.ReadAsStringAsync().Result;
                    _repo.Add(apiResponse);
                    return task.Result;
                }
            );
        }
    }
}