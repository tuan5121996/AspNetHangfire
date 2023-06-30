using AspNetHangfire.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace AspNetHangfire.Controllers
{
    [Route("api/[cointroller]")]
    [ApiController]
    public class HangfireJobTestController : Controller
    {
        private readonly IHangfireTestJobService _hangfireTestService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        public HangfireJobTestController(IHangfireTestJobService hangfireTestService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            _hangfireTestService = hangfireTestService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet("/AddFireAndForgetJob")]
        public ActionResult AddFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _hangfireTestService.AddFireAndForgetJob());
            return Ok();
        }

        [HttpGet("/AddDelayedJob")]
        public ActionResult AddDelayedJob()
        {
            _backgroundJobClient.Schedule(() => _hangfireTestService.AddDelayedJob(), TimeSpan.FromMinutes(2));
            return Ok();
        }

        [HttpGet("/AddRecurringJob")]
        public ActionResult AddRecurringJob()
        {
            _recurringJobManager.AddOrUpdate("TestJobId", () => _hangfireTestService.AddReccuringJob(), Cron.Minutely);
            return Ok();
        }

        [HttpGet("/AddContinuationJob")]
        public ActionResult AddContinuationJob()
        {
            var parentJobId = _backgroundJobClient.Enqueue(() => _hangfireTestService.AddFireAndForgetJob());
            _backgroundJobClient.ContinueJobWith(parentJobId, () => _hangfireTestService.AddContinuationJob());
            return Ok();
        }
    }
}
