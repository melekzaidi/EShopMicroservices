using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValudObjects;

public record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string? EmailAdress { get; }=default!;
    public string AdressLine { get; }=default!;
    public string State {  get; } = default!;   
    public string ZipCode { get; }=default!;        

}
