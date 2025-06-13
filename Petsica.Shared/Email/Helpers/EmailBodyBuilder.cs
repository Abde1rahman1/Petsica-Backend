namespace Petsica.Shared.Email.Helpers
{
    public static class EmailBodyBuilder
    {
        public static string GenerateEmailBody(string template, Dictionary<string, string> templateModel)
        {

            var basePath = Directory.GetCurrentDirectory();

           
            var templatePath = Path.Combine(basePath, "Email", "Templates", $"{template}.html");


            using var streamReader = new StreamReader(templatePath);
            var body = streamReader.ReadToEnd();

            foreach (var item in templateModel)
            {
                body = body.Replace(item.Key, item.Value);
            }

            return body;
        }
    }
}
