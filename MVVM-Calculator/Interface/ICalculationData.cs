using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCalculator.Model;

namespace WPFCalculator.Interface
{
    public class ICalculationData
    {
        public string? Result { get; set; }
        public string? Operation { get; set; }
        public string? NumberValue { get; set; }
        public double? ResultValue { get; set; }
    }
}
