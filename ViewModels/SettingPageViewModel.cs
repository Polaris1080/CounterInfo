using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Models;
using CounterInfo.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CounterInfo.ViewModels
{
    public partial class SettingPageViewModel : ObservableObject
    {
        [ObservableProperty][NotifyPropertyChangedFor(nameof(AppendModeVisible))][NotifyPropertyChangedFor(nameof(ChangeModeVisible))]
        public partial bool ChangeMode { get; set; }
        public Visibility AppendModeVisible =>  ChangeMode ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ChangeModeVisible => !ChangeMode ? Visibility.Visible : Visibility.Collapsed;

        [ObservableProperty]
        public partial int ID { get; set; } = 0;

        [ObservableProperty]
        public partial string Name { get; set; } = "";

        [ObservableProperty]
        public partial int IssueInterval { get; set; } = 3;

        [ObservableProperty]
        public partial int ReceptInterval { get; set; } = 2;

        [ObservableProperty]
        public partial int AbsenceParcent { get; set; } = 1;

        [ObservableProperty]
        public partial int Range { get; set; } = 0;

        [ObservableProperty]
        public partial int Order { get; set; } = 0;

        public SettingPageViewModel() { }

        public SettingPageViewModel(CounterSetting cs) {
            ID             = cs.ID;
            Name           = cs.Name;
             IssueInterval = cs.IssueInterval;
            ReceptInterval = cs.ReceptInterval;
            AbsenceParcent = cs.AbsenceParcent;
            Range          = cs.Range;
        }

        public CounterSetting ToCounterSetting() => new() {
            ID             = ID,
            Name           = Name,
             IssueInterval =  IssueInterval,
            ReceptInterval = ReceptInterval,
            AbsenceParcent = AbsenceParcent,
            Range          = Range
        };
    }
}
