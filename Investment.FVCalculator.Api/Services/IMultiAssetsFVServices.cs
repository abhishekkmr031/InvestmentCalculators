using Investment.FVCalculator.Common.Models;

namespace Investment.FVCalculator.Api.Services
{
    public interface IMultiAssetsFVServices
    {
        FVValue CalculateAssestFV(Asset asset);
    }
}