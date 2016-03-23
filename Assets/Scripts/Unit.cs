using System;
using System.Collections.Generic;

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

	// Debuff Types
	public readonly int STUN 		= 0;
	public readonly int DAMAGE 		= 1;
	public readonly int ACID 		= 2;
	public readonly int BLEED 		= 3;
	public readonly int SPEED 		= 4;
	public readonly int DODGE 		= 5;
	public readonly int ARMOR 		= 6;
	public readonly int WALL 		= 7;

	// Debuff Durations
	public readonly int STUN_DUR	= 1;
	public readonly int DEBUFF_DUR 	= 3;
	public readonly int DOT_DUR		= 4;

	/*==================================
	 Vars that CANNOT be changed safely
	===================================*/
	// Character specific stats
	protected int BASE_HEALTH;	// Starting/max health
	protected int BASE_SPEED;	// Speed to determine turn order
	protected int BASE_DODGE;	// Dodge rating to determine incomiing hits
	protected int BASE_CRIT;	// Global crit chance
	protected int[] BASE_DMG;	// Minimum and Maximum damage range
	protected float BASE_ARMOR;	// Amount of physical damage protection
	protected int CAT;			// Category of character (class)
	protected bool IS_MECH;		// Tells whether or not the unit is mechanical
	protected bool IS_FRIENDLY;	// Tells whether the unit is friendly (to the player)
	protected List<Debuff> DEBUFFS;	// List to keep track of buffs and debuffs (USE INDICES FROM ABOVE) 
								
	// Ability specific stats
	protected int[] CRIT_MODS;	// Crit modifiers for each ability
	protected float[] DMG_MODS;	// Damage modifiers for each ability
	protected int[] ACC_MODS;	// Accuracy mods for each ability
	protected float[] DEBUFF_MODS;	// Strength of individual debuffs

	/*==================================
	Vars that CAN/WILL be changed safely
	==================================*/
	protected int CurrHealth;	// Keeps track of the units current health
	protected int CurrSpeed;	// Keeps track of the units current health
	protected int CurrDodge;	// Keeps track of the units current health
	protected int CurrArmor;	// Keeps track of the units current health
	protected int Level;		// Keeps track of the units level
	protected int Rank;			// Tracks where in the line the unit is positioned
	protected int XP;			// Keeps track of unit XP
	protected bool HasPlayed;	// Keeps track of if the unit has performed an action this turn

	public Unit () {}

	public virtual void SetStats (int NewLevel, int NewRank, int NewHealth){}

	internal bool CheckValidAttack (int Ability, Unit Source, Unit Target)
	{
		if ((Source.GetFriendly() && !Target.GetFriendly()) || 
			(!Source.GetFriendly() && Target.GetFriendly())) 
		{
			return true;
		}
		return false;
	}

	internal bool CheckHit (int Ability, Unit Source, Unit Target)
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

	internal bool CheckCrit (int Ability, Unit Source) // CHECK THIS
	{
		Random roll = new Random ();
		if (roll.Next (100) < this.GetCrit (Ability)) 
		{
			return true;
		}
		return false;
	}

	internal void AddHealth (int Amount)
	{
		this.CurrHealth = this.CurrHealth + Amount;

		if (this.CurrHealth > this.GetBaseHealth ()) 
		{
			this.CurrHealth = this.GetBaseHealth ();
		}
	}

	internal void DecreaseHealth (int Amount)
	{
		this.CurrHealth = this.CurrHealth - Amount;

		if (this.CurrHealth < 0) 
		{
			this.CurrHealth = 0;
		}
	}

	internal int RollDamage (int AttackID, int Min, int Max, Unit Enemy)
	{
		int DmgTemp;
		Random roll = new Random ();

		float Mod = this.GetDmgMods ()[AttackID];
		Max += (int) (Max * Mod);
		Min += (int) (Min * Mod);

		int Range = Max - Min;

		DmgTemp = roll.Next (Range) + Min;

		if (CheckCrit(AttackID, this)) 
		{
			DmgTemp = DmgTemp * 2;
		}

		DmgTemp -= (int) (DmgTemp * Enemy.GetArmor());

		return DmgTemp;
	}

	internal void AddDebuff (Debuff D)
	{
		this.DEBUFFS.Add (D);
	}

	internal void RemoveDebuff (Debuff D)
	{
		this.DEBUFFS.Remove(D);
	}

	public List<Debuff> GetDebuffs ()
	{
		return this.DEBUFFS;
	}

	public int GetBaseHealth ()
	{
		return this.BASE_HEALTH;
	}

	public int GetHealth ()
	{
		return this.CurrHealth;
	}

	public bool GetFriendly ()
	{
		return this.IS_FRIENDLY;
	}

	public int GetSpeed ()
	{
		return this.BASE_SPEED;
	}

	public int GetDodge ()
	{
		return this.BASE_DODGE;
	}

	public int GetBaseCrit ()
	{
		return this.BASE_CRIT;
	}

	public int GetCrit (int Ability)
	{
		return this.BASE_CRIT + this.CRIT_MODS [Ability];
	}

	public float GetArmor ()
	{
		return this.ARMOR;
	}

	public float[] GetDmgMods ()
	{
		return this.DMG_MODS;
	}
}