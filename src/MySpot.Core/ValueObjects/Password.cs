namespace MySpot.Core.ValueObjects;

public record Password
{
    public string Value { get; set; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 6)
        {
            throw new ArgumentException("Password cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static implicit operator string(Password password) => password.Value;
    public static implicit operator Password(string value) => new(value);


    public override string ToString() => Value;
}