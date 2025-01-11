using DTO.Common;

namespace DTO.Account
{
    public class UserType : DbProcess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
