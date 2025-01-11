using System.Collections.Generic;
using DTO.Account;

namespace kindergartenNetwork.Models.MainModels
{
    public class ConstantModel
    {
        public ConstantModel()
        {
            LstConstants = new List<Constant>();
        }
        public List<Constant> LstConstants { get; set; }
    }

    public class UpdateConstantModel
    {
        public UpdateConstantModel()
        {
            OConstant = new Constant();
            LstConstants = new List<Constant>();
        }
        public Constant OConstant { get; set; }
        public List<Constant> LstConstants { get; set; } 
    }
}