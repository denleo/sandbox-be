using Sandbox.Utility.Result;

namespace Sandbox.Wordbook.Application;

public static class ApplicationErrors
{
    public static Error UserNotFound =>
        new("User.NotFound", "User was not found");

    public static Error TranslationAlreadyExists =>
        new("Translation.AlreadyExists", "Translation already exists");

    public static Error TranslationPermissionFailure =>
        new("Translation.PermissionRestricted", "Cannot change translations of other users");

    public static Error TranslationNotFound =>
        new("Translation.NotFound", "Translation was not found");

    public static Error TranslationResultNotFound =>
        new("TranslationResult.NotFound", "Translation result was not found");
}