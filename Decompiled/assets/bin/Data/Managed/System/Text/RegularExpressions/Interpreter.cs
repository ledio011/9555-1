using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200006A RID: 106
	internal class Interpreter : BaseMachine
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000864C File Offset: 0x0000684C
		public Interpreter(ushort[] program)
		{
			this.program = program;
			this.qs = null;
			this.group_count = this.ReadProgramCount(1) + 1;
			this.match_min = this.ReadProgramCount(3);
			this.program_start = 7;
			this.groups = new int[this.group_count];
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000086B0 File Offset: 0x000068B0
		private int ReadProgramCount(int ptr)
		{
			int num = (int)this.program[ptr + 1];
			num <<= 16;
			return num + (int)this.program[ptr];
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000086DC File Offset: 0x000068DC
		public override Match Scan(Regex regex, string text, int start, int end)
		{
			this.text = text;
			this.text_end = end;
			this.scan_ptr = start;
			if (this.Eval(Interpreter.Mode.Match, ref this.scan_ptr, this.program_start))
			{
				return this.GenerateMatch(regex);
			}
			return Match.Empty;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000871C File Offset: 0x0000691C
		private void Reset()
		{
			this.ResetGroups();
			this.fast = (this.repeat = null);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008740 File Offset: 0x00006940
		private bool Eval(Interpreter.Mode mode, ref int ref_ptr, int pc)
		{
			int num = ref_ptr;
			Interpreter.RepeatContext repeatContext;
			int start;
			int count;
			for (;;)
			{
				OpFlags opFlags;
				for (;;)
				{
					ushort num2 = this.program[pc];
					OpCode opCode = (OpCode)(num2 & 255);
					opFlags = (OpFlags)(num2 & 65280);
					switch (opCode)
					{
					case OpCode.False:
						goto IL_4B8;
					case OpCode.True:
						goto IL_4BD;
					case OpCode.Position:
						if (!this.IsPosition((Position)this.program[pc + 1], num))
						{
							goto Block_44;
						}
						pc += 2;
						break;
					case OpCode.String:
					{
						bool flag = (ushort)(opFlags & OpFlags.RightToLeft) != 0;
						bool flag2 = (ushort)(opFlags & OpFlags.IgnoreCase) != 0;
						int num3 = (int)this.program[pc + 1];
						if (flag)
						{
							num -= num3;
							if (num < 0)
							{
								goto Block_46;
							}
						}
						else if (num + num3 > this.text_end)
						{
							goto Block_47;
						}
						pc += 2;
						for (int i = 0; i < num3; i++)
						{
							char c = this.text[num + i];
							if (flag2)
							{
								c = char.ToLower(c);
							}
							if (c != (char)this.program[pc++])
							{
								goto Block_49;
							}
						}
						if (!flag)
						{
							num += num3;
						}
						break;
					}
					case OpCode.Reference:
					{
						bool flag3 = (ushort)(opFlags & OpFlags.RightToLeft) != 0;
						bool flag4 = (ushort)(opFlags & OpFlags.IgnoreCase) != 0;
						int lastDefined = this.GetLastDefined((int)this.program[pc + 1]);
						if (lastDefined < 0)
						{
							goto Block_52;
						}
						int index = this.marks[lastDefined].Index;
						int length = this.marks[lastDefined].Length;
						if (flag3)
						{
							num -= length;
							if (num < 0)
							{
								goto Block_54;
							}
						}
						else if (num + length > this.text_end)
						{
							goto Block_55;
						}
						pc += 2;
						if (flag4)
						{
							for (int j = 0; j < length; j++)
							{
								if (char.ToLower(this.text[num + j]) != char.ToLower(this.text[index + j]))
								{
									goto Block_57;
								}
							}
						}
						else
						{
							for (int k = 0; k < length; k++)
							{
								if (this.text[num + k] != this.text[index + k])
								{
									goto Block_59;
								}
							}
						}
						if (!flag3)
						{
							num += length;
						}
						break;
					}
					case OpCode.Character:
					case OpCode.Category:
					case OpCode.NotCategory:
					case OpCode.Range:
					case OpCode.Set:
						if (!this.EvalChar(mode, ref num, ref pc, false))
						{
							goto Block_61;
						}
						break;
					case OpCode.In:
					{
						int num4 = pc + (int)this.program[pc + 1];
						pc += 2;
						if (!this.EvalChar(mode, ref num, ref pc, true))
						{
							goto Block_62;
						}
						pc = num4;
						break;
					}
					case OpCode.Open:
						this.Open((int)this.program[pc + 1], num);
						pc += 2;
						break;
					case OpCode.Close:
						this.Close((int)this.program[pc + 1], num);
						pc += 2;
						break;
					case OpCode.Balance:
						goto IL_7DB;
					case OpCode.BalanceStart:
					{
						int ptr = num;
						if (!this.Eval(Interpreter.Mode.Match, ref num, pc + 5))
						{
							goto Block_63;
						}
						if (!this.Balance((int)this.program[pc + 1], (int)this.program[pc + 2], this.program[pc + 3] == 1, ptr))
						{
							goto Block_65;
						}
						pc += (int)this.program[pc + 4];
						break;
					}
					case OpCode.IfDefined:
					{
						int lastDefined2 = this.GetLastDefined((int)this.program[pc + 2]);
						if (lastDefined2 < 0)
						{
							pc += (int)this.program[pc + 1];
						}
						else
						{
							pc += 3;
						}
						break;
					}
					case OpCode.Sub:
						if (!this.Eval(Interpreter.Mode.Match, ref num, pc + 2))
						{
							goto Block_67;
						}
						pc += (int)this.program[pc + 1];
						break;
					case OpCode.Test:
					{
						int cp = this.Checkpoint();
						int num5 = num;
						if (this.Eval(Interpreter.Mode.Match, ref num5, pc + 3))
						{
							pc += (int)this.program[pc + 1];
						}
						else
						{
							this.Backtrack(cp);
							pc += (int)this.program[pc + 2];
						}
						break;
					}
					case OpCode.Branch:
						goto IL_88A;
					case OpCode.Jump:
						pc += (int)this.program[pc + 1];
						break;
					case OpCode.Repeat:
						goto IL_8EE;
					case OpCode.Until:
						goto IL_957;
					case OpCode.FastRepeat:
						goto IL_C6F;
					case OpCode.Anchor:
						goto IL_96;
					case OpCode.Info:
						goto IL_FE9;
					}
				}
				for (;;)
				{
					IL_88A:
					int cp2 = this.Checkpoint();
					if (this.Eval(Interpreter.Mode.Match, ref num, pc + 2))
					{
						break;
					}
					this.Backtrack(cp2);
					pc += (int)this.program[pc + 1];
					if ((this.program[pc] & 255) == 0)
					{
						goto Block_70;
					}
				}
				IL_FF3:
				ref_ptr = num;
				if (mode == Interpreter.Mode.Match)
				{
					return true;
				}
				if (mode != Interpreter.Mode.Count)
				{
					break;
				}
				this.fast.Count++;
				if (this.fast.IsMaximum || (this.fast.IsLazy && this.fast.IsMinimum))
				{
					return true;
				}
				pc = this.fast.Expression;
				continue;
				IL_96:
				int num6 = (int)this.program[pc + 1];
				int num7 = (int)this.program[pc + 2];
				bool flag5 = (ushort)(opFlags & OpFlags.RightToLeft) != 0;
				int num8 = (!flag5) ? (num + num7) : (num - num7);
				int num9 = this.text_end - this.match_min + num7;
				int num10 = 0;
				OpCode opCode2 = (OpCode)(this.program[pc + 3] & 255);
				if (opCode2 == OpCode.Position && num6 == 6)
				{
					switch (this.program[pc + 4])
					{
					case 2:
						if (flag5 || num7 == 0)
						{
							if (flag5)
							{
								num = num7;
							}
							if (this.TryMatch(ref num, pc + num6))
							{
								goto IL_FF3;
							}
						}
						break;
					case 3:
						if (num8 == 0)
						{
							num = 0;
							if (this.TryMatch(ref num, pc + num6))
							{
								goto IL_FF3;
							}
							num8++;
						}
						while ((flag5 && num8 >= 0) || (!flag5 && num8 <= num9))
						{
							if (num8 == 0 || this.text[num8 - 1] == '\n')
							{
								if (flag5)
								{
									num = ((num8 != num9) ? (num8 + num7) : num8);
								}
								else
								{
									num = ((num8 != 0) ? (num8 - num7) : num8);
								}
								if (this.TryMatch(ref num, pc + num6))
								{
									goto IL_FF3;
								}
							}
							if (flag5)
							{
								num8--;
							}
							else
							{
								num8++;
							}
						}
						break;
					case 4:
						if (num8 == this.scan_ptr)
						{
							num = ((!flag5) ? (this.scan_ptr - num7) : (this.scan_ptr + num7));
							if (this.TryMatch(ref num, pc + num6))
							{
								goto IL_FF3;
							}
						}
						break;
					}
					break;
				}
				if (this.qs != null || (opCode2 == OpCode.String && num6 == (int)(6 + this.program[pc + 4])))
				{
					bool flag6 = (this.program[pc + 3] & 1024) != 0;
					if (this.qs == null)
					{
						bool ignore = (this.program[pc + 3] & 512) != 0;
						string @string = this.GetString(pc + 3);
						this.qs = new QuickSearch(@string, ignore, flag6);
					}
					while ((flag5 && num8 >= num10) || (!flag5 && num8 <= num9))
					{
						if (flag6)
						{
							num8 = this.qs.Search(this.text, num8, num10);
							if (num8 != -1)
							{
								num8 += this.qs.Length;
							}
						}
						else
						{
							num8 = this.qs.Search(this.text, num8, num9);
						}
						if (num8 < 0)
						{
							break;
						}
						num = ((!flag6) ? (num8 - num7) : (num8 + num7));
						if (this.TryMatch(ref num, pc + num6))
						{
							goto IL_FF3;
						}
						if (flag6)
						{
							num8 -= 2;
						}
						else
						{
							num8++;
						}
					}
					break;
				}
				if (opCode2 == OpCode.True)
				{
					while ((flag5 && num8 >= num10) || (!flag5 && num8 <= num9))
					{
						num = num8;
						if (this.TryMatch(ref num, pc + num6))
						{
							goto IL_FF3;
						}
						if (flag5)
						{
							num8--;
						}
						else
						{
							num8++;
						}
					}
					break;
				}
				while ((flag5 && num8 >= num10) || (!flag5 && num8 <= num9))
				{
					num = num8;
					if (this.Eval(Interpreter.Mode.Match, ref num, pc + 3))
					{
						num = ((!flag5) ? (num8 - num7) : (num8 + num7));
						if (this.TryMatch(ref num, pc + num6))
						{
							goto IL_FF3;
						}
					}
					if (flag5)
					{
						num8--;
					}
					else
					{
						num8++;
					}
				}
				break;
				IL_4BD:
				IL_7DB:
				goto IL_FF3;
				IL_8EE:
				this.repeat = new Interpreter.RepeatContext(this.repeat, this.ReadProgramCount(pc + 2), this.ReadProgramCount(pc + 4), (ushort)(opFlags & OpFlags.Lazy) != 0, pc + 6);
				if (this.Eval(Interpreter.Mode.Match, ref num, pc + (int)this.program[pc + 1]))
				{
					goto IL_FF3;
				}
				goto IL_941;
				IL_957:
				repeatContext = this.repeat;
				if (this.deep == repeatContext)
				{
					goto IL_FF3;
				}
				start = repeatContext.Start;
				count = repeatContext.Count;
				while (!repeatContext.IsMinimum)
				{
					repeatContext.Count++;
					repeatContext.Start = num;
					this.deep = repeatContext;
					if (!this.Eval(Interpreter.Mode.Match, ref num, repeatContext.Expression))
					{
						goto Block_73;
					}
					if (this.deep != repeatContext)
					{
						goto IL_FF3;
					}
				}
				if (num == repeatContext.Start)
				{
					this.repeat = repeatContext.Previous;
					this.deep = null;
					if (this.Eval(Interpreter.Mode.Match, ref num, pc + 1))
					{
						goto IL_FF3;
					}
					goto IL_A28;
				}
				else
				{
					if (repeatContext.IsLazy)
					{
						for (;;)
						{
							this.repeat = repeatContext.Previous;
							this.deep = null;
							int cp3 = this.Checkpoint();
							if (this.Eval(Interpreter.Mode.Match, ref num, pc + 1))
							{
								break;
							}
							this.Backtrack(cp3);
							this.repeat = repeatContext;
							if (repeatContext.IsMaximum)
							{
								goto Block_80;
							}
							repeatContext.Count++;
							repeatContext.Start = num;
							this.deep = repeatContext;
							if (!this.Eval(Interpreter.Mode.Match, ref num, repeatContext.Expression))
							{
								goto Block_81;
							}
							if (this.deep != repeatContext)
							{
								break;
							}
							if (num == repeatContext.Start)
							{
								goto Block_83;
							}
						}
						goto IL_FF3;
					}
					int count2 = this.stack.Count;
					while (!repeatContext.IsMaximum)
					{
						int num11 = this.Checkpoint();
						int value = num;
						int start2 = repeatContext.Start;
						repeatContext.Count++;
						repeatContext.Start = num;
						this.deep = repeatContext;
						if (!this.Eval(Interpreter.Mode.Match, ref num, repeatContext.Expression))
						{
							repeatContext.Count--;
							repeatContext.Start = start2;
							this.Backtrack(num11);
							break;
						}
						if (this.deep != repeatContext)
						{
							this.stack.Count = count2;
							goto IL_FF3;
						}
						this.stack.Push(num11);
						this.stack.Push(value);
						if (num == repeatContext.Start)
						{
							break;
						}
					}
					this.repeat = repeatContext.Previous;
					for (;;)
					{
						this.deep = null;
						if (this.Eval(Interpreter.Mode.Match, ref num, pc + 1))
						{
							break;
						}
						if (this.stack.Count == count2)
						{
							goto Block_88;
						}
						repeatContext.Count--;
						num = this.stack.Pop();
						this.Backtrack(this.stack.Pop());
					}
					this.stack.Count = count2;
					goto IL_FF3;
				}
				IL_C6F:
				this.fast = new Interpreter.RepeatContext(this.fast, this.ReadProgramCount(pc + 2), this.ReadProgramCount(pc + 4), (ushort)(opFlags & OpFlags.Lazy) != 0, pc + 6);
				this.fast.Start = num;
				int cp4 = this.Checkpoint();
				pc += (int)this.program[pc + 1];
				ushort num12 = this.program[pc];
				int num13 = -1;
				int num14 = -1;
				int num15 = 0;
				OpCode opCode3 = (OpCode)(num12 & 255);
				if (opCode3 == OpCode.Character || opCode3 == OpCode.String)
				{
					OpFlags opFlags2 = (OpFlags)(num12 & 65280);
					if ((ushort)(opFlags2 & OpFlags.Negate) == 0)
					{
						if (opCode3 == OpCode.String)
						{
							int num16 = 0;
							if ((ushort)(opFlags2 & OpFlags.RightToLeft) != 0)
							{
								num16 = (int)(this.program[pc + 1] - 1);
							}
							num13 = (int)this.program[pc + 2 + num16];
						}
						else
						{
							num13 = (int)this.program[pc + 1];
						}
						if ((ushort)(opFlags2 & OpFlags.IgnoreCase) != 0)
						{
							num14 = (int)char.ToUpper((char)num13);
						}
						else
						{
							num14 = num13;
						}
						if ((ushort)(opFlags2 & OpFlags.RightToLeft) != 0)
						{
							num15 = -1;
						}
						else
						{
							num15 = 0;
						}
					}
				}
				if (this.fast.IsLazy)
				{
					if (!this.fast.IsMinimum && !this.Eval(Interpreter.Mode.Count, ref num, this.fast.Expression))
					{
						goto Block_97;
					}
					for (;;)
					{
						int num17 = num + num15;
						if (num13 < 0 || (num17 >= 0 && num17 < this.text_end && (num13 == (int)this.text[num17] || num14 == (int)this.text[num17])))
						{
							this.deep = null;
							if (this.Eval(Interpreter.Mode.Match, ref num, pc))
							{
								break;
							}
						}
						if (this.fast.IsMaximum)
						{
							goto Block_103;
						}
						this.Backtrack(cp4);
						if (!this.Eval(Interpreter.Mode.Count, ref num, this.fast.Expression))
						{
							goto Block_104;
						}
					}
					this.fast = this.fast.Previous;
					goto IL_FF3;
				}
				else
				{
					if (!this.Eval(Interpreter.Mode.Count, ref num, this.fast.Expression))
					{
						goto Block_105;
					}
					int num18;
					if (this.fast.Count > 0)
					{
						num18 = (num - this.fast.Start) / this.fast.Count;
					}
					else
					{
						num18 = 0;
					}
					for (;;)
					{
						int num19 = num + num15;
						if (num13 < 0 || (num19 >= 0 && num19 < this.text_end && (num13 == (int)this.text[num19] || num14 == (int)this.text[num19])))
						{
							this.deep = null;
							if (this.Eval(Interpreter.Mode.Match, ref num, pc))
							{
								break;
							}
						}
						this.fast.Count--;
						if (!this.fast.IsMinimum)
						{
							goto Block_112;
						}
						num -= num18;
						this.Backtrack(cp4);
					}
					this.fast = this.fast.Previous;
					goto IL_FF3;
				}
			}
			IL_4B8:
			Block_44:
			Block_46:
			Block_47:
			Block_49:
			Block_52:
			Block_54:
			Block_55:
			Block_57:
			Block_59:
			Block_61:
			Block_62:
			Block_63:
			Block_65:
			Block_67:
			Block_70:
			goto IL_1067;
			IL_941:
			this.repeat = this.repeat.Previous;
			goto IL_1067;
			Block_73:
			repeatContext.Start = start;
			repeatContext.Count = count;
			goto IL_1067;
			IL_A28:
			this.repeat = repeatContext;
			Block_80:
			goto IL_1067;
			Block_81:
			repeatContext.Start = start;
			repeatContext.Count = count;
			Block_83:
			goto IL_1067;
			Block_88:
			this.repeat = repeatContext;
			goto IL_1067;
			Block_97:
			this.fast = this.fast.Previous;
			goto IL_1067;
			Block_103:
			this.fast = this.fast.Previous;
			goto IL_1067;
			Block_104:
			this.fast = this.fast.Previous;
			goto IL_1067;
			Block_105:
			this.fast = this.fast.Previous;
			goto IL_1067;
			Block_112:
			this.fast = this.fast.Previous;
			IL_FE9:
			IL_1067:
			if (mode == Interpreter.Mode.Match)
			{
				return false;
			}
			if (mode != Interpreter.Mode.Count)
			{
				return false;
			}
			if (!this.fast.IsLazy && this.fast.IsMinimum)
			{
				return true;
			}
			ref_ptr = this.fast.Start;
			return false;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009800 File Offset: 0x00007A00
		private bool EvalChar(Interpreter.Mode mode, ref int ptr, ref int pc, bool multi)
		{
			bool flag = false;
			char c = '\0';
			bool flag3;
			for (;;)
			{
				ushort num = this.program[pc];
				OpCode opCode = (OpCode)(num & 255);
				OpFlags opFlags = (OpFlags)(num & 65280);
				pc++;
				bool flag2 = (ushort)(opFlags & OpFlags.IgnoreCase) != 0;
				if (!flag)
				{
					if ((ushort)(opFlags & OpFlags.RightToLeft) != 0)
					{
						if (ptr <= 0)
						{
							break;
						}
						c = this.text[--ptr];
					}
					else
					{
						if (ptr >= this.text_end)
						{
							return false;
						}
						c = this.text[ptr++];
					}
					if (flag2)
					{
						c = char.ToLower(c);
					}
					flag = true;
				}
				flag3 = ((ushort)(opFlags & OpFlags.Negate) != 0);
				switch (opCode)
				{
				case OpCode.False:
					return false;
				case OpCode.True:
					return true;
				case OpCode.Character:
					if (c == (char)this.program[pc++])
					{
						goto Block_7;
					}
					break;
				case OpCode.Category:
					if (CategoryUtils.IsCategory((Category)this.program[pc++], c))
					{
						goto Block_8;
					}
					break;
				case OpCode.NotCategory:
					if (!CategoryUtils.IsCategory((Category)this.program[pc++], c))
					{
						goto Block_9;
					}
					break;
				case OpCode.Range:
				{
					int num2 = (int)this.program[pc++];
					int num3 = (int)this.program[pc++];
					if (num2 <= (int)c && (int)c <= num3)
					{
						goto Block_11;
					}
					break;
				}
				case OpCode.Set:
				{
					int num4 = (int)this.program[pc++];
					int num5 = (int)this.program[pc++];
					int num6 = pc;
					pc += num5;
					int num7 = (int)c - num4;
					if (num7 >= 0 && num7 < num5 << 4)
					{
						if (((int)this.program[num6 + (num7 >> 4)] & 1 << (num7 & 15)) != 0)
						{
							goto Block_13;
						}
					}
					break;
				}
				}
				if (!multi)
				{
					return flag3;
				}
			}
			return false;
			Block_7:
			return !flag3;
			Block_8:
			return !flag3;
			Block_9:
			return !flag3;
			Block_11:
			return !flag3;
			Block_13:
			return !flag3;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009A38 File Offset: 0x00007C38
		private bool TryMatch(ref int ref_ptr, int pc)
		{
			this.Reset();
			int num = ref_ptr;
			this.marks[this.groups[0]].Start = num;
			if (this.Eval(Interpreter.Mode.Match, ref num, pc))
			{
				this.marks[this.groups[0]].End = num;
				ref_ptr = num;
				return true;
			}
			return false;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00009A98 File Offset: 0x00007C98
		private bool IsPosition(Position pos, int ptr)
		{
			switch (pos)
			{
			case Position.Start:
			case Position.StartOfString:
				return ptr == 0;
			case Position.StartOfLine:
				return ptr == 0 || this.text[ptr - 1] == '\n';
			case Position.StartOfScan:
				return ptr == this.scan_ptr;
			case Position.End:
				return ptr == this.text_end || (ptr == this.text_end - 1 && this.text[ptr] == '\n');
			case Position.EndOfString:
				return ptr == this.text_end;
			case Position.EndOfLine:
				return ptr == this.text_end || this.text[ptr] == '\n';
			case Position.Boundary:
				if (this.text_end == 0)
				{
					return false;
				}
				if (ptr == 0)
				{
					return this.IsWordChar(this.text[ptr]);
				}
				if (ptr == this.text_end)
				{
					return this.IsWordChar(this.text[ptr - 1]);
				}
				return this.IsWordChar(this.text[ptr]) != this.IsWordChar(this.text[ptr - 1]);
			case Position.NonBoundary:
				if (this.text_end == 0)
				{
					return false;
				}
				if (ptr == 0)
				{
					return !this.IsWordChar(this.text[ptr]);
				}
				if (ptr == this.text_end)
				{
					return !this.IsWordChar(this.text[ptr - 1]);
				}
				return this.IsWordChar(this.text[ptr]) == this.IsWordChar(this.text[ptr - 1]);
			default:
				return false;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009C48 File Offset: 0x00007E48
		private bool IsWordChar(char c)
		{
			return CategoryUtils.IsCategory(Category.Word, c);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00009C54 File Offset: 0x00007E54
		private string GetString(int pc)
		{
			int num = (int)this.program[pc + 1];
			int num2 = pc + 2;
			char[] array = new char[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (char)this.program[num2++];
			}
			return new string(array);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00009CA0 File Offset: 0x00007EA0
		private void Open(int gid, int ptr)
		{
			int num = this.groups[gid];
			if (num < this.mark_start || this.marks[num].IsDefined)
			{
				num = this.CreateMark(num);
				this.groups[gid] = num;
			}
			this.marks[num].Start = ptr;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00009CFC File Offset: 0x00007EFC
		private void Close(int gid, int ptr)
		{
			this.marks[this.groups[gid]].End = ptr;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009D18 File Offset: 0x00007F18
		private bool Balance(int gid, int balance_gid, bool capture, int ptr)
		{
			int num = this.groups[balance_gid];
			if (num == -1 || this.marks[num].Index < 0)
			{
				return false;
			}
			if (gid > 0 && capture)
			{
				this.Open(gid, this.marks[num].Index + this.marks[num].Length);
				this.Close(gid, ptr);
			}
			this.groups[balance_gid] = this.marks[num].Previous;
			return true;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009DA8 File Offset: 0x00007FA8
		private int Checkpoint()
		{
			this.mark_start = this.mark_end;
			return this.mark_start;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009DBC File Offset: 0x00007FBC
		private void Backtrack(int cp)
		{
			for (int i = 0; i < this.groups.Length; i++)
			{
				int num = this.groups[i];
				while (cp <= num)
				{
					num = this.marks[num].Previous;
				}
				this.groups[i] = num;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00009E14 File Offset: 0x00008014
		private void ResetGroups()
		{
			int num = this.groups.Length;
			if (this.marks == null)
			{
				this.marks = new Mark[num * 10];
			}
			for (int i = 0; i < num; i++)
			{
				this.groups[i] = i;
				this.marks[i].Start = -1;
				this.marks[i].End = -1;
				this.marks[i].Previous = -1;
			}
			this.mark_start = 0;
			this.mark_end = num;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009EA4 File Offset: 0x000080A4
		private int GetLastDefined(int gid)
		{
			int num = this.groups[gid];
			while (num >= 0 && !this.marks[num].IsDefined)
			{
				num = this.marks[num].Previous;
			}
			return num;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009EF0 File Offset: 0x000080F0
		private int CreateMark(int previous)
		{
			if (this.mark_end == this.marks.Length)
			{
				Mark[] array = new Mark[this.marks.Length * 2];
				this.marks.CopyTo(array, 0);
				this.marks = array;
			}
			int num = this.mark_end++;
			this.marks[num].Start = (this.marks[num].End = -1);
			this.marks[num].Previous = previous;
			return num;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009F80 File Offset: 0x00008180
		private void GetGroupInfo(int gid, out int first_mark_index, out int n_caps)
		{
			first_mark_index = -1;
			n_caps = 0;
			for (int i = this.groups[gid]; i >= 0; i = this.marks[i].Previous)
			{
				if (this.marks[i].IsDefined)
				{
					if (first_mark_index < 0)
					{
						first_mark_index = i;
					}
					n_caps++;
				}
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009FE8 File Offset: 0x000081E8
		private void PopulateGroup(Group g, int first_mark_index, int n_caps)
		{
			int num = 1;
			for (int i = this.marks[first_mark_index].Previous; i >= 0; i = this.marks[i].Previous)
			{
				if (this.marks[i].IsDefined)
				{
					Capture cap = new Capture(this.text, this.marks[i].Index, this.marks[i].Length);
					g.Captures.SetValue(cap, n_caps - 1 - num);
					num++;
				}
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000A088 File Offset: 0x00008288
		private Match GenerateMatch(Regex regex)
		{
			int num;
			int n_caps;
			this.GetGroupInfo(0, out num, out n_caps);
			if (!this.needs_groups_or_captures)
			{
				return new Match(regex, this, this.text, this.text_end, 0, this.marks[num].Index, this.marks[num].Length);
			}
			Match match = new Match(regex, this, this.text, this.text_end, this.groups.Length, this.marks[num].Index, this.marks[num].Length, n_caps);
			this.PopulateGroup(match, num, n_caps);
			for (int i = 1; i < this.groups.Length; i++)
			{
				this.GetGroupInfo(i, out num, out n_caps);
				Group g;
				if (num < 0)
				{
					g = Group.Fail;
				}
				else
				{
					g = new Group(this.text, this.marks[num].Index, this.marks[num].Length, n_caps);
					this.PopulateGroup(g, num, n_caps);
				}
				match.Groups.SetValue(g, i);
			}
			return match;
		}

		// Token: 0x04000987 RID: 2439
		private ushort[] program;

		// Token: 0x04000988 RID: 2440
		private int program_start;

		// Token: 0x04000989 RID: 2441
		private string text;

		// Token: 0x0400098A RID: 2442
		private int text_end;

		// Token: 0x0400098B RID: 2443
		private int group_count;

		// Token: 0x0400098C RID: 2444
		private int match_min;

		// Token: 0x0400098D RID: 2445
		private QuickSearch qs;

		// Token: 0x0400098E RID: 2446
		private int scan_ptr;

		// Token: 0x0400098F RID: 2447
		private Interpreter.RepeatContext repeat;

		// Token: 0x04000990 RID: 2448
		private Interpreter.RepeatContext fast;

		// Token: 0x04000991 RID: 2449
		private Interpreter.IntStack stack = default(Interpreter.IntStack);

		// Token: 0x04000992 RID: 2450
		private Interpreter.RepeatContext deep;

		// Token: 0x04000993 RID: 2451
		private Mark[] marks;

		// Token: 0x04000994 RID: 2452
		private int mark_start;

		// Token: 0x04000995 RID: 2453
		private int mark_end;

		// Token: 0x04000996 RID: 2454
		private int[] groups;

		// Token: 0x0200006B RID: 107
		private struct IntStack
		{
			// Token: 0x060001F4 RID: 500 RVA: 0x0000A1AC File Offset: 0x000083AC
			public int Pop()
			{
				return this.values[--this.count];
			}

			// Token: 0x060001F5 RID: 501 RVA: 0x0000A1D4 File Offset: 0x000083D4
			public void Push(int value)
			{
				if (this.values == null)
				{
					this.values = new int[8];
				}
				else if (this.count == this.values.Length)
				{
					int num = this.values.Length;
					num += num >> 1;
					int[] array = new int[num];
					for (int i = 0; i < this.count; i++)
					{
						array[i] = this.values[i];
					}
					this.values = array;
				}
				this.values[this.count++] = value;
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000A268 File Offset: 0x00008468
			// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000A270 File Offset: 0x00008470
			public int Count
			{
				get
				{
					return this.count;
				}
				set
				{
					if (value > this.count)
					{
						throw new SystemException("can only truncate the stack");
					}
					this.count = value;
				}
			}

			// Token: 0x04000997 RID: 2455
			private int[] values;

			// Token: 0x04000998 RID: 2456
			private int count;
		}

		// Token: 0x0200006C RID: 108
		private enum Mode
		{
			// Token: 0x0400099A RID: 2458
			Search,
			// Token: 0x0400099B RID: 2459
			Match,
			// Token: 0x0400099C RID: 2460
			Count
		}

		// Token: 0x0200006D RID: 109
		private class RepeatContext
		{
			// Token: 0x060001F8 RID: 504 RVA: 0x0000A290 File Offset: 0x00008490
			public RepeatContext(Interpreter.RepeatContext previous, int min, int max, bool lazy, int expr_pc)
			{
				this.previous = previous;
				this.min = min;
				this.max = max;
				this.lazy = lazy;
				this.expr_pc = expr_pc;
				this.start = -1;
				this.count = 0;
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000A2CC File Offset: 0x000084CC
			// (set) Token: 0x060001FA RID: 506 RVA: 0x0000A2D4 File Offset: 0x000084D4
			public int Count
			{
				get
				{
					return this.count;
				}
				set
				{
					this.count = value;
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x060001FB RID: 507 RVA: 0x0000A2E0 File Offset: 0x000084E0
			// (set) Token: 0x060001FC RID: 508 RVA: 0x0000A2E8 File Offset: 0x000084E8
			public int Start
			{
				get
				{
					return this.start;
				}
				set
				{
					this.start = value;
				}
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x060001FD RID: 509 RVA: 0x0000A2F4 File Offset: 0x000084F4
			public bool IsMinimum
			{
				get
				{
					return this.min <= this.count;
				}
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x060001FE RID: 510 RVA: 0x0000A308 File Offset: 0x00008508
			public bool IsMaximum
			{
				get
				{
					return this.max <= this.count;
				}
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x060001FF RID: 511 RVA: 0x0000A31C File Offset: 0x0000851C
			public bool IsLazy
			{
				get
				{
					return this.lazy;
				}
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000200 RID: 512 RVA: 0x0000A324 File Offset: 0x00008524
			public int Expression
			{
				get
				{
					return this.expr_pc;
				}
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000201 RID: 513 RVA: 0x0000A32C File Offset: 0x0000852C
			public Interpreter.RepeatContext Previous
			{
				get
				{
					return this.previous;
				}
			}

			// Token: 0x0400099D RID: 2461
			private int start;

			// Token: 0x0400099E RID: 2462
			private int min;

			// Token: 0x0400099F RID: 2463
			private int max;

			// Token: 0x040009A0 RID: 2464
			private bool lazy;

			// Token: 0x040009A1 RID: 2465
			private int expr_pc;

			// Token: 0x040009A2 RID: 2466
			private Interpreter.RepeatContext previous;

			// Token: 0x040009A3 RID: 2467
			private int count;
		}
	}
}
