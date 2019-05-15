using System;

namespace Greenfinch.Core.Models.Newsletter
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Reason { get; set; }
        public string Referrer { get; set; }
        public DateTime StartDate { get; set; }
    }
}
