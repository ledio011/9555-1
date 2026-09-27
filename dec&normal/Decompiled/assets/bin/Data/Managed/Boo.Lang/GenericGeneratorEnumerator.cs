using System;
using System.Collections;
using System.Collections.Generic;

namespace Boo.Lang
{
	// Token: 0x02000004 RID: 4
	public abstract class GenericGeneratorEnumerator<T> : IEnumerator<T>, IEnumerator, IDisposable
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002100 File Offset: 0x00000300
		public GenericGeneratorEnumerator()
		{
			this._state = 0;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002110 File Offset: 0x00000310
		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		public T Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002128 File Offset: 0x00000328
		public virtual void Dispose()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000212C File Offset: 0x0000032C
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600000B RID: 11
		public abstract bool MoveNext();

		// Token: 0x0600000C RID: 12 RVA: 0x00002134 File Offset: 0x00000334
		protected bool Yield(int state, T value)
		{
			this._state = state;
			this._current = value;
			return true;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002148 File Offset: 0x00000348
		protected bool YieldDefault(int state)
		{
			this._state = state;
			this._current = default(T);
			return true;
		}

		// Token: 0x04000001 RID: 1
		protected T _current;

		// Token: 0x04000002 RID: 2
		public int _state;
	}
}
