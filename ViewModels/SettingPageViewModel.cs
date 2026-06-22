using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Models;
using CounterInfo.Pages;
using CounterInfo.Repository;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CounterInfo.ViewModels
{
    public partial class SettingPageViewModel : ObservableObject
    {
        /* Value */
        private readonly CounterSettingRepository repository = new();
        private readonly int id = 0;
        private          int order;


        /* Property */
        [ObservableProperty][NotifyPropertyChangedFor(nameof(AppendModeVisible))][NotifyPropertyChangedFor(nameof(ChangeModeVisible))]
        public partial bool ChangeMode { get; set; } = false;
        public Visibility AppendModeVisible =>  ChangeMode ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ChangeModeVisible => !ChangeMode ? Visibility.Visible : Visibility.Collapsed;

        [ObservableProperty] public partial string Name           { get; set; } = "";
        [ObservableProperty] public partial int     IssueInterval { get; set; } = 3;
        [ObservableProperty] public partial int    ReceptInterval { get; set; } = 2;
        [ObservableProperty] public partial int    AbsenceParcent { get; set; } = 1;
        [ObservableProperty] public partial int    Range          { get; set; } = 1;


        /* Initnitialize */
        public SettingPageViewModel() {
            Task.Run(async () => { order = await repository.GetCountAsync(); });
        }

        public SettingPageViewModel(CounterSetting cs) {
            id             = cs.ID;
            order          = cs.Order;
            Name           = cs.Name;
             IssueInterval = cs.IssueInterval;
            ReceptInterval = cs.ReceptInterval;
            AbsenceParcent = cs.AbsenceParcent;
            Range          = cs.Range;
            ChangeMode     = true;
        }

        public CounterSetting ToCounterSetting() => new() {
            ID             = id,
            Order          = order,
            Name           = Name,
             IssueInterval =  IssueInterval,
            ReceptInterval = ReceptInterval,
            AbsenceParcent = AbsenceParcent,
            Range          = Range
        };
    }
}
