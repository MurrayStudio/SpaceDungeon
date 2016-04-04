/**
 * Unit.cs
 *
 * Class created for the final project in CS 44Something, Game Design. 
 * Manages Unit stats, interactions between Units, and allllllll combat. I'm friggin dying. 
 */

using System;
using System.Collections.Generic;

public abstract class Unit
{
	/*==================================
				  CONSTANTS
	==================================*/
	// Allies 
	public readonly int ENFORCER 	= 0;
	public readonly int RIFLEMAN 	= 1;
	public readonly int MEDIC 		= 2;
	public readonly int ENGINEER 	= 3;

	// Enemies
	public readonly int INFECTED 	= 0;
	public readonly int FREIGHT 	= 1;
	public readonly int SECURITY 	= 2;
	public readonly int PSYCHIC 	= 3;
	public readonly int MEDIBOT 	= 4;

	// Rank + target constants
	public readonly int ONE 		= 0;
	public readonly int TWO 		= 1;
	public readonly int THREE 		= 2;
	public readonly int FOUR 		= 3;
	public readonly int SELF		= 4;
	public readonly int ALLIES		= 5;
	public readonly int ENEMIES		= 6;

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

	// Hit Indicators
	public readonly bool SUCCESS 	= true;
	public readonly bool FAILURE 	= false;

	/*==================================
	 Vars that CANNOT be changed safely
	===================================*/
	// Character Base stats
	protected int BASE_HEALTH;	// Starting/max health
	protected int BASE_SPEED;	// Speed to determine turn order
	protected int BASE_DODGE;	// Dodge rating to determine incomiing hits
	protected int BASE_CRIT;	// Global crit chance
	protected int[] BASE_DMG;	// Minimum and Maximum damage range
	protected float BASE_ARMOR;	// Amount of physical damage protection

	// Character extra stats
	protected int CAT;			// Category of character (class)
	protected bool IS_MECH;		// Tells whether or not the unit is mechanical
	protected bool IS_FRIENDLY;	// Tells whether the unit is friendly (to the player)
	protected List<Debuff> DEBUFFS;	// List to keep track of buffs and debuffs (USE INDICES FROM ABOVE) 
								
	// Ability specific stats
	protected int[] CRIT_MODS;		// Crit modifiers for each ability
	protected float[] DMG_MODS;		// Damage modifiers for each ability
	protected int[] ACC_MODS;		// Accuracy mods for each ability
	protected float[] DEBUFF_MODS;	// Strength of individual debuffs
	protected bool[][] VALID_RANKS;	// Ranks that can be hit by ability
	protected bool[] IS_MULTI_HIT;	// Whether or not the ability can hit all targets or one

	/*==================================
	Vars that CAN/WILL be changed safely
	==================================*/
	protected int CurrHealth;	// Keeps track of the units current health
	protected int CurrSpeed;	// Keeps track of the units current speed
	protected int CurrDodge;	// Keeps track of the units current dodge
	protected int CurrArmor;	// Keeps track of the units current armor
	protected int level;		// Keeps track of the units level
	protected int rank;			// Keeps track of the units rank (position in line)
	protected int xp;			// Keeps track of the units XP
	protected bool HasPlayed;	// Keeps track of if the unit has performed an action this turn


	public Unit () {}


	public virtual void SetStats (int NewLevel, int NewRank, int NewHealth) {}

