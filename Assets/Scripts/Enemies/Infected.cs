using UnityEngine;
using System.Collections;

public class Infected : Unit
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int CLAW 			= 0;
	private readonly int ACID_SPIT	 	= 1;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {4, 9}, {5, 11}, {6, 12}, {7, 14}, {8, 15}};
	private readonly int[] LVL_HEALTH = new int[] {15, 20, 25, 30, 35};
	private readonly int[] LVL_DODGE = new int[] {10, 15, 20, 25, 30};
	private readonly int[] LVL_SPEED = new int[] {3, 3, 4, 4, 5};
	private readonly int[] LVL_CRIT = new int[] {5, 6, 6, 7, 7};

	public Infected () : base ()
	{
		int NewLevel = 0;
		BaseHealth = LVL_HEALTH[NewLevel];
		BaseSpeed = LVL_SPEED[NewLevel];
		BaseDodge = LVL_DODGE[NewLevel];
		BaseCrit = LVL_CRIT[NewLevel];
		BaseDmg = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BaseArmor = 0;

		CritMods = new int[] {0, 0};
		DmgMods = new float[] {0f, -0.20f};
		AccMods = new int[] {85, 85};

		CurrHealth = BaseHealth;
		Level = 1;
		Rank = 1;
		Category = "Infected";
		IsMech = false;
		IsFriendly = false;
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

	public override bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target) {return false;}
}

