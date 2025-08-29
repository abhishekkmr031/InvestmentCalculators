using Investment.FVCalculator.Application.Services;
using Investment.FVCalculator.Common.Models;

namespace Investment.FVCalculator.Application.UnitTests.Services;

public class MultiAssetsFvServicesTests
{
    private readonly MultiAssetsFvServices _sut = new();

    [Fact]
    public void CalculateAssetsFv_ReturnsAssetsValue()
    {
        var asset = new Asset()
        {
            Name = "MF",
            SIPAmount = 5000,
            StartAmount = 5000,
            Interest = 5,
            InvestYears = 5,
            HoldYears = 5
        };
        var fv = 347864;
        var totalInvestment = asset.StartAmount + (asset.SIPAmount * asset.InvestYears * 12);
        var totalInterestEarned = fv - totalInvestment;
        
        var result = _sut.CalculateAssetsFv(asset);
        
        Assert.Equal(347864, result.FutureValue);
        Assert.Equal(totalInvestment, result.Investment);
        Assert.Equal(totalInterestEarned, result.TotalInterest);
    }
}