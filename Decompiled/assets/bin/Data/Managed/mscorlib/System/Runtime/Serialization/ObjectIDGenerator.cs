using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000307 RID: 775
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with.NET")]
	[Serializable]
	public class ObjectIDGenerator
	{
		// Token: 0x060017D2 RID: 6098 RVA: 0x00056A38 File Offset: 0x00054C38
		public ObjectIDGenerator()
		{
			this.table = new Hashtable(ObjectIDGenerator.comparer, ObjectIDGenerator.comparer);
			this.current = 1L;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00056A6C File Offset: 0x00054C6C
		public virtual long GetId(object obj, out bool firstTime)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			object obj2 = this.table[obj];
			if (obj2 != null)
			{
				firstTime = false;
				return (long)obj2;
			}
			firstTime = true;
			this.table.Add(obj, this.current);
			long result;
			this.current = (result = this.current) + 1L;
			return result;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x00056AD4 File Offset: 0x00054CD4
		internal long NextId
		{
			get
			{
				long result;
				this.current = (result = this.current) + 1L;
				return result;
			}
		}

		// Token: 0x04000C5D RID: 3165
		private Hashtable table;

		// Token: 0x04000C5E RID: 3166
		private long current;

		// Token: 0x04000C5F RID: 3167
		private static ObjectIDGenerator.InstanceComparer comparer = new ObjectIDGenerator.InstanceComparer();

		// Token: 0x02000308 RID: 776
		private class InstanceComparer : IComparer, IHashCodeProvider
		{
			// Token: 0x060017D7 RID: 6103 RVA: 0x00056AFC File Offset: 0x00054CFC
			int IComparer.Compare(object o1, object o2)
			{
				if (o1 is string)
				{
					return (!o1.Equals(o2)) ? 1 : 0;
				}
				return (o1 != o2) ? 1 : 0;
			}

			// Token: 0x060017D8 RID: 6104 RVA: 0x00056B2C File Offset: 0x00054D2C
			int IHashCodeProvider.GetHashCode(object o)
			{
				return object.InternalGetHashCode(o);
			}
		}
	}
}
