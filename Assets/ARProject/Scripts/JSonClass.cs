using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSonClass : MonoBehaviour
{	
	[SerializeField] private RootObject root = new RootObject();

	public void SetData(string jsonData)
	{
		Debug.Log("Data set");
		root = JsonUtility.FromJson<RootObject>(jsonData);
	}

	public RootObject GetRootObject()
	{
		return root;
	}
}
[System.Serializable]
public class RootObject
{
	public List<Datum> data;
}

[System.Serializable]
public class Datum
{
	public int id;
	public string model;
	public string model_checksum;
	public string uv_map;
	public string uv_map_checksum;
	public string diffused_uv_map;
	public string diffused_uv_map_checksum;
}


