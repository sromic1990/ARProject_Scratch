using System.Collections.Generic;
using Dummiesman;
using Sirenix.OdinInspector;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;

namespace ARProject.Scripts.Controllers
{
	public class MainController : Controller 
	{
		
		[SerializeField] private GameObject loadedObject;
		[SerializeField] private Transform gameobjectParent;
		private void Start()
		{
			App.GetLevelData().listOfDownloadedModels = new List<GameObject>();
			App.GetLevelData().downloadedModelIDs = new List<int>();
			
		}

		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.PlayButtonPressed:
					App.GetNotificationCenter().Notify(Notification.StartWebservice);
					break;
				
				case Notification.SelectionButtonPressed:
					hideAllModels();
					OnSelected(param.intData[0]);
					break;
				case Notification.ModelDownloaded:
					// load model
					LoadObjFromPath();
					ShowModel();
					break;
				
			}
		}

		private void ShowModel()
		{
			App.GetLevelData().listOfDownloadedModels[App.GetLevelData().listOfDownloadedModels.Count - 1].Show();
		}

		private void hideAllModels()
		{
			for (int i = 0; i < App.GetLevelData().listOfDownloadedModels.Count; i++)
			{
				App.GetLevelData().listOfDownloadedModels[i].Hide();
			}
		}

		private void OnSelected(int i)
		{
			Debug.Log("Button Pressed "+i);
			Debug.Log("obj link" +App.GetLevelData().root.data[i].model);
			App.GetLevelData().selectionButtonPressed = i;
			
			//Check whether model is downloaded or not, 
			if (App.GetLevelData().downloadedModelIDs.Contains(i))
			{
//				
				int index = -1;
				for (int j = 0; j <App.GetLevelData().downloadedModelIDs.Count ; j++)
				{
					if (App.GetLevelData().downloadedModelIDs[j] == i)
					{
						index = j;
						break;
					}
				
				}
				
				App.GetLevelData().listOfDownloadedModels[index].Show();
			}
			else
			{
				// download
				// fire notification on download success
				// load obj from path
				// add it to the gameobject list in level data
				// add downoloaded model id  in level data
				// show model
				NotificationParam downloadData = new NotificationParam(Mode.stringData);
				downloadData.stringData.Add(App.GetLevelData().root.data[i].model);
				downloadData.stringData.Add( i.ToString());
				downloadData.stringData.Add(".obj");
				
				App.GetNotificationCenter().Notify(Notification.StartDownloadingModel, downloadData);
				
			}
		
//			Debug.Log();
		}

		private void LoadObjFromPath()
		{
			string objPath = Application.persistentDataPath + "/" +"Model/" + App.GetLevelData().selectionButtonPressed.ToString()+".obj";
				loadedObject = new OBJLoader().Load(objPath);
				loadedObject.transform.parent = gameobjectParent;
				
				App.GetLevelData().listOfDownloadedModels.Add(loadedObject);
				App.GetLevelData().downloadedModelIDs.Add(App.GetLevelData().selectionButtonPressed);
		}

	}
}
