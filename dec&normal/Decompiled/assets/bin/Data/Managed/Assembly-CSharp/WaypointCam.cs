using System;
using UnityEngine;

// Token: 0x02000A87 RID: 2695
public class WaypointCam : MonoBehaviour
{
	// Token: 0x06004E64 RID: 20068 RVA: 0x001ACBB0 File Offset: 0x001AADB0
	private void Awake()
	{
		WaypointCam.waypoints = base.gameObject.GetComponentsInChildren<Transform>();
	}

	// Token: 0x06004E65 RID: 20069 RVA: 0x001ACBC4 File Offset: 0x001AADC4
	private void OnDrawGizmos()
	{
		if (this.draw)
		{
			WaypointCam.waypoints = base.gameObject.GetComponentsInChildren<Transform>();
			foreach (Transform transform in WaypointCam.waypoints)
			{
				Gizmos.color = this.WaypointsColor;
				Gizmos.DrawSphere(transform.position, 1f);
				Gizmos.color = this.WaypointsColor;
				Gizmos.DrawWireSphere(transform.position, 6f);
			}
		}
	}

	// Token: 0x04003CE3 RID: 15587
	public Color WaypointsColor = new Color(1f, 0f, 0f, 1f);

	// Token: 0x04003CE4 RID: 15588
	public bool draw = true;

	// Token: 0x04003CE5 RID: 15589
	public static Transform[] waypoints;
}
