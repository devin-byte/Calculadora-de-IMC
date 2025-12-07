using Microsoft.Maui.Controls;
using CalculadoraIMCApp.Views;

namespace CalculadoraIMCApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
    }
}
