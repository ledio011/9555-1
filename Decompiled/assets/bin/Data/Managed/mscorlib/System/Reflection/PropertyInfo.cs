using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001EC RID: 492
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_PropertyInfo))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public abstract class PropertyInfo : MemberInfo, _PropertyInfo
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600123C RID: 4668
		public abstract PropertyAttributes Attributes { get; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600123D RID: 4669
		public abstract bool CanRead { get; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600123E RID: 4670
		public abstract bool CanWrite { get; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00044CC4 File Offset: 0x00042EC4
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001240 RID: 4672
		public abstract Type PropertyType { get; }

		// Token: 0x06001241 RID: 4673 RVA: 0x00044CC8 File Offset: 0x00042EC8
		public MethodInfo GetGetMethod()
		{
			return this.GetGetMethod(false);
		}

		// Token: 0x06001242 RID: 4674
		public abstract MethodInfo GetGetMethod(bool nonPublic);

		// Token: 0x06001243 RID: 4675
		public abstract ParameterInfo[] GetIndexParameters();

		// Token: 0x06001244 RID: 4676
		public abstract MethodInfo GetSetMethod(bool nonPublic);

		// Token: 0x06001245 RID: 4677 RVA: 0x00044CD4 File Offset: 0x00042ED4
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		// Token: 0x06001246 RID: 4678
		public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x06001247 RID: 4679 RVA: 0x00044CE4 File Offset: 0x00042EE4
		[DebuggerStepThrough]
		[DebuggerHidden]
		public virtual void SetValue(object obj, object value, object[] index)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, index, null);
		}

		// Token: 0x06001248 RID: 4680
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);
	}
}
