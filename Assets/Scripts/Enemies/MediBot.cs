using UnityEngine;
using System.Collections;

public class MediBot : Unit
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int RESTORE 		= 0;
	private readonly int STIMS		 	= 1;
	private readonly int SELF_DESTRUCT	= 2;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {5, 10}, {6, 12}, {7, 13}, {8, 15}, {9, 16} };
	private readonly int[] LVL_HEALTH = new int[] {16, 21, 26, 31, 36};
	private readonly int[] LVL_DODGE = new int[] {0, 5, 10, 15, 20};
	private readonly int[] LVL_SPEED = new int[] {1, 2, 2, 3, 3};
	private readonly int[] LVL_CRIT = new int[] {5, 5, 6, 6, 7};

	public MediBot () : base ()
	{
		int NewLevel = 0;
		BASE_HEALTH = LVL_HEALTH[NewLevel];
		BASE_SPEED = LVL_SPEED[NewLevel];
		BASE_DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BASE_ARMOR = 0;

		CRIT_MODS = new int[] {0, 0, 0};
		DMG_MODS = new float[] {0f, 0f, 0.5f};
		ACC_MODS = new int[] {0, 0, 85};

		CurrHealth = BASE_HEALTH;
		Level = 1;
		Rank = 1;
		CAT = MEDIBOT;
		IS_MECH = true;
		IS_FRIENDLY = false;
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

