namespace Petsica.Shared.Email.Helpers
{
    public static class EmailBodyBuilder
    {
        public static string GenerateEmailBody(string template, Dictionary<string, string> templateModel)
        {
            // Get the base path of the current application directory
            var basePath = Directory.GetCurrentDirectory();

            // Construct the path to the Templates folder, assuming it’s copied to the output directory
            var templatePath = Path.Combine(basePath, "Email", "Templates", $"{template}.html");

            // Use a using statement to ensure the StreamReader is properly disposed
            using var streamReader = new StreamReader(templatePath);
            var body = streamReader.ReadToEnd();

            // Replace placeholders with values from the template model
            foreach (var item in templateModel)
            {
                body = body.Replace(item.Key, item.Value);
            }

            return body;
        }
    }
}
