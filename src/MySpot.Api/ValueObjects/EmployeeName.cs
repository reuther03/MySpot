﻿using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects;

public record EmployeeName(string Value)
{
    public string Value { get; } = Value ?? throw new InvalidEmployeeNameException();

    public static implicit operator string(EmployeeName name)
        => name.Value;

    public static implicit operator EmployeeName(string name)
        => new(name);
}