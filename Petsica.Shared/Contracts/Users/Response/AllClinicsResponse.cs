using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Users.Response;
public record AllClinicsResponse
(string UserName,
string Photo,
string Address,
 string ClinicID ,
 string WorkingHours,
 string ContactInfo 
);
