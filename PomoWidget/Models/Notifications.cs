using Microsoft.Toolkit.Uwp.Notifications;

namespace PomoWidget;

public class Notifications
{
	public static void SendNotification(string title, string message)
	{
		new ToastContentBuilder()
			.AddArgument("action", "viewConversation")	// TODO use in events...
			.AddArgument("conversationId", 9813)
			.AddText(title)
			.AddText(message)
			.Show();
	}
}
