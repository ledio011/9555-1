using System;
using UnityEngine;

// Token: 0x02000A73 RID: 2675
public class SingletonDontDestoryUnity<T> : MonoBehaviour where T : SingletonDontDestoryUnity<T>
{
	// Token: 0x17000FD5 RID: 4053
	// (get) Token: 0x06004DE9 RID: 19945 RVA: 0x001AA214 File Offset: 0x001A8414
	public static T Instance
	{
		get
		{
			return (T)((object)SingletonDontDestoryUnity<T>.mInstance);
		}
	}

	// Token: 0x17000FD6 RID: 4054
	// (get) Token: 0x06004DEA RID: 19946 RVA: 0x001AA220 File Offset: 0x001A8420
	// (set) Token: 0x06004DEB RID: 19947 RVA: 0x001AA228 File Offset: 0x001A8428
	public static bool Exists { get; private set; }

	// Token: 0x17000FD7 RID: 4055
	// (get) Token: 0x06004DEC RID: 19948 RVA: 0x001AA230 File Offset: 0x001A8430
	public bool IsInit
	{
		get
		{
			return this.mIsInit;
		}
	}

	// Token: 0x06004DED RID: 19949 RVA: 0x001AA238 File Offset: 0x001A8438
	protected virtual void Awake()
	{
		if (SingletonDontDestoryUnity<T>.mInstance != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		SingletonDontDestoryUnity<T>.Exists = false;
		SingletonDontDestoryUnity<T>.mInstance = this;
		SingletonDontDestoryUnity<T>.Exists = true;
		Object.DontDestroyOnLoad(this);
		this.mIsInit = true;
	}

	// Token: 0x06004DEE RID: 19950 RVA: 0x001AA280 File Offset: 0x001A8480
	protected virtual void OnDestroy()
	{
		if (SingletonDontDestoryUnity<T>.mInstance == this)
		{
			SingletonDontDestoryUnity<T>.Exists = false;
			SingletonDontDestoryUnity<T>.mInstance = null;
		}
	}

	// Token: 0x04003C8B RID: 15499
	private static SingletonDontDestoryUnity<T> mInstance;

	// Token: 0x04003C8C RID: 15500
	private bool mIsInit;
}
