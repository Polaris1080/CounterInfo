namespace CounterInfo.Server.Extensions;

using CounterInfo.Server.Models;
using CounterInfo.Shared;



public static class RegistrationStateExtension
{
    extension(CounterInfomation ci)
    {
        public RegistrationState ToRegistrationState() => (ci.IssueTime, ci.ReceptTime, ci.Called) switch
        {
            (null, _, _) => RegistrationState.UnIssue,
            (_, null, _) => RegistrationState.Issue,
            (_, _, true) => RegistrationState.Called,
            (_, _, false) => RegistrationState.Recept
        };
    }
}
