using UserManagementApp.Views;

namespace UserManagementApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("HomePage", typeof(HomePage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));

            InitializeComponent();
        }
    }
}
