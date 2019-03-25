using System.Collections;
using ARProject.Scripts.Controllers;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.DataRelated;
using UnityEngine;

namespace ARProject.Scripts
{
	public class DownloadHandler : GameElement
	{
		[SerializeField] private JSonClass json;

		public void OnDownloadButtonPressed()
		{
			DownloadFromRoot(App.GetLevelData().root);
		}

		private void DownloadFromRoot(RootObject root)
		{
			//Download and place code here
		
			//first link
			string url = root.data[0].diffused_uv_map;
			StartCoroutine(DownloadFromUrl(url, root.data[0].id.ToString()));
		}

		private IEnumerator DownloadFromUrl(string url, string modelID)
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
			string downloadPath = Application.persistentDataPath + "/Model/"+modelID+".jpg";
			System.IO.File.WriteAllBytes(downloadPath, www.bytes);
		}
	}
}
