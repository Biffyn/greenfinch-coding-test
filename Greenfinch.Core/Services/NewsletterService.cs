using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Serilog;
using System.Text.RegularExpressions;
using Greenfinch.Interfaces;
using Greenfinch.Interfaces.Services;
using Greenfinch.Core.Models.Validation;
using Greenfinch.Core.Models.Newsletter;
using Greenfinch.Core.Models.Models;

namespace Greenfinch.Core.Services
{
    public class NewsletterService : INewsletterService
    {
        private IApiContext _db;
        private readonly ILogger _logger ;

        public NewsletterService(IApiContext db, ILogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<ValidationErrorList> ValidateSubscription(Subscription subscription)
        {
            _logger.ForContext<NewsletterService>().Information("ValidateSubscription(subscription: {@Subscription})", subscription);

            ValidationErrorList errorList = new ValidationErrorList();

            if (subscription == null)
            {
                errorList.AddError("form", "nullSubscription");
                return errorList;
            }

            await ValidateEmail(subscription, errorList);
            await ValidateReferrer(subscription, errorList);

            return errorList;
        }

        public async Task<ApiResponse<bool>> Subscribe(Subscription subscription)
        {
            _logger.ForContext<NewsletterService>().Information("Subscribe(subscription: {@Subscription})");
            var errorList = new ValidationErrorList();

            try
            {
                subscription.StartDate = DateTime.UtcNow;

                _db.Subscriptions.Add(subscription);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Unhandled Exception");
                errorList.AddError("form", "unknownError");
                return new ApiResponse<bool>(false, errorList.Errors);
            }

            return new ApiResponse<bool>(true, null);

        }

        private async Task<ValidationErrorList> ValidateEmail(Subscription subscription, ValidationErrorList errorList)
        {
            _logger.ForContext<NewsletterService>().Debug("ValidateEmail(subscription: {@Subscription})", subscription);

            if (string.IsNullOrWhiteSpace(subscription.Email))
            {
                errorList.AddError(nameof(subscription.Email), "required");
            }

            if (!string.IsNullOrWhiteSpace(subscription.Email) &&
                (!new EmailAddressAttribute().IsValid(subscription.Email) ||
                !Regex.Match(subscription.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase).Success))
            {
                errorList.AddError(nameof(subscription.Email), "email");
            }

            if (await _db.Subscriptions.AnyAsync(s => s.Email == subscription.Email))
            {
                errorList.AddError(nameof(subscription.Email), "existingEmail");
            }

            return errorList;
        }

        private async Task<ValidationErrorList> ValidateReferrer(Subscription subscription, ValidationErrorList errorList)
        {
            _logger.ForContext<NewsletterService>().Debug("ValidateName(subscription: {@Subscription})", subscription);

            if (string.IsNullOrWhiteSpace(subscription.Referrer))
            {
                errorList.AddError(nameof(subscription.Referrer), "required");
            }

            return errorList;
        }
 
    }
}
