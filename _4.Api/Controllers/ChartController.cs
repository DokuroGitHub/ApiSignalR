using Application.Hubs.Chart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace RealTimeCharts.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IHubContext<ChartHub> _chartHub;

        public ChartController(IHubContext<ChartHub> hub)
        {
            _chartHub = hub;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var random = new Random();
            var colors = new List<string>() {
                "rgba(0, 123, 255, 0.6)",
                "rgba(255, 99, 132, 0.6)",
                "rgba(54, 162, 235, 0.6)",
                "rgba(255, 206, 86, 0.6)",
                "rgba(75, 192, 192, 0.6)",
            };
            var chartModels = new List<ChartModel>();
            int i = 1;
            foreach (var color in colors)
            {
                chartModels.Add(new ChartModel()
                {
                    Data = new List<int>() { random.Next(1, 50) },
                    Label = "Data " + i++.ToString(),
                    BackgroundColor = color,
                });
            }
            _chartHub.Clients.All.SendAsync("TransferChartData", chartModels);
            return Ok(new { Message = "Request Completed" });
        }
    }
}


public class ChartModel
{
    public List<int> Data { get; set; }
    public string? Label { get; set; }
    public string? BackgroundColor { get; set; }

    public ChartModel()
    {
        Data = new List<int>();
    }
}
