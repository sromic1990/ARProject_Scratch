using System.Collections;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace ARProject.Scripts.Controllers
{
	public class DownloadHandler : Controller
	{
	

		private void DownloadFromURL(string URL, string ID, string fileExtension)
		{
			//Download and place code here
		
			//first link
			Debug.Log("Download model method");
			StartCoroutine(DownloadFromUrl(URL, ID, fileExtension));
		}

		private IEnumerator DownloadFromUrl(string url, string modelID, string fileExtension)
		{
			Debug.Log("DownloadFromUrl started");
			Debug.Log("url = "+url);
			WWW www = new WWW(url);
			while (!www.isDone)
			{
				yield return null;
				Debug.Log("www is downloading");
			}
		
			Debug.Log("www bytestream = "+www.text);
			string downloadPath = Application.persistentDataPath + "/Model/"+modelID+fileExtension;
			System.IO.File.WriteAllBytes(downloadPath, www.bytes);
			App.GetNotificationCenter().Notify(Notification.ModelDownloaded);
		}

		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.StartDownloadingModel:
					Debug.Log("Downloading model");
					DownloadFromURL(param.stringData[0], param.stringData[1], param.stringData[2]);
					break;
			}
		}
	}
}
