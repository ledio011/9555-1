using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001A4 RID: 420
	[ComDefaultInterface(typeof(_ILGenerator))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public class ILGenerator : _ILGenerator
	{
		// Token: 0x06001001 RID: 4097 RVA: 0x0003C8F0 File Offset: 0x0003AAF0
		internal ILGenerator(Module m, TokenGenerator token_gen, int size)
		{
			if (size < 0)
			{
				size = 128;
			}
			this.code = new byte[size];
			this.token_fixups = new ILTokenInfo[8];
			this.module = m;
			this.token_gen = token_gen;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0003C940 File Offset: 0x0003AB40
		private void add_token_fixup(MemberInfo mi)
		{
			if (this.num_token_fixups == this.token_fixups.Length)
			{
				ILTokenInfo[] array = new ILTokenInfo[this.num_token_fixups * 2];
				this.token_fixups.CopyTo(array, 0);
				this.token_fixups = array;
			}
			this.token_fixups[this.num_token_fixups].member = mi;
			this.token_fixups[this.num_token_fixups++].code_pos = this.code_len;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0003C9C0 File Offset: 0x0003ABC0
		private void make_room(int nbytes)
		{
			if (this.code_len + nbytes < this.code.Length)
			{
				return;
			}
			byte[] destinationArray = new byte[(this.code_len + nbytes) * 2 + 128];
			Array.Copy(this.code, 0, destinationArray, 0, this.code.Length);
			this.code = destinationArray;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0003CA18 File Offset: 0x0003AC18
		private void emit_int(int val)
		{
			this.code[this.code_len++] = (byte)(val & 255);
			this.code[this.code_len++] = (byte)(val >> 8 & 255);
			this.code[this.code_len++] = (byte)(val >> 16 & 255);
			this.code[this.code_len++] = (byte)(val >> 24 & 255);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0003CAB0 File Offset: 0x0003ACB0
		private void ll_emit(OpCode opcode)
		{
			if (opcode.Size == 2)
			{
				this.code[this.code_len++] = opcode.op1;
			}
			this.code[this.code_len++] = opcode.op2;
			switch (opcode.StackBehaviourPush)
			{
			case StackBehaviour.Push1:
			case StackBehaviour.Pushi:
			case StackBehaviour.Pushi8:
			case StackBehaviour.Pushr4:
			case StackBehaviour.Pushr8:
			case StackBehaviour.Pushref:
			case StackBehaviour.Varpush:
				this.cur_stack++;
				break;
			case StackBehaviour.Push1_push1:
				this.cur_stack += 2;
				break;
			}
			if (this.max_stack < this.cur_stack)
			{
				this.max_stack = this.cur_stack;
			}
			switch (opcode.StackBehaviourPop)
			{
			case StackBehaviour.Pop1:
			case StackBehaviour.Popi:
			case StackBehaviour.Popref:
				this.cur_stack--;
				break;
			case StackBehaviour.Pop1_pop1:
			case StackBehaviour.Popi_pop1:
			case StackBehaviour.Popi_popi:
			case StackBehaviour.Popi_popi8:
			case StackBehaviour.Popi_popr4:
			case StackBehaviour.Popi_popr8:
			case StackBehaviour.Popref_pop1:
			case StackBehaviour.Popref_popi:
				this.cur_stack -= 2;
				break;
			case StackBehaviour.Popi_popi_popi:
			case StackBehaviour.Popref_popi_popi:
			case StackBehaviour.Popref_popi_popi8:
			case StackBehaviour.Popref_popi_popr4:
			case StackBehaviour.Popref_popi_popr8:
			case StackBehaviour.Popref_popi_popref:
				this.cur_stack -= 3;
				break;
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0003CC40 File Offset: 0x0003AE40
		private static int target_len(OpCode opcode)
		{
			if (opcode.OperandType == OperandType.InlineBrTarget)
			{
				return 4;
			}
			return 1;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0003CC54 File Offset: 0x0003AE54
		public virtual LocalBuilder DeclareLocal(Type localType)
		{
			return this.DeclareLocal(localType, false);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0003CC60 File Offset: 0x0003AE60
		public virtual LocalBuilder DeclareLocal(Type localType, bool pinned)
		{
			if (localType == null)
			{
				throw new ArgumentNullException("localType");
			}
			if (localType.IsUserType)
			{
				throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
			}
			LocalBuilder localBuilder = new LocalBuilder(localType, this);
			localBuilder.is_pinned = pinned;
			if (this.locals != null)
			{
				LocalBuilder[] array = new LocalBuilder[this.locals.Length + 1];
				Array.Copy(this.locals, array, this.locals.Length);
				array[this.locals.Length] = localBuilder;
				this.locals = array;
			}
			else
			{
				this.locals = new LocalBuilder[1];
				this.locals[0] = localBuilder;
			}
			localBuilder.position = (ushort)(this.locals.Length - 1);
			return localBuilder;
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0003CD10 File Offset: 0x0003AF10
		public virtual Label DefineLabel()
		{
			if (this.labels == null)
			{
				this.labels = new ILGenerator.LabelData[4];
			}
			else if (this.num_labels >= this.labels.Length)
			{
				ILGenerator.LabelData[] destinationArray = new ILGenerator.LabelData[this.labels.Length * 2];
				Array.Copy(this.labels, destinationArray, this.labels.Length);
				this.labels = destinationArray;
			}
			this.labels[this.num_labels] = new ILGenerator.LabelData(-1, 0);
			return new Label(this.num_labels++);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0003CDAC File Offset: 0x0003AFAC
		public virtual void Emit(OpCode opcode)
		{
			this.make_room(2);
			this.ll_emit(opcode);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0003CDBC File Offset: 0x0003AFBC
		public virtual void Emit(OpCode opcode, byte arg)
		{
			this.make_room(3);
			this.ll_emit(opcode);
			this.code[this.code_len++] = arg;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0003CDF0 File Offset: 0x0003AFF0
		[ComVisible(true)]
		public virtual void Emit(OpCode opcode, ConstructorInfo con)
		{
			int token = this.token_gen.GetToken(con);
			this.make_room(6);
			this.ll_emit(opcode);
			if (con.DeclaringType.Module == this.module)
			{
				this.add_token_fixup(con);
			}
			this.emit_int(token);
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				this.cur_stack -= con.GetParameterCount();
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0003CE60 File Offset: 0x0003B060
		public virtual void Emit(OpCode opcode, FieldInfo field)
		{
			int token = this.token_gen.GetToken(field);
			this.make_room(6);
			this.ll_emit(opcode);
			if (field.DeclaringType.Module == this.module)
			{
				this.add_token_fixup(field);
			}
			this.emit_int(token);
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0003CEAC File Offset: 0x0003B0AC
		public virtual void Emit(OpCode opcode, int arg)
		{
			this.make_room(6);
			this.ll_emit(opcode);
			this.emit_int(arg);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0003CEC4 File Offset: 0x0003B0C4
		public virtual void Emit(OpCode opcode, Label label)
		{
			int num = ILGenerator.target_len(opcode);
			this.make_room(6);
			this.ll_emit(opcode);
			if (this.cur_stack > this.labels[label.label].maxStack)
			{
				this.labels[label.label].maxStack = this.cur_stack;
			}
			if (this.fixups == null)
			{
				this.fixups = new ILGenerator.LabelFixup[4];
			}
			else if (this.num_fixups >= this.fixups.Length)
			{
				ILGenerator.LabelFixup[] destinationArray = new ILGenerator.LabelFixup[this.fixups.Length * 2];
				Array.Copy(this.fixups, destinationArray, this.fixups.Length);
				this.fixups = destinationArray;
			}
			this.fixups[this.num_fixups].offset = num;
			this.fixups[this.num_fixups].pos = this.code_len;
			this.fixups[this.num_fixups].label_idx = label.label;
			this.num_fixups++;
			this.code_len += num;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0003CFEC File Offset: 0x0003B1EC
		public virtual void Emit(OpCode opcode, LocalBuilder local)
		{
			if (local == null)
			{
				throw new ArgumentNullException("local");
			}
			uint position = (uint)local.position;
			bool flag = false;
			bool flag2 = false;
			this.make_room(6);
			if (local.ilgen != this)
			{
				throw new ArgumentException("Trying to emit a local from a different ILGenerator.");
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Pop1)
			{
				this.cur_stack--;
				flag2 = true;
			}
			else
			{
				this.cur_stack++;
				if (this.cur_stack > this.max_stack)
				{
					this.max_stack = this.cur_stack;
				}
				flag = (opcode.StackBehaviourPush == StackBehaviour.Pushi);
			}
			if (flag)
			{
				if (position < 256U)
				{
					this.code[this.code_len++] = 18;
					this.code[this.code_len++] = (byte)position;
				}
				else
				{
					this.code[this.code_len++] = 254;
					this.code[this.code_len++] = 13;
					this.code[this.code_len++] = (byte)(position & 255U);
					this.code[this.code_len++] = (byte)(position >> 8 & 255U);
				}
			}
			else if (flag2)
			{
				if (position < 4U)
				{
					this.code[this.code_len++] = (byte)(10U + position);
				}
				else if (position < 256U)
				{
					this.code[this.code_len++] = 19;
					this.code[this.code_len++] = (byte)position;
				}
				else
				{
					this.code[this.code_len++] = 254;
					this.code[this.code_len++] = 14;
					this.code[this.code_len++] = (byte)(position & 255U);
					this.code[this.code_len++] = (byte)(position >> 8 & 255U);
				}
			}
			else if (position < 4U)
			{
				this.code[this.code_len++] = (byte)(6U + position);
			}
			else if (position < 256U)
			{
				this.code[this.code_len++] = 17;
				this.code[this.code_len++] = (byte)position;
			}
			else
			{
				this.code[this.code_len++] = 254;
				this.code[this.code_len++] = 12;
				this.code[this.code_len++] = (byte)(position & 255U);
				this.code[this.code_len++] = (byte)(position >> 8 & 255U);
			}
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0003D32C File Offset: 0x0003B52C
		public virtual void Emit(OpCode opcode, MethodInfo meth)
		{
			if (meth == null)
			{
				throw new ArgumentNullException("meth");
			}
			if (meth is DynamicMethod && (opcode == OpCodes.Ldftn || opcode == OpCodes.Ldvirtftn || opcode == OpCodes.Ldtoken))
			{
				throw new ArgumentException("Ldtoken, Ldftn and Ldvirtftn OpCodes cannot target DynamicMethods.");
			}
			int token = this.token_gen.GetToken(meth);
			this.make_room(6);
			this.ll_emit(opcode);
			Type declaringType = meth.DeclaringType;
			if (declaringType != null && declaringType.Module == this.module)
			{
				this.add_token_fixup(meth);
			}
			this.emit_int(token);
			if (meth.ReturnType != ILGenerator.void_type)
			{
				this.cur_stack++;
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				this.cur_stack -= meth.GetParameterCount();
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0003D418 File Offset: 0x0003B618
		private void Emit(OpCode opcode, MethodInfo method, int token)
		{
			this.make_room(6);
			this.ll_emit(opcode);
			Type declaringType = method.DeclaringType;
			if (declaringType != null && declaringType.Module == this.module)
			{
				this.add_token_fixup(method);
			}
			this.emit_int(token);
			if (method.ReturnType != ILGenerator.void_type)
			{
				this.cur_stack++;
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				this.cur_stack -= method.GetParameterCount();
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0003D4A0 File Offset: 0x0003B6A0
		public virtual void Emit(OpCode opcode, string str)
		{
			int token = this.token_gen.GetToken(str);
			this.make_room(6);
			this.ll_emit(opcode);
			this.emit_int(token);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0003D4D0 File Offset: 0x0003B6D0
		public virtual void Emit(OpCode opcode, Type cls)
		{
			this.make_room(6);
			this.ll_emit(opcode);
			this.emit_int(this.token_gen.GetToken(cls));
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0003D4F4 File Offset: 0x0003B6F4
		[MonoLimitation("vararg methods are not supported")]
		public virtual void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			short value = opcode.Value;
			if (value != OpCodes.Call.Value && value != OpCodes.Callvirt.Value)
			{
				throw new NotSupportedException("Only Call and CallVirt are allowed");
			}
			if ((methodInfo.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				optionalParameterTypes = null;
			}
			if (optionalParameterTypes == null)
			{
				this.Emit(opcode, methodInfo);
				return;
			}
			if ((methodInfo.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException("Method is not VarArgs method and optional types were passed");
			}
			int token = this.token_gen.GetToken(methodInfo, optionalParameterTypes);
			this.Emit(opcode, methodInfo, token);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0003D59C File Offset: 0x0003B79C
		public virtual void MarkLabel(Label loc)
		{
			if (loc.label < 0 || loc.label >= this.num_labels)
			{
				throw new ArgumentException("The label is not valid");
			}
			if (this.labels[loc.label].addr >= 0)
			{
				throw new ArgumentException("The label was already defined");
			}
			this.labels[loc.label].addr = this.code_len;
			if (this.labels[loc.label].maxStack > this.cur_stack)
			{
				this.cur_stack = this.labels[loc.label].maxStack;
			}
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0003D658 File Offset: 0x0003B858
		internal void label_fixup()
		{
			for (int i = 0; i < this.num_fixups; i++)
			{
				if (this.labels[this.fixups[i].label_idx].addr < 0)
				{
					throw new ArgumentException("Label not marked");
				}
				int num = this.labels[this.fixups[i].label_idx].addr - (this.fixups[i].pos + this.fixups[i].offset);
				if (this.fixups[i].offset == 1)
				{
					this.code[this.fixups[i].pos] = (byte)((sbyte)num);
				}
				else
				{
					int num2 = this.code_len;
					this.code_len = this.fixups[i].pos;
					this.emit_int(num);
					this.code_len = num2;
				}
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0003D758 File Offset: 0x0003B958
		[Obsolete("Use ILOffset")]
		internal static int Mono_GetCurrentOffset(ILGenerator ig)
		{
			return ig.code_len;
		}

		// Token: 0x040006B4 RID: 1716
		private const int defaultFixupSize = 4;

		// Token: 0x040006B5 RID: 1717
		private const int defaultLabelsSize = 4;

		// Token: 0x040006B6 RID: 1718
		private const int defaultExceptionStackSize = 2;

		// Token: 0x040006B7 RID: 1719
		private static readonly Type void_type = typeof(void);

		// Token: 0x040006B8 RID: 1720
		private byte[] code;

		// Token: 0x040006B9 RID: 1721
		private int code_len;

		// Token: 0x040006BA RID: 1722
		private int max_stack;

		// Token: 0x040006BB RID: 1723
		private int cur_stack;

		// Token: 0x040006BC RID: 1724
		private LocalBuilder[] locals;

		// Token: 0x040006BD RID: 1725
		private ILExceptionInfo[] ex_handlers;

		// Token: 0x040006BE RID: 1726
		private int num_token_fixups;

		// Token: 0x040006BF RID: 1727
		private ILTokenInfo[] token_fixups;

		// Token: 0x040006C0 RID: 1728
		private ILGenerator.LabelData[] labels;

		// Token: 0x040006C1 RID: 1729
		private int num_labels;

		// Token: 0x040006C2 RID: 1730
		private ILGenerator.LabelFixup[] fixups;

		// Token: 0x040006C3 RID: 1731
		private int num_fixups;

		// Token: 0x040006C4 RID: 1732
		internal Module module;

		// Token: 0x040006C5 RID: 1733
		private int cur_block;

		// Token: 0x040006C6 RID: 1734
		private Stack open_blocks;

		// Token: 0x040006C7 RID: 1735
		private TokenGenerator token_gen;

		// Token: 0x040006C8 RID: 1736
		private ArrayList sequencePointLists;

		// Token: 0x040006C9 RID: 1737
		private SequencePointList currentSequence;

		// Token: 0x020001A5 RID: 421
		private struct LabelData
		{
			// Token: 0x0600101A RID: 4122 RVA: 0x0003D760 File Offset: 0x0003B960
			public LabelData(int addr, int maxStack)
			{
				this.addr = addr;
				this.maxStack = maxStack;
			}

			// Token: 0x040006CA RID: 1738
			public int addr;

			// Token: 0x040006CB RID: 1739
			public int maxStack;
		}

		// Token: 0x020001A6 RID: 422
		private struct LabelFixup
		{
			// Token: 0x040006CC RID: 1740
			public int offset;

			// Token: 0x040006CD RID: 1741
			public int pos;

			// Token: 0x040006CE RID: 1742
			public int label_idx;
		}
	}
}
