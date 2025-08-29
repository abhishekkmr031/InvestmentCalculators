using FluentValidation;
using Investment.FVCalculator.Application.Services;
using Investment.FVCalculator.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Investment.FVCalculator.Api.Controllers;

[ApiController]
[Route("multiasset")]
[EnableRateLimiting("fixedMyPolicy")]
public class MultiAssetsFvController(IMultiAssetsFvServices multiAssetsFvServices, IValidator<Asset> validator) : Controller
{
    private readonly IMultiAssetsFvServices _multiAssetsFvServices = multiAssetsFvServices;
    private readonly IValidator<Asset> _validator = validator;

    [HttpPost]
    public async Task<IActionResult> CalculateAssetsFv([FromBody] Asset asset)
    {
        var validationResult = await _validator.ValidateAsync(asset);
        
        if (!validationResult.IsValid) return BadRequest();
        
        var response = _multiAssetsFvServices.CalculateAssetsFv(asset);
        
        return Ok(response);
    }
}
