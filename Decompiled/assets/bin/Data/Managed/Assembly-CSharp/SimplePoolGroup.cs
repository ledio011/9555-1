using System;
using System.Collections.Generic;

// Token: 0x02000122 RID: 290
public class SimplePoolGroup<T>
{
	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0004F964 File Offset: 0x0004DB64
	public Dictionary<string, SimplePool<T>> ObjPoolDic
	{
		get
		{
			return this.mObjPoolDic;
		}
	}

	// Token: 0x170001BA RID: 442
	// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0004F96C File Offset: 0x0004DB6C
	public List<T> EnableList
	{
		get
		{
			return this.mEnableList;
		}
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x0004F974 File Offset: 0x0004DB74
	public void Reset(SimplePool<T>.CreateFunc createFunc, SimplePool<T>.DestroyFunc destroyFunc, int poolCount)
	{
		this.mCreateFunc = createFunc;
		this.mDestroyFunc = destroyFunc;
		this.mPoolCount = poolCount;
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0004F98C File Offset: 0x0004DB8C
	public void Init(string poolKey, object data = null)
	{
		T t = this.Get(poolKey, data);
		this.Recycle(t, poolKey);
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0004F9AC File Offset: 0x0004DBAC
	public T Get(string poolKey, object data = null)
	{
		SimplePool<T> simplePool = null;
		this.mObjPoolDic.TryGetValue(poolKey, ref simplePool);
		T t = default(T);
		T t2;
		if (simplePool != null)
		{
			t2 = simplePool.Get(data);
		}
		else
		{
			simplePool = new SimplePool<T>();
			simplePool.Reset(this.mCreateFunc, this.mDestroyFunc, this.mPoolCount);
			this.mObjPoolDic.Add(poolKey, simplePool);
			t2 = simplePool.Get(data);
		}
		if (t2 != null)
		{
			this.mEnableList.Add(t2);
		}
		return t2;
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0004FA34 File Offset: 0x0004DC34
	public void Recycle(T t, string poolKey)
	{
		this.mEnableList.Remove(t);
		SimplePool<T> simplePool;
		this.mObjPoolDic.TryGetValue(poolKey, ref simplePool);
		if (simplePool != null)
		{
			simplePool.Recycle(t);
		}
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x0004FA6C File Offset: 0x0004DC6C
	public void Clear()
	{
		List<SimplePool<T>> list = new List<SimplePool<T>>(this.mObjPoolDic.Values);
		for (int i = 0; i < list.Count; i++)
		{
			list[i].Clear();
		}
		this.mEnableList.Clear();
	}

	// Token: 0x040009B0 RID: 2480
	private Dictionary<string, SimplePool<T>> mObjPoolDic = new Dictionary<string, SimplePool<T>>();

	// Token: 0x040009B1 RID: 2481
	private List<T> mEnableList = new List<T>();

	// Token: 0x040009B2 RID: 2482
	private int mPoolCount = 20;

	// Token: 0x040009B3 RID: 2483
	private SimplePool<T>.CreateFunc mCreateFunc;

	// Token: 0x040009B4 RID: 2484
	private SimplePool<T>.DestroyFunc mDestroyFunc;
}
