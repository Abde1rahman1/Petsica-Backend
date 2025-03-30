using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Error
{
    public static class MarketErrors
    {
        public static readonly Errors CategoryNotFound =
           new("Category.NotFound", "No Category was found with the given ID", StatusCodes.Status404NotFound);

        public static readonly Errors InvalidCreateCategory =
            new("Category.InvalidCreate", "Failed to create the Category.", StatusCodes.Status400BadRequest);

    }
}
