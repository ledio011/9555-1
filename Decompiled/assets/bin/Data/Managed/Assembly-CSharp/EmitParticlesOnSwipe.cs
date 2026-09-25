using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
[RequireComponent(typeof(EmitParticles))]
public class EmitParticlesOnSwipe : MonoBehaviour
{
	// Token: 0x0600003F RID: 63 RVA: 0x00003678 File Offset: 0x00001878
	private void Awake()
	{
		this.emitter = base.GetComponent<EmitParticles>();
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003688 File Offset: 0x00001888
	private void OnSwipe(SwipeGesture gesture)
	{
		if (this.constrained)
		{
			switch (gesture.Direction)
			{
			case 1:
				this.emitter.EmitRight();
				goto IL_80;
			case 2:
				this.emitter.EmitLeft();
				goto IL_80;
			case 4:
				this.emitter.EmitUp();
				goto IL_80;
			case 8:
				this.emitter.EmitDown();
				goto IL_80;
			}
			return;
			IL_80:;
		}
		else
		{
			Vector3 dir;
			dir..ctor(gesture.Move.x, gesture.Move.y, 0f);
			this.emitter.Emit(dir);
		}
	}

	// Token: 0x0400003A RID: 58
	public bool constrained;

	// Token: 0x0400003B RID: 59
	private EmitParticles emitter;
}
