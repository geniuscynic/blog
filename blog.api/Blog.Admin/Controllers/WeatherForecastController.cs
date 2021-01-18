using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Blog.IService;
using Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Logging;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IApiMethodService _services;
        private readonly IApiDescriptionGroupCollectionProvider _apiDescription;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IApiMethodService services, IApiDescriptionGroupCollectionProvider apiDescription)
        {
            _logger = logger;
            _services = services;
            _apiDescription = apiDescription;
        }

        /// <summary>
        /// 获取 weather
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("InitApiMethod")]
        public void InitApiMethod()
        {
            //_services.
            //var _apiDescriptionsProvider = _services.GetService<IApiDescriptionGroupCollectionProvider>();
             _apiDescription.ApiDescriptionGroups.Items
                .SelectMany(group => group.Items)
                .Select(p => new ApiMethod
                {
                    RoutePath = p.RelativePath,
                    HttpMethod = p.HttpMethod    ,
                    Action = p.ActionDescriptor.RouteValues["action"],
                    Controller = p.ActionDescriptor.RouteValues["Controller"],


                })
                .OrderBy(p => p.RoutePath)
                .ThenBy(p => p.HttpMethod)
                .ForAll(t=> _services.Add(t));
        }

    }
}
