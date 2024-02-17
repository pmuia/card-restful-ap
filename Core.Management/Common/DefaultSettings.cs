
namespace Core.Management.Common
{
    public sealed class DefaultSettings
    {
        private static readonly object SyncRoot = new object();

        private DefaultSettings() { }

        private static DefaultSettings instance;
        public static DefaultSettings Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    if (instance == null)
                        instance = new DefaultSettings();

                    /*
                     * Only OptimaERP group should know this :-(
                     */
                    instance.RootUser = "su";
                    instance.RootPassword = "H@rd2Cr@k!";
                    instance.AuditUser = "auditor";
                    instance.AuditPassword = "Ch@11enge";
                    instance.FirstName = "Super";
                    instance.LastName = "User";
                    instance.Password = "yeknod!";
                    instance.RootEmail = "info@NitoPOS.co.ke";
                    instance.TablePrefix = "NitoPOS_";
                    instance.ApplicationDisplayName = "NitoPOS™";
                    instance.ApplicationCopyright = $"Copyright © 2012-{DateTime.Now.Year}. {Environment.NewLine}All rights reserved.";
                    instance.CompanyName = "NitoPOS Limited";
                    instance.PageSizes = new List<int> { 15, 25, 50, 100, 200, 400 };
                    instance.EndDateTimeSpan = new TimeSpan(23, 59, 59);
                    instance.ClusteredId = "ClusteredId";
                    instance.SuperUserDescription = "Perform all the operations.";
                    instance.StandardUserDescription = "Perform normal operations.";
                    instance.OptimaERPMobileNumber = "+254718212885";
                    instance.StandardUser = "standardUser";
                    instance.StandardPassword = "H@rd2Cr@k!";
                    instance.StandardUserEmail = "standardUser@NitoPOS.co.ke";
                    instance.StandardFirstName = "Standard";
                    instance.StandardLastName = "User";
                    instance.AdministratorUserDescription = "Perform all Administrator operations.";
                    instance.APIUserDescription = "Perform API operations.";
                }

                return instance;
            }
        }

        public string RootUser { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string RootPassword { get; private set; }

        public string AuditUser { get; private set; }

        public string AuditPassword { get; private set; }

        public string Password { get; private set; }

        public string RootEmail { get; private set; }

        public string ApplicationDisplayName { get; private set; }

        public string ApplicationCopyright { get; private set; }

        public string TablePrefix { get; private set; }

        public TimeSpan EndDateTimeSpan { get; private set; }

        public string CompanyName { get; private set; }

        public string ClusteredId { get; private set; }

        public List<int> PageSizes { get; private set; }

        public string SuperUserDescription { get; private set; }

        public string StandardUserDescription { get; private set; }

        public string AdministratorUserDescription { get; private set; }

        public string APIUserDescription { get; private set; }

        public string OptimaERPMobileNumber { get; private set; }

        public string StandardUser { get; private set; }

        public string StandardPassword { get; private set; }

        public string StandardUserEmail { get; private set; }

        public string StandardFirstName { get; private set; }

        public string StandardLastName { get; private set; }
    }
}
