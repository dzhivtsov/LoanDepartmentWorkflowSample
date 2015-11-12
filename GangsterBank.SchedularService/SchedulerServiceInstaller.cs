namespace GangsterBank.SchedulerService
{
    using System.ComponentModel;

    [RunInstaller(true)]
    public partial class SchedulerServiceInstaller : System.Configuration.Install.Installer
    {
        #region Constructors and Destructors

        public SchedulerServiceInstaller()
        {
            this.InitializeComponent();
        }

        #endregion
    }
}