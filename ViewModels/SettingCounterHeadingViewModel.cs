using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Pages;
using CounterInfo.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CounterInfo.ViewModels
{
    class SettingCounterHeadingViewModel : ObservableObject
    {
        CounterSettingRepository counterSettingRepository = new();

        public int ID { get;  }
        public string Name { get;  } = "";
        private int order;

        public SettingCounterHeadingViewModel(Models.CounterSetting cs)
        {
            // Param
            ID = cs.ID;
            Name = cs.Name;
            order = cs.Order;

            // Command
              UpCommand = new AsyncRelayCommand(OrderUp);
             SetCommand = new AsyncRelayCommand(Setting);
            DownCommand = new AsyncRelayCommand(OrderDown);
        }

        public ICommand   UpCommand { get; }
        public ICommand  SetCommand { get; }
        public ICommand DownCommand { get; }

        private async Task OrderUp() {
            if (order > 0)
            {
                await counterSettingRepository.ChangeOrderAsync(1);
                //Message
            }            
        }
        private async Task OrderDown() {
            
        }
        private async Task Setting()
        {
            WeakReferenceMessenger.Default.Send(new Messages.OpenSettingPageMessage(ID));
        }
    }
}
