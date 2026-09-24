using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000033 RID: 51
	[AddComponentMenu("UI/Button", 30)]
	public class Button : Selectable, IEventSystemHandler, IPointerClickHandler, ISubmitHandler
	{
		// Token: 0x0600013F RID: 319 RVA: 0x000054B0 File Offset: 0x000036B0
		protected Button()
		{
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000054C4 File Offset: 0x000036C4
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000054CC File Offset: 0x000036CC
		public Button.ButtonClickedEvent onClick
		{
			get
			{
				return this.m_OnClick;
			}
			set
			{
				this.m_OnClick = value;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000054D8 File Offset: 0x000036D8
		private void Press()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.m_OnClick.Invoke();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000054FC File Offset: 0x000036FC
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.Press();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005510 File Offset: 0x00003710
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Press();
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.DoStateTransition(Selectable.SelectionState.Pressed, false);
			base.StartCoroutine(this.OnFinishSubmit());
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005544 File Offset: 0x00003744
		private IEnumerator OnFinishSubmit()
		{
			float fadeTime = base.colors.fadeDuration;
			float elapsedTime = 0f;
			while (elapsedTime < fadeTime)
			{
				elapsedTime += Time.unscaledDeltaTime;
				yield return null;
			}
			this.DoStateTransition(base.currentSelectionState, false);
			yield break;
		}

		// Token: 0x0400009B RID: 155
		[FormerlySerializedAs("onClick")]
		[SerializeField]
		private Button.ButtonClickedEvent m_OnClick = new Button.ButtonClickedEvent();

		// Token: 0x02000035 RID: 53
		[Serializable]
		public class ButtonClickedEvent : UnityEvent
		{
		}
	}
}
