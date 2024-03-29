﻿using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class AddManufacturerRequestValidator : RequestBaseValidator<AddManufacturerRequest, Manufacturer>
    {
        public AddManufacturerRequestValidator()
        {
            RuleFor(manufacturer => manufacturer.Name).NotNull().NotEmpty();
        }
    }
}
