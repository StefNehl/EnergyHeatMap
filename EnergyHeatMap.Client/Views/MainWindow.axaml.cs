using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EnergyHeatMap.Client.ViewModels;
using System;

namespace EnergyHeatMap.Client.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            DataContextChanged += async (object sender, EventArgs e) =>
            {
                var dataContext = DataContext as MainWindowViewModel;
                
                if(dataContext == null)
                    return;

                await dataContext.InitApplication();
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }
}
