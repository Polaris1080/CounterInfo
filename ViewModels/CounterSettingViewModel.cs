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
    internal class CounterSettingViewModel : ObservableObject
    {

        public int ID { get => _id; set => SetProperty(ref _id, value); }
        private int _id;


        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        private string _name = "";

        public int Order { get => _order; set => SetProperty(ref _order, value); }
        private int _order;

        public CounterSettingViewModel(CounterSetting cs) {
            ID = cs.ID;
            Name = cs.Name;
            Order = cs.Order;

            DeleteCommand =  new AsyncRelayCommand(Delete);
        }

        public ICommand DeleteCommand { get; }

        async Task Delete() {
            WeakReferenceMessenger.Default.Send(new Messages.CounterSettingDeleteMessage(ID));
        }
    }
}
