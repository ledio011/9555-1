using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020003DB RID: 987
	[ComVisible(true)]
	[Serializable]
	public abstract class ValueType
	{
		// Token: 0x06001E96 RID: 7830
		[MethodImpl(4096)]
		private static extern bool InternalEquals(object o1, object o2, out object[] fields);

		// Token: 0x06001E97 RID: 7831 RVA: 0x00072078 File Offset: 0x00070278
		internal static bool DefaultEquals(object o1, object o2)
		{
			if (o2 == null)
			{
				return false;
			}
			object[] array;
			bool result = ValueType.InternalEquals(o1, o2, out array);
			if (array == null)
			{
				return result;
			}
			for (int i = 0; i < array.Length; i += 2)
			{
				object obj = array[i];
				object obj2 = array[i + 1];
				if (obj == null)
				{
					if (obj2 != null)
					{
						return false;
					}
				}
				else if (!obj.Equals(obj2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x000720E4 File Offset: 0x000702E4
		public override bool Equals(object obj)
		{
			return ValueType.DefaultEquals(this, obj);
		}

		// Token: 0x06001E99 RID: 7833
		[MethodImpl(4096)]
		internal static extern int InternalGetHashCode(object o, out object[] fields);

		// Token: 0x06001E9A RID: 7834 RVA: 0x000720F0 File Offset: 0x000702F0
		public override int GetHashCode()
		{
			object[] array;
			int num = ValueType.InternalGetHashCode(this, out array);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						num ^= array[i].GetHashCode();
					}
				}
			}
			return num;
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00072134 File Offset: 0x00070334
		public override string ToString()
		{
			return base.GetType().FullName;
		}
	}
}
