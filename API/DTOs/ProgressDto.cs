using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ProgressDto
    {
        public int Value { get; set; }
        public int Percent { get; set; }
        public string Trend { get; set; } = "up";
        public string Status { get; set; } = "red";
    }
}