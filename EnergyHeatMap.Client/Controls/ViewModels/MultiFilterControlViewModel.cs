using EnergyHeatMap.Client.ViewModels;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace EnergyHeatMap.Client.Controls.ViewModels
{
    public class MultiFilterControlViewModel : ViewModelBase
    {
        public MultiFilterControlViewModel()
        {
            var items = new ObservableCollection<MutliFilterItem>();
            items.Add(new MutliFilterItem()
            {
                Name = "Test 1",
                IsSelected = true,
            });

            items.Add(new MutliFilterItem()
            {
                Name = "Test 2",
                IsSelected = false,
            });

            items.Add(new MutliFilterItem()
            {
                Name = "Test 3",
                IsSelected = false,
            });

            Items = items;
        }

        public ObservableCollection<MutliFilterItem> Items { get; set; }
    }

    public class MutliFilterItem : ViewModelBase
    {
        private string _name = "";

        public string Name 
        { 
            get => _name; 
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => this.RaiseAndSetIfChanged(ref _isSelected, value); 
        }
    }
}
