using FluentValidation;
using Investment.FVCalculator.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.FVCalculator.Common.Validators
{
    public class AssetRequestValidators : AbstractValidator<Asset>
    {
        public AssetRequestValidators()
        {
            RuleFor(x => x.StartAmount)
                .GreaterThan(0)
                .WithMessage("Starting amount cannot be less than 0");

            RuleFor(x => x.SIPAmount)
                .GreaterThan(499)
                .WithMessage("SIP amount cannot be less than 500");

            RuleFor(x => x.Interest)
               .GreaterThan(0)
               .LessThan(100)
               .WithMessage("Interest cannot be less than 0 or greater than 99");

            RuleFor(x => x.InvestYears)
               .GreaterThan(0)
               .LessThan(51)
               .WithMessage("Investing year can only be between 1 and 51");

            RuleFor(x => x)
               .Must(x=>x.HoldYears >= x.InvestYears)
               .WithMessage("Holding cannot be less than investing years");

            RuleFor(x => x.HoldYears)
               .GreaterThan(0)
               .LessThan(51)
               .WithMessage("Holding year can only be between 1 and 51");
        }
    }
}
