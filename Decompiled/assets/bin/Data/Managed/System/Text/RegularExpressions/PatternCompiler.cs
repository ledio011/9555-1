using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200007C RID: 124
	internal class PatternCompiler : ICompiler
	{
		// Token: 0x06000244 RID: 580 RVA: 0x0000ADDC File Offset: 0x00008FDC
		public PatternCompiler()
		{
			this.pgm = new ArrayList();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		public static ushort EncodeOp(OpCode op, OpFlags flags)
		{
			return (ushort)(op | (OpCode)(flags & (OpFlags)65280));
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000ADFC File Offset: 0x00008FFC
		public IMachineFactory GetMachineFactory()
		{
			ushort[] array = new ushort[this.pgm.Count];
			this.pgm.CopyTo(array);
			return new InterpreterFactory(array);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000AE2C File Offset: 0x0000902C
		public void EmitFalse()
		{
			this.Emit(OpCode.False);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000AE38 File Offset: 0x00009038
		public void EmitTrue()
		{
			this.Emit(OpCode.True);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000AE44 File Offset: 0x00009044
		private void EmitCount(int count)
		{
			this.Emit((ushort)(count & 65535));
			this.Emit((ushort)((uint)count >> 16));
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AE6C File Offset: 0x0000906C
		public void EmitCharacter(char c, bool negate, bool ignore, bool reverse)
		{
			this.Emit(OpCode.Character, PatternCompiler.MakeFlags(negate, ignore, reverse, false));
			if (ignore)
			{
				c = char.ToLower(c);
			}
			this.Emit((ushort)c);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000AE94 File Offset: 0x00009094
		public void EmitCategory(Category cat, bool negate, bool reverse)
		{
			this.Emit(OpCode.Category, PatternCompiler.MakeFlags(negate, false, reverse, false));
			this.Emit((ushort)cat);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000AEB0 File Offset: 0x000090B0
		public void EmitNotCategory(Category cat, bool negate, bool reverse)
		{
			this.Emit(OpCode.NotCategory, PatternCompiler.MakeFlags(negate, false, reverse, false));
			this.Emit((ushort)cat);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000AECC File Offset: 0x000090CC
		public void EmitRange(char lo, char hi, bool negate, bool ignore, bool reverse)
		{
			this.Emit(OpCode.Range, PatternCompiler.MakeFlags(negate, ignore, reverse, false));
			this.Emit((ushort)lo);
			this.Emit((ushort)hi);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000AEF0 File Offset: 0x000090F0
		public void EmitSet(char lo, BitArray set, bool negate, bool ignore, bool reverse)
		{
			this.Emit(OpCode.Set, PatternCompiler.MakeFlags(negate, ignore, reverse, false));
			this.Emit((ushort)lo);
			int num = set.Length + 15 >> 4;
			this.Emit((ushort)num);
			int num2 = 0;
			while (num-- != 0)
			{
				ushort num3 = 0;
				for (int i = 0; i < 16; i++)
				{
					if (num2 >= set.Length)
					{
						break;
					}
					if (set[num2++])
					{
						num3 |= (ushort)(1 << i);
					}
				}
				this.Emit(num3);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000AF84 File Offset: 0x00009184
		public void EmitString(string str, bool ignore, bool reverse)
		{
			this.Emit(OpCode.String, PatternCompiler.MakeFlags(false, ignore, reverse, false));
			int length = str.Length;
			this.Emit((ushort)length);
			if (ignore)
			{
				str = str.ToLower();
			}
			for (int i = 0; i < length; i++)
			{
				this.Emit((ushort)str[i]);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public void EmitPosition(Position pos)
		{
			this.Emit(OpCode.Position, OpFlags.None);
			this.Emit((ushort)pos);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000AFF4 File Offset: 0x000091F4
		public void EmitOpen(int gid)
		{
			this.Emit(OpCode.Open);
			this.Emit((ushort)gid);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B008 File Offset: 0x00009208
		public void EmitClose(int gid)
		{
			this.Emit(OpCode.Close);
			this.Emit((ushort)gid);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B01C File Offset: 0x0000921C
		public void EmitBalanceStart(int gid, int balance, bool capture, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.BalanceStart);
			this.Emit((ushort)gid);
			this.Emit((ushort)balance);
			this.Emit((!capture) ? 0 : 1);
			this.EmitLink(tail);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B05C File Offset: 0x0000925C
		public void EmitBalance()
		{
			this.Emit(OpCode.Balance);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B068 File Offset: 0x00009268
		public void EmitReference(int gid, bool ignore, bool reverse)
		{
			this.Emit(OpCode.Reference, PatternCompiler.MakeFlags(false, ignore, reverse, false));
			this.Emit((ushort)gid);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B084 File Offset: 0x00009284
		public void EmitIfDefined(int gid, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.IfDefined);
			this.EmitLink(tail);
			this.Emit((ushort)gid);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B0A4 File Offset: 0x000092A4
		public void EmitSub(LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.Sub);
			this.EmitLink(tail);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B0BC File Offset: 0x000092BC
		public void EmitTest(LinkRef yes, LinkRef tail)
		{
			this.BeginLink(yes);
			this.BeginLink(tail);
			this.Emit(OpCode.Test);
			this.EmitLink(yes);
			this.EmitLink(tail);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B0E4 File Offset: 0x000092E4
		public void EmitBranch(LinkRef next)
		{
			this.BeginLink(next);
			this.Emit(OpCode.Branch, OpFlags.None);
			this.EmitLink(next);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B100 File Offset: 0x00009300
		public void EmitJump(LinkRef target)
		{
			this.BeginLink(target);
			this.Emit(OpCode.Jump, OpFlags.None);
			this.EmitLink(target);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000B11C File Offset: 0x0000931C
		public void EmitRepeat(int min, int max, bool lazy, LinkRef until)
		{
			this.BeginLink(until);
			this.Emit(OpCode.Repeat, PatternCompiler.MakeFlags(false, false, false, lazy));
			this.EmitLink(until);
			this.EmitCount(min);
			this.EmitCount(max);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B150 File Offset: 0x00009350
		public void EmitUntil(LinkRef repeat)
		{
			this.ResolveLink(repeat);
			this.Emit(OpCode.Until);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B164 File Offset: 0x00009364
		public void EmitFastRepeat(int min, int max, bool lazy, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.FastRepeat, PatternCompiler.MakeFlags(false, false, false, lazy));
			this.EmitLink(tail);
			this.EmitCount(min);
			this.EmitCount(max);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000B198 File Offset: 0x00009398
		public void EmitIn(LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.In);
			this.EmitLink(tail);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public void EmitAnchor(bool reverse, int offset, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.Anchor, PatternCompiler.MakeFlags(false, false, reverse, false));
			this.EmitLink(tail);
			this.Emit((ushort)offset);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B1DC File Offset: 0x000093DC
		public void EmitInfo(int count, int min, int max)
		{
			this.Emit(OpCode.Info);
			this.EmitCount(count);
			this.EmitCount(min);
			this.EmitCount(max);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B1FC File Offset: 0x000093FC
		public LinkRef NewLink()
		{
			return new PatternCompiler.PatternLinkStack();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000B204 File Offset: 0x00009404
		public void ResolveLink(LinkRef lref)
		{
			PatternCompiler.PatternLinkStack patternLinkStack = (PatternCompiler.PatternLinkStack)lref;
			while (patternLinkStack.Pop())
			{
				this.pgm[patternLinkStack.OffsetAddress] = (ushort)patternLinkStack.GetOffset(this.CurrentAddress);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B24C File Offset: 0x0000944C
		public void EmitBranchEnd()
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B250 File Offset: 0x00009450
		public void EmitAlternationEnd()
		{
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B254 File Offset: 0x00009454
		private static OpFlags MakeFlags(bool negate, bool ignore, bool reverse, bool lazy)
		{
			OpFlags opFlags = OpFlags.None;
			if (negate)
			{
				opFlags |= OpFlags.Negate;
			}
			if (ignore)
			{
				opFlags |= OpFlags.IgnoreCase;
			}
			if (reverse)
			{
				opFlags |= OpFlags.RightToLeft;
			}
			if (lazy)
			{
				opFlags |= OpFlags.Lazy;
			}
			return opFlags;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B2A0 File Offset: 0x000094A0
		private void Emit(OpCode op)
		{
			this.Emit(op, OpFlags.None);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B2AC File Offset: 0x000094AC
		private void Emit(OpCode op, OpFlags flags)
		{
			this.Emit(PatternCompiler.EncodeOp(op, flags));
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B2BC File Offset: 0x000094BC
		private void Emit(ushort word)
		{
			this.pgm.Add(word);
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000B2D0 File Offset: 0x000094D0
		private int CurrentAddress
		{
			get
			{
				return this.pgm.Count;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000B2E0 File Offset: 0x000094E0
		private void BeginLink(LinkRef lref)
		{
			PatternCompiler.PatternLinkStack patternLinkStack = (PatternCompiler.PatternLinkStack)lref;
			patternLinkStack.BaseAddress = this.CurrentAddress;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000B300 File Offset: 0x00009500
		private void EmitLink(LinkRef lref)
		{
			PatternCompiler.PatternLinkStack patternLinkStack = (PatternCompiler.PatternLinkStack)lref;
			patternLinkStack.OffsetAddress = this.CurrentAddress;
			this.Emit(0);
			patternLinkStack.Push();
		}

		// Token: 0x040009DC RID: 2524
		private ArrayList pgm;

		// Token: 0x0200007D RID: 125
		private class PatternLinkStack : LinkStack
		{
			// Token: 0x1700008D RID: 141
			// (set) Token: 0x0600026D RID: 621 RVA: 0x0000B338 File Offset: 0x00009538
			public int BaseAddress
			{
				set
				{
					this.link.base_addr = value;
				}
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x0600026E RID: 622 RVA: 0x0000B348 File Offset: 0x00009548
			// (set) Token: 0x0600026F RID: 623 RVA: 0x0000B358 File Offset: 0x00009558
			public int OffsetAddress
			{
				get
				{
					return this.link.offset_addr;
				}
				set
				{
					this.link.offset_addr = value;
				}
			}

			// Token: 0x06000270 RID: 624 RVA: 0x0000B368 File Offset: 0x00009568
			public int GetOffset(int target_addr)
			{
				return target_addr - this.link.base_addr;
			}

			// Token: 0x06000271 RID: 625 RVA: 0x0000B378 File Offset: 0x00009578
			protected override object GetCurrent()
			{
				return this.link;
			}

			// Token: 0x06000272 RID: 626 RVA: 0x0000B388 File Offset: 0x00009588
			protected override void SetCurrent(object l)
			{
				this.link = (PatternCompiler.PatternLinkStack.Link)l;
			}

			// Token: 0x040009DD RID: 2525
			private PatternCompiler.PatternLinkStack.Link link;

			// Token: 0x0200007E RID: 126
			private struct Link
			{
				// Token: 0x040009DE RID: 2526
				public int base_addr;

				// Token: 0x040009DF RID: 2527
				public int offset_addr;
			}
		}
	}
}
