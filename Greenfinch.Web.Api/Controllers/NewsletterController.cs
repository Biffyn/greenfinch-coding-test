using System;
using System.Threading.Tasks;
using Greenfinch.Core.Models.Models;
using Greenfinch.Core.Models.Newsletter;
using Greenfinch.Core.Models.Validation;
using Greenfinch.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Greenfinch.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [RequireHttps]
    public class NewsletterController : ControllerBase
    {
        private readonly ILogger _logger;
        private INewsletterService _newsletterService;

        public NewsletterController(INewsletterService newsletterService, ILogger logger)
        {
            _newsletterService = newsletterService;
            _logger = logger;
        }

        /// <summary>
        /// Subscribes a user to the news letter
        /// </summary>        
        /// <response code="200">An API response model with the added or updated insurance id or with a list of validation errors</response>    
        /// <response code="500">Unknown error occured</response> 
        [Route("/api/newsletter/subscribe")]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Subscribe([FromBody]Subscription subscription)
        {
            _logger.ForContext<NewsletterController>().Information("Subscribe(subscription: {@Subscription})", subscription); ;
 
            try
            {
                ValidationErrorList validationErrors = await _newsletterService.ValidateSubscription(subscription);
                if (!validationErrors.isValid)
                {
                    return new JsonResult(new ApiResponse<bool>(false, validationErrors.Errors));
                }

                return new JsonResult(await _newsletterService.Subscribe(subscription));
            }
            catch (Exception ex)
            {
                _logger.ForContext<NewsletterController>().Error($"Something went wrong: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}