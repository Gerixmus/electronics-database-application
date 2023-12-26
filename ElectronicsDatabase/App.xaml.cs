using ElectronicsDatabase.Pages;

namespace ElectronicsDatabase
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PartListPage());

            /*MainPage = new NavigationPage(new MainPage());*/

            /*MainPage = new PartDetailPage();*/

            /*MainPage = new AppShell();*/
        }
    }
}
