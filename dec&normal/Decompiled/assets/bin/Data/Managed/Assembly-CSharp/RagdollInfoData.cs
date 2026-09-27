using System;
using UnityEngine;

// Token: 0x02000117 RID: 279
[Serializable]
public class RagdollInfoData
{
	// Token: 0x040008EF RID: 2287
	public string Path;

	// Token: 0x040008F0 RID: 2288
	public int ColliderType;

	// Token: 0x040008F1 RID: 2289
	public Vector3 ColliderCenter;

	// Token: 0x040008F2 RID: 2290
	public float ColliderRadius;

	// Token: 0x040008F3 RID: 2291
	public float Height;

	// Token: 0x040008F4 RID: 2292
	public int Direction;

	// Token: 0x040008F5 RID: 2293
	public Vector3 size;

	// Token: 0x040008F6 RID: 2294
	public float Mass;

	// Token: 0x040008F7 RID: 2295
	public float Drag;

	// Token: 0x040008F8 RID: 2296
	public float AngularDrag;

	// Token: 0x040008F9 RID: 2297
	public bool UseGravity;

	// Token: 0x040008FA RID: 2298
	public bool IsKinematic;

	// Token: 0x040008FB RID: 2299
	public bool HasJoint;

	// Token: 0x040008FC RID: 2300
	public string ConnectBodyPath;

	// Token: 0x040008FD RID: 2301
	public Vector3 Anchor;

	// Token: 0x040008FE RID: 2302
	public Vector3 Axis;

	// Token: 0x040008FF RID: 2303
	public Vector3 SwingAxis;

	// Token: 0x04000900 RID: 2304
	public MySoftJointLimit LowTwistLimit;

	// Token: 0x04000901 RID: 2305
	public MySoftJointLimit HighTwistLimit;

	// Token: 0x04000902 RID: 2306
	public MySoftJointLimit Swing1Limit;

	// Token: 0x04000903 RID: 2307
	public MySoftJointLimit Swing2Limit;
}
