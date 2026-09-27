using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000163 RID: 355
	[ComVisible(true)]
	[Serializable]
	public abstract class MulticastDelegate : Delegate
	{
		// Token: 0x06000D66 RID: 3430 RVA: 0x00033B0C File Offset: 0x00031D0C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00033B18 File Offset: 0x00031D18
		public sealed override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			MulticastDelegate multicastDelegate = obj as MulticastDelegate;
			if (multicastDelegate == null)
			{
				return false;
			}
			if (this.prev == null)
			{
				return multicastDelegate.prev == null;
			}
			return this.prev.Equals(multicastDelegate.prev);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00033B70 File Offset: 0x00031D70
		public sealed override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00033B78 File Offset: 0x00031D78
		public sealed override Delegate[] GetInvocationList()
		{
			MulticastDelegate multicastDelegate = (MulticastDelegate)this.Clone();
			multicastDelegate.kpm_next = null;
			while (multicastDelegate.prev != null)
			{
				multicastDelegate.prev.kpm_next = multicastDelegate;
				multicastDelegate = multicastDelegate.prev;
			}
			if (multicastDelegate.kpm_next == null)
			{
				MulticastDelegate multicastDelegate2 = (MulticastDelegate)multicastDelegate.Clone();
				multicastDelegate2.prev = null;
				multicastDelegate2.kpm_next = null;
				return new Delegate[]
				{
					multicastDelegate2
				};
			}
			ArrayList arrayList = new ArrayList();
			while (multicastDelegate != null)
			{
				MulticastDelegate multicastDelegate3 = (MulticastDelegate)multicastDelegate.Clone();
				multicastDelegate3.prev = null;
				multicastDelegate3.kpm_next = null;
				arrayList.Add(multicastDelegate3);
				multicastDelegate = multicastDelegate.kpm_next;
			}
			return (Delegate[])arrayList.ToArray(typeof(Delegate));
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00033C3C File Offset: 0x00031E3C
		protected sealed override Delegate CombineImpl(Delegate follow)
		{
			if (base.GetType() != follow.GetType())
			{
				throw new ArgumentException(Locale.GetText("Incompatible Delegate Types."));
			}
			MulticastDelegate multicastDelegate = (MulticastDelegate)follow.Clone();
			multicastDelegate.SetMulticastInvoke();
			MulticastDelegate multicastDelegate2 = multicastDelegate;
			for (MulticastDelegate multicastDelegate3 = ((MulticastDelegate)follow).prev; multicastDelegate3 != null; multicastDelegate3 = multicastDelegate3.prev)
			{
				multicastDelegate2.prev = (MulticastDelegate)multicastDelegate3.Clone();
				multicastDelegate2 = multicastDelegate2.prev;
			}
			multicastDelegate2.prev = (MulticastDelegate)this.Clone();
			multicastDelegate2 = multicastDelegate2.prev;
			for (MulticastDelegate multicastDelegate3 = this.prev; multicastDelegate3 != null; multicastDelegate3 = multicastDelegate3.prev)
			{
				multicastDelegate2.prev = (MulticastDelegate)multicastDelegate3.Clone();
				multicastDelegate2 = multicastDelegate2.prev;
			}
			return multicastDelegate;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00033D00 File Offset: 0x00031F00
		private bool BaseEquals(MulticastDelegate value)
		{
			return base.Equals(value);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00033D0C File Offset: 0x00031F0C
		private static MulticastDelegate KPM(MulticastDelegate needle, MulticastDelegate haystack, out MulticastDelegate tail)
		{
			MulticastDelegate multicastDelegate = needle;
			MulticastDelegate multicastDelegate2 = needle.kpm_next = null;
			for (;;)
			{
				while (multicastDelegate2 != null && !multicastDelegate2.BaseEquals(multicastDelegate))
				{
					multicastDelegate2 = multicastDelegate2.kpm_next;
				}
				multicastDelegate = multicastDelegate.prev;
				if (multicastDelegate == null)
				{
					break;
				}
				multicastDelegate2 = ((multicastDelegate2 != null) ? multicastDelegate2.prev : needle);
				if (multicastDelegate.BaseEquals(multicastDelegate2))
				{
					multicastDelegate.kpm_next = multicastDelegate2.kpm_next;
				}
				else
				{
					multicastDelegate.kpm_next = multicastDelegate2;
				}
			}
			MulticastDelegate multicastDelegate3 = haystack;
			multicastDelegate2 = needle;
			multicastDelegate = haystack;
			for (;;)
			{
				while (multicastDelegate2 != null && !multicastDelegate2.BaseEquals(multicastDelegate))
				{
					multicastDelegate2 = multicastDelegate2.kpm_next;
					multicastDelegate3 = multicastDelegate3.prev;
				}
				multicastDelegate2 = ((multicastDelegate2 != null) ? multicastDelegate2.prev : needle);
				if (multicastDelegate2 == null)
				{
					break;
				}
				multicastDelegate = multicastDelegate.prev;
				if (multicastDelegate == null)
				{
					goto Block_8;
				}
			}
			tail = multicastDelegate.prev;
			return multicastDelegate3;
			Block_8:
			tail = null;
			return null;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00033DF4 File Offset: 0x00031FF4
		protected sealed override Delegate RemoveImpl(Delegate value)
		{
			if (value == null)
			{
				return this;
			}
			MulticastDelegate multicastDelegate2;
			MulticastDelegate multicastDelegate = MulticastDelegate.KPM((MulticastDelegate)value, this, out multicastDelegate2);
			if (multicastDelegate == null)
			{
				return this;
			}
			MulticastDelegate multicastDelegate3 = null;
			MulticastDelegate result = null;
			for (MulticastDelegate multicastDelegate4 = this; multicastDelegate4 != multicastDelegate; multicastDelegate4 = multicastDelegate4.prev)
			{
				MulticastDelegate multicastDelegate5 = (MulticastDelegate)multicastDelegate4.Clone();
				if (multicastDelegate3 != null)
				{
					multicastDelegate3.prev = multicastDelegate5;
				}
				else
				{
					result = multicastDelegate5;
				}
				multicastDelegate3 = multicastDelegate5;
			}
			for (MulticastDelegate multicastDelegate4 = multicastDelegate2; multicastDelegate4 != null; multicastDelegate4 = multicastDelegate4.prev)
			{
				MulticastDelegate multicastDelegate6 = (MulticastDelegate)multicastDelegate4.Clone();
				if (multicastDelegate3 != null)
				{
					multicastDelegate3.prev = multicastDelegate6;
				}
				else
				{
					result = multicastDelegate6;
				}
				multicastDelegate3 = multicastDelegate6;
			}
			if (multicastDelegate3 != null)
			{
				multicastDelegate3.prev = null;
			}
			return result;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00033EB4 File Offset: 0x000320B4
		public static bool operator !=(MulticastDelegate d1, MulticastDelegate d2)
		{
			if (d1 == null)
			{
				return d2 != null;
			}
			return !d1.Equals(d2);
		}

		// Token: 0x0400057F RID: 1407
		private MulticastDelegate prev;

		// Token: 0x04000580 RID: 1408
		private MulticastDelegate kpm_next;
	}
}
