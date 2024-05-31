using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScientificCalculator.ViewModels
{
    [INotifyPropertyChanged]
    internal partial class MainPageViewModel
    {
        [ObservableProperty]
        private string inputText = "";
    }
}
