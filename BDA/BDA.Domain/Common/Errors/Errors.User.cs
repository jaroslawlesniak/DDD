using ErrorOr;

namespace BDA.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(code: "User.DuplicatedEmail", description: "Email is already in use.");
    }
}