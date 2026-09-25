using System;
using System.Collections.Generic;

// Token: 0x02000121 RID: 289
public class SimplePool<T>
{
	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0004F720 File Offset: 0x0004D920
	public int PoolCount
	{
		get
		{
			return this.mPoolCount;
		}
	}

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0004F728 File Offset: 0x0004D928
	public List<T> EnableObjList
	{
		get
		{
			return this.mEnableObjList;
		}
	}

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0004F730 File Offset: 0x0004D930
	public List<T> DisableObjList
	{
		get
		{
			return this.mDisableObjList;
		}
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x0004F738 File Offset: 0x0004D938
	public void SimpleClear()
	{
		this.mEnableObjList.Clear();
		this.mDisableObjList.Clear();
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x0004F750 File Offset: 0x0004D950
	public void Reset(SimplePool<T>.CreateFunc createFunc, SimplePool<T>.DestroyFunc destroyFunc, int poolCount)
	{
		this.SimpleClear();
		this.mCreateFunc = createFunc;
		this.mDestroyFunc = destroyFunc;
		this.mPoolCount = poolCount;
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0004F770 File Offset: 0x0004D970
	public void Clear()
	{
		for (int i = 0; i < this.mEnableObjList.Count; i++)
		{
			if (this.mDestroyFunc != null)
			{
				this.mDestroyFunc(this.mEnableObjList[i]);
			}
		}
		for (int j = 0; j < this.mDisableObjList.Count; j++)
		{
			if (this.mDestroyFunc != null)
			{
				this.mDestroyFunc(this.mDisableObjList[j]);
			}
		}
		this.mEnableObjList.Clear();
		this.mDisableObjList.Clear();
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0004F810 File Offset: 0x0004DA10
	public void Init(object data = null)
	{
		T t = this.Get(data);
		this.Recycle(t);
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0004F82C File Offset: 0x0004DA2C
	public T Get(object data = null)
	{
		T t = default(T);
		if (this.mDisableObjList.Count > 0)
		{
			t = this.mDisableObjList[0];
			this.mDisableObjList.RemoveAt(0);
			this.mEnableObjList.Add(t);
		}
		else if (this.mEnableObjList.Count < this.mPoolCount && this.mCreateFunc != null)
		{
			t = this.mCreateFunc(data);
			if (t != null)
			{
				this.mEnableObjList.Add(t);
			}
		}
		return t;
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0004F8C4 File Offset: 0x0004DAC4
	public void Recycle(T t)
	{
		this.mEnableObjList.Remove(t);
		if (this.mDisableObjList.Contains(t))
		{
			return;
		}
		if (this.mDisableObjList.Count + this.mEnableObjList.Count <= this.mPoolCount)
		{
			this.mDisableObjList.Add(t);
		}
		else
		{
			this.mDestroyFunc(t);
		}
	}

	// Token: 0x040009AB RID: 2475
	private int mPoolCount = 20;

	// Token: 0x040009AC RID: 2476
	private List<T> mEnableObjList = new List<T>();

	// Token: 0x040009AD RID: 2477
	private List<T> mDisableObjList = new List<T>();

	// Token: 0x040009AE RID: 2478
	private SimplePool<T>.CreateFunc mCreateFunc;

	// Token: 0x040009AF RID: 2479
	private SimplePool<T>.DestroyFunc mDestroyFunc;

	// Token: 0x02000AC5 RID: 2757
	// (Invoke) Token: 0x06004F9D RID: 20381
	public delegate T CreateFunc(object data);

	// Token: 0x02000AC6 RID: 2758
	// (Invoke) Token: 0x06004FA1 RID: 20385
	public delegate void DestroyFunc(T t);
}
