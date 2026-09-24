using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
[ExecuteInEditMode]
public class WaterSimple : MonoBehaviour
{
	// Token: 0x06000275 RID: 629 RVA: 0x0000AC0C File Offset: 0x00008E0C
	private void Update()
	{
		if (!base.renderer)
		{
			return;
		}
		Material sharedMaterial = base.renderer.sharedMaterial;
		if (!sharedMaterial)
		{
			return;
		}
		Vector4 vector = sharedMaterial.GetVector("WaveSpeed");
		float @float = sharedMaterial.GetFloat("_WaveScale");
		float num = Time.time / 20f;
		Vector4 vector2 = vector * (num * @float);
		Vector4 vector3 = new Vector4(Mathf.Repeat(vector2.x, 1f), Mathf.Repeat(vector2.y, 1f), Mathf.Repeat(vector2.z, 1f), Mathf.Repeat(vector2.w, 1f));
		sharedMaterial.SetVector("_WaveOffset", vector3);
		Vector3 vector4 = new Vector3(1f / @float, 1f / @float, 1f);
		Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(vector3.x, vector3.y, 0f), Quaternion.identity, vector4);
		sharedMaterial.SetMatrix("_WaveMatrix", matrix);
		matrix = Matrix4x4.TRS(new Vector3(vector3.z, vector3.w, 0f), Quaternion.identity, vector4 * 0.45f);
		sharedMaterial.SetMatrix("_WaveMatrix2", matrix);
	}
}
