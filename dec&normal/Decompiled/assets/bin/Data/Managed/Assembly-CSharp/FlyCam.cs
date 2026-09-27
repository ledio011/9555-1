using System;
using UnityEngine;

// Token: 0x02000A86 RID: 2694
public class FlyCam : MonoBehaviour
{
	// Token: 0x06004E62 RID: 20066 RVA: 0x001AC9F0 File Offset: 0x001AABF0
	private void Update()
	{
		if (WaypointCam.waypoints.Length > 0)
		{
			Vector3 vector = base.transform.InverseTransformPoint(new Vector3(WaypointCam.waypoints[this.currentWaypoint].position.x, WaypointCam.waypoints[this.currentWaypoint].position.y, WaypointCam.waypoints[this.currentWaypoint].position.z));
			Vector3 vector2;
			vector2..ctor(WaypointCam.waypoints[this.currentWaypoint].position.x, WaypointCam.waypoints[this.currentWaypoint].position.y, WaypointCam.waypoints[this.currentWaypoint].position.z);
			Quaternion quaternion = Quaternion.LookRotation(vector2 - base.transform.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, quaternion, Time.deltaTime * this.rotateSpeed);
			Vector3 vector3 = base.transform.TransformDirection(Vector3.forward);
			base.transform.position += vector3 * this.moveSpeed * Time.deltaTime;
			if (vector.magnitude < this.magnitudeMax)
			{
				this.currentWaypoint++;
				if (this.currentWaypoint >= WaypointCam.waypoints.Length)
				{
					this.currentWaypoint = 0;
				}
			}
		}
	}

	// Token: 0x04003CDF RID: 15583
	private int currentWaypoint;

	// Token: 0x04003CE0 RID: 15584
	public float rotateSpeed = 1f;

	// Token: 0x04003CE1 RID: 15585
	public float moveSpeed = 10f;

	// Token: 0x04003CE2 RID: 15586
	public float magnitudeMax = 10f;
}
