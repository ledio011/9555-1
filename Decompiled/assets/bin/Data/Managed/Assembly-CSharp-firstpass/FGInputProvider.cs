using System;
using UnityEngine;

// Token: 0x0200003D RID: 61
public abstract class FGInputProvider : MonoBehaviour
{
	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000199 RID: 409
	public abstract int MaxSimultaneousFingers { get; }

	// Token: 0x0600019A RID: 410
	public abstract void GetInputState(int fingerIndex, out bool down, out Vector2 position);
}
