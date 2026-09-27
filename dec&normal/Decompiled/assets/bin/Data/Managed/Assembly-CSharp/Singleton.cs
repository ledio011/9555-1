using System;

// Token: 0x02000A72 RID: 2674
public class Singleton<T> where T : class, new()
{
	// Token: 0x17000FD3 RID: 4051
	// (get) Token: 0x06004DE5 RID: 19941 RVA: 0x001AA1C8 File Offset: 0x001A83C8
	public static T Instance
	{
		get
		{
			if (Singleton<T>.mInstance == null)
			{
				Singleton<T>.mInstance = Activator.CreateInstance<T>();
			}
			return Singleton<T>.mInstance;
		}
	}

	// Token: 0x17000FD4 RID: 4052
	// (get) Token: 0x06004DE6 RID: 19942 RVA: 0x001AA1E8 File Offset: 0x001A83E8
	public static bool Exists
	{
		get
		{
			return Singleton<T>.mInstance != null;
		}
	}

	// Token: 0x06004DE7 RID: 19943 RVA: 0x001AA1FC File Offset: 0x001A83FC
	public static void ClearInstace()
	{
		Singleton<T>.mInstance = (T)((object)null);
	}

	// Token: 0x04003C8A RID: 15498
	private static T mInstance;
}
