using UnityEngine;
using System.Collections;

public class Team : MonoBehaviour {

	private Unit[] myTeam;
	public int ENFORCER_LVL = 1;
	public int RIFLEMAN_LVL = 1;
	public int MEDIC_LVL = 1;
	public int ENGINEER_LVL = 1;

	// Use this for initialization
	void Start () {
		myTeam = new Unit[4] {new Enforcer(), new Rifleman(), new Medic(), new Engineer()};
		// GET STATS FROM PREFS 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
