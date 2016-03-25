using UnityEngine;
using System.Collections;

public class Medic : Unit
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int PISTOL 		= 0;
	private readonly int WAVE	 		= 1;
	private readonly int BULWARK 		= 2;
	private readonly int ADRENALINE		= 3;
	private readonly int TASER	 		= 4;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {4, 9}, {5, 11}, {6, 13}, {6, 14}, {7, 16}};
	private readonly int[] LVL_HEALTH = new int[] {24, 29, 34, 39, 44};
	private readonly int[] LVL_DODGE = new int[] {0, 5, 10, 15, 20};
	private readonly int[] LVL_SPEED = new int[] {4, 4, 5, 5, 6};
	private readonly int[] LVL_CRIT = new int[] {2, 3, 3, 4, 4};

	public Medic () : base ()
	{
		int NewLevel = 0;
		BASE_HEALTH = LVL_HEALTH[NewLevel];
		BASE_SPEED = LVL_SPEED[NewLevel];
		BASE_DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BASE_ARMOR = 0;

		CRIT_MODS = new int[] {5, 0, 0, 0, 0};
		DMG_MODS = new float[] {0f, 0f, 0f, 0f, -0.5f};
		ACC_MODS = new int[] {85, 0, 0, 0, 85};
		DEBUFF_MODS = new float[] {0f, 0f, 0.25f, 0.15f, -0.15f};

		CurrHealth = BASE_HEALTH;
		Level = 1;
		Rank = THREE;
		CAT = MEDIC;
		IS_MECH = false;
		IS_FRIENDLY = true;
		XP = 0;
		HasPlayed = false;
	}

	public override void SetStats (int NewLevel, int NewRank, int NewHealth)
	{
		NewLevel--;
		this.BASE_HEALTH = this.LVL_HEALTH[NewLevel];
		this.BASE_SPEED = this.LVL_SPEED[NewLevel];
		this.BASE_DODGE = this.LVL_DODGE[NewLevel];
		this.BASE_CRIT = this.LVL_CRIT[NewLevel];
		this.BASE_DMG = new int[] {this.LVL_DMG[NewLevel, 0], this.LVL_DMG[NewLevel, 1]};
		this.BASE_ARMOR = 0;

		this.CurrHealth = NewHealth;
		this.Level = NewLevel;
		this.Rank = NewRank;
	}


	public override bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target) {return false;}
}