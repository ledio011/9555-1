using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class EmitParticles : MonoBehaviour
{
	// Token: 0x0600004C RID: 76 RVA: 0x000037FC File Offset: 0x000019FC
	public void Emit()
	{
		this.emitter.Emit();
	}

	// Token: 0x0600004D RID: 77 RVA: 0x0000380C File Offset: 0x00001A0C
	public void Emit(Vector3 dir)
	{
		this.Emit(Quaternion.LookRotation(dir));
	}

	// Token: 0x0600004E RID: 78 RVA: 0x0000381C File Offset: 0x00001A1C
	public void Emit(Quaternion rot)
	{
		this.emitter.transform.rotation = rot;
		this.Emit();
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00003838 File Offset: 0x00001A38
	public void EmitLeft()
	{
		this.Emit(this.left.rotation);
	}

	// Token: 0x06000050 RID: 80 RVA: 0x0000384C File Offset: 0x00001A4C
	public void EmitRight()
	{
		this.Emit(this.right.rotation);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003860 File Offset: 0x00001A60
	public void EmitUp()
	{
		this.Emit(this.up.rotation);
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00003874 File Offset: 0x00001A74
	public void EmitDown()
	{
		this.Emit(this.down.rotation);
	}

	// Token: 0x04000040 RID: 64
	public ParticleEmitter emitter;

	// Token: 0x04000041 RID: 65
	public Transform left;

	// Token: 0x04000042 RID: 66
	public Transform right;

	// Token: 0x04000043 RID: 67
	public Transform up;

	// Token: 0x04000044 RID: 68
	public Transform down;
}
