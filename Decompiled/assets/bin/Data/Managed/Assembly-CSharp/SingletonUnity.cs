using System;
using UnityEngine;

// Token: 0x02000A74 RID: 2676
public class SingletonUnity<T> : MonoBehaviour where T : SingletonUnity<T>
{
	// Token: 0x17000FD8 RID: 4056
	// (get) Token: 0x06004DF0 RID: 19952 RVA: 0x001AA2A8 File Offset: 0x001A84A8
	public static T Instance
	{
		get
		{
			return (T)((object)SingletonUnity<T>.mInstance);
		}
	}

	// Token: 0x17000FD9 RID: 4057
	// (get) Token: 0x06004DF1 RID: 19953 RVA: 0x001AA2B4 File Offset: 0x001A84B4
	// (set) Token: 0x06004DF2 RID: 19954 RVA: 0x001AA2BC File Offset: 0x001A84BC
	public static bool Exists { get; private set; }

	// Token: 0x06004DF3 RID: 19955 RVA: 0x001AA2C4 File Offset: 0x001A84C4
	protected virtual void Awake()
	{
		if (SingletonUnity<T>.mInstance == null)
		{
			SingletonUnity<T>.mInstance = this;
			SingletonUnity<T>.Exists = true;
		}
		else if (SingletonUnity<T>.mInstance != this)
		{
			Debug.LogWarning("Two Instance" + typeof(T).ToString());
			Object.Destroy(SingletonUnity<T>.mInstance);
			SingletonUnity<T>.mInstance = this;
		}
	}

	// Token: 0x06004DF4 RID: 19956 RVA: 0x001AA330 File Offset: 0x001A8530
	protected virtual void OnDestroy()
	{
		if (SingletonUnity<T>.mInstance == this)
		{
			SingletonUnity<T>.Exists = false;
			SingletonUnity<T>.mInstance = null;
		}
	}

	// Token: 0x04003C8E RID: 15502
	private static SingletonUnity<T> mInstance;
}
