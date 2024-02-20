namespace MySpot.Core.ValueObjects;

public record Role
{

    public static IEnumerable<string> AvailableRoles { get; } = new[] { "admin", "user" };
    public string Value { get; }

    public Role(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Role cannot be empty.", nameof(value));
        }

        Value = value;
    }

    public static Role Admin() => new("admin");
    public static Role User() => new("user");

    public static implicit operator string(Role role) => role.Value;
    public static implicit operator Role(string role) => new(role);

    public override string ToString() => Value;
}