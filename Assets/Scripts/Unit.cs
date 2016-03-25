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
	protected int[][] VALID_RANKS;	// Ranks that can be hit by ability
	protected bool[] IS_MULTI_HIT;	// Whether or not the ability can hit all targets or one

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


	public virtual void SetStats (int NewLevel, int NewRank, int NewHealth) {}


	public virtual bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target) {return false;}

	/*
	public void Encounter (Unit[] Allies, Unit[] Enemies)
	{
		if (Allies.Length > 4 || Allies.Length < 1 || Enemies.Length > 4 || Enemies.Length < 1)
		{
			return;
		}
		Unit[] TurnOrder = Order (Allies, Enemies);

		//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
	}
	*/

	internal Unit[] Order (Unit[] Allies, Unit[] Enemies) //TODO Clean this up, can be done without the Rolls array
	{
		int NumUnits = Allies.Length + Enemies.Length;
		Unit[] TempOrder = new Unit[NumUnits];
		int[] Rolls = new int[NumUnits];
		int i;

		for (i = 0; i < NumUnits; i++)
		{
			if (i >= Allies.Length)
			{
				Rolls[i] = Enemies[i - Allies.Length].RollSpeed();
				TempOrder [i - Allies.Length] = Enemies [i];
				continue;
			}
			Rolls [i] = Allies [i].RollSpeed();
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
		return this.GetSpeed () + roll.Next(10);
	}


	public bool CheckValidMove (int MoveID, Unit Target)
	{
		if (MoveID == ALLIES || MoveID == ENEMIES)
		{
			return false;
		}

		if ((this.GetFriendly() && !Target.GetFriendly()) || 
			(!this.GetFriendly() && Target.GetFriendly())) 
		{
			if (this.GetAttackRange (MoveID) [0] == SELF && this.Equals (Target))
			{
				return true;
			}

			int MaxRank = this.GetAttackRange (MoveID) [1];
			int MinRank = this.GetAttackRange (MoveID) [0];

			int TempRange = MaxRank - MinRank;
			for (int i = 0; i < TempRange; i++)
			{
				if (Target.GetRank () == i)
				{
					return true;
				}
			} 
		}
		return false;
	}


	public bool CheckValidMove (int MoveID, Unit[] Targets)
	{
		if ( (MoveID <= SELF && MoveID >= ONE) || !this.IsMultiHit(MoveID) ) 
		{
			return false;
		}

		if ((this.GetFriendly () && !Targets [0].GetFriendly ()) ||
		    (!this.GetFriendly () && Targets [0].GetFriendly ())) 
		{
			int MaxRank = this.GetAttackRange (MoveID) [1];
			int MinRank = this.GetAttackRange (MoveID) [0];

			foreach (Unit Target in Targets)
			{
				for (int i = GetMinRank(Targets); i < GetMaxRank(Targets); i++) 
				{
					if (Target.GetRank () != i) 
					{
						return false;
					}
				}
			}	 
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
		int AccTemp = this.ACC_MODS [MoveID] - Target.BASE_DODGE;

		if (AccTemp > 90)
			AccTemp = 90;

		if (roll.Next (100) < AccTemp) 
		{
			return true;
		}
		return false;
	}


	internal int RollDamage (int MoveID, int Min, int Max, Unit Enemy)
	{
		int DmgTemp;
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

		DmgTemp -= (int) (DmgTemp * Enemy.GetArmor());

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


	public int GetBaseSpeed ()
	{
		return this.BASE_SPEED;
	}


	public int GetSpeed ()
	{
		return this.CurrSpeed;
	}


	public int GetBaseDodge ()
	{
		return this.BASE_DODGE;
	}


	public int GetDodge ()
	{
		return this.CurrDodge;
	}


	public int GetBaseCrit ()
	{
		return this.BASE_CRIT;
	}


	public int GetCrit (int Ability)
	{
		return this.BASE_CRIT + this.CRIT_MODS [Ability];
	}


	public float GetBaseArmor ()
	{
		return this.BASE_ARMOR;
	}


	public float GetArmor ()
	{
		return this.CurrArmor;
	}


	public float[] GetDmgMods ()
	{
		return this.DMG_MODS;
	}


	public bool GetFriendly ()
	{
		return this.IS_FRIENDLY;
	}


	public int GetRank ()
	{
		return this.Rank;
	}


	protected void SetRank (int NewRank)
	{
		this.Rank = NewRank;
	}


	public int[] GetAttackRange (int MoveID)
	{
		return this.VALID_RANKS[MoveID];
	} 


	public bool IsMultiHit (int MoveID)
	{
		return this.IS_MULTI_HIT [MoveID];
	}
}