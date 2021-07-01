using System;
namespace UkrainiansId.Application.ViewModels
{
    public abstract class SocialData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string SocialNetworkId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialType { get; set; }
    }
}