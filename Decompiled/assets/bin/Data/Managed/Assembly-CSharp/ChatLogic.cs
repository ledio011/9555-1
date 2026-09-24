using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class ChatLogic : MonoBehaviour
{
	// Token: 0x06000ADB RID: 2779 RVA: 0x00050F1C File Offset: 0x0004F11C
	private void Awake()
	{
		ChatLogic.Instance = this;
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x00050F24 File Offset: 0x0004F124
	public static void AddFriend(friend_info friend)
	{
		if (ChatLogic.FriendList.ContainsKey(friend.characterId))
		{
			return;
		}
		ChatLogic.FriendList.Add(friend.characterId, friend);
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x00050F58 File Offset: 0x0004F158
	private void Start()
	{
		base.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x00050F8C File Offset: 0x0004F18C
	private void Update()
	{
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x00050F90 File Offset: 0x0004F190
	public void OnClickExit()
	{
	}

	// Token: 0x040009EA RID: 2538
	public ChatType CurrentChatType;

	// Token: 0x040009EB RID: 2539
	public static Dictionary<long, friend_info> FriendList = new Dictionary<long, friend_info>();

	// Token: 0x040009EC RID: 2540
	public static ChatLogic Instance;
}
