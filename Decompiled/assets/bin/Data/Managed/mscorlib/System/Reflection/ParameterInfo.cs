using System;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001E6 RID: 486
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_ParameterInfo))]
	[Serializable]
	public class ParameterInfo : ICustomAttributeProvider, _ParameterInfo
	{
		// Token: 0x0600122A RID: 4650 RVA: 0x00044A00 File Offset: 0x00042C00
		protected ParameterInfo()
		{
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00044A08 File Offset: 0x00042C08
		internal ParameterInfo(ParameterBuilder pb, Type type, MemberInfo member, int position)
		{
			this.ClassImpl = type;
			this.MemberImpl = member;
			if (pb != null)
			{
				this.NameImpl = pb.Name;
				this.PositionImpl = pb.Position - 1;
				this.AttrsImpl = (ParameterAttributes)pb.Attributes;
			}
			else
			{
				this.NameImpl = null;
				this.PositionImpl = position - 1;
				this.AttrsImpl = ParameterAttributes.None;
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00044A74 File Offset: 0x00042C74
		internal ParameterInfo(ParameterInfo pinfo, MemberInfo member)
		{
			this.ClassImpl = pinfo.ParameterType;
			this.MemberImpl = member;
			this.NameImpl = pinfo.Name;
			this.PositionImpl = pinfo.Position;
			this.AttrsImpl = pinfo.Attributes;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00044AB4 File Offset: 0x00042CB4
		public override string ToString()
		{
			Type type = this.ClassImpl;
			while (type.HasElementType)
			{
				type = type.GetElementType();
			}
			bool flag = type.IsPrimitive || this.ClassImpl == typeof(void) || this.ClassImpl.Namespace == this.MemberImpl.DeclaringType.Namespace;
			string text = (!flag) ? this.ClassImpl.FullName : this.ClassImpl.Name;
			if (!this.IsRetval)
			{
				text += ' ';
				text += this.NameImpl;
			}
			return text;
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x00044B6C File Offset: 0x00042D6C
		public virtual Type ParameterType
		{
			get
			{
				return this.ClassImpl;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x00044B74 File Offset: 0x00042D74
		public virtual ParameterAttributes Attributes
		{
			get
			{
				return this.AttrsImpl;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x00044B7C File Offset: 0x00042D7C
		public bool IsIn
		{
			get
			{
				return (this.Attributes & ParameterAttributes.In) != ParameterAttributes.None;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x00044B8C File Offset: 0x00042D8C
		public bool IsOptional
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Optional) != ParameterAttributes.None;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00044BA0 File Offset: 0x00042DA0
		public bool IsOut
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Out) != ParameterAttributes.None;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00044BB0 File Offset: 0x00042DB0
		public bool IsRetval
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Retval) != ParameterAttributes.None;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00044BC0 File Offset: 0x00042DC0
		public virtual MemberInfo Member
		{
			get
			{
				return this.MemberImpl;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x00044BC8 File Offset: 0x00042DC8
		public virtual string Name
		{
			get
			{
				return this.NameImpl;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x00044BD0 File Offset: 0x00042DD0
		public virtual int Position
		{
			get
			{
				return this.PositionImpl;
			}
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00044BD8 File Offset: 0x00042DD8
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00044BE4 File Offset: 0x00042DE4
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00044BF0 File Offset: 0x00042DF0
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00044BFC File Offset: 0x00042DFC
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if (this.IsIn)
			{
				num++;
			}
			if (this.IsOut)
			{
				num++;
			}
			if (this.IsOptional)
			{
				num++;
			}
			if (this.marshalAs != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if (this.IsIn)
			{
				array[num++] = new InAttribute();
			}
			if (this.IsOptional)
			{
				array[num++] = new OptionalAttribute();
			}
			if (this.IsOut)
			{
				array[num++] = new OutAttribute();
			}
			if (this.marshalAs != null)
			{
				array[num++] = this.marshalAs.ToMarshalAsAttribute();
			}
			return array;
		}

		// Token: 0x04000937 RID: 2359
		protected Type ClassImpl;

		// Token: 0x04000938 RID: 2360
		protected object DefaultValueImpl;

		// Token: 0x04000939 RID: 2361
		protected MemberInfo MemberImpl;

		// Token: 0x0400093A RID: 2362
		protected string NameImpl;

		// Token: 0x0400093B RID: 2363
		protected int PositionImpl;

		// Token: 0x0400093C RID: 2364
		protected ParameterAttributes AttrsImpl;

		// Token: 0x0400093D RID: 2365
		private UnmanagedMarshal marshalAs;
	}
}
