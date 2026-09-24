using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI
{
	// Token: 0x02000076 RID: 118
	internal class ObjectPool<T> where T : new()
	{
		// Token: 0x0600039C RID: 924 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000F3DC File Offset: 0x0000D5DC
		public int countAll { get; private set; }

		// Token: 0x0600039F RID: 927 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		public T Get()
		{
			T t;
			if (this.m_Stack.Count == 0)
			{
				t = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
				this.countAll++;
			}
			else
			{
				t = this.m_Stack.Pop();
			}
			if (this.m_ActionOnGet != null)
			{
				this.m_ActionOnGet(t);
			}
			return t;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000F460 File Offset: 0x0000D660
		public void Release(T element)
		{
			if (this.m_Stack.Count > 0 && object.ReferenceEquals(this.m_Stack.Peek(), element))
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x040001D3 RID: 467
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x040001D4 RID: 468
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x040001D5 RID: 469
		private readonly UnityAction<T> m_ActionOnRelease;
	}
}
