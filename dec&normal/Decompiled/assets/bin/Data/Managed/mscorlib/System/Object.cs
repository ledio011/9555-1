using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200016C RID: 364
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Serializable]
	public class Object
	{
		// Token: 0x06000DED RID: 3565 RVA: 0x00037C20 File Offset: 0x00035E20
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public Object()
		{
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00037C24 File Offset: 0x00035E24
		public virtual bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00037C2C File Offset: 0x00035E2C
		public static bool Equals(object objA, object objB)
		{
			return objA == objB || (objA != null && objB != null && objA.Equals(objB));
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00037C4C File Offset: 0x00035E4C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Finalize()
		{
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00037C50 File Offset: 0x00035E50
		public virtual int GetHashCode()
		{
			return object.InternalGetHashCode(this);
		}

		// Token: 0x06000DF2 RID: 3570
		[MethodImpl(4096)]
		public extern Type GetType();

		// Token: 0x06000DF3 RID: 3571
		[MethodImpl(4096)]
		protected extern object MemberwiseClone();

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00037C58 File Offset: 0x00035E58
		public virtual string ToString()
		{
			return this.GetType().ToString();
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00037C68 File Offset: 0x00035E68
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool ReferenceEquals(object objA, object objB)
		{
			return objA == objB;
		}

		// Token: 0x06000DF6 RID: 3574
		[MethodImpl(4096)]
		internal static extern int InternalGetHashCode(object o);

		// Token: 0x06000DF7 RID: 3575
		[MethodImpl(4096)]
		internal extern IntPtr obj_address();

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00037C70 File Offset: 0x00035E70
		private void FieldGetter(string typeName, string fieldName, ref object val)
		{
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00037C74 File Offset: 0x00035E74
		private void FieldSetter(string typeName, string fieldName, object val)
		{
		}
	}
}
