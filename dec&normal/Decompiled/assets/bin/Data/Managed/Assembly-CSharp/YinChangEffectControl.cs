using System;
using UnityEngine;

// Token: 0x020001D5 RID: 469
public class YinChangEffectControl : MonoBehaviour
{
	// Token: 0x060010E6 RID: 4326 RVA: 0x0006D8B8 File Offset: 0x0006BAB8
	public void InitEffect(EffInfoData effInfoData, float duration, Vector3 pos, Quaternion rotation, YinChangEffectControl.onFinishedDelegate dele = null)
	{
		this.effectId = effInfoData.ID;
		this.speed = 1f / duration;
		this.mEndTime = Time.time + duration;
		this.progress = 0f;
		this.mMaterial = base.GetComponent<Renderer>().sharedMaterial;
		this.meshFilter = base.GetComponent<MeshFilter>();
		this.mMaterial.SetFloat("_Progress", 0f);
		if (effInfoData != null)
		{
			if (effInfoData.AreaType == 1)
			{
				MeshCreate meshCreate = new SectorMeshCreate();
				float distance = (float)effInfoData.Param1 / 100f;
				this.meshFilter.sharedMesh = meshCreate.Create(distance, 360f);
			}
			else if (effInfoData.AreaType == 2)
			{
				MeshCreate meshCreate = new RectangleCreate();
				float distance2 = (float)effInfoData.Param1 / 100f;
				float parm = (float)effInfoData.Param2 / 100f;
				this.meshFilter.sharedMesh = meshCreate.Create(distance2, parm);
			}
			else if (effInfoData.AreaType == 3)
			{
				MeshCreate meshCreate = new SectorMeshCreate();
				float distance3 = (float)effInfoData.Param1 / 100f;
				float parm2 = (float)effInfoData.Param2;
				this.meshFilter.sharedMesh = meshCreate.Create(distance3, parm2);
			}
		}
		base.transform.position = pos;
		base.transform.rotation = rotation;
		this.onFinished = dele;
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x0006DA14 File Offset: 0x0006BC14
	private void Start()
	{
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x0006DA18 File Offset: 0x0006BC18
	private void Update()
	{
		if (this.mEndTime > 0f)
		{
			this.progress += Time.deltaTime * this.speed;
			if (this.progress > 1f)
			{
				this.progress = 1f;
			}
			this.mMaterial.SetFloat("_Progress", this.progress);
			if (this.progress >= 1f)
			{
				if (this.onFinished != null)
				{
					this.onFinished();
				}
				Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04001475 RID: 5237
	public string effectId = string.Empty;

	// Token: 0x04001476 RID: 5238
	private float mEndTime;

	// Token: 0x04001477 RID: 5239
	private MeshFilter meshFilter;

	// Token: 0x04001478 RID: 5240
	private float speed;

	// Token: 0x04001479 RID: 5241
	private float progress;

	// Token: 0x0400147A RID: 5242
	private Material mMaterial;

	// Token: 0x0400147B RID: 5243
	private YinChangEffectControl.onFinishedDelegate onFinished;

	// Token: 0x02000AC8 RID: 2760
	// (Invoke) Token: 0x06004FA9 RID: 20393
	public delegate void onFinishedDelegate();
}
