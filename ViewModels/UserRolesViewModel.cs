using System.Collections.Generic;
namespace WebClothes.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
        public IEnumerable<string> AvailableRoles { get; set; }
    }
}
