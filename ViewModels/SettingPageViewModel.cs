using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CounterInfo.ViewModels
{
    public class SettingPageViewModel : ObservableObject
    {
        public bool ChangeMode
        {
            get => _changeMode; set => SetProperty(ref _changeMode, value);
        }
        private bool _changeMode = false;


        public Visibility AppendModeVisible => ChangeMode ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ChangeModeVisible => !ChangeMode ? Visibility.Visible : Visibility.Collapsed;

        public int ID { get => _id; set => SetProperty(ref _id, value); }
        private int _id = 0;


        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        private string _name = "";

        public int Order { get => _order; set => SetProperty(ref _order, value); }
        private int _order;

        public SettingPageViewModel() {

            DeleteCommand =  new AsyncRelayCommand(Delete);
        }

        public ICommand DeleteCommand { get; }

        async Task Delete() {
            WeakReferenceMessenger.Default.Send(new Messages.CounterSettingDeleteMessage(ID));
        }
    }
}
