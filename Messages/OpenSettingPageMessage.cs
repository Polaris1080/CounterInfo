using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CounterInfo.Messages
{
    class OpenSettingPageMessage : ValueChangedMessage<int?>
    {
        public OpenSettingPageMessage(int? id) : base(id)
        {

        }
    }
}
