using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CounterInfo.Messages
{
    class CounterSettingDeleteMessage : ValueChangedMessage<int>
    {
        public CounterSettingDeleteMessage(int id) : base(id)
        {

        }
    }
}
