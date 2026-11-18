using CommunityToolkit.Mvvm.Messaging.Messages;
namespace CounterInfo.Messages.SettingMenuPage;
internal sealed class OpenSettingPageMessage(int? CounterId) : ValueChangedMessage<int?>(CounterId) { }