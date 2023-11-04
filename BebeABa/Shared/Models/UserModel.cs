using System.Collections.Generic;

namespace Shared.Models
{
    public class UserModel
    {
        public long UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserFullName { get; set; }
        public string UserPassword { get; set; }
        public bool IsDoctor { get; set; }

        public List<ChildrenModel> Childrens { get; set; }
        public bool CheckRememberMe { get; set; }
    }
}
