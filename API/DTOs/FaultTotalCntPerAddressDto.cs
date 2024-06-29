using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class FaultTotalCntPerAddressDto
    {
        public int Address { get; set; }
        public int TotalCnt { get; set; }
    }
}