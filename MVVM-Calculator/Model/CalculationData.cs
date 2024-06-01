using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCalculator.Interface;

namespace WPFCalculator.Model
{
    public class CalculationData
    {
        public string? Result { get; set; }
        public string? Operation { get; set; }
        public string? NumberValue { get; set; }
        public double? ResultValue { get; set; }
    }
}
