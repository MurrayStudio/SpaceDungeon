using System;


public abstract class Unit
{
	/*==================================
				  CONSTANTS
	==================================*/
	// Enemies
	public readonly int INFECTED 	= 0;
	public readonly int FREIGHT 	= 1;
	public readonly int SECURITY 	= 2;
	public readonly int PSYCHIC 	= 3;
	public readonly int MEDIBOT 	= 4;

	// Allies 
	public readonly int ENFORCER 	= 0;
	public readonly int RIFLEMAN 	= 1;
	public readonly int MEDIC 		= 2;
	public readonly int ENGINEER 	= 3;

	// Debuff indices
	public readonly int STUN 		= 0;
	public readonly int BLEED 		= 1;
	public readonly int ACID 		= 2;
	public readonly int ARMOR 		= 3;
	public readonly int SPEED 		= 4;
	public readonly int DAMAGE 		= 5;
	public readonly int WALL 		= 6;
	public readonly int NUM_DEBUFFS	= 7;

	// Durations
	public readonly int STUN_DUR	= 1;
	public readonly int DEBUFF_DUR 	= 3;
	public readonly int DOT_DUR		= 4;

	/*==================================
	 Vars that CANNOT be changed safely
	===================================*/
	// Character specific stats
	protected int MAX_HEALTH;	// Starting/max health
	protected int BASE_SPEED;	// Speed to determine turn order
	protected int BASE_DODGE;	// Dodge rating to determine incomiing hits
	protected int BASE_CRIT;	// Global crit chance
	protected int[] BASE_DMG;	// Minimum and Maximum damage range
	protected float BASE_ARMOR;	// Amount of physical damage protection
	protected int CAT;			// Category of character (class)
	protected bool IS_MECH;		// Tells whether or not the unit is mechanical
	protected bool IS_FRIENDLY;	// Tells whether the unit is friendly (to the player)
	protected int[][,] DEBUFFS;	// List to keep track of buffs and debuffs (USE INDICES FROM ABOVE) Built as follows: 
								// { Stuns: { {Remaining_Duration, Power}, {...} }, Bleeds: { {...}, {...} }, ... }

	// Ability specific stats
	protected int[] CRIT_MODS;	// Crit modifiers for each ability
	protected float[] DMG_MODS;	// Damage modifiers for each ability
	protected int[] ACC_MODS;	// Accuracy mods for each ability

	/*==================================
	Vars that CAN/WILL be changed safely
	==================================*/
	protected int CurrHealth;	// Keeps track of the units current health
	protected int Level;		// Keeps track of the units level
	protected int Rank;			// Tracks where in the line the unit is positioned
	protected int XP;			// Keeps track of unit XP
	protected bool HasPlayed;	// Keeps track of if the unit has performed an action this turn

	public Unit ()
	{
		DEBUFFS = new int[NUM_DEBUFFS][,];
	}

	public virtual void SetStats (int NewLevel, int NewRank, int NewHealth){}

	public bool CheckValidAttack (int Ability, Unit Source, Unit Target)
	{
		if ((Source.GetFriendly() && !Target.GetFriendly()) || 
			(!Source.GetFriendly() && Target.GetFriendly())) 
		{
			return true;
		}
		return false;
	}

	public bool CheckHit (int Ability, Unit Source, Unit Target)
	{
		Random roll = new Random ();
		int AccTemp = Source.ACC_MODS [Ability] - Target.BASE_DODGE;

		if (AccTemp > 90)
			AccTemp = 90;

		if (roll.Next (100) < AccTemp) 
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

	public void AddHealth (int Amount)
	{
		this.CurrHealth = this.CurrHealth + Amount;

		if (this.CurrHealth > this.GetMaxHealth ()) 
		{
			this.CurrHealth = this.GetMaxHealth ();
		}
	}

	public void DecreaseHealth (int Amount)
	{
		this.CurrHealth = this.CurrHealth - Amount;

		if (this.CurrHealth < 0) 
		{
			this.CurrHealth = 0;
		}
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

	public void AddDebuff (int Type, float Power)
	{
		// Cycle through the TYPE of the debuff 
		int[] Count = new int[NUM_DEBUFFS];

		for (int i = 0; i < NUM_DEBUFFS; i++)
		{
			Count [i] = this.DEBUFFS[i].GetLength (0);
			if (i == Type)
			{
				Count [i] = Count [i] + 1;
			}
		}

	}

	public void GetDubuffs ()
	{
		for (int i = 0; i < this.DEBUFFS.GetLength (0); i++)
		{
			return;
		}
	}

	public int GetHealth ()
	{
		return this.CurrHealth;
	}

	public int GetMaxHealth ()
	{
		return this.MAX_HEALTH;
	}

	public bool GetFriendly ()
	{
		return this.IS_FRIENDLY;
	}
}