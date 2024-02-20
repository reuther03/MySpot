namespace MySpot.Core.ValueObjects;

public sealed record Fullname
{
    public string Value { get; set; }

    public Fullname(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 3)
        {
            throw new ArgumentException("Fullname cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static implicit operator Fullname(string value) =>  new(value);
    public static implicit operator string(Fullname fullname) => fullname.Value;

    public override string ToString() => Value;

}