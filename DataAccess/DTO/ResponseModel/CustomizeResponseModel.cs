﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.ResponseModel
{
    public class CustomizeResponseModel
    {
        public int Id { get; set; }
        public int? LipstickId { get; set; }
        public string? EngravingText { get; set; }
        public string? QrCodeUrl { get; set; }
        public string Lipstick { get; set; }
    }
}
