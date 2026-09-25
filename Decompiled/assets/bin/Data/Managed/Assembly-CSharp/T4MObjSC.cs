using System;
using UnityEngine;

// Token: 0x02000A83 RID: 2691
[ExecuteInEditMode]
public class T4MObjSC : MonoBehaviour
{
	// Token: 0x06004E58 RID: 20056 RVA: 0x001ABB5C File Offset: 0x001A9D5C
	public void Awake()
	{
		if (this.Master == 1)
		{
			if (this.PlayerCamera == null && Camera.main)
			{
				this.PlayerCamera = Camera.main.transform;
			}
			else if (this.PlayerCamera == null && !Camera.main)
			{
				Camera[] array = Object.FindObjectsOfType(typeof(Camera)) as Camera[];
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].GetComponent<AudioListener>())
					{
						this.PlayerCamera = array[i].transform;
					}
				}
			}
			if (this.enabledLayerCul)
			{
				this.distances[26] = this.CloseView;
				this.distances[27] = this.NormalView;
				this.distances[28] = this.FarView;
				this.distances[29] = this.BackGroundView;
				this.PlayerCamera.camera.layerCullDistances = this.distances;
			}
			if (this.EnabledLODSystem && this.ObjPosition.Length > 0 && this.Mode == 1)
			{
				if (this.ObjLodScript[0].gameObject != null)
				{
					if (this.LODbasedOnScript)
					{
						base.InvokeRepeating("LODScript", Random.Range(0f, this.Interval), this.Interval);
					}
					else
					{
						base.InvokeRepeating("LODLay", Random.Range(0f, this.Interval), this.Interval);
					}
				}
			}
			else if (this.EnabledLODSystem && this.ObjPosition.Length > 0 && this.Mode == 2 && this.ObjLodScript[0] != null)
			{
				for (int j = 0; j < this.ObjPosition.Length; j++)
				{
					if (this.ObjLodScript[j] != null)
					{
						if (this.LODbasedOnScript)
						{
							this.ObjLodScript[j].ActivateLODScrpt();
						}
						else
						{
							this.ObjLodScript[j].ActivateLODLay();
						}
					}
				}
			}
			if (this.enabledBillboard && this.BillboardPosition.Length > 0 && this.BillScript[0] != null)
			{
				if (this.BilBbasedOnScript)
				{
					base.InvokeRepeating("BillScrpt", Random.Range(0f, this.BillInterval), this.BillInterval);
				}
				else
				{
					base.InvokeRepeating("BillLay", Random.Range(0f, this.BillInterval), this.BillInterval);
				}
			}
		}
	}

	// Token: 0x06004E59 RID: 20057 RVA: 0x001ABE10 File Offset: 0x001AA010
	private void OnGUI()
	{
		if (!Application.isPlaying && this.Master == 1)
		{
			if (this.LayerCullPreview && this.enabledLayerCul)
			{
				GUI.color = Color.green;
				GUI.Label(new Rect(0f, 0f, 200f, 200f), "LayerCull Preview ON");
			}
			else
			{
				GUI.color = Color.red;
				GUI.Label(new Rect(0f, 0f, 200f, 200f), "LayerCull Preview OFF");
			}
			if (this.LODPreview && this.ObjPosition.Length > 0)
			{
				GUI.color = Color.green;
				GUI.Label(new Rect(0f, 20f, 200f, 200f), "LOD Preview ON");
			}
			else if (this.LODPreview && this.ObjPosition.Length == 0)
			{
				GUI.color = Color.red;
				GUI.Label(new Rect(0f, 20f, 200f, 200f), "Activate the LOD First");
			}
			else
			{
				GUI.color = Color.red;
				GUI.Label(new Rect(0f, 20f, 200f, 200f), "LOD Preview OFF");
			}
			if (this.BillboardPreview && this.BillboardPosition.Length > 0)
			{
				GUI.color = Color.green;
				GUI.Label(new Rect(0f, 40f, 200f, 200f), "Billboard Preview ON");
			}
			else if (this.BillboardPreview && this.BillboardPosition.Length == 0)
			{
				GUI.color = Color.red;
				GUI.Label(new Rect(0f, 40f, 200f, 200f), "Activate the Billboard First");
			}
			else
			{
				GUI.color = Color.red;
				GUI.Label(new Rect(0f, 40f, 200f, 200f), "Billboard Preview OFF");
			}
		}
	}

	// Token: 0x06004E5A RID: 20058 RVA: 0x001AC02C File Offset: 0x001AA22C
	private void LateUpdate()
	{
		if (this.ActiveWind)
		{
			Color color = this.Wind * Mathf.Sin(Time.realtimeSinceStartup * this.WindFrequency);
			color.a = this.Wind.w;
			Color color2 = this.Wind * Mathf.Sin(Time.realtimeSinceStartup * this.GrassWindFrequency);
			color2.a = this.Wind.w;
			Shader.SetGlobalColor("_Wind", color);
			Shader.SetGlobalColor("_GrassWind", color2);
			Shader.SetGlobalColor("_TranslucencyColor", this.TranslucencyColor);
			Shader.SetGlobalFloat("_TranslucencyViewDependency;", 0.65f);
		}
		if (this.PlayerCamera && !Application.isPlaying && this.Master == 1)
		{
			if (this.LayerCullPreview && this.enabledLayerCul)
			{
				this.distances[26] = this.CloseView;
				this.distances[27] = this.NormalView;
				this.distances[28] = this.FarView;
				this.distances[29] = this.BackGroundView;
				this.PlayerCamera.camera.layerCullDistances = this.distances;
			}
			else
			{
				this.distances[26] = this.PlayerCamera.camera.farClipPlane;
				this.distances[27] = this.PlayerCamera.camera.farClipPlane;
				this.distances[28] = this.PlayerCamera.camera.farClipPlane;
				this.distances[29] = this.PlayerCamera.camera.farClipPlane;
				this.PlayerCamera.camera.layerCullDistances = this.distances;
			}
			if (this.LODPreview)
			{
				if (this.EnabledLODSystem && this.ObjPosition.Length > 0 && this.Mode == 1)
				{
					if (this.ObjLodScript[0].gameObject != null)
					{
						if (this.LODbasedOnScript)
						{
							this.LODScript();
						}
						else
						{
							this.LODLay();
						}
					}
				}
				else if (this.EnabledLODSystem && this.ObjPosition.Length > 0 && this.Mode == 2 && this.ObjLodScript[0] != null)
				{
					for (int i = 0; i < this.ObjPosition.Length; i++)
					{
						if (this.ObjLodScript[i] != null)
						{
							if (this.LODbasedOnScript)
							{
								this.ObjLodScript[i].AFLODScrpt();
							}
							else
							{
								this.ObjLodScript[i].AFLODLay();
							}
						}
					}
				}
			}
			if (this.BillboardPreview && this.enabledBillboard && this.BillboardPosition.Length > 0 && this.BillScript[0] != null)
			{
				if (this.BilBbasedOnScript)
				{
					this.BillScrpt();
				}
				else
				{
					this.BillLay();
				}
			}
		}
	}

	// Token: 0x06004E5B RID: 20059 RVA: 0x001AC338 File Offset: 0x001AA538
	private void BillScrpt()
	{
		for (int i = 0; i < this.BillboardPosition.Length; i++)
		{
			if (Vector3.Distance(this.BillboardPosition[i], this.PlayerCamera.position) <= this.BillMaxViewDistance)
			{
				if (this.BillStatus[i] != 1)
				{
					this.BillScript[i].Render.enabled = true;
					this.BillStatus[i] = 1;
				}
				if (this.Axis == 0)
				{
					this.BillScript[i].Transf.LookAt(new Vector3(this.PlayerCamera.position.x, this.BillScript[i].Transf.position.y, this.PlayerCamera.position.z), Vector3.up);
				}
				else
				{
					this.BillScript[i].Transf.LookAt(this.PlayerCamera.position, Vector3.up);
				}
			}
			else if (this.BillStatus[i] != 0 && !this.BillScript[i].Render.enabled)
			{
				this.BillScript[i].Render.enabled = false;
				this.BillStatus[i] = 0;
			}
		}
	}

	// Token: 0x06004E5C RID: 20060 RVA: 0x001AC488 File Offset: 0x001AA688
	private void BillLay()
	{
		for (int i = 0; i < this.BillboardPosition.Length; i++)
		{
			int layer = this.BillScript[i].gameObject.layer;
			if (Vector3.Distance(this.BillboardPosition[i], this.PlayerCamera.position) <= this.distances[layer])
			{
				if (this.Axis == 0)
				{
					this.BillScript[i].Transf.LookAt(new Vector3(this.PlayerCamera.position.x, this.BillScript[i].Transf.position.y, this.PlayerCamera.position.z), Vector3.up);
				}
				else
				{
					this.BillScript[i].Transf.LookAt(this.PlayerCamera.position, Vector3.up);
				}
			}
		}
	}

	// Token: 0x06004E5D RID: 20061 RVA: 0x001AC580 File Offset: 0x001AA780
	private void LODScript()
	{
		if (this.OldPlayerPos == this.PlayerCamera.position)
		{
			return;
		}
		this.OldPlayerPos = this.PlayerCamera.position;
		for (int i = 0; i < this.ObjPosition.Length; i++)
		{
			float num = Vector3.Distance(new Vector3(this.ObjPosition[i].x, this.PlayerCamera.position.y, this.ObjPosition[i].z), this.PlayerCamera.position);
			if (num <= this.MaxViewDistance)
			{
				if (num < this.LOD2Start && this.ObjLodStatus[i] != 1)
				{
					Renderer lod = this.ObjLodScript[i].LOD2;
					bool flag = false;
					this.ObjLodScript[i].LOD3.enabled = flag;
					lod.enabled = flag;
					this.ObjLodScript[i].LOD1.enabled = true;
					this.ObjLodStatus[i] = 1;
				}
				else if (num >= this.LOD2Start && num < this.LOD3Start && this.ObjLodStatus[i] != 2)
				{
					Renderer lod2 = this.ObjLodScript[i].LOD1;
					bool flag = false;
					this.ObjLodScript[i].LOD3.enabled = flag;
					lod2.enabled = flag;
					this.ObjLodScript[i].LOD2.enabled = true;
					this.ObjLodStatus[i] = 2;
				}
				else if (num >= this.LOD3Start && this.ObjLodStatus[i] != 3)
				{
					Renderer lod3 = this.ObjLodScript[i].LOD2;
					bool flag = false;
					this.ObjLodScript[i].LOD1.enabled = flag;
					lod3.enabled = flag;
					this.ObjLodScript[i].LOD3.enabled = true;
					this.ObjLodStatus[i] = 3;
				}
			}
			else if (this.ObjLodStatus[i] != 0)
			{
				Renderer lod4 = this.ObjLodScript[i].LOD1;
				bool flag = false;
				this.ObjLodScript[i].LOD3.enabled = flag;
				flag = flag;
				this.ObjLodScript[i].LOD2.enabled = flag;
				lod4.enabled = flag;
				this.ObjLodStatus[i] = 0;
			}
		}
	}

	// Token: 0x06004E5E RID: 20062 RVA: 0x001AC7B4 File Offset: 0x001AA9B4
	private void LODLay()
	{
		if (this.OldPlayerPos == this.PlayerCamera.position)
		{
			return;
		}
		this.OldPlayerPos = this.PlayerCamera.position;
		for (int i = 0; i < this.ObjPosition.Length; i++)
		{
			float num = Vector3.Distance(new Vector3(this.ObjPosition[i].x, this.PlayerCamera.position.y, this.ObjPosition[i].z), this.PlayerCamera.position);
			int layer = this.ObjLodScript[i].gameObject.layer;
			if (num <= this.distances[layer] + 5f)
			{
				if (num < this.LOD2Start && this.ObjLodStatus[i] != 1)
				{
					Renderer lod = this.ObjLodScript[i].LOD2;
					bool enabled = false;
					this.ObjLodScript[i].LOD3.enabled = enabled;
					lod.enabled = enabled;
					this.ObjLodScript[i].LOD1.enabled = true;
					this.ObjLodStatus[i] = 1;
				}
				else if (num >= this.LOD2Start && num < this.LOD3Start && this.ObjLodStatus[i] != 2)
				{
					Renderer lod2 = this.ObjLodScript[i].LOD1;
					bool enabled = false;
					this.ObjLodScript[i].LOD3.enabled = enabled;
					lod2.enabled = enabled;
					this.ObjLodScript[i].LOD2.enabled = true;
					this.ObjLodStatus[i] = 2;
				}
				else if (num >= this.LOD3Start && this.ObjLodStatus[i] != 3)
				{
					Renderer lod3 = this.ObjLodScript[i].LOD2;
					bool enabled = false;
					this.ObjLodScript[i].LOD1.enabled = enabled;
					lod3.enabled = enabled;
					this.ObjLodScript[i].LOD3.enabled = true;
					this.ObjLodStatus[i] = 3;
				}
			}
		}
	}

	// Token: 0x04003CB9 RID: 15545
	[HideInInspector]
	public string ConvertType = string.Empty;

	// Token: 0x04003CBA RID: 15546
	[HideInInspector]
	public bool EnabledLODSystem = true;

	// Token: 0x04003CBB RID: 15547
	[HideInInspector]
	public Vector3[] ObjPosition;

	// Token: 0x04003CBC RID: 15548
	[HideInInspector]
	public T4MLodObjSC[] ObjLodScript;

	// Token: 0x04003CBD RID: 15549
	[HideInInspector]
	public int[] ObjLodStatus;

	// Token: 0x04003CBE RID: 15550
	[HideInInspector]
	public float MaxViewDistance = 60f;

	// Token: 0x04003CBF RID: 15551
	[HideInInspector]
	public float LOD2Start = 20f;

	// Token: 0x04003CC0 RID: 15552
	[HideInInspector]
	public float LOD3Start = 40f;

	// Token: 0x04003CC1 RID: 15553
	[HideInInspector]
	public float Interval = 0.5f;

	// Token: 0x04003CC2 RID: 15554
	[HideInInspector]
	public Transform PlayerCamera;

	// Token: 0x04003CC3 RID: 15555
	private Vector3 OldPlayerPos;

	// Token: 0x04003CC4 RID: 15556
	[HideInInspector]
	public int Mode = 1;

	// Token: 0x04003CC5 RID: 15557
	[HideInInspector]
	public int Master;

	// Token: 0x04003CC6 RID: 15558
	[HideInInspector]
	public bool enabledBillboard = true;

	// Token: 0x04003CC7 RID: 15559
	[HideInInspector]
	public Vector3[] BillboardPosition;

	// Token: 0x04003CC8 RID: 15560
	[HideInInspector]
	public float BillInterval = 0.05f;

	// Token: 0x04003CC9 RID: 15561
	[HideInInspector]
	public int[] BillStatus;

	// Token: 0x04003CCA RID: 15562
	[HideInInspector]
	public float BillMaxViewDistance = 30f;

	// Token: 0x04003CCB RID: 15563
	[HideInInspector]
	public T4MBillBObjSC[] BillScript;

	// Token: 0x04003CCC RID: 15564
	[HideInInspector]
	public bool enabledLayerCul = true;

	// Token: 0x04003CCD RID: 15565
	[HideInInspector]
	public float BackGroundView = 1000f;

	// Token: 0x04003CCE RID: 15566
	[HideInInspector]
	public float FarView = 200f;

	// Token: 0x04003CCF RID: 15567
	[HideInInspector]
	public float NormalView = 60f;

	// Token: 0x04003CD0 RID: 15568
	[HideInInspector]
	public float CloseView = 30f;

	// Token: 0x04003CD1 RID: 15569
	private float[] distances = new float[32];

	// Token: 0x04003CD2 RID: 15570
	[HideInInspector]
	public int Axis;

	// Token: 0x04003CD3 RID: 15571
	[HideInInspector]
	public bool LODbasedOnScript = true;

	// Token: 0x04003CD4 RID: 15572
	[HideInInspector]
	public bool BilBbasedOnScript = true;

	// Token: 0x04003CD5 RID: 15573
	[HideInInspector]
	public Material T4MMaterial;

	// Token: 0x04003CD6 RID: 15574
	[HideInInspector]
	public MeshFilter T4MMesh;

	// Token: 0x04003CD7 RID: 15575
	[HideInInspector]
	public Color TranslucencyColor = new Color(0.73f, 0.85f, 0.4f, 1f);

	// Token: 0x04003CD8 RID: 15576
	[HideInInspector]
	public Vector4 Wind = new Vector4(0.85f, 0.075f, 0.4f, 0.5f);

	// Token: 0x04003CD9 RID: 15577
	[HideInInspector]
	public float WindFrequency = 0.75f;

	// Token: 0x04003CDA RID: 15578
	[HideInInspector]
	public float GrassWindFrequency = 1.5f;

	// Token: 0x04003CDB RID: 15579
	[HideInInspector]
	public bool ActiveWind;

	// Token: 0x04003CDC RID: 15580
	public bool LayerCullPreview;

	// Token: 0x04003CDD RID: 15581
	public bool LODPreview;

	// Token: 0x04003CDE RID: 15582
	public bool BillboardPreview;
}
