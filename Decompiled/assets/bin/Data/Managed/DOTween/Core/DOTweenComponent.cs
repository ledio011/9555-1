using System;
using System.Collections;
using UnityEngine;

namespace DG.Tweening.Core
{
	// Token: 0x0200000D RID: 13
	[AddComponentMenu("")]
	public class DOTweenComponent : MonoBehaviour, IDOTweenInit
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000021A8 File Offset: 0x000003A8
		private void Awake()
		{
			this.inspectorUpdater = 0;
			this._unscaledTime = Time.realtimeSinceStartup;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021BC File Offset: 0x000003BC
		private void Start()
		{
			if (DOTween.instance != this)
			{
				this._duplicateToDestroy = true;
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021E0 File Offset: 0x000003E0
		private void Update()
		{
			this._unscaledDeltaTime = Time.realtimeSinceStartup - this._unscaledTime;
			if (DOTween.useSmoothDeltaTime && this._unscaledDeltaTime > DOTween.maxSmoothUnscaledTime)
			{
				this._unscaledDeltaTime = DOTween.maxSmoothUnscaledTime;
			}
			if (TweenManager.hasActiveDefaultTweens)
			{
				TweenManager.Update(UpdateType.Normal, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * DOTween.timeScale, this._unscaledDeltaTime * DOTween.timeScale);
			}
			this._unscaledTime = Time.realtimeSinceStartup;
			if (DOTween.isUnityEditor)
			{
				this.inspectorUpdater++;
				if (DOTween.showUnityEditorReport && TweenManager.hasActiveTweens)
				{
					if (TweenManager.totActiveTweeners > DOTween.maxActiveTweenersReached)
					{
						DOTween.maxActiveTweenersReached = TweenManager.totActiveTweeners;
					}
					if (TweenManager.totActiveSequences > DOTween.maxActiveSequencesReached)
					{
						DOTween.maxActiveSequencesReached = TweenManager.totActiveSequences;
					}
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022AC File Offset: 0x000004AC
		private void LateUpdate()
		{
			if (TweenManager.hasActiveLateTweens)
			{
				TweenManager.Update(UpdateType.Late, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * DOTween.timeScale, this._unscaledDeltaTime * DOTween.timeScale);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022E0 File Offset: 0x000004E0
		private void FixedUpdate()
		{
			if (TweenManager.hasActiveFixedTweens && Time.timeScale > 0f)
			{
				TweenManager.Update(UpdateType.Fixed, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * DOTween.timeScale, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) / Time.timeScale * DOTween.timeScale);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002340 File Offset: 0x00000540
		private void OnDrawGizmos()
		{
			if (!DOTween.drawGizmos || !DOTween.isUnityEditor)
			{
				return;
			}
			int count = DOTween.GizmosDelegates.Count;
			if (count == 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				DOTween.GizmosDelegates[i]();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002388 File Offset: 0x00000588
		private void OnDestroy()
		{
			if (this._duplicateToDestroy)
			{
				return;
			}
			if (DOTween.showUnityEditorReport)
			{
				Debugger.LogReport(string.Concat(new object[]
				{
					"REPORT > Max overall simultaneous active Tweeners/Sequences: ",
					DOTween.maxActiveTweenersReached,
					"/",
					DOTween.maxActiveSequencesReached
				}));
			}
			if (DOTween.instance == this)
			{
				DOTween.instance = null;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023F4 File Offset: 0x000005F4
		private void OnApplicationQuit()
		{
			DOTween.isQuitting = true;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023FC File Offset: 0x000005FC
		public IDOTweenInit SetCapacity(int tweenersCapacity, int sequencesCapacity)
		{
			TweenManager.SetCapacities(tweenersCapacity, sequencesCapacity);
			return this;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002408 File Offset: 0x00000608
		internal IEnumerator WaitForCompletion(Tween t)
		{
			while (t.active && !t.isComplete)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002418 File Offset: 0x00000618
		internal IEnumerator WaitForRewind(Tween t)
		{
			while (t.active && (!t.playedOnce || t.position * (float)(t.completedLoops + 1) > 0f))
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002428 File Offset: 0x00000628
		internal IEnumerator WaitForKill(Tween t)
		{
			while (t.active)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002438 File Offset: 0x00000638
		internal IEnumerator WaitForElapsedLoops(Tween t, int elapsedLoops)
		{
			while (t.active && t.completedLoops < elapsedLoops)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002450 File Offset: 0x00000650
		internal IEnumerator WaitForPosition(Tween t, float position)
		{
			while (t.active && t.position * (float)(t.completedLoops + 1) < position)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002468 File Offset: 0x00000668
		internal IEnumerator WaitForStart(Tween t)
		{
			while (t.active && !t.playedOnce)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002478 File Offset: 0x00000678
		internal static void Create()
		{
			if (DOTween.instance != null)
			{
				return;
			}
			GameObject gameObject = new GameObject("[DOTween]");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			DOTween.instance = gameObject.AddComponent<DOTweenComponent>();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024A4 File Offset: 0x000006A4
		internal static void DestroyInstance()
		{
			if (DOTween.instance != null)
			{
				UnityEngine.Object.Destroy(DOTween.instance.gameObject);
			}
			DOTween.instance = null;
		}

		// Token: 0x04000017 RID: 23
		public int inspectorUpdater;

		// Token: 0x04000018 RID: 24
		private float _unscaledTime;

		// Token: 0x04000019 RID: 25
		private float _unscaledDeltaTime;

		// Token: 0x0400001A RID: 26
		private bool _duplicateToDestroy;
	}
}
