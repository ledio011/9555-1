using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public abstract class GestureRecognizerDelegate : MonoBehaviour
{
	// Token: 0x0600007B RID: 123
	public abstract bool CanBegin(Gesture gesture, FingerGestures.IFingerList touches);
}
