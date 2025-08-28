using Investment.FVCalculator.Common.Models;

namespace Investment.FVCalculator.Api.Services;

public class MultiAssetsFVServices : IMultiAssetsFVServices
{
    public FVValue CalculateAssestFV(Asset asset)
    {
        var response = new FVValue();

       (response.FutureValue,response.Investment, response.TotalInterest) = CalculatePortfolio(asset.StartAmount, asset.SIPAmount, asset.Interest, asset.InvestYears, asset.HoldYears);

        response.Name = asset.Name;
        response.Interest = asset.Interest;
        response.InvestYears = asset.InvestYears;
        response.HoldYears = asset.HoldYears;
        response.SIPAmount = asset.SIPAmount;
        response.StartAmount = asset.StartAmount;
        return response;
    }

    public static (double FinalValue, double Invested, double Gain) CalculatePortfolio(
        double initialLumpSum,
        double monthlySIP,
        double annualRate,
        int investYears,
        int holdYears)
    {
        double r = annualRate / 100 / 12;
        int investMonths = investYears * 12;
        int holdMonths = (holdYears-investYears) * 12;

        // 1. Lump sum grows for investYears + holdYears
        double lumpFV = (double)initialLumpSum * Math.Pow(1 + r, investMonths + holdMonths);

        // 2. SIP FV during invest years
        double sipFV = (double)monthlySIP *
                       ((Math.Pow(1 + r, investMonths) - 1) / r) *
                       (1 + r);

        // Then grow SIP FV further for hold years
        double sipFVAfter = sipFV * Math.Pow(1 + r, holdMonths);

        // 3. Total portfolio value
        double finalValue = (double)Math.Round(lumpFV + sipFVAfter, 2);

        // Total invested = lump sum + SIP invested for invest years
        double invested = initialLumpSum + (monthlySIP * investMonths);

        double gain = finalValue - invested;

        finalValue = Math.Round(finalValue, 3);
        invested = Math.Round(invested, 3);
        gain = Math.Round(gain, 3);

        return (finalValue, invested, gain);
    }
}
