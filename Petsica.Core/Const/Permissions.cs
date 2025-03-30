namespace Petsica.Core.Const
{
    public static class Permissions
    {
        public static string Type { get; } = "permissions";

        public const string GetPets = "pets:read";
        public const string AddPets = "pets:add";
        public const string UpdatePets = "pets:update";
        public const string DeletePets = "pets:delete";

        public const string GetCategories = "categories:read";
        public const string AddCategories = "categories:add";
        public const string UpdateCategories = "categories:update";

        public const string GetUsers = "users:read";
        public const string AddUsers = "users:add";
        public const string UpdateUsers = "users:update";

        public const string GetRoles = "roles:read";
        public const string AddRoles = "roles:add";
        public const string UpdateRoles = "roles:update";

        public const string GetUserFollows = "userFollows:read";
        public const string AddUserFollows = "userFollows:add";
        public const string UpdateUserFollows = "userFollows:update";

        public const string GetLikes = "likes:read";
        public const string AddLikes = "likes:add";
        public const string UpdateLikes = "likes:update";

        public const string GetComments = "comments:read";
        public const string AddComments = "comments:add";
        public const string UpdateComments = "comments:update";

        public const string GetPosts = "posts:read";
        public const string AddPosts = "posts:add";
        public const string UpdatePosts = "posts:update";

        public static IList<string?> GetAllPermissions() =>
            typeof(Permissions).GetFields().Select(x => x.GetValue(x) as string).ToList();
    }
}

