using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001B2 RID: 434
	[ComVisible(true)]
	public struct OpCode
	{
		// Token: 0x0600107B RID: 4219 RVA: 0x0003E7C8 File Offset: 0x0003C9C8
		internal OpCode(int p, int q)
		{
			this.op1 = (byte)(p & 255);
			this.op2 = (byte)(p >> 8 & 255);
			this.push = (byte)(p >> 16 & 255);
			this.pop = (byte)(p >> 24 & 255);
			this.size = (byte)(q & 255);
			this.type = (byte)(q >> 8 & 255);
			this.args = (byte)(q >> 16 & 255);
			this.flow = (byte)(q >> 24 & 255);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0003E858 File Offset: 0x0003CA58
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003E868 File Offset: 0x0003CA68
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is OpCode))
			{
				return false;
			}
			OpCode opCode = (OpCode)obj;
			return opCode.op1 == this.op1 && opCode.op2 == this.op2;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0003E8B4 File Offset: 0x0003CAB4
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x0003E8BC File Offset: 0x0003CABC
		public string Name
		{
			get
			{
				if (this.op1 == 255)
				{
					return OpCodeNames.names[(int)this.op2];
				}
				return OpCodeNames.names[256 + (int)this.op2];
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x0003E8F0 File Offset: 0x0003CAF0
		public int Size
		{
			get
			{
				return (int)this.size;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x0003E8F8 File Offset: 0x0003CAF8
		public OperandType OperandType
		{
			get
			{
				return (OperandType)this.args;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0003E900 File Offset: 0x0003CB00
		public StackBehaviour StackBehaviourPop
		{
			get
			{
				return (StackBehaviour)this.pop;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x0003E908 File Offset: 0x0003CB08
		public StackBehaviour StackBehaviourPush
		{
			get
			{
				return (StackBehaviour)this.push;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x0003E910 File Offset: 0x0003CB10
		public short Value
		{
			get
			{
				if (this.size == 1)
				{
					return (short)this.op2;
				}
				return (short)((int)this.op1 << 8 | (int)this.op2);
			}
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0003E938 File Offset: 0x0003CB38
		public static bool operator ==(OpCode a, OpCode b)
		{
			return a.op1 == b.op1 && a.op2 == b.op2;
		}

		// Token: 0x0400071D RID: 1821
		internal byte op1;

		// Token: 0x0400071E RID: 1822
		internal byte op2;

		// Token: 0x0400071F RID: 1823
		private byte push;

		// Token: 0x04000720 RID: 1824
		private byte pop;

		// Token: 0x04000721 RID: 1825
		private byte size;

		// Token: 0x04000722 RID: 1826
		private byte type;

		// Token: 0x04000723 RID: 1827
		private byte args;

		// Token: 0x04000724 RID: 1828
		private byte flow;
	}
}
