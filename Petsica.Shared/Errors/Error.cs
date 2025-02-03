namespace Petsica.Shared.Error
{
    public record Errors(string Code, string Description, int? StatusCode)
    {
        public static readonly Errors None = new(string.Empty, string.Empty, null);
    }
}