	// Use this one for:
	//		- One Target
	public virtual bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target) {return false;}

	// Use this one for:
	// 		- Set of targets
	public virtual bool[] MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit[] Targets) {return new bool[] {false};}


	public Unit[] Order (Unit[] Allies, Unit[] Enemies) //TODO Clean this up, can be done without the Rolls array
	{
		int NumUnits = Allies.Length + Enemies.Length;
		Unit[] TempOrder = new Unit[NumUnits];
		int[] Rolls = new int[NumUnits];
		int i;

		for (i = 0; i < NumUnits; i++)
		{
			if (i >= Allies.Length)
			{
				Rolls[i] = Enemies[i - Allies.Length].RollSpeed() + CurrSpeed;
				TempOrder [i] = Enemies [i - Allies.Length];
				continue;
			}
			Rolls [i] = Allies [i].RollSpeed() + CurrSpeed;
			TempOrder [i] = Allies [i];
		}

		// Insertion Sort
		for (i = 0; i < NumUnits; i++)
		{
			int j = i;
			int TempNum;
			Unit TempUnit;
			while (j > 0 && Rolls [j - 1] > Rolls [j])
			{
				TempNum = Rolls [j];
				Rolls [j] = Rolls [j - 1];
				Rolls [j - 1] = TempNum;

				TempUnit = TempOrder [j];
				TempOrder [j] = TempOrder [j - 1];
				TempOrder [j - 1] = TempUnit;
			}
		}
		return TempOrder;
	}


	internal int RollSpeed ()
	{
		Random roll = new Random ();
		return CurrSpeed + roll.Next(10);
	}


	public bool CheckValidMove (int MoveID, Unit Target)
	{
		if (this.GetAttackRange (MoveID) [SELF] && this.Equals (Target))
		{
			return true;
		}



		return false;
	}


	internal int GetMinRank (Unit[] Units)
	{
		int TempMin = 99999;
		foreach (Unit U in Units)
		{
			if (U.Rank < TempMin)
			{
				TempMin = U.Rank;
			}
		}
		return TempMin;
	}


	internal int GetMaxRank (Unit[] Units)
	{
		int TempMax = -99999;
		foreach (Unit U in Units)
		{
			if (U.Rank > TempMax)
			{
				TempMax = U.Rank;
			}
		}
		return TempMax;
	}


	internal bool CheckHit (int MoveID, Unit Target)
	{
		Random roll = new Random ();
		int AccTemp = this.ACC_MODS [MoveID] - Target.BASE_DODGE;

		if (AccTemp > 90)
			AccTemp = 90;

		if (roll.Next (100) < AccTemp) 
		{
			return true;
		}
		return false;
	}


	internal int RollDamage (int MoveID, int[] DmgRange, Unit Target)
	{
		int DmgTemp;
		int Min = DmgRange[0];
		int Max = DmgRange[1];
		Random roll = new Random ();

		float Mod = DMG_MODS[MoveID];
		Max += (int) (Max * Mod);
		Min += (int) (Min * Mod);

		int Range = Max - Min;

		DmgTemp = roll.Next (Range) + Min;

		if (CheckCrit(MoveID, this)) 
		{
			DmgTemp = DmgTemp * 2;
		}

		DmgTemp -= (int) (DmgTemp * Target.Armor);

		return DmgTemp;
	}


	internal bool CheckCrit (int MoveID, Unit Source) // CHECK THIS
	{
		Random roll = new Random ();
		if (roll.Next (100) < this.GetCrit (MoveID)) 
		{
			return true;
		}
		return false;
	}


	internal void AddHealth (int Amount)
	{
		CurrHealth = CurrHealth + Amount;

		if (CurrHealth > BASE_HEALTH) 
		{
			CurrHealth = BASE_HEALTH;
		}
	}


	internal void RemoveHealth (int Amount)
	{
		CurrHealth = CurrHealth - Amount;

		if (CurrHealth < 0) 
		{
			CurrHealth = 0;
		}
	}


	internal void MoveUnit (Unit[] Team, Unit Target, int Magnitude)
	{
		int StartRank = Target.Rank;
		int EndRank = StartRank + Magnitude;

	    if (StartRank == EndRank || 
	    	(StartRank == FOUR && Magnitude < 0) || 
	    	(StartRank == ONE && Magnitude > 0))
	    {
	        return;
	    }

	    for (int i = 0; i < Math.Abs(Magnitude); i++)
	    {
	    	if (Magnitude > 0)
	    	{
				Target.ForwardOne (Team);
	    	}
	    	else if (Magnitude < 0)
	    	{
				Target.BackwardOne (Team);
	    	}
	    }
	}


	internal void BackwardOne (Unit[] Team)
	{
		int StartRank = rank;
		rank -= 1;

	    foreach (Unit U in Team)
	    {
			if (!U.Equals (this) && U.Rank == rank)
	    	{
				U.Rank = StartRank;
	    	}
	    }
	}


	internal void ForwardOne (Unit[] Team)
	{
		int StartRank = rank;
        rank += 1;

	    foreach (Unit U in Team)
	    {
			if (!U.Equals (this) && U.Rank == rank)
	    	{
				U.Rank = StartRank;
	    	}
	    }
	}


	internal void AddDebuff (Debuff D)
	{
		DEBUFFS.Add (D);
	}


	internal void RemoveDebuff (Debuff D)
	{
		DEBUFFS.Remove(D);
	}


	public List<Debuff> Debuffs
	{
        get {
            return DEBUFFS;
        }
    }


	public int BaseHealth
	{
		get
        {
            return BASE_HEALTH;
        }
	}


	public int Health
	{
		get
        {
            return this.CurrHealth;
        }
	}


	public int BaseSpeed
	{
		get
        {
            return BASE_SPEED;
        }
	}


	public int Speed
	{
		get
        {
            return CurrSpeed;
        }
	}


	public int BaseDodge
	{
		get
        {
            return BASE_DODGE;
        }
	}


	public int Dodge
	{
		get
        {
            return CurrDodge;
        }
	}


	public int BaseCrit
	{
		get
        {
            return BASE_CRIT;
        }
	}


	public int GetCrit (int Ability)
	{
		return BASE_CRIT + CRIT_MODS [Ability];
	}


	public float BaseArmor
	{
		get
        {
            return BASE_ARMOR;
        }
	}


	public float Armor
	{
		get
        {
            return CurrArmor;
        }
	}


	public float[] DmgMods
	{
		get
        {
            return DMG_MODS;
        }
	}


	public bool Friendly
	{
		get
        {
            return IS_FRIENDLY;
        }
	}


	public int Rank
	{
		get
        {
            return rank;
        }
        set
        {
            rank = value;
        }
	}


	public bool[] GetAttackRange (int MoveID)
	{
		return VALID_RANKS[MoveID];
	} 


	public bool IsMultiHit (int MoveID)
	{
		return IS_MULTI_HIT [MoveID];
	}


	public int Level
	{
		get
        {
            return level;
        }
	}


	public int XP
	{
		get
        {
            return xp;
        }
    }


	public void GiveXP (int Amount)
	{
		xp += Amount;

        // TODO Handle level ups here
    }
}