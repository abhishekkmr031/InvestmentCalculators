using Investment.FVCalculator.Common.Models;

namespace Investment.FVCalculator.Application.Services
{
    public interface IMultiAssetsFvServices
    {
        FVValue CalculateAssetsFv(Asset asset);
    }
}