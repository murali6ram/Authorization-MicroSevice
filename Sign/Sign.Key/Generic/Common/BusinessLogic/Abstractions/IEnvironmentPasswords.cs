namespace Common.BusinessLogic.Abstractions;

public interface IEnvironmentPasswords
{
    string DbPassword { get; }

    string? MailPassword { get; }
}
