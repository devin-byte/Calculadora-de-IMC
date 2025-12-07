using Microsoft.Maui.Controls;
using CalculadoraIMCApp.ViewModels;

namespace CalculadoraIMCApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new BMIViewModel();
        }
    }
}
