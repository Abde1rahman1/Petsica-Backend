﻿using Petsica.Shared.Contracts.Community;
using Petsica.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Abstractions.Community;
public interface IPostService
{
    Task<Result<PostResponse>> AddAsync(PostRequest request, CancellationToken cancellationToken = default);
}
