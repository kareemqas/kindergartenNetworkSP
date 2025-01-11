using System.ComponentModel.DataAnnotations;
using DTO.Common;

namespace DTO.Account
{
    public class Constant : DbProcess
    {
        public Constant()
        {
            ParentId = -1;
        }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Icon { get; set; }
        public bool IsDeleted { get; set; }
        public Constant OParent { get; set; }
    }
    

}
