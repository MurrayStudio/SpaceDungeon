using UnityEngine;
using System.Collections;
using UnityEngineInternal;
using UnityEngine.SocialPlatforms;
using Random = System.Random;

public class Unit
{
	/*==================================
	Vars that CANNOT be changed safely
	===================================*/
	// Character specific stats
	protected int MAX_HEALTH;
	protected int SPEED;
	protected int DODGE;
	protected float BASE_CRIT;
	protected int[] BASE_DMG;
	protected int Cat;

	// Ability specific stats
	protected float[] CRIT;
	protected float[] DMG_MODS;
	protected int[] ACC;

	/*==================================
	Vars that CAN/WILL be changed safely
	===================================*/
	protected int CurrHealth;
	protected int Level;
	protected int Rank;

	public Unit ()
	{}

	public bool CheckHit (Unit Source, Unit Target, int Ability)
	{
		Random roll = new Random ();
		int AccTemp = Source.ACC [Ability] - Target.DODGE;

		if (AccTemp > 90)
			AccTemp = 90;

		if (roll.Next(100) < AccTemp)
		{
			return true;
		}
		return false;
	}
}

