using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012B RID: 299
[Serializable]
public class ChatMSGLinePool
{
	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0005252C File Offset: 0x0005072C
	public List<ChatMessageLineLogic> EnableMsgList
	{
		get
		{
			return this.mEnableMsgList;
		}
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00052534 File Offset: 0x00050734
	public List<ChatMessageLineLogic> DisableMsgList
	{
		get
		{
			return this.mDisableMsgList;
		}
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x0005253C File Offset: 0x0005073C
	public void Reset(ChatMessageLineLogic prefab = null)
	{
		if (this.mEnableMsgList.Count > 0)
		{
			for (int i = this.mEnableMsgList.Count - 1; i >= 0; i--)
			{
				this.RecycleItem(this.mEnableMsgList[i]);
			}
		}
		this.mEnableMsgList.Clear();
		for (int j = 0; j < this.mDisableMsgList.Count; j++)
		{
			UnityVersionUtil.SetActiveRecursive(this.mDisableMsgList[j].gameObject, false);
		}
		this.mCount = this.mDisableMsgList.Count;
		if (prefab != null)
		{
			this.MSGPrefab = prefab;
			UnityVersionUtil.SetActiveRecursive(this.MSGPrefab.gameObject, false);
		}
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x00052600 File Offset: 0x00050800
	public ChatMessageLineLogic AddItem(bool IsNew)
	{
		ChatMessageLineLogic chatMessageLineLogic;
		if (this.mDisableMsgList.Count > 0)
		{
			chatMessageLineLogic = this.mDisableMsgList[0];
			this.mDisableMsgList.RemoveAt(0);
			this.mEnableMsgList.Add(chatMessageLineLogic);
		}
		else if (this.mCount < 10)
		{
			this.mCount++;
			GameObject gameObject = Object.Instantiate(this.MSGPrefab.gameObject) as GameObject;
			gameObject.transform.parent = this.MSGPrefab.transform.parent;
			gameObject.transform.localScale = Vector3.one;
			gameObject.gameObject.name = string.Format("{0}", this.mEnableMsgList.Count);
			chatMessageLineLogic = gameObject.GetComponent<ChatMessageLineLogic>();
			UIEventListener messageListener = chatMessageLineLogic.MessageListener;
			messageListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(messageListener.onDrag, new UIEventListener.VectorDelegate(this.OnDrag));
			this.mEnableMsgList.Add(chatMessageLineLogic);
		}
		else if (IsNew)
		{
			chatMessageLineLogic = this.mEnableMsgList[0];
			this.mEnableMsgList.RemoveAt(0);
			this.mEnableMsgList.Add(chatMessageLineLogic);
		}
		else
		{
			chatMessageLineLogic = this.mEnableMsgList[this.mEnableMsgList.Count - 1];
			this.mEnableMsgList.RemoveAt(this.mEnableMsgList.Count - 1);
			this.mEnableMsgList.Insert(0, chatMessageLineLogic);
		}
		UnityVersionUtil.SetActiveRecursive(chatMessageLineLogic.gameObject, true);
		return chatMessageLineLogic;
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x00052788 File Offset: 0x00050988
	public Vector3 GetPreLastMSGPOS()
	{
		if (this.mEnableMsgList.Count > 1)
		{
			return this.mEnableMsgList[1].transform.localPosition;
		}
		return Vector3.zero;
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x000527C4 File Offset: 0x000509C4
	public Vector3 GetPreNewestMSGPOS()
	{
		if (this.mEnableMsgList.Count > 1)
		{
			return this.mEnableMsgList[this.mEnableMsgList.Count - 2].transform.localPosition;
		}
		return Vector3.zero;
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x0005280C File Offset: 0x00050A0C
	public ChatMessageLineLogic GetLatestMSGLine()
	{
		if (this.mEnableMsgList.Count > 0)
		{
			return this.mEnableMsgList[0];
		}
		return null;
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x00052830 File Offset: 0x00050A30
	public ChatMessageLineLogic GetPreLatestMSGLine()
	{
		if (this.mEnableMsgList.Count > 1)
		{
			return this.mEnableMsgList[1];
		}
		return null;
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x00052854 File Offset: 0x00050A54
	public ChatMessageLineLogic GetNewestMSGLine()
	{
		if (this.mEnableMsgList.Count > 0)
		{
			return this.mEnableMsgList[this.mEnableMsgList.Count - 1];
		}
		return null;
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x00052884 File Offset: 0x00050A84
	public void RecycleItem(ChatMessageLineLogic curItem)
	{
		this.mEnableMsgList.Remove(curItem);
		UnityVersionUtil.SetActiveRecursive(curItem.gameObject, false);
		this.mDisableMsgList.Add(curItem);
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x000528AC File Offset: 0x00050AAC
	private void OnDrag(GameObject obj, Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(obj, delta);
		}
	}

	// Token: 0x04000A13 RID: 2579
	public const int MAX_MESSAGE_NUM = 10;

	// Token: 0x04000A14 RID: 2580
	private int mCount;

	// Token: 0x04000A15 RID: 2581
	public ChatMessageLineLogic MSGPrefab;

	// Token: 0x04000A16 RID: 2582
	private List<ChatMessageLineLogic> mEnableMsgList = new List<ChatMessageLineLogic>();

	// Token: 0x04000A17 RID: 2583
	private List<ChatMessageLineLogic> mDisableMsgList = new List<ChatMessageLineLogic>();

	// Token: 0x04000A18 RID: 2584
	public UIEventListener.VectorDelegate onDrag;
}
