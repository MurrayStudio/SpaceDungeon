using UnityEngine;
using System.Collections;

public class Rifleman : Unit
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int RIFLE 			= 0;
	private readonly int BAYONET	 	= 1;
	private readonly int SHOTGUN		= 2;
	private readonly int NET_GUN 		= 3;
	private readonly int RELOAD 		= 4;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {5, 10}, {6, 12}, {7, 13}, {8, 15}, {9, 16}};
	private readonly int[] LVL_HEALTH = new int[] {23, 28, 33, 38, 43};
	private readonly int[] LVL_DODGE = new int[] {10, 15, 20, 25, 30};
	private readonly int[] LVL_SPEED = new int[] {5, 5, 6, 6, 7};
	private readonly int[] LVL_CRIT = new int[] {5, 5, 6, 6, 7};

	public Rifleman () : base ()
	{
		int NewLevel = 0;
		BASE_HEALTH = LVL_HEALTH[NewLevel];
		BASE_SPEED = LVL_SPEED[NewLevel];
		BASE_DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BASE_ARMOR = 0;

		CRIT_MODS = new int[] {10, 0, 0, 0, 0};
		DMG_MODS = new float[] {0f, 0f, -0.4f, -0.8f, 0f};
		ACC_MODS = new int[] {85, 85, 85, 85, 0};
		DEBUFF_MODS = new float[] {0f, 0f, 0f, 0f, 0.15f};

		CurrHealth = BASE_HEALTH;
		Level = 1;
		Rank = 1;
		CAT = RIFLEMAN;
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

	public bool Rifle (Unit Enemy) 		// Stats from rampart
	{
		if (!CheckHit (RIFLE, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (RIFLE, BASE_DMG[0], BASE_DMG[1], Enemy));
		return true;
	}

	public bool Bayonet (Unit Enemy)
	{
		if (!CheckHit (BAYONET, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BAYONET, BASE_DMG[0], BASE_DMG[1], Enemy));
		return true;
	}

	public bool Shotgun (Unit Enemy)
	{
		if (!CheckHit (SHOTGUN, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (SHOTGUN, BASE_DMG[0], BASE_DMG[1], Enemy));
		return true;
	}

	public bool NetGun (Unit Enemy)
	{
		if (!CheckHit (NET_GUN, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (NET_GUN, BASE_DMG[0], BASE_DMG[1], Enemy));
		return true;
	}

	public void Reload ()
	{
		Debuff D1 = new Debuff(DEBUFF_DUR, DEBUFF_MODS[RELOAD], DODGE);
		Debuff D2 = new Debuff(DEBUFF_DUR, DEBUFF_MODS[RELOAD], DAMAGE);
		this.AddDebuff (D1);
		this.AddDebuff (D2);
	}
}