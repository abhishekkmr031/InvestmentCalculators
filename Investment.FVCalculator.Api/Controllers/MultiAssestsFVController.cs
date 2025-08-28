using FluentValidation;
using Investment.FVCalculator.Api.Services;
using Investment.FVCalculator.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Investment.FVCalculator.Api.Controllers;

[ApiController]
[Route("multiasset")]
[EnableRateLimiting("fixedMyPolicy")]
public class MultiAssestsFVController(IMultiAssetsFVServices multiAssetsFVServices, IValidator<Asset> validator) : Controller
{
    private readonly IMultiAssetsFVServices _multiAssetsFVServices = multiAssetsFVServices;
    private readonly IValidator<Asset> _validator = validator;

    [HttpPost]
    public async Task<IActionResult> CalculateAssestFV([FromBody] Asset asset)
    {
        var validationResult = await _validator.ValidateAsync(asset);
        if (validationResult.IsValid)
        {
            var response = _multiAssetsFVServices.CalculateAssestFV(asset);
            return Ok(response);
        }

        return BadRequest();        
    }
}
