using System;
using System.Collections.Generic;

// Token: 0x02000031 RID: 49
public class PointCloudGesture : DiscreteGesture
{
	// Token: 0x040000CD RID: 205
	public List<PointCloudRegognizer.Point> RawPoints = new List<PointCloudRegognizer.Point>(64);

	// Token: 0x040000CE RID: 206
	public List<PointCloudRegognizer.Point> NormalizedPoints = new List<PointCloudRegognizer.Point>(64);

	// Token: 0x040000CF RID: 207
	public PointCloudGestureTemplate RecognizedTemplate;

	// Token: 0x040000D0 RID: 208
	public float MatchDistance;

	// Token: 0x040000D1 RID: 209
	public float MatchScore;
}
