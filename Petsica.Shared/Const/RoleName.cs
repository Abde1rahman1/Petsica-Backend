namespace Petsica.Core.Const
{
    public static class RoleName
    {
        public const string Admin = "ADMIN";
        public const string Member = "MEMBER";
        public const string Sitter = "SITTER";
        public const string Seller = "SELLER";
        public const string Clinic = "CLINIC";

        public static IList<string?> GetAllRoleNames() =>
            typeof(RoleName).GetFields().Select(x => x.GetValue(x) as string).ToList();
    }
}

