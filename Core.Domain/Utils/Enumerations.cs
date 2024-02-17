
using System.ComponentModel;

namespace Core.Domain.Utils
{
    public enum AuditLogEventType
    {
        [Description("Entity_Added")]
        Entity_Added = 0XBEEF/*48879*/,
        [Description("Entity_Modified")]
        Entity_Modified = 0XBEEF + 1,
        [Description("Entity_Deleted")]
        Entity_Deleted = 0XBEEF + 2,
        [Description("Sys_Login")]
        Sys_Login = 0XBEEF + 3,
        [Description("Sys_Logout")]
        Sys_Logout = 0XBEEF + 4,
        [Description("Sys_Other")]
        Sys_Other = 0XBEEF + 5
    }
    public enum RecordStatus
    {
        [Description("New")]
        New = 0,
        [Description("Edited")]
        Edited = 1,
        [Description("Approved")]
        Approved = 2,
        [Description("Rejected")]
        Rejected = 3,
        [Description("Processed")]
        Processed = 4,
        [Description("To Do")]
        ToDo = 5,
        [Description("In Progress")]
        InProgress = 6,
        [Description("Done")]
        Done = 7,
    }

    [Flags]
    public enum DLRStatus
    {
        [Description("UnKnown")]
        UnKnown = 1,
        [Description("Failed")]
        Failed = 2,
        [Description("Pending")]
        Pending = 4,
        [Description("Delivered")]
        Delivered = 8,
        [Description("Not Applicable")]
        NotApplicable = 16,
        [Description("Submitted")]
        Submitted = 32,
    }

    [Flags]
    public enum AuthType
    {
        [Description("Approve")]
        Approve = 1,
        [Description("Reject")]
        Reject = 2,
    }

    public enum WellKnownUserRoles
    {
        [Description("Super Administrator")]
        SuperAdministrator = 1,
        [Description("Administrator")]
        Administrator = 2,
        [Description("APIAccount")]
        APIAccount = 3,
        [Description("Standard User")]
        StandardUser = 4,
        [Description("Human Resource")]
        HumanResource = 5,
        [Description("Auditor")]
        Auditor = 6,
        [Description("Member")]
        Member = 7
    }

    public enum RoleType
    {
        [Description("Internal Role")]
        InternalRole,
        [Description("External Role")]
        ExternalRole
    }
    public enum Roles
    {
        Root = 1,
        Admin,
        Webapi,
        Regular,
        User,
        Member
    }

    public enum AuthPolicy
    {
        GlobalRights,
        ElevatedRights
    }

    public enum AssignableRoles
    {
        Admin = 2,
        Webapi,
        Regular
    }   
}
