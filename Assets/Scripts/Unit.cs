using UnityEngine;
using System.Collections;
using UnityEngineInternal;
using UnityEngine.SocialPlatforms;
using Random = System.Random;
using UnityEngine.Events;

public class Unit
{
	/*==================================
	 Vars that CANNOT be changed safely
	===================================*/
	// Character specific stats
	protected int MAX_HEALTH;		// Starting/max health
	protected int SPEED;			// Speed to determine turn order
	protected int DODGE;			// Dodge rating to determine incomiing hits
	protected int BASE_CRIT;		// Global crit chance
	protected int[] BASE_DMG;		// Minimum and Maximum damage range
	protected int CAT;				// Category of character (class)
	protected float ARMOR;			// Amount of physical damage protection
	protected bool IS_STUNNED;		// Tracks if the character is stunned
	protected int[,] DEBUFFS;		// List to keep track of buffs and debuffs
	protected bool IS_MECH;			// Tells whether or not the unit is mechanical
	protected int XP;				// Keeps track of unit XP

	// Ability specific stats
	protected int[] CRIT_MODS;		// Crit modifiers for each ability
	protected float[] DMG_MODS;		// Damage modifiers for each ability
	protected int[] ACC_MODS;		// Accuracy mods for each ability

	/*==================================
	Vars that CAN/WILL be changed safely
	==================================*/
	protected int CurrHealth;
	protected int Level;
	protected int Rank;

	/*==================================
				  CONSTANTS
	==================================*/

	public Unit ()
	{
		DEBUFFS = new int[,] { };
	}

	public bool CheckValidAttack (int Ability, Unit Source, Unit Target)
	{
		if ((Source is Friendly && Target is Enemy) || (Source is Enemy && Target is Friendly))
		{
			return true;
		}
		return false;
	}

	public bool CheckHit (int Ability, Unit Source, Unit Target)
	{
		Random roll = new Random ();
		int AccTemp = Source.ACC_MODS [Ability] - Target.DODGE;

		if (AccTemp > 90)
			AccTemp = 90;

		if (roll.Next(100) < AccTemp)
		{
			return true;
		}
		return false;
	}

	public bool CheckCrit (int Ability, Unit Source) // CHECK THIS
	{
		Random roll = new Random ();
		int CritTemp = Source.CRIT_MODS [Ability] + Source.BASE_CRIT;
		if (roll.Next (100) < CritTemp)
		{
			return true;
		}
		return false;
	}

	public void DecreaseHealth (int Amount)
	{
		
	}

	public int RollDamage (int Min, int Max, bool IsCrit)
	{
		int DmgTemp;
		Random roll = new Random ();

		int Range = Max - Min;

		DmgTemp = roll.Next (Range) + Min;

		if (IsCrit)
		{
			DmgTemp = DmgTemp * 2;
		}

		return DmgTemp;
	}
}

