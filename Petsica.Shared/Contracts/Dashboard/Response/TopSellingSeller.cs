﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Dashboard.Response;
public record TopSellingSeller 
(
string SellerId,
int TotalQuantitySold,
decimal TotalSales
);
