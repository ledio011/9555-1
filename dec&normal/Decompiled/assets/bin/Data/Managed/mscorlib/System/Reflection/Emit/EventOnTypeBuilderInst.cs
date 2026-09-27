using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200019E RID: 414
	internal class EventOnTypeBuilderInst : EventInfo
	{
		// Token: 0x06000FA4 RID: 4004 RVA: 0x0003C3B0 File Offset: 0x0003A5B0
		internal EventOnTypeBuilderInst(MonoGenericClass instantiation, EventBuilder evt)
		{
			this.instantiation = instantiation;
			this.evt = evt;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0003C3C8 File Offset: 0x0003A5C8
		public override EventAttributes Attributes
		{
			get
			{
				return this.evt.attrs;
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003C3D8 File Offset: 0x0003A5D8
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			if (this.evt.add_method == null || (!nonPublic && !this.evt.add_method.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, this.evt.add_method);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003C428 File Offset: 0x0003A628
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			if (this.evt.remove_method == null || (!nonPublic && !this.evt.remove_method.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, this.evt.remove_method);
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x0003C478 File Offset: 0x0003A678
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0003C480 File Offset: 0x0003A680
		public override string Name
		{
			get
			{
				return this.evt.name;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0003C490 File Offset: 0x0003A690
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0003C498 File Offset: 0x0003A698
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0003C4A0 File Offset: 0x0003A6A0
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0003C4A8 File Offset: 0x0003A6A8
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400068D RID: 1677
		private MonoGenericClass instantiation;

		// Token: 0x0400068E RID: 1678
		private EventBuilder evt;
	}
}
