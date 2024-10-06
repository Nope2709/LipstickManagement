using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.ResponseModel
{
    public class AddressResponseModel
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
    }
}
