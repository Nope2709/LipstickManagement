using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.RequestModel
{
    public class AddressRequestModel
    {

        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
    }
    public class CreateAddressRequestModel
    {
        
        public int? AccountId { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
    }
    public class UpdateAddressRequestModel
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
    }
}
