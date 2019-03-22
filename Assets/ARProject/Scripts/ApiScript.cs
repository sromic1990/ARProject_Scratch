using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiScript : MonoBehaviour 
{

	[SerializeField] private JSonClass jsonClass;
	
	private const string URL = "http://webappfactory.co/armodel/combined.php";

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(GenerateRequest());
	}

	IEnumerator GenerateRequest() 
	{
		WWW www = new WWW(URL);
		while (!www.isDone)
			yield return null;
		
		if (string.IsNullOrEmpty (www.error)) 
		{
			Debug.Log (www.text);
			string jsonResponse = www.text;
			jsonClass.SetData(jsonResponse);
		} 
		else
			Debug.Log (www.error);
	}
}
