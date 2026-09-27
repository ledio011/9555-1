using System;
using System.Collections;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200008C RID: 140
	internal class CharacterClass : Expression
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public CharacterClass(bool negate, bool ignore)
		{
			this.negate = negate;
			this.ignore = ignore;
			this.intervals = new IntervalCollection();
			int length = 144;
			this.pos_cats = new BitArray(length);
			this.neg_cats = new BitArray(length);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000CA68 File Offset: 0x0000AC68
		public CharacterClass(Category cat, bool negate) : this(false, false)
		{
			this.AddCategory(cat, negate);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		public void AddCategory(Category cat, bool negate)
		{
			if (negate)
			{
				this.neg_cats[(int)cat] = true;
			}
			else
			{
				this.pos_cats[(int)cat] = true;
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
		public void AddCharacter(char c)
		{
			this.AddRange(c, c);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000CACC File Offset: 0x0000ACCC
		public void AddRange(char lo, char hi)
		{
			Interval i = new Interval((int)lo, (int)hi);
			if (this.ignore)
			{
				if (CharacterClass.upper_case_characters.Intersects(i))
				{
					Interval i2;
					if (i.low < CharacterClass.upper_case_characters.low)
					{
						i2 = new Interval(CharacterClass.upper_case_characters.low + 32, i.high + 32);
						i.high = CharacterClass.upper_case_characters.low - 1;
					}
					else
					{
						i2 = new Interval(i.low + 32, CharacterClass.upper_case_characters.high + 32);
						i.low = CharacterClass.upper_case_characters.high + 1;
					}
					this.intervals.Add(i2);
				}
				else if (CharacterClass.upper_case_characters.Contains(i))
				{
					i.high += 32;
					i.low += 32;
				}
			}
			this.intervals.Add(i);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
		public override void Compile(ICompiler cmp, bool reverse)
		{
			IntervalCollection metaCollection = this.intervals.GetMetaCollection(new IntervalCollection.CostDelegate(CharacterClass.GetIntervalCost));
			int num = metaCollection.Count;
			for (int i = 0; i < this.pos_cats.Length; i++)
			{
				if (this.pos_cats[i] || this.neg_cats[i])
				{
					num++;
				}
			}
			if (num == 0)
			{
				return;
			}
			LinkRef linkRef = cmp.NewLink();
			if (num > 1)
			{
				cmp.EmitIn(linkRef);
			}
			foreach (object obj in metaCollection)
			{
				Interval interval = (Interval)obj;
				if (interval.IsDiscontiguous)
				{
					BitArray bitArray = new BitArray(interval.Size);
					foreach (object obj2 in this.intervals)
					{
						Interval i2 = (Interval)obj2;
						if (interval.Contains(i2))
						{
							for (int j = i2.low; j <= i2.high; j++)
							{
								bitArray[j - interval.low] = true;
							}
						}
					}
					cmp.EmitSet((char)interval.low, bitArray, this.negate, this.ignore, reverse);
				}
				else if (interval.IsSingleton)
				{
					cmp.EmitCharacter((char)interval.low, this.negate, this.ignore, reverse);
				}
				else
				{
					cmp.EmitRange((char)interval.low, (char)interval.high, this.negate, this.ignore, reverse);
				}
			}
			for (int k = 0; k < this.pos_cats.Length; k++)
			{
				if (this.pos_cats[k])
				{
					if (this.neg_cats[k])
					{
						cmp.EmitCategory(Category.AnySingleline, this.negate, reverse);
					}
					else
					{
						cmp.EmitCategory((Category)k, this.negate, reverse);
					}
				}
				else if (this.neg_cats[k])
				{
					cmp.EmitNotCategory((Category)k, this.negate, reverse);
				}
			}
			if (num > 1)
			{
				if (this.negate)
				{
					cmp.EmitTrue();
				}
				else
				{
					cmp.EmitFalse();
				}
				cmp.ResolveLink(linkRef);
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000CE94 File Offset: 0x0000B094
		public override void GetWidth(out int min, out int max)
		{
			min = (max = 1);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000CEAC File Offset: 0x0000B0AC
		public override bool IsComplex()
		{
			return false;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		private static double GetIntervalCost(Interval i)
		{
			if (i.IsDiscontiguous)
			{
				return (double)(3 + (i.Size + 15 >> 4));
			}
			if (i.IsSingleton)
			{
				return 2.0;
			}
			return 3.0;
		}

		// Token: 0x04000A1D RID: 2589
		private const int distance_between_upper_and_lower_case = 32;

		// Token: 0x04000A1E RID: 2590
		private static Interval upper_case_characters = new Interval(65, 90);

		// Token: 0x04000A1F RID: 2591
		private bool negate;

		// Token: 0x04000A20 RID: 2592
		private bool ignore;

		// Token: 0x04000A21 RID: 2593
		private BitArray pos_cats;

		// Token: 0x04000A22 RID: 2594
		private BitArray neg_cats;

		// Token: 0x04000A23 RID: 2595
		private IntervalCollection intervals;
	}
}
