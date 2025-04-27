namespace Petsica.Shared.Email.Helpers
{
    public static class EmailBodyBuilder
    {
        public static string GenerateEmailBody(string template, Dictionary<string, string> templateModel)
        {
            //var templatePath = $"{Directory.GetCurrentDirectory()}/../Petsica.Shared/Email/Templates/{template}.html";

            var basePath = Directory.GetCurrentDirectory();
            var templatePath = Path.Combine(basePath, "..", "Petsica.Shared", "Email", "Templates", $"{template}.html");

            var streamReader = new StreamReader(templatePath);
            var body = streamReader.ReadToEnd();
            streamReader.Close();

            foreach (var item in templateModel)
                body = body.Replace(item.Key, item.Value);

            return body;
        }
    }
}
