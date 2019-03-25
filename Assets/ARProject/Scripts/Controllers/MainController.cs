using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace ARProject.Scripts.Controllers
{
	public class MainController : Controller 
	{
		private void Start()
		{
			
		}

		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.PlayButtonPressed:
					App.GetNotificationCenter().Notify(Notification.StartWebservice);
					break;
				
				case Notification.SelectionButtonPressed:
					OnSelected(param.intData[0]);
					break;
			}
		}

		private void OnSelected(int i)
		{
			Debug.Log("Button Pressed "+i);
			
			//Check whether model is downloaded or not, 
		}
	}
}
