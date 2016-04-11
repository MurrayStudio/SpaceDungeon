using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Engineer : Unit
{
    /*==================================
			   Ability Indexes
	===================================*/
    private readonly int FLASHBANG      = 0;
    private readonly int ION            = 1;
    private readonly int LIGHT_WALL     = 2;
    private readonly int RATCHET		= 3;
	private readonly int SNARE	 		= 4;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {4, 7}, {5, 8}, {6, 10}, {6, 11}, {7, 13} };
	private readonly int[] LVL_HEALTH = new int[] {19, 23, 27, 31, 35};
	private readonly int[] LVL_DODGE = new int[] {10, 15, 20, 25, 30};
	private readonly int[] LVL_SPEED = new int[] {6, 6, 7, 7, 8};
	private readonly int[] LVL_CRIT = new int[] {7, 8, 8, 9, 9};

	public Engineer () : base ()
	{
		int NewLevel = 0;
		BaseHealth = LVL_HEALTH[NewLevel];
		BaseSpeed = LVL_SPEED[NewLevel];
		BaseDodge = LVL_DODGE[NewLevel];
		BaseCrit = LVL_CRIT[NewLevel];
		BaseDmg = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BaseArmor = 0;

		CritMods = new int[] {3, 0, 0, 0, 0};
		DmgMods = new float[] {0f, 0f, 0f, -0.30f, -0f};
		AccMods = new int[] {85, 85, 85, 85, 85};
		DebuffMods = new float[] { 0f, 0f, 0f, 0.15f, 0f };
		HitRanks = new bool[][] {
			new bool [] { false, true, true, true, false, false, false },	// Flashbang	2-4 
			new bool [] { false, false, false, false, false, false, true },	// Ion Pulse	enemies
			new bool [] { false, false, false, false, false, true, false },	// Light Wall	allies
			new bool [] { true, true, true, true, false, false, false },	// Ratchet Gun	1-4
			new bool [] { false, false, true, true, false, false, false }	// Snare		3-4
		};
		FromRanks = new bool[][] {
			new bool [] { false, false, true, true },	// Ratchet Gun	3-4
			new bool [] { false, false, false, true },	// Ion Pulse	4
			new bool [] { false, false, true, true },	// Flashbang	3-4 
			new bool [] { false, false, true, true },	// Snare		3-4
			new bool [] { false, false, false, true }	// Light Wall	4
		};
		IsMultiHit = new bool[] { false, true, false, false, false };
		Debuffs = new List<Debuff>();

		CurrHealth = BaseHealth;
		Level = 1;
		Rank = FOUR;
		Category = "Engineer";
		IsMech = false;
		IsFriendly = true;
		XP = 0;
		HasPlayed = false;
	}

	public override void SetStats (int NewLevel, int NewRank)
	{
		this.BaseHealth = this.LVL_HEALTH[NewLevel];
		this.BaseSpeed = this.LVL_SPEED[NewLevel];
		this.BaseDodge = this.LVL_DODGE[NewLevel];
		this.BaseCrit = this.LVL_CRIT[NewLevel];
		this.BaseDmg = new int[] {this.LVL_DMG[NewLevel, 0], this.LVL_DMG[NewLevel, 1]};
		this.BaseArmor = 0;

		this.CurrHealth = this.LVL_HEALTH[NewLevel];
		this.Level = NewLevel;
		this.Rank = NewRank;
	}


	public override bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target) 
	{
		if (MoveID != LIGHT_WALL)
		{
			if (!CheckHit (MoveID, Target))
			{
				return FAILURE;
			}	
		}

		if (MoveID == FLASHBANG)
		{
			Debuff D1 = new Debuff (STUN_DUR, DebuffMods [FLASHBANG], STUN);
			Target.AddDebuff (D1);
			return SUCCESS;
		}
		else if (MoveID == ION)
		{
			Debuff D2 = new Debuff(STUN_DUR, DebuffMods [ION], STUN);
			Debuff D3 = new Debuff(DEBUFF_DUR, DebuffMods [ION], SPEED);
			foreach (Unit U in Enemies)
			{
				if (U.GetIsMech())
				{
					U.AddDebuff (D2);
					continue;
				}
				U.AddDebuff(D3);
			}
			return SUCCESS;
		}
		else if  (MoveID == LIGHT_WALL)
		{
			Debuff D4 = new Debuff (DEBUFF_DUR, DebuffMods [LIGHT_WALL], WALL);
			foreach (Unit U in Allies)
			{
				U.AddDebuff(D4);
			}
			return SUCCESS;
		}
		else if (MoveID == RATCHET)
		{
			Target.RemoveHealth (this.RollDamage (MoveID, this.BaseDmg, Target));
			Debuff D5 = new Debuff (DEBUFF_DUR, DebuffMods [RATCHET], DAMAGE);
			this.AddDebuff (D5);
			return SUCCESS;
		}
		else if (MoveID == SNARE)
		{
			Target.MoveUnit(Enemies, Target, 2);

		}
		return FAILURE;
	}
}