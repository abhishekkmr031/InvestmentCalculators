namespace Investment.FVCalculator.Common.Models
{
    public class FVValue : Asset
    {
        public double Investment { get; set; }
        public double TotalInterest { get; set; }
        public double FutureValue { get; set; }
    }
}
