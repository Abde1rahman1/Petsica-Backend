﻿namespace Petsica.Shared.Contracts.Pets.Response
{
    public record PetsMatingResponse(
  int PetID,
  string UserName,
  string Species,
  string Photo,
  string Gender,
  string Name,
  string Breed
      );
}