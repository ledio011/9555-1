using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A37 RID: 2615
public class NoticeLogic : MonoBehaviour
{
	// Token: 0x06004C46 RID: 19526 RVA: 0x0019D9D8 File Offset: 0x0019BBD8
	public static void AddNotifyData(string data, bool isWarning = true, bool isFilterRepeate = false)
	{
		string text = string.Empty;
		if (!string.IsNullOrEmpty(data))
		{
			char c = data.get_Chars(0);
			if (c != '#')
			{
				text = data;
			}
			else
			{
				text = StrDictionary.GetServerDictionaryString(data);
			}
		}
		NoticeLogic.NoticeData noticeData = new NoticeLogic.NoticeData(text, isWarning);
		if (NoticeLogic.notifyDataList.Count > 0 && isFilterRepeate)
		{
			if (NoticeLogic.notifyDataList[NoticeLogic.notifyDataList.Count - 1].Str != text)
			{
				NoticeLogic.notifyDataList.Add(noticeData);
			}
		}
		else
		{
			NoticeLogic.notifyDataList.Add(noticeData);
		}
	}

	// Token: 0x06004C47 RID: 19527 RVA: 0x0019DA74 File Offset: 0x0019BC74
	public static void AddNotifyData2Client(bool isFilterRepeate, string data, bool isWarning = false, params object[] args)
	{
		string text = string.Empty;
		if (!string.IsNullOrEmpty(data))
		{
			char c = data.get_Chars(0);
			if (c != '#')
			{
				text = data;
			}
			else
			{
				text = StrDictionary.GetClientDictionaryString(data, args);
			}
		}
		NoticeLogic.NoticeData noticeData = new NoticeLogic.NoticeData(text, isWarning);
		if (NoticeLogic.notifyDataList.Count > 0 && isFilterRepeate)
		{
			if (NoticeLogic.notifyDataList[NoticeLogic.notifyDataList.Count - 1].Str != text)
			{
				NoticeLogic.notifyDataList.Add(noticeData);
			}
		}
		else
		{
			NoticeLogic.notifyDataList.Add(noticeData);
		}
	}

	// Token: 0x06004C48 RID: 19528 RVA: 0x0019DB10 File Offset: 0x0019BD10
	public static NoticeLogic.NoticeData GetNotifyData()
	{
		NoticeLogic.NoticeData result = null;
		if (NoticeLogic.notifyDataList.Count > 0)
		{
			result = NoticeLogic.notifyDataList[0];
			NoticeLogic.notifyDataList.RemoveAt(0);
		}
		return result;
	}

	// Token: 0x06004C49 RID: 19529 RVA: 0x0019DB48 File Offset: 0x0019BD48
	private void Start()
	{
		for (int i = 0; i < this.mNewsObject.Length; i++)
		{
			UnityVersionUtil.SetActiveRecursive(this.mNewsObject[i], false);
			this.mfNewTimes[i] = Time.realtimeSinceStartup;
			this.mObjTransform[i] = this.mNewsObject[i].transform;
		}
	}

	// Token: 0x06004C4A RID: 19530 RVA: 0x0019DBA0 File Offset: 0x0019BDA0
	private void AddNotice(NoticeLogic.NoticeData notice)
	{
		for (int i = 0; i < this.mObjTransform.Length; i++)
		{
			Vector3 localPosition = this.mObjTransform[i].localPosition;
			localPosition.y += 32f;
			if (localPosition.y > 214f)
			{
				localPosition.y = 150f;
				this.mNewLabels[i].text = notice.Str;
				UnityVersionUtil.SetActiveRecursive(this.mNewsObject[i], true);
				this.mfNewTimes[i] = Time.realtimeSinceStartup;
				this.mNewsBottomPic[i].color = new Color(0f, 0f, 0f, 0.7f);
			}
			this.mObjTransform[i].localPosition = localPosition;
		}
	}

	// Token: 0x06004C4B RID: 19531 RVA: 0x0019DC68 File Offset: 0x0019BE68
	private void Update()
	{
		NoticeLogic.NoticeData notifyData = NoticeLogic.GetNotifyData();
		if (notifyData != null)
		{
			this.AddNotice(notifyData);
		}
		for (int i = 0; i < this.mNewsObject.Length; i++)
		{
			if (UnityVersionUtil.IsActive(this.mNewsObject[i]) && Time.realtimeSinceStartup - this.mfNewTimes[i] > (float)this.mTimeMax)
			{
				this.mfNewTimes[i] = Time.realtimeSinceStartup;
				UnityVersionUtil.SetActiveRecursive(this.mNewsObject[i], false);
			}
		}
	}

	// Token: 0x040039FC RID: 14844
	private static List<NoticeLogic.NoticeData> notifyDataList = new List<NoticeLogic.NoticeData>();

	// Token: 0x040039FD RID: 14845
	public UILabel[] mNewLabels;

	// Token: 0x040039FE RID: 14846
	public GameObject[] mNewsObject;

	// Token: 0x040039FF RID: 14847
	public UISprite[] mNewsBottomPic;

	// Token: 0x04003A00 RID: 14848
	private Transform[] mObjTransform = new Transform[3];

	// Token: 0x04003A01 RID: 14849
	private float[] mfNewTimes = new float[3];

	// Token: 0x04003A02 RID: 14850
	private int mTimeMax = 5;

	// Token: 0x02000A38 RID: 2616
	public class NoticeData
	{
		// Token: 0x06004C4C RID: 19532 RVA: 0x0019DCE8 File Offset: 0x0019BEE8
		public NoticeData(string str, bool isWarning = false)
		{
			this.Str = str;
			this.IsWarning = isWarning;
		}

		// Token: 0x04003A03 RID: 14851
		public string Str;

		// Token: 0x04003A04 RID: 14852
		public bool IsWarning;
	}
}
