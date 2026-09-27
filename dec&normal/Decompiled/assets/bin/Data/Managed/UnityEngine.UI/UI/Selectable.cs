using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000082 RID: 130
	[DisallowMultipleComponent]
	[SelectionBase]
	[AddComponentMenu("UI/Selectable", 70)]
	[ExecuteInEditMode]
	public class Selectable : UIBehaviour, IDeselectHandler, IEventSystemHandler, IMoveHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, ISelectHandler
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x00011858 File Offset: 0x0000FA58
		protected Selectable()
		{
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x000118B8 File Offset: 0x0000FAB8
		public static List<Selectable> allSelectables
		{
			get
			{
				return Selectable.s_List;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000118C0 File Offset: 0x0000FAC0
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x000118C8 File Offset: 0x0000FAC8
		public Navigation navigation
		{
			get
			{
				return this.m_Navigation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Navigation>(ref this.m_Navigation, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x000118E4 File Offset: 0x0000FAE4
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x000118EC File Offset: 0x0000FAEC
		public Selectable.Transition transition
		{
			get
			{
				return this.m_Transition;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Selectable.Transition>(ref this.m_Transition, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00011908 File Offset: 0x0000FB08
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00011910 File Offset: 0x0000FB10
		public ColorBlock colors
		{
			get
			{
				return this.m_Colors;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ColorBlock>(ref this.m_Colors, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0001192C File Offset: 0x0000FB2C
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x00011934 File Offset: 0x0000FB34
		public SpriteState spriteState
		{
			get
			{
				return this.m_SpriteState;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<SpriteState>(ref this.m_SpriteState, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00011950 File Offset: 0x0000FB50
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x00011958 File Offset: 0x0000FB58
		public AnimationTriggers animationTriggers
		{
			get
			{
				return this.m_AnimationTriggers;
			}
			set
			{
				if (SetPropertyUtility.SetClass<AnimationTriggers>(ref this.m_AnimationTriggers, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00011974 File Offset: 0x0000FB74
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0001197C File Offset: 0x0000FB7C
		public Graphic targetGraphic
		{
			get
			{
				return this.m_TargetGraphic;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Graphic>(ref this.m_TargetGraphic, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00011998 File Offset: 0x0000FB98
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x000119A0 File Offset: 0x0000FBA0
		public bool interactable
		{
			get
			{
				return this.m_Interactable;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_Interactable, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000119BC File Offset: 0x0000FBBC
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x000119C4 File Offset: 0x0000FBC4
		private bool isPointerInside { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x000119D0 File Offset: 0x0000FBD0
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x000119D8 File Offset: 0x0000FBD8
		private bool isPointerDown { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x000119E4 File Offset: 0x0000FBE4
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x000119EC File Offset: 0x0000FBEC
		private bool hasSelection { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x000119F8 File Offset: 0x0000FBF8
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x00011A08 File Offset: 0x0000FC08
		public Image image
		{
			get
			{
				return this.m_TargetGraphic as Image;
			}
			set
			{
				this.m_TargetGraphic = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00011A14 File Offset: 0x0000FC14
		public Animator animator
		{
			get
			{
				return base.GetComponent<Animator>();
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00011A1C File Offset: 0x0000FC1C
		protected override void Awake()
		{
			if (this.m_TargetGraphic == null)
			{
				this.m_TargetGraphic = base.GetComponent<Graphic>();
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00011A3C File Offset: 0x0000FC3C
		protected override void OnCanvasGroupChanged()
		{
			bool flag = true;
			Transform transform = base.transform;
			while (transform != null)
			{
				transform.GetComponents<CanvasGroup>(this.m_CanvasGroupCache);
				bool flag2 = false;
				for (int i = 0; i < this.m_CanvasGroupCache.Count; i++)
				{
					if (!this.m_CanvasGroupCache[i].interactable)
					{
						flag = false;
						flag2 = true;
					}
					if (this.m_CanvasGroupCache[i].ignoreParentGroups)
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					break;
				}
				transform = transform.parent;
			}
			if (flag != this.m_GroupsAllowInteraction)
			{
				this.m_GroupsAllowInteraction = flag;
				this.OnSetProperty();
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00011AEC File Offset: 0x0000FCEC
		public virtual bool IsInteractable()
		{
			return this.m_GroupsAllowInteraction && this.m_Interactable;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00011B04 File Offset: 0x0000FD04
		protected override void OnDidApplyAnimationProperties()
		{
			this.OnSetProperty();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00011B0C File Offset: 0x0000FD0C
		protected override void OnEnable()
		{
			base.OnEnable();
			Selectable.s_List.Add(this);
			Selectable.SelectionState currentSelectionState = Selectable.SelectionState.Normal;
			if (this.hasSelection)
			{
				currentSelectionState = Selectable.SelectionState.Highlighted;
			}
			this.m_CurrentSelectionState = currentSelectionState;
			this.InternalEvaluateAndTransitionToSelectionState(true);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00011B48 File Offset: 0x0000FD48
		private void OnSetProperty()
		{
			this.InternalEvaluateAndTransitionToSelectionState(false);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00011B54 File Offset: 0x0000FD54
		protected override void OnDisable()
		{
			Selectable.s_List.Remove(this);
			this.InstantClearState();
			base.OnDisable();
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00011B70 File Offset: 0x0000FD70
		protected Selectable.SelectionState currentSelectionState
		{
			get
			{
				return this.m_CurrentSelectionState;
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00011B78 File Offset: 0x0000FD78
		protected virtual void InstantClearState()
		{
			string normalTrigger = this.m_AnimationTriggers.normalTrigger;
			this.isPointerInside = false;
			this.isPointerDown = false;
			this.hasSelection = false;
			switch (this.m_Transition)
			{
			case Selectable.Transition.ColorTint:
				this.StartColorTween(Color.white, true);
				break;
			case Selectable.Transition.SpriteSwap:
				this.DoSpriteSwap(null);
				break;
			case Selectable.Transition.Animation:
				this.TriggerAnimation(normalTrigger);
				break;
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		protected virtual void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			Color a;
			Sprite newSprite;
			string triggername;
			switch (state)
			{
			case Selectable.SelectionState.Normal:
				a = this.m_Colors.normalColor;
				newSprite = null;
				triggername = this.m_AnimationTriggers.normalTrigger;
				break;
			case Selectable.SelectionState.Highlighted:
				a = this.m_Colors.highlightedColor;
				newSprite = this.m_SpriteState.highlightedSprite;
				triggername = this.m_AnimationTriggers.highlightedTrigger;
				break;
			case Selectable.SelectionState.Pressed:
				a = this.m_Colors.pressedColor;
				newSprite = this.m_SpriteState.pressedSprite;
				triggername = this.m_AnimationTriggers.pressedTrigger;
				break;
			case Selectable.SelectionState.Disabled:
				a = this.m_Colors.disabledColor;
				newSprite = this.m_SpriteState.disabledSprite;
				triggername = this.m_AnimationTriggers.disabledTrigger;
				break;
			default:
				a = Color.black;
				newSprite = null;
				triggername = string.Empty;
				break;
			}
			if (base.gameObject.activeInHierarchy)
			{
				switch (this.m_Transition)
				{
				case Selectable.Transition.ColorTint:
					this.StartColorTween(a * this.m_Colors.colorMultiplier, instant);
					break;
				case Selectable.Transition.SpriteSwap:
					this.DoSpriteSwap(newSprite);
					break;
				case Selectable.Transition.Animation:
					this.TriggerAnimation(triggername);
					break;
				}
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00011D30 File Offset: 0x0000FF30
		public Selectable FindSelectable(Vector3 dir)
		{
			dir = dir.normalized;
			Vector3 v = Quaternion.Inverse(base.transform.rotation) * dir;
			Vector3 b = base.transform.TransformPoint(Selectable.GetPointOnRectEdge(base.transform as RectTransform, v));
			float num = float.NegativeInfinity;
			Selectable result = null;
			for (int i = 0; i < Selectable.s_List.Count; i++)
			{
				Selectable selectable = Selectable.s_List[i];
				if (!(selectable == this) && !(selectable == null))
				{
					if (selectable.IsInteractable() && selectable.navigation.mode != Navigation.Mode.None)
					{
						RectTransform rectTransform = selectable.transform as RectTransform;
						Vector3 position = (!(rectTransform != null)) ? Vector3.zero : rectTransform.rect.center;
						Vector3 rhs = selectable.transform.TransformPoint(position) - b;
						float num2 = Vector3.Dot(dir, rhs);
						if (num2 > 0f)
						{
							float num3 = num2 / rhs.sqrMagnitude;
							if (num3 > num)
							{
								num = num3;
								result = selectable;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00011E84 File Offset: 0x00010084
		private static Vector3 GetPointOnRectEdge(RectTransform rect, Vector2 dir)
		{
			if (rect == null)
			{
				return Vector3.zero;
			}
			if (dir != Vector2.zero)
			{
				dir /= Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
			}
			dir = rect.rect.center + Vector2.Scale(rect.rect.size, dir * 0.5f);
			return dir;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00011F14 File Offset: 0x00010114
		private void Navigate(AxisEventData eventData, Selectable sel)
		{
			if (sel != null && sel.IsActive())
			{
				eventData.selectedObject = sel.gameObject;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00011F3C File Offset: 0x0001013C
		public virtual Selectable FindSelectableOnLeft()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnLeft;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.left);
			}
			return null;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00011F98 File Offset: 0x00010198
		public virtual Selectable FindSelectableOnRight()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnRight;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.right);
			}
			return null;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00011FF4 File Offset: 0x000101F4
		public virtual Selectable FindSelectableOnUp()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnUp;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.up);
			}
			return null;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00012050 File Offset: 0x00010250
		public virtual Selectable FindSelectableOnDown()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnDown;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.down);
			}
			return null;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000120AC File Offset: 0x000102AC
		public virtual void OnMove(AxisEventData eventData)
		{
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				this.Navigate(eventData, this.FindSelectableOnLeft());
				break;
			case MoveDirection.Up:
				this.Navigate(eventData, this.FindSelectableOnUp());
				break;
			case MoveDirection.Right:
				this.Navigate(eventData, this.FindSelectableOnRight());
				break;
			case MoveDirection.Down:
				this.Navigate(eventData, this.FindSelectableOnDown());
				break;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00012124 File Offset: 0x00010324
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (this.m_TargetGraphic == null)
			{
				return;
			}
			this.m_TargetGraphic.CrossFadeColor(targetColor, (!instant) ? this.m_Colors.fadeDuration : 0f, true, true);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00012164 File Offset: 0x00010364
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (this.image == null)
			{
				return;
			}
			this.image.overrideSprite = newSprite;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00012184 File Offset: 0x00010384
		private void TriggerAnimation(string triggername)
		{
			if (this.animator == null || !this.animator.enabled || !this.animator.isActiveAndEnabled || this.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			this.animator.ResetTrigger(this.m_AnimationTriggers.normalTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.pressedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.highlightedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.disabledTrigger);
			this.animator.SetTrigger(triggername);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00012248 File Offset: 0x00010448
		protected bool IsHighlighted(BaseEventData eventData)
		{
			if (!this.IsActive())
			{
				return false;
			}
			if (this.IsPressed())
			{
				return false;
			}
			bool flag = this.hasSelection;
			if (eventData is PointerEventData)
			{
				PointerEventData pointerEventData = eventData as PointerEventData;
				flag |= ((this.isPointerDown && !this.isPointerInside && pointerEventData.pointerPress == base.gameObject) || (!this.isPointerDown && this.isPointerInside && pointerEventData.pointerPress == base.gameObject) || (!this.isPointerDown && this.isPointerInside && pointerEventData.pointerPress == null));
			}
			else
			{
				flag |= this.isPointerInside;
			}
			return flag;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001231C File Offset: 0x0001051C
		[Obsolete("Is Pressed no longer requires eventData", false)]
		protected bool IsPressed(BaseEventData eventData)
		{
			return this.IsPressed();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00012324 File Offset: 0x00010524
		protected bool IsPressed()
		{
			return this.IsActive() && this.isPointerInside && this.isPointerDown;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00012348 File Offset: 0x00010548
		protected void UpdateSelectionState(BaseEventData eventData)
		{
			if (this.IsPressed())
			{
				this.m_CurrentSelectionState = Selectable.SelectionState.Pressed;
				return;
			}
			if (this.IsHighlighted(eventData))
			{
				this.m_CurrentSelectionState = Selectable.SelectionState.Highlighted;
				return;
			}
			this.m_CurrentSelectionState = Selectable.SelectionState.Normal;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00012378 File Offset: 0x00010578
		private void EvaluateAndTransitionToSelectionState(BaseEventData eventData)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateSelectionState(eventData);
			this.InternalEvaluateAndTransitionToSelectionState(false);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00012394 File Offset: 0x00010594
		private void InternalEvaluateAndTransitionToSelectionState(bool instant)
		{
			Selectable.SelectionState state = this.m_CurrentSelectionState;
			if (this.IsActive() && !this.IsInteractable())
			{
				state = Selectable.SelectionState.Disabled;
			}
			this.DoStateTransition(state, instant);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000123C8 File Offset: 0x000105C8
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.IsInteractable() && this.navigation.mode != Navigation.Mode.None)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			}
			this.isPointerDown = true;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00012420 File Offset: 0x00010620
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.isPointerDown = false;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001243C File Offset: 0x0001063C
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.isPointerInside = true;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001244C File Offset: 0x0001064C
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.isPointerInside = false;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001245C File Offset: 0x0001065C
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.hasSelection = true;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001246C File Offset: 0x0001066C
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.hasSelection = false;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001247C File Offset: 0x0001067C
		public virtual void Select()
		{
			if (EventSystem.current.alreadySelecting)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}

		// Token: 0x0400020E RID: 526
		private static List<Selectable> s_List = new List<Selectable>();

		// Token: 0x0400020F RID: 527
		[FormerlySerializedAs("navigation")]
		[SerializeField]
		private Navigation m_Navigation = Navigation.defaultNavigation;

		// Token: 0x04000210 RID: 528
		[FormerlySerializedAs("transition")]
		[SerializeField]
		private Selectable.Transition m_Transition = Selectable.Transition.ColorTint;

		// Token: 0x04000211 RID: 529
		[FormerlySerializedAs("colors")]
		[SerializeField]
		private ColorBlock m_Colors = ColorBlock.defaultColorBlock;

		// Token: 0x04000212 RID: 530
		[FormerlySerializedAs("spriteState")]
		[SerializeField]
		private SpriteState m_SpriteState;

		// Token: 0x04000213 RID: 531
		[SerializeField]
		[FormerlySerializedAs("animationTriggers")]
		private AnimationTriggers m_AnimationTriggers = new AnimationTriggers();

		// Token: 0x04000214 RID: 532
		[Tooltip("Can the Selectable be interacted with?")]
		[SerializeField]
		private bool m_Interactable = true;

		// Token: 0x04000215 RID: 533
		[FormerlySerializedAs("m_HighlightGraphic")]
		[SerializeField]
		[FormerlySerializedAs("highlightGraphic")]
		private Graphic m_TargetGraphic;

		// Token: 0x04000216 RID: 534
		private bool m_GroupsAllowInteraction = true;

		// Token: 0x04000217 RID: 535
		private Selectable.SelectionState m_CurrentSelectionState;

		// Token: 0x04000218 RID: 536
		private readonly List<CanvasGroup> m_CanvasGroupCache = new List<CanvasGroup>();

		// Token: 0x02000083 RID: 131
		protected enum SelectionState
		{
			// Token: 0x0400021D RID: 541
			Normal,
			// Token: 0x0400021E RID: 542
			Highlighted,
			// Token: 0x0400021F RID: 543
			Pressed,
			// Token: 0x04000220 RID: 544
			Disabled
		}

		// Token: 0x02000084 RID: 132
		public enum Transition
		{
			// Token: 0x04000222 RID: 546
			None,
			// Token: 0x04000223 RID: 547
			ColorTint,
			// Token: 0x04000224 RID: 548
			SpriteSwap,
			// Token: 0x04000225 RID: 549
			Animation
		}
	}
}
