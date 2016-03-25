using UnityEngine;
using System.Collections;

public class EnforcerInterface : MonoBehaviour 
{
	public Enforcer me;

	// Use this for initialization
	void Start () 
	{
		me = new Enforcer ();//TODO Get new constructor values from prefs between levels? or figure out DontDestroyOnLoad
	}
}
