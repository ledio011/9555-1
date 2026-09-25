using System;

// Token: 0x02000A7C RID: 2684
public class UUID
{
	// Token: 0x06004E11 RID: 19985 RVA: 0x001AAD8C File Offset: 0x001A8F8C
	public static long GenUUID()
	{
		long num = UUID.id;
		UUID.id = num + 1L;
		return num;
	}

	// Token: 0x04003CA2 RID: 15522
	private static long id = 99L;
}
