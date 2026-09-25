using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
[RequireComponent(typeof(SampleUI))]
public class SampleBase : MonoBehaviour
{
	// Token: 0x06000057 RID: 87 RVA: 0x000038C8 File Offset: 0x00001AC8
	protected virtual string GetHelpText()
	{
		return string.Empty;
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000058 RID: 88 RVA: 0x000038D0 File Offset: 0x00001AD0
	public SampleUI UI
	{
		get
		{
			return this.ui;
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x000038D8 File Offset: 0x00001AD8
	protected virtual void Awake()
	{
		this.ui = base.GetComponent<SampleUI>();
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000038E8 File Offset: 0x00001AE8
	protected virtual void Start()
	{
		this.ui.helpText = this.GetHelpText();
	}

	// Token: 0x0600005B RID: 91 RVA: 0x000038FC File Offset: 0x00001AFC
	public static Vector3 GetWorldPos(Vector2 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		float num = -ray.origin.z / ray.direction.z;
		return ray.GetPoint(num);
	}

	// Token: 0x04000047 RID: 71
	private SampleUI ui;
}
