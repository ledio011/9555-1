using System;
using UnityEngine;

// Token: 0x020008AC RID: 2220
public class AreaCheckTool
{
	// Token: 0x06003BD3 RID: 15315 RVA: 0x00104DC4 File Offset: 0x00102FC4
	public static bool CheckInCircle(Vector3 targetPos, Vector3 centralPos, float range)
	{
		return VectorXZ.Distance(targetPos, centralPos) <= range;
	}

	// Token: 0x06003BD4 RID: 15316 RVA: 0x00104DE0 File Offset: 0x00102FE0
	public static bool CheckInCenterRectangle(Vector3 targetPos, Transform sourceTransform, float forwardDistance, float sidewardDistance)
	{
		Vector3 vector = sourceTransform.InverseTransformPoint(targetPos);
		return Mathf.Abs(vector.z) < forwardDistance / 2f && Mathf.Abs(vector.x) <= sidewardDistance / 2f;
	}

	// Token: 0x06003BD5 RID: 15317 RVA: 0x00104E28 File Offset: 0x00103028
	public static bool CheckInForwardRectangle(Vector3 targetPos, Transform sourceTransform, float forwardDistance, float sidewardDistance)
	{
		Vector3 vector = sourceTransform.InverseTransformPoint(targetPos);
		return vector.z > 0f && vector.z < forwardDistance && Mathf.Abs(vector.x) <= sidewardDistance / 2f;
	}

	// Token: 0x06003BD6 RID: 15318 RVA: 0x00104E78 File Offset: 0x00103078
	public static bool CheckInSector(Vector3 targetPos, Transform sourceTransform, float forwardDistance, float angle)
	{
		Vector3 vector = targetPos - sourceTransform.position;
		return vector.magnitude < forwardDistance && Vector3.Angle(vector, sourceTransform.forward) <= angle / 2f + 1f;
	}

	// Token: 0x06003BD7 RID: 15319 RVA: 0x00104EC4 File Offset: 0x001030C4
	public static bool CheckInRectangle(Vector2 targetPos, Vector2 leftUpPos, Vector2 leftDownPos, Vector2 rightDownPos, Vector2 rightUpPos)
	{
		Vector2 vector = leftDownPos - leftUpPos;
		Vector2 vector2 = rightDownPos - leftDownPos;
		Vector2 vector3 = rightUpPos - rightDownPos;
		Vector2 vector4 = leftUpPos - rightUpPos;
		Vector2 vector5 = targetPos - leftUpPos;
		Vector2 vector6 = targetPos - leftDownPos;
		Vector2 vector7 = targetPos - rightDownPos;
		Vector2 vector8 = targetPos - rightUpPos;
		return (vector.x * vector5.y - vector.y * vector5.x) * (vector3.x * vector7.y - vector3.y * vector7.x) > 0f && (vector2.x * vector6.y - vector2.y * vector6.x) * (vector4.x * vector8.y - vector4.y * vector8.x) > 0f;
	}

	// Token: 0x06003BD8 RID: 15320 RVA: 0x00104FB0 File Offset: 0x001031B0
	public static bool CheckInCircle(Vector2 targetPos, Vector2 sourcePos, float Range)
	{
		return (targetPos - sourcePos).sqrMagnitude <= Range * Range;
	}
}
