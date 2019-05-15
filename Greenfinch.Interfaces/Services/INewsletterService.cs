using Greenfinch.Core.Models.Models;
using Greenfinch.Core.Models.Newsletter;
using Greenfinch.Core.Models.Validation;
using System.Threading.Tasks;

namespace Greenfinch.Interfaces.Services
{
    public interface INewsletterService
    {
        Task<ValidationErrorList> ValidateSubscription(Subscription subscription);
        Task<ApiResponse<bool>> Subscribe(Subscription subscription);
    }
}
