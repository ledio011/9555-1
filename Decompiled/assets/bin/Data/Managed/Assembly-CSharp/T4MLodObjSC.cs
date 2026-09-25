using System;
using UnityEngine;

// Token: 0x02000A82 RID: 2690
public class T4MLodObjSC : MonoBehaviour
{
	// Token: 0x06004E53 RID: 20051 RVA: 0x001AB620 File Offset: 0x001A9820
	public void ActivateLODScrpt()
	{
		if (this.Mode != 2)
		{
			return;
		}
		if (this.PlayerCamera == null)
		{
			this.PlayerCamera = Camera.main.transform;
		}
		base.InvokeRepeating("AFLODScrpt", Random.Range(0f, this.Interval), this.Interval);
	}

	// Token: 0x06004E54 RID: 20052 RVA: 0x001AB67C File Offset: 0x001A987C
	public void ActivateLODLay()
	{
		if (this.Mode != 2)
		{
			return;
		}
		if (this.PlayerCamera == null)
		{
			this.PlayerCamera = Camera.main.transform;
		}
		base.InvokeRepeating("AFLODLay", Random.Range(0f, this.Interval), this.Interval);
	}

	// Token: 0x06004E55 RID: 20053 RVA: 0x001AB6D8 File Offset: 0x001A98D8
	public void AFLODLay()
	{
		if (this.OldPlayerPos == this.PlayerCamera.position)
		{
			return;
		}
		this.OldPlayerPos = this.PlayerCamera.position;
		float num = Vector3.Distance(new Vector3(base.transform.position.x, this.PlayerCamera.position.y, base.transform.position.z), this.PlayerCamera.position);
		int layer = base.gameObject.layer;
		if (num <= this.PlayerCamera.camera.layerCullDistances[layer] + 5f)
		{
			if (num < this.LOD2Start && this.ObjLodStatus != 1)
			{
				Renderer lod = this.LOD3;
				bool enabled = false;
				this.LOD2.enabled = enabled;
				lod.enabled = enabled;
				this.LOD1.enabled = true;
				this.ObjLodStatus = 1;
			}
			else if (num >= this.LOD2Start && num < this.LOD3Start && this.ObjLodStatus != 2)
			{
				Renderer lod2 = this.LOD1;
				bool enabled = false;
				this.LOD3.enabled = enabled;
				lod2.enabled = enabled;
				this.LOD2.enabled = true;
				this.ObjLodStatus = 2;
			}
			else if (num >= this.LOD3Start && this.ObjLodStatus != 3)
			{
				Renderer lod3 = this.LOD1;
				bool enabled = false;
				this.LOD2.enabled = enabled;
				lod3.enabled = enabled;
				this.LOD3.enabled = true;
				this.ObjLodStatus = 3;
			}
		}
	}

	// Token: 0x06004E56 RID: 20054 RVA: 0x001AB87C File Offset: 0x001A9A7C
	public void AFLODScrpt()
	{
		if (this.OldPlayerPos == this.PlayerCamera.position)
		{
			return;
		}
		this.OldPlayerPos = this.PlayerCamera.position;
		float num = Vector3.Distance(new Vector3(base.transform.position.x, this.PlayerCamera.position.y, base.transform.position.z), this.PlayerCamera.position);
		if (num <= this.MaxViewDistance)
		{
			if (num < this.LOD2Start && this.ObjLodStatus != 1)
			{
				Renderer lod = this.LOD3;
				bool flag = false;
				this.LOD2.enabled = flag;
				lod.enabled = flag;
				this.LOD1.enabled = true;
				this.ObjLodStatus = 1;
			}
			else if (num >= this.LOD2Start && num < this.LOD3Start && this.ObjLodStatus != 2)
			{
				Renderer lod2 = this.LOD1;
				bool flag = false;
				this.LOD3.enabled = flag;
				lod2.enabled = flag;
				this.LOD2.enabled = true;
				this.ObjLodStatus = 2;
			}
			else if (num >= this.LOD3Start && this.ObjLodStatus != 3)
			{
				Renderer lod3 = this.LOD1;
				bool flag = false;
				this.LOD2.enabled = flag;
				lod3.enabled = flag;
				this.LOD3.enabled = true;
				this.ObjLodStatus = 3;
			}
		}
		else if (this.ObjLodStatus != 0)
		{
			Renderer lod4 = this.LOD1;
			bool flag = false;
			this.LOD3.enabled = flag;
			flag = flag;
			this.LOD2.enabled = flag;
			lod4.enabled = flag;
			this.ObjLodStatus = 0;
		}
	}

	// Token: 0x04003CAE RID: 15534
	[HideInInspector]
	public Renderer LOD1;

	// Token: 0x04003CAF RID: 15535
	[HideInInspector]
	public Renderer LOD2;

	// Token: 0x04003CB0 RID: 15536
	[HideInInspector]
	public Renderer LOD3;

	// Token: 0x04003CB1 RID: 15537
	[HideInInspector]
	public float Interval = 0.5f;

	// Token: 0x04003CB2 RID: 15538
	[HideInInspector]
	public Transform PlayerCamera;

	// Token: 0x04003CB3 RID: 15539
	[HideInInspector]
	public int Mode;

	// Token: 0x04003CB4 RID: 15540
	private Vector3 OldPlayerPos;

	// Token: 0x04003CB5 RID: 15541
	[HideInInspector]
	public int ObjLodStatus;

	// Token: 0x04003CB6 RID: 15542
	[HideInInspector]
	public float MaxViewDistance = 60f;

	// Token: 0x04003CB7 RID: 15543
	[HideInInspector]
	public float LOD2Start = 20f;

	// Token: 0x04003CB8 RID: 15544
	[HideInInspector]
	public float LOD3Start = 40f;
}
