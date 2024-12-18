﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValudObjects;

public record Payment
{
    public string? CardName { get; } = default!;
    public string? CardNumber { get; }=default!;
    public string? Expiration { get; } = default!;
    public string CVV { get; } =default!;
    public int PaymentMethod { get; } = default!;
}
