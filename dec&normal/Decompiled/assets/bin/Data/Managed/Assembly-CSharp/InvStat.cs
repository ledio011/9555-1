using System;

// Token: 0x02000026 RID: 38
[Serializable]
public class InvStat
{
	// Token: 0x060000AB RID: 171 RVA: 0x000055E4 File Offset: 0x000037E4
	public static string GetName(InvStat.Identifier i)
	{
		return i.ToString();
	}

	// Token: 0x060000AC RID: 172 RVA: 0x000055F4 File Offset: 0x000037F4
	public static string GetDescription(InvStat.Identifier i)
	{
		switch (i)
		{
		case InvStat.Identifier.Strength:
			return "Strength increases melee damage";
		case InvStat.Identifier.Constitution:
			return "Constitution increases health";
		case InvStat.Identifier.Agility:
			return "Agility increases armor";
		case InvStat.Identifier.Intelligence:
			return "Intelligence increases mana";
		case InvStat.Identifier.Damage:
			return "Damage adds to the amount of damage done in combat";
		case InvStat.Identifier.Crit:
			return "Crit increases the chance of landing a critical strike";
		case InvStat.Identifier.Armor:
			return "Armor protects from damage";
		case InvStat.Identifier.Health:
			return "Health prolongs life";
		case InvStat.Identifier.Mana:
			return "Mana increases the number of spells that can be cast";
		default:
			return null;
		}
	}

	// Token: 0x060000AD RID: 173 RVA: 0x0000566C File Offset: 0x0000386C
	public static int CompareArmor(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Armor)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Damage)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00005740 File Offset: 0x00003940
	public static int CompareWeapon(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Damage)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Armor)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x040000B3 RID: 179
	public InvStat.Identifier id;

	// Token: 0x040000B4 RID: 180
	public InvStat.Modifier modifier;

	// Token: 0x040000B5 RID: 181
	public int amount;

	// Token: 0x02000027 RID: 39
	public enum Identifier
	{
		// Token: 0x040000B7 RID: 183
		Strength,
		// Token: 0x040000B8 RID: 184
		Constitution,
		// Token: 0x040000B9 RID: 185
		Agility,
		// Token: 0x040000BA RID: 186
		Intelligence,
		// Token: 0x040000BB RID: 187
		Damage,
		// Token: 0x040000BC RID: 188
		Crit,
		// Token: 0x040000BD RID: 189
		Armor,
		// Token: 0x040000BE RID: 190
		Health,
		// Token: 0x040000BF RID: 191
		Mana,
		// Token: 0x040000C0 RID: 192
		Other
	}

	// Token: 0x02000028 RID: 40
	public enum Modifier
	{
		// Token: 0x040000C2 RID: 194
		Added,
		// Token: 0x040000C3 RID: 195
		Percent
	}
}
