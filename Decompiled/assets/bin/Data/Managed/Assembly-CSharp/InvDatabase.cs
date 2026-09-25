using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000022 RID: 34
[AddComponentMenu("NGUI/Examples/Item Database")]
[ExecuteInEditMode]
public class InvDatabase : MonoBehaviour
{
	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000091 RID: 145 RVA: 0x00004D50 File Offset: 0x00002F50
	public static InvDatabase[] list
	{
		get
		{
			if (InvDatabase.mIsDirty)
			{
				InvDatabase.mIsDirty = false;
				InvDatabase.mList = NGUITools.FindActive<InvDatabase>();
			}
			return InvDatabase.mList;
		}
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00004D74 File Offset: 0x00002F74
	private void OnEnable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00004D7C File Offset: 0x00002F7C
	private void OnDisable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00004D84 File Offset: 0x00002F84
	private InvBaseItem GetItem(int id16)
	{
		int i = 0;
		int count = this.items.Count;
		while (i < count)
		{
			InvBaseItem invBaseItem = this.items[i];
			if (invBaseItem.id16 == id16)
			{
				return invBaseItem;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00004DCC File Offset: 0x00002FCC
	private static InvDatabase GetDatabase(int dbID)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.databaseID == dbID)
			{
				return invDatabase;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00004E0C File Offset: 0x0000300C
	public static InvBaseItem FindByID(int id32)
	{
		InvDatabase database = InvDatabase.GetDatabase(id32 >> 16);
		return (!(database != null)) ? null : database.GetItem(id32 & 65535);
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00004E44 File Offset: 0x00003044
	public static InvBaseItem FindByName(string exact)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			int j = 0;
			int count = invDatabase.items.Count;
			while (j < count)
			{
				InvBaseItem invBaseItem = invDatabase.items[j];
				if (invBaseItem.name == exact)
				{
					return invBaseItem;
				}
				j++;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00004EB8 File Offset: 0x000030B8
	public static int FindItemID(InvBaseItem item)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.items.Contains(item))
			{
				return invDatabase.databaseID << 16 | item.id16;
			}
			i++;
		}
		return -1;
	}

	// Token: 0x0400009A RID: 154
	private static InvDatabase[] mList;

	// Token: 0x0400009B RID: 155
	private static bool mIsDirty = true;

	// Token: 0x0400009C RID: 156
	public int databaseID;

	// Token: 0x0400009D RID: 157
	public List<InvBaseItem> items = new List<InvBaseItem>();

	// Token: 0x0400009E RID: 158
	public UIAtlas iconAtlas;
}
