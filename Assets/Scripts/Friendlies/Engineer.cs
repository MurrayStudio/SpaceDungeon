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
		DmgMods = new float[] {0f, -0.30f, -0.80f, -0.50f};
		AccMods = new int[] {85, 85, 85, 85, 0};
		ValidRanks = new bool[][] {
			new bool [] { true, true, true, true, false, false, false },	// Ratchet Gun	1-4
			new bool [] { true, true, false, false, false, false, false },	// Ion Pulse	enemies
			new bool [] { true, true, true, false, false, false, false },	// Flashbang	2-4 
			new bool [] { true, true, true, true, false, false, false },	// Snare		3-4
			new bool [] { false, false, false, false, true, false, false }	// Light Wall	allies
		};
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

	public override void SetStats (int NewLevel, int NewRank, int NewHealth)
	{
		NewLevel--;
		this.BaseHealth = this.LVL_HEALTH[NewLevel];
		this.BaseSpeed = this.LVL_SPEED[NewLevel];
		this.BaseDodge = this.LVL_DODGE[NewLevel];
		this.BaseCrit = this.LVL_CRIT[NewLevel];
		this.BaseDmg = new int[] {this.LVL_DMG[NewLevel, 0], this.LVL_DMG[NewLevel, 1]};
		this.BaseArmor = 0;

		this.CurrHealth = NewHealth;
		this.Level = NewLevel;
		this.Rank = NewRank;
	}


	public override bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target) 
	{
		if (MoveID == 0)
		{

		}
		return FAILURE;
	}
}