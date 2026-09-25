using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200008F RID: 143
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Toggle", 35)]
	public class Toggle : Selectable, IEventSystemHandler, IPointerClickHandler, ISubmitHandler, ICanvasElement
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00013E54 File Offset: 0x00012054
		protected Toggle()
		{
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00013E70 File Offset: 0x00012070
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x00013E78 File Offset: 0x00012078
		public ToggleGroup group
		{
			get
			{
				return this.m_Group;
			}
			set
			{
				this.m_Group = value;
				this.SetToggleGroup(this.m_Group, true);
				this.PlayEffect(true);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00013E98 File Offset: 0x00012098
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00013E9C File Offset: 0x0001209C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetToggleGroup(this.m_Group, false);
			this.PlayEffect(true);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00013EB8 File Offset: 0x000120B8
		protected override void OnDisable()
		{
			this.SetToggleGroup(null, false);
			base.OnDisable();
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00013EC8 File Offset: 0x000120C8
		private void SetToggleGroup(ToggleGroup newGroup, bool setMemberValue)
		{
			ToggleGroup group = this.m_Group;
			if (this.m_Group != null)
			{
				this.m_Group.UnregisterToggle(this);
			}
			if (setMemberValue)
			{
				this.m_Group = newGroup;
			}
			if (this.m_Group != null && this.IsActive())
			{
				this.m_Group.RegisterToggle(this);
			}
			if (newGroup != null && newGroup != group && this.isOn && this.IsActive())
			{
				this.m_Group.NotifyToggleOn(this);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00013F68 File Offset: 0x00012168
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00013F70 File Offset: 0x00012170
		public bool isOn
		{
			get
			{
				return this.m_IsOn;
			}
			set
			{
				this.Set(value);
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00013F7C File Offset: 0x0001217C
		private void Set(bool value)
		{
			this.Set(value, true);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00013F88 File Offset: 0x00012188
		private void Set(bool value, bool sendCallback)
		{
			if (this.m_IsOn == value)
			{
				return;
			}
			this.m_IsOn = value;
			if (this.m_Group != null && this.IsActive() && (this.m_IsOn || (!this.m_Group.AnyTogglesOn() && !this.m_Group.allowSwitchOff)))
			{
				this.m_IsOn = true;
				this.m_Group.NotifyToggleOn(this);
			}
			this.PlayEffect(this.toggleTransition == Toggle.ToggleTransition.None);
			if (sendCallback)
			{
				this.onValueChanged.Invoke(this.m_IsOn);
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001402C File Offset: 0x0001222C
		private void PlayEffect(bool instant)
		{
			if (this.graphic == null)
			{
				return;
			}
			this.graphic.CrossFadeAlpha((!this.m_IsOn) ? 0f : 1f, (!instant) ? 0.1f : 0f, true);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00014088 File Offset: 0x00012288
		protected override void Start()
		{
			this.PlayEffect(true);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00014094 File Offset: 0x00012294
		private void InternalToggle()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.isOn = !this.isOn;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000140BC File Offset: 0x000122BC
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.InternalToggle();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000140D0 File Offset: 0x000122D0
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.InternalToggle();
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000140D8 File Offset: 0x000122D8
		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000140E0 File Offset: 0x000122E0
		virtual Transform get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400024E RID: 590
		public Toggle.ToggleTransition toggleTransition = Toggle.ToggleTransition.Fade;

		// Token: 0x0400024F RID: 591
		public Graphic graphic;

		// Token: 0x04000250 RID: 592
		[SerializeField]
		private ToggleGroup m_Group;

		// Token: 0x04000251 RID: 593
		public Toggle.ToggleEvent onValueChanged = new Toggle.ToggleEvent();

		// Token: 0x04000252 RID: 594
		[SerializeField]
		[FormerlySerializedAs("m_IsActive")]
		[Tooltip("Is the toggle currently on or off?")]
		private bool m_IsOn;

		// Token: 0x02000090 RID: 144
		[Serializable]
		public class ToggleEvent : UnityEvent<bool>
		{
		}

		// Token: 0x02000091 RID: 145
		public enum ToggleTransition
		{
			// Token: 0x04000254 RID: 596
			None,
			// Token: 0x04000255 RID: 597
			Fade
		}
	}
}
