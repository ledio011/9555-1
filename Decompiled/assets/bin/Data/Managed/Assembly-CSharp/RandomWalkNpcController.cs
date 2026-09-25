using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A71 RID: 2673
public class RandomWalkNpcController : MonoBehaviour
{
	// Token: 0x06004DDF RID: 19935 RVA: 0x001A9E98 File Offset: 0x001A8098
	private void Awake()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			this.PointList.Add(base.transform.GetChild(i));
		}
		this.sqrRecycleDistance = this.RecycleDistance * this.RecycleDistance;
	}

	// Token: 0x06004DE0 RID: 19936 RVA: 0x001A9EEC File Offset: 0x001A80EC
	private void Update()
	{
		if (Time.time - this.lastCheckTime > this.CheckTimeInterval)
		{
			this.lastCheckTime = Time.time;
			this.CheckWalkNpc();
		}
	}

	// Token: 0x06004DE1 RID: 19937 RVA: 0x001A9F24 File Offset: 0x001A8124
	private void CheckWalkNpc()
	{
		if (this.mMainPlayer == null)
		{
			if (this.objManager == null)
			{
				this.objManager = Singleton<ObjManager>.Instance;
			}
			this.mMainPlayer = this.objManager.MainPlayer;
			if (this.mMainPlayer == null)
			{
				return;
			}
		}
		for (int i = this.mCurNpcList.Count - 1; i >= 0; i--)
		{
			if (this.mCurNpcList[i] == null)
			{
				this.mCurNpcList.RemoveAt(i);
			}
			else if (Vector3.SqrMagnitude(this.mCurNpcList[i].Position - this.mMainPlayer.Position) > this.sqrRecycleDistance)
			{
				ObjNPC objNPC = this.mCurNpcList[i];
				this.mCurNpcList.Remove(objNPC);
				this.objManager.RecycleNpc(objNPC);
			}
		}
		if (this.mCurNpcList.Count < this.MaxWalkNpcNum)
		{
			List<Transform> list = new List<Transform>();
			list.Clear();
			for (int j = 0; j < this.PointList.Count; j++)
			{
				if (Vector3.SqrMagnitude(this.PointList[j].position - this.mMainPlayer.Position) < this.sqrRecycleDistance)
				{
					list.Add(this.PointList[j]);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			ObjInitNpcData objInitNpcData = new ObjInitNpcData();
			objInitNpcData.mServerID = UUID.GenUUID();
			objInitNpcData.mPos = list[Random.Range(0, list.Count)].position;
			objInitNpcData.mDir = MathUtil.HeadingToVector3((float)Random.Range(0, 360));
			objInitNpcData.npcInfoData = DataManager.GetNpcDataByID(this.NpcIdList[Random.Range(0, this.NpcIdList.Count)]);
			this.objManager.CreateNPC(objInitNpcData, new ObjManager.OnGetNPC(this.OnCreateNpc), null);
		}
	}

	// Token: 0x06004DE2 RID: 19938 RVA: 0x001AA130 File Offset: 0x001A8330
	private void OnCreateNpc(ObjNPC objNpc)
	{
		this.mCurNpcList.Add(objNpc);
		objNpc.WalkMoveTo(this.PointList[Random.Range(0, this.PointList.Count)].position, 1f, new ObjCharacter.TargetArriveFinsh(this.OnArrivePoint));
	}

	// Token: 0x06004DE3 RID: 19939 RVA: 0x001AA184 File Offset: 0x001A8384
	private void OnArrivePoint(ObjCharacter objCha)
	{
		vp_Timer.In((float)Random.Range(0, 4), delegate()
		{
			if (objCha != null && UnityVersionUtil.IsActive(objCha.gameObject) && objCha.NavMeshAgent.enabled)
			{
				objCha.WalkMoveTo(this.PointList[Random.Range(0, this.PointList.Count)].position, 1f, new ObjCharacter.TargetArriveFinsh(this.OnArrivePoint));
			}
		}, null);
	}

	// Token: 0x04003C80 RID: 15488
	public List<string> NpcIdList;

	// Token: 0x04003C81 RID: 15489
	private List<Transform> PointList = new List<Transform>();

	// Token: 0x04003C82 RID: 15490
	public float RecycleDistance;

	// Token: 0x04003C83 RID: 15491
	public float CheckTimeInterval = 3f;

	// Token: 0x04003C84 RID: 15492
	public int MaxWalkNpcNum;

	// Token: 0x04003C85 RID: 15493
	private List<ObjNPC> mCurNpcList = new List<ObjNPC>();

	// Token: 0x04003C86 RID: 15494
	private ObjMainPlayer mMainPlayer;

	// Token: 0x04003C87 RID: 15495
	private float sqrRecycleDistance;

	// Token: 0x04003C88 RID: 15496
	private ObjManager objManager;

	// Token: 0x04003C89 RID: 15497
	private float lastCheckTime;
}
