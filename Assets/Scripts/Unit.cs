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
	public readonly String STUN 		= "Stun";
	public readonly String BLEED 		= "Bleed";
	public readonly String ACID 		= "Acid";
	public readonly String ARMOR 		= "Armor";
	public readonly String SPEED 		= "Speed";
	public readonly String DODGE 		= "Dodge";
	public readonly String DAMAGE 		= "Damage";
	public readonly String WALL 		= "Light Wall";

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
	protected int BaseHealth;	// Starting/max health
	protected int BaseSpeed;	// Speed to determine turn order
	protected int BaseDodge;	// Dodge rating to determine incomiing hits
	protected int BaseCrit;	// Global crit chance
	protected int[] BaseDmg;	// Minimum and Maximum damage range
	protected float BaseArmor;	// Amount of physical damage protection

	// Character extra stats
	protected String Category;			// Category of character (class)
	protected bool IsMech;		// Tells whether or not the unit is mechanical
	protected bool IsFriendly;	// Tells whether the unit is friendly (to the player)
	protected List<Debuff> Debuffs;	// List to keep track of buffs and debuffs (USE INDICES FROM ABOVE) 
								
	// Ability specific stats
	public int[] CritMods;		// Crit modifiers for each ability
	public float[] DmgMods;		// Damage modifiers for each ability
	public int[] AccMods;		// Accuracy mods for each ability
	protected float[] DebuffMods;	// Strength of individual debuffs
	protected bool[][] HitRanks;	// Ranks that can be hit by ability
	protected bool[][] FromRanks;	// Ranks that can be hit by ability
	protected bool[] IsMultiHit;	// Whether or not the ability can hit all targets or one
	protected String[] AbilNames;

	/*==================================
	Vars that CAN/WILL be changed safely
	==================================*/
	protected int CurrHealth;	// Keeps track of the units current health
	protected int CurrSpeed;	// Keeps track of the units current speed
	protected int CurrDodge;	// Keeps track of the units current dodge
	protected int CurrArmor;	// Keeps track of the units current armor
	protected int Level;		// Keeps track of the units level
	protected int Rank;			// Keeps track of the units rank (position in line)
	protected int XP;			// Keeps track of the units XP
	protected bool HasPlayed;	// Keeps track of if the unit has performed an action this turn


	public Unit () {}


	public virtual void SetStats (int NewLevel, int NewRank) {}

	// Use this one for:
	//		- One Target
	public virtual bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target) {return false;}


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
				Rolls [i] = Enemies [i - Allies.Length].RollSpeed () + Enemies [i - Allies.Length].GetSpeed ();
				TempOrder [i] = Enemies [i - Allies.Length];
				continue;
			}
			Rolls [i] = Allies [i].RollSpeed() + Allies [i].GetSpeed();
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
		return this.GetSpeed () + roll.Next(8);
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
			if (U.GetRank() < TempMin)
			{
				TempMin = U.GetRank ();
			}
		}
		return TempMin;
	}


	internal int GetMaxRank (Unit[] Units)
	{
		int TempMax = -99999;
		foreach (Unit U in Units)
		{
			if (U.GetRank() > TempMax)
			{
				TempMax = U.GetRank ();
			}
		}
		return TempMax;
	}


	internal bool CheckHit (int MoveID, Unit Target)
	{
		Random roll = new Random ();
		int AccTemp = this.AccMods [MoveID] - Target.BaseDodge;

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

		float Mod = this.GetDmgMods ()[MoveID];
		Max += (int) (Max * Mod);
		Min += (int) (Min * Mod);

		int Range = Max - Min;

		DmgTemp = roll.Next (Range) + Min;

		if (CheckCrit(MoveID, this)) 
		{
			DmgTemp = DmgTemp * 2;
		}

		DmgTemp -= (int) (DmgTemp * Target.GetArmor());

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
		this.CurrHealth = this.CurrHealth + Amount;

		if (this.CurrHealth > this.GetBaseHealth ()) 
		{
			this.CurrHealth = this.GetBaseHealth ();
		}
	}


	internal void RemoveHealth (int Amount)
	{
		this.CurrHealth = this.CurrHealth - Amount;

		if (this.CurrHealth < 0) 
		{
			this.CurrHealth = 0;
		}
	}


	internal void MoveUnit (Unit[] Team, Unit Target, int Magnitude)
	{
		int StartRank = Target.GetRank ();
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
		int StartRank = this.GetRank ();
		this.SetRank (this.GetRank () - 1);

	    foreach (Unit U in Team)
	    {
			if (!U.Equals (this) && U.GetRank () == this.GetRank ())
	    	{
				U.SetRank (StartRank);
	    	}
	    }
	}


	internal void ForwardOne (Unit[] Team)
	{
		int StartRank = this.GetRank ();
		this.SetRank (this.GetRank () + 1);

	    foreach (Unit U in Team)
	    {
			if (!U.Equals (this) && U.GetRank () == this.GetRank ())
	    	{
				U.SetRank (StartRank);
	    	}
	    }
	}


	internal void AddDebuff (Debuff D)
	{
		this.Debuffs.Add (D);
	}


	internal void RemoveDebuff (Debuff D)
	{
		this.Debuffs.Remove(D);
	}


	public List<Debuff> GetDebuffs ()
	{
		return this.Debuffs;
	}


	public int GetBaseHealth ()
	{
		return this.BaseHealth;
	}


	public int GetHealth ()
	{
		return this.CurrHealth;
	}


	public int GetBaseSpeed ()
	{
		return this.BaseSpeed;
	}


	public int GetSpeed ()
	{
		return this.CurrSpeed;
	}


	public int GetBaseDodge ()
	{
		return this.BaseDodge;
	}


	public int GetDodge ()
	{
		return this.CurrDodge;
	}


	public int GetBaseCrit ()
	{
		return this.BaseCrit;
	}


	public int GetCrit (int Ability)
	{
		return this.BaseCrit + this.CritMods [Ability];
	}


	public float GetBaseArmor ()
	{
		return this.BaseArmor;
	}


	public float GetArmor ()
	{
		return this.CurrArmor;
	}


	public float[] GetDmgMods ()
	{
		return this.DmgMods;
	}


	public bool GetFriendly ()
	{
		return this.IsFriendly;
	}


	public int GetRank ()
	{
		return this.Rank;
	}


	protected void SetRank (int NewRank)
	{
		this.Rank = NewRank;
	}


	public bool[] GetAttackRange (int MoveID)
	{
		return this.HitRanks[MoveID];
	} 


	public bool GetIsMultiHit (int MoveID)
	{
		return this.IsMultiHit [MoveID];
	}


	public int GetLevel ()
	{
		return this.Level;
	}


	public int GetXP ()
	{
		return this.XP;
	}


	public void GiveXP (int Amount)
	{
		this.XP += Amount;

		// TODO Handle level ups here
	}

	public String GetCategory ()
	{
		return this.Category;
	}

	public bool GetIsMech ()
	{
		return this.IsMech;
	}
}