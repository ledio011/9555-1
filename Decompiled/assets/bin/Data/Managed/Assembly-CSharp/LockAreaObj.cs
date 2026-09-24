using System;
using UnityEngine;

// Token: 0x02000202 RID: 514
public class LockAreaObj : MonoBehaviour
{
	// Token: 0x06001173 RID: 4467 RVA: 0x00071104 File Offset: 0x0006F304
	public void SetCurMapLockData(SingleMapLockData mapData)
	{
		this.mCurMapData = mapData;
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x00071110 File Offset: 0x0006F310
	private void OnCollisionEnter(Collision other)
	{
		ObjMainPlayer component = other.gameObject.GetComponent<ObjMainPlayer>();
		if (component != null)
		{
			if (this.mCurMapData != null)
			{
				if (this.mCurMapData.UnlockLevel > 1000)
				{
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101814}", new object[]
					{
						this.mCurMapData.UnlockLevel
					}), true, false);
				}
				else
				{
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101813}", new object[]
					{
						this.mCurMapData.UnlockLevel
					}), true, false);
				}
			}
		}
		else if (other.gameObject.layer == 29 && this.mCurMapData != null)
		{
			if (this.mCurMapData.UnlockLevel > 1000)
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101814}", new object[]
				{
					this.mCurMapData.UnlockLevel
				}), true, false);
			}
			else
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101813}", new object[]
				{
					this.mCurMapData.UnlockLevel
				}), true, false);
			}
		}
	}

	// Token: 0x0400174B RID: 5963
	private SingleMapLockData mCurMapData;
}
