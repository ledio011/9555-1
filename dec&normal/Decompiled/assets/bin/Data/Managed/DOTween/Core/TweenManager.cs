using System;
using System.Collections.Generic;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Core
{
	// Token: 0x0200001F RID: 31
	internal static class TweenManager
	{
		// Token: 0x0600005F RID: 95 RVA: 0x000034FC File Offset: 0x000016FC
		internal static TweenerCore<T1, T2, TPlugOptions> GetTweener<T1, T2, TPlugOptions>() where TPlugOptions : struct, IPlugOptions
		{
			if (TweenManager.totPooledTweeners > 0)
			{
				Type typeFromHandle = typeof(T1);
				Type typeFromHandle2 = typeof(T2);
				Type typeFromHandle3 = typeof(TPlugOptions);
				for (int i = TweenManager._maxPooledTweenerId; i > TweenManager._minPooledTweenerId - 1; i--)
				{
					Tween tween = TweenManager._pooledTweeners[i];
					if (tween != null && tween.typeofT1 == typeFromHandle && tween.typeofT2 == typeFromHandle2 && tween.typeofTPlugOptions == typeFromHandle3)
					{
						TweenerCore<T1, T2, TPlugOptions> tweenerCore = (TweenerCore<T1, T2, TPlugOptions>)tween;
						TweenManager.AddActiveTween(tweenerCore);
						TweenManager._pooledTweeners[i] = null;
						if (TweenManager._maxPooledTweenerId != TweenManager._minPooledTweenerId)
						{
							if (i == TweenManager._maxPooledTweenerId)
							{
								TweenManager._maxPooledTweenerId--;
							}
							else if (i == TweenManager._minPooledTweenerId)
							{
								TweenManager._minPooledTweenerId++;
							}
						}
						TweenManager.totPooledTweeners--;
						return tweenerCore;
					}
				}
				if (TweenManager.totTweeners >= TweenManager.maxTweeners)
				{
					TweenManager._pooledTweeners[TweenManager._maxPooledTweenerId] = null;
					TweenManager._maxPooledTweenerId--;
					TweenManager.totPooledTweeners--;
					TweenManager.totTweeners--;
				}
			}
			else if (TweenManager.totTweeners >= TweenManager.maxTweeners - 1)
			{
				int num = TweenManager.maxTweeners;
				int num2 = TweenManager.maxSequences;
				TweenManager.IncreaseCapacities(TweenManager.CapacityIncreaseMode.TweenersOnly);
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning("Max Tweens reached: capacity has automatically been increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup".Replace("#0", num + "/" + num2).Replace("#1", TweenManager.maxTweeners + "/" + TweenManager.maxSequences));
				}
			}
			TweenerCore<T1, T2, TPlugOptions> tweenerCore2 = new TweenerCore<T1, T2, TPlugOptions>();
			TweenManager.totTweeners++;
			TweenManager.AddActiveTween(tweenerCore2);
			return tweenerCore2;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000036AC File Offset: 0x000018AC
		internal static void SetUpdateType(Tween t, UpdateType updateType, bool isIndependentUpdate)
		{
			if (!t.active || t.updateType == updateType)
			{
				t.updateType = updateType;
				t.isIndependentUpdate = isIndependentUpdate;
				return;
			}
			if (t.updateType == UpdateType.Normal)
			{
				TweenManager.totActiveDefaultTweens--;
				TweenManager.hasActiveDefaultTweens = (TweenManager.totActiveDefaultTweens > 0);
			}
			else if (t.updateType == UpdateType.Fixed)
			{
				TweenManager.totActiveFixedTweens--;
				TweenManager.hasActiveFixedTweens = (TweenManager.totActiveFixedTweens > 0);
			}
			else
			{
				TweenManager.totActiveLateTweens--;
				TweenManager.hasActiveLateTweens = (TweenManager.totActiveLateTweens > 0);
			}
			t.updateType = updateType;
			t.isIndependentUpdate = isIndependentUpdate;
			if (updateType == UpdateType.Normal)
			{
				TweenManager.totActiveDefaultTweens++;
				TweenManager.hasActiveDefaultTweens = true;
				return;
			}
			if (updateType == UpdateType.Fixed)
			{
				TweenManager.totActiveFixedTweens++;
				TweenManager.hasActiveFixedTweens = true;
				return;
			}
			TweenManager.totActiveLateTweens++;
			TweenManager.hasActiveLateTweens = true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003788 File Offset: 0x00001988
		internal static void Despawn(Tween t, bool modifyActiveLists = true)
		{
			if (t.onKill != null)
			{
				Tween.OnTweenCallback(t.onKill);
			}
			if (modifyActiveLists)
			{
				TweenManager.RemoveActiveTween(t);
			}
			if (t.isRecyclable)
			{
				TweenType tweenType = t.tweenType;
				if (tweenType != TweenType.Tweener)
				{
					if (tweenType == TweenType.Sequence)
					{
						TweenManager._PooledSequences.Push(t);
						TweenManager.totPooledSequences++;
						Sequence sequence = (Sequence)t;
						int count = sequence.sequencedTweens.Count;
						for (int i = 0; i < count; i++)
						{
							TweenManager.Despawn(sequence.sequencedTweens[i], false);
						}
					}
				}
				else
				{
					if (TweenManager._maxPooledTweenerId == -1)
					{
						TweenManager._maxPooledTweenerId = TweenManager.maxTweeners - 1;
						TweenManager._minPooledTweenerId = TweenManager.maxTweeners - 1;
					}
					if (TweenManager._maxPooledTweenerId < TweenManager.maxTweeners - 1)
					{
						TweenManager._pooledTweeners[TweenManager._maxPooledTweenerId + 1] = t;
						TweenManager._maxPooledTweenerId++;
						if (TweenManager._minPooledTweenerId > TweenManager._maxPooledTweenerId)
						{
							TweenManager._minPooledTweenerId = TweenManager._maxPooledTweenerId;
						}
					}
					else
					{
						int j = TweenManager._maxPooledTweenerId;
						while (j > -1)
						{
							if (TweenManager._pooledTweeners[j] == null)
							{
								TweenManager._pooledTweeners[j] = t;
								if (j < TweenManager._minPooledTweenerId)
								{
									TweenManager._minPooledTweenerId = j;
								}
								if (TweenManager._maxPooledTweenerId < TweenManager._minPooledTweenerId)
								{
									TweenManager._maxPooledTweenerId = TweenManager._minPooledTweenerId;
									break;
								}
								break;
							}
							else
							{
								j--;
							}
						}
					}
					TweenManager.totPooledTweeners++;
				}
			}
			else
			{
				TweenType tweenType = t.tweenType;
				if (tweenType != TweenType.Tweener)
				{
					if (tweenType == TweenType.Sequence)
					{
						TweenManager.totSequences--;
						Sequence sequence2 = (Sequence)t;
						int count2 = sequence2.sequencedTweens.Count;
						for (int k = 0; k < count2; k++)
						{
							TweenManager.Despawn(sequence2.sequencedTweens[k], false);
						}
					}
				}
				else
				{
					TweenManager.totTweeners--;
				}
			}
			t.active = false;
			t.Reset();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003948 File Offset: 0x00001B48
		internal static void SetCapacities(int tweenersCapacity, int sequencesCapacity)
		{
			if (tweenersCapacity < sequencesCapacity)
			{
				tweenersCapacity = sequencesCapacity;
			}
			TweenManager.maxActive = tweenersCapacity + sequencesCapacity;
			TweenManager.maxTweeners = tweenersCapacity;
			TweenManager.maxSequences = sequencesCapacity;
			Array.Resize<Tween>(ref TweenManager._activeTweens, TweenManager.maxActive);
			Array.Resize<Tween>(ref TweenManager._pooledTweeners, tweenersCapacity);
			TweenManager._KillList.Capacity = TweenManager.maxActive;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000399C File Offset: 0x00001B9C
		internal static void Update(UpdateType updateType, float deltaTime, float independentTime)
		{
			if (TweenManager._requiresActiveReorganization)
			{
				TweenManager.ReorganizeActiveTweens();
			}
			TweenManager.isUpdateLoop = true;
			bool flag = false;
			int num = TweenManager._maxActiveLookupId + 1;
			for (int i = 0; i < num; i++)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (tween != null && tween.updateType == updateType)
				{
					if (!tween.active)
					{
						flag = true;
						TweenManager.MarkForKilling(tween);
					}
					else if (tween.isPlaying)
					{
						tween.creationLocked = true;
						float num2 = (tween.isIndependentUpdate ? independentTime : deltaTime) * tween.timeScale;
						if (!tween.delayComplete)
						{
							num2 = tween.UpdateDelay(tween.elapsedDelay + num2);
							if (num2 <= -1f)
							{
								flag = true;
								TweenManager.MarkForKilling(tween);
								goto IL_202;
							}
							if (num2 <= 0f)
							{
								goto IL_202;
							}
							if (tween.playedOnce && tween.onPlay != null)
							{
								Tween.OnTweenCallback(tween.onPlay);
							}
						}
						if (!tween.startupDone && !tween.Startup())
						{
							flag = true;
							TweenManager.MarkForKilling(tween);
						}
						else
						{
							float num3 = tween.position;
							bool flag2 = num3 >= tween.duration;
							int num4 = tween.completedLoops;
							if (tween.duration <= 0f)
							{
								num3 = 0f;
								num4 = ((tween.loops == -1) ? (tween.completedLoops + 1) : tween.loops);
							}
							else
							{
								if (tween.isBackwards)
								{
									num3 -= num2;
									while (num3 < 0f && num4 > -1)
									{
										num3 += tween.duration;
										num4--;
									}
									if (num4 < 0 || (flag2 && num4 < 1))
									{
										num3 = 0f;
										num4 = (flag2 ? 1 : 0);
									}
								}
								else
								{
									num3 += num2;
									while (num3 >= tween.duration && (tween.loops == -1 || num4 < tween.loops))
									{
										num3 -= tween.duration;
										num4++;
									}
								}
								if (flag2)
								{
									num4--;
								}
								if (tween.loops != -1 && num4 >= tween.loops)
								{
									num3 = tween.duration;
								}
							}
							if (Tween.DoGoto(tween, num3, num4, UpdateMode.Update))
							{
								flag = true;
								TweenManager.MarkForKilling(tween);
							}
						}
					}
				}
				IL_202:;
			}
			if (flag)
			{
				if (TweenManager._despawnAllCalledFromUpdateLoopCallback)
				{
					TweenManager._despawnAllCalledFromUpdateLoopCallback = false;
				}
				else
				{
					TweenManager.DespawnActiveTweens(TweenManager._KillList);
				}
				TweenManager._KillList.Clear();
			}
			TweenManager.isUpdateLoop = false;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003BE4 File Offset: 0x00001DE4
		internal static int FilteredOperation(OperationType operationType, FilterType filterType, object id, bool optionalBool, float optionalFloat, object optionalObj = null, object[] optionalArray = null)
		{
			int num = 0;
			bool flag = false;
			int num2 = (optionalArray == null) ? 0 : optionalArray.Length;
			for (int i = TweenManager._maxActiveLookupId; i > -1; i--)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (tween != null && tween.active)
				{
					bool flag2 = false;
					switch (filterType)
					{
					case FilterType.All:
						flag2 = true;
						break;
					case FilterType.TargetOrId:
						flag2 = (id.Equals(tween.id) || id.Equals(tween.target));
						break;
					case FilterType.TargetAndId:
						flag2 = (id.Equals(tween.id) && optionalObj != null && optionalObj.Equals(tween.target));
						break;
					case FilterType.AllExceptTargetsOrIds:
						flag2 = true;
						for (int j = 0; j < num2; j++)
						{
							object obj = optionalArray[j];
							if (obj.Equals(tween.id) || obj.Equals(tween.target))
							{
								flag2 = false;
								break;
							}
						}
						break;
					}
					if (flag2)
					{
						switch (operationType)
						{
						case OperationType.Complete:
						{
							bool autoKill = tween.autoKill;
							if (TweenManager.Complete(tween, false, (optionalFloat > 0f) ? UpdateMode.Update : UpdateMode.Goto))
							{
								num += ((!optionalBool) ? 1 : (autoKill ? 1 : 0));
								if (autoKill)
								{
									if (TweenManager.isUpdateLoop)
									{
										tween.active = false;
									}
									else
									{
										flag = true;
										TweenManager._KillList.Add(tween);
									}
								}
							}
							break;
						}
						case OperationType.Despawn:
							num++;
							if (TweenManager.isUpdateLoop)
							{
								tween.active = false;
							}
							else
							{
								TweenManager.Despawn(tween, false);
								flag = true;
								TweenManager._KillList.Add(tween);
							}
							break;
						case OperationType.Flip:
							if (TweenManager.Flip(tween))
							{
								num++;
							}
							break;
						case OperationType.Goto:
							TweenManager.Goto(tween, optionalFloat, optionalBool, UpdateMode.Goto);
							num++;
							break;
						case OperationType.Pause:
							if (TweenManager.Pause(tween))
							{
								num++;
							}
							break;
						case OperationType.Play:
							if (TweenManager.Play(tween))
							{
								num++;
							}
							break;
						case OperationType.PlayForward:
							if (TweenManager.PlayForward(tween))
							{
								num++;
							}
							break;
						case OperationType.PlayBackwards:
							if (TweenManager.PlayBackwards(tween))
							{
								num++;
							}
							break;
						case OperationType.Rewind:
							if (TweenManager.Rewind(tween, optionalBool))
							{
								num++;
							}
							break;
						case OperationType.SmoothRewind:
							if (TweenManager.SmoothRewind(tween))
							{
								num++;
							}
							break;
						case OperationType.Restart:
							if (TweenManager.Restart(tween, optionalBool))
							{
								num++;
							}
							break;
						case OperationType.TogglePause:
							if (TweenManager.TogglePause(tween))
							{
								num++;
							}
							break;
						case OperationType.IsTweening:
							if ((!tween.isComplete || !tween.autoKill) && (!optionalBool || tween.isPlaying))
							{
								num++;
							}
							break;
						}
					}
				}
			}
			if (flag)
			{
				for (int k = TweenManager._KillList.Count - 1; k > -1; k--)
				{
					TweenManager.RemoveActiveTween(TweenManager._KillList[k]);
				}
				TweenManager._KillList.Clear();
			}
			return num;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003EC8 File Offset: 0x000020C8
		internal static bool Complete(Tween t, bool modifyActiveLists = true, UpdateMode updateMode = UpdateMode.Goto)
		{
			if (t.loops == -1)
			{
				return false;
			}
			if (!t.isComplete)
			{
				Tween.DoGoto(t, t.duration, t.loops, updateMode);
				t.isPlaying = false;
				if (t.autoKill)
				{
					if (TweenManager.isUpdateLoop)
					{
						t.active = false;
					}
					else
					{
						TweenManager.Despawn(t, modifyActiveLists);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003F28 File Offset: 0x00002128
		internal static bool Flip(Tween t)
		{
			t.isBackwards = !t.isBackwards;
			return true;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003F3C File Offset: 0x0000213C
		internal static void ForceInit(Tween t, bool isSequenced = false)
		{
			if (t.startupDone)
			{
				return;
			}
			if (!t.Startup() && !isSequenced)
			{
				if (TweenManager.isUpdateLoop)
				{
					t.active = false;
					return;
				}
				TweenManager.RemoveActiveTween(t);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003F68 File Offset: 0x00002168
		internal static bool Goto(Tween t, float to, bool andPlay = false, UpdateMode updateMode = UpdateMode.Goto)
		{
			bool isPlaying = t.isPlaying;
			t.isPlaying = andPlay;
			t.delayComplete = true;
			t.elapsedDelay = t.delay;
			int num = Mathf.FloorToInt(to / t.duration);
			float num2 = to % t.duration;
			if (t.loops != -1 && num >= t.loops)
			{
				num = t.loops;
				num2 = t.duration;
			}
			else if (num2 >= t.duration)
			{
				num2 = 0f;
			}
			bool flag = Tween.DoGoto(t, num2, num, updateMode);
			if (!andPlay && isPlaying && !flag && t.onPause != null)
			{
				Tween.OnTweenCallback(t.onPause);
			}
			return flag;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004008 File Offset: 0x00002208
		internal static bool Pause(Tween t)
		{
			if (t.isPlaying)
			{
				t.isPlaying = false;
				if (t.onPause != null)
				{
					Tween.OnTweenCallback(t.onPause);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004030 File Offset: 0x00002230
		internal static bool Play(Tween t)
		{
			if (!t.isPlaying && ((!t.isBackwards && !t.isComplete) || (t.isBackwards && (t.completedLoops > 0 || t.position > 0f))))
			{
				t.isPlaying = true;
				if (t.playedOnce && t.delayComplete && t.onPlay != null)
				{
					Tween.OnTweenCallback(t.onPlay);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000040A4 File Offset: 0x000022A4
		internal static bool PlayBackwards(Tween t)
		{
			if (!t.isBackwards)
			{
				t.isBackwards = true;
				TweenManager.Play(t);
				return true;
			}
			return TweenManager.Play(t);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000040C4 File Offset: 0x000022C4
		internal static bool PlayForward(Tween t)
		{
			if (t.isBackwards)
			{
				t.isBackwards = false;
				TweenManager.Play(t);
				return true;
			}
			return TweenManager.Play(t);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000040E4 File Offset: 0x000022E4
		internal static bool Restart(Tween t, bool includeDelay = true)
		{
			bool flag = !t.isPlaying;
			t.isBackwards = false;
			TweenManager.Rewind(t, includeDelay);
			t.isPlaying = true;
			if (flag && t.playedOnce && t.delayComplete && t.onPlay != null)
			{
				Tween.OnTweenCallback(t.onPlay);
			}
			return true;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004138 File Offset: 0x00002338
		internal static bool Rewind(Tween t, bool includeDelay = true)
		{
			bool isPlaying = t.isPlaying;
			t.isPlaying = false;
			bool result = false;
			if (t.delay > 0f)
			{
				if (includeDelay)
				{
					result = (t.delay > 0f && t.elapsedDelay > 0f);
					t.elapsedDelay = 0f;
					t.delayComplete = false;
				}
				else
				{
					result = (t.elapsedDelay < t.delay);
					t.elapsedDelay = t.delay;
					t.delayComplete = true;
				}
			}
			if (t.position > 0f || t.completedLoops > 0 || !t.startupDone)
			{
				result = true;
				if (!Tween.DoGoto(t, 0f, 0, UpdateMode.Goto) && isPlaying && t.onPause != null)
				{
					Tween.OnTweenCallback(t.onPause);
				}
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004204 File Offset: 0x00002404
		internal static bool SmoothRewind(Tween t)
		{
			bool result = false;
			if (t.delay > 0f)
			{
				result = (t.elapsedDelay < t.delay);
				t.elapsedDelay = t.delay;
				t.delayComplete = true;
			}
			if (t.position > 0f || t.completedLoops > 0 || !t.startupDone)
			{
				result = true;
				if (t.loopType == LoopType.Incremental)
				{
					t.PlayBackwards();
				}
				else
				{
					t.Goto(t.ElapsedDirectionalPercentage() * t.duration, false);
					t.PlayBackwards();
				}
			}
			else
			{
				t.isPlaying = false;
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004298 File Offset: 0x00002498
		internal static bool TogglePause(Tween t)
		{
			if (t.isPlaying)
			{
				return TweenManager.Pause(t);
			}
			return TweenManager.Play(t);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000042B0 File Offset: 0x000024B0
		private static void MarkForKilling(Tween t)
		{
			t.active = false;
			TweenManager._KillList.Add(t);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000042C4 File Offset: 0x000024C4
		private static void AddActiveTween(Tween t)
		{
			if (TweenManager._requiresActiveReorganization)
			{
				TweenManager.ReorganizeActiveTweens();
			}
			t.active = true;
			t.updateType = DOTween.defaultUpdateType;
			t.isIndependentUpdate = DOTween.defaultTimeScaleIndependent;
			t.activeId = (TweenManager._maxActiveLookupId = TweenManager.totActiveTweens);
			TweenManager._activeTweens[TweenManager.totActiveTweens] = t;
			if (t.updateType == UpdateType.Normal)
			{
				TweenManager.totActiveDefaultTweens++;
				TweenManager.hasActiveDefaultTweens = true;
			}
			else if (t.updateType == UpdateType.Fixed)
			{
				TweenManager.totActiveFixedTweens++;
				TweenManager.hasActiveFixedTweens = true;
			}
			else
			{
				TweenManager.totActiveLateTweens++;
				TweenManager.hasActiveLateTweens = true;
			}
			TweenManager.totActiveTweens++;
			if (t.tweenType == TweenType.Tweener)
			{
				TweenManager.totActiveTweeners++;
			}
			else
			{
				TweenManager.totActiveSequences++;
			}
			TweenManager.hasActiveTweens = true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004398 File Offset: 0x00002598
		private static void ReorganizeActiveTweens()
		{
			if (TweenManager.totActiveTweens <= 0)
			{
				TweenManager._maxActiveLookupId = -1;
				TweenManager._requiresActiveReorganization = false;
				TweenManager._reorganizeFromId = -1;
				return;
			}
			if (TweenManager._reorganizeFromId == TweenManager._maxActiveLookupId)
			{
				TweenManager._maxActiveLookupId--;
				TweenManager._requiresActiveReorganization = false;
				TweenManager._reorganizeFromId = -1;
				return;
			}
			int num = 1;
			int num2 = TweenManager._maxActiveLookupId + 1;
			TweenManager._maxActiveLookupId = TweenManager._reorganizeFromId - 1;
			for (int i = TweenManager._reorganizeFromId + 1; i < num2; i++)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (tween == null)
				{
					num++;
				}
				else
				{
					tween.activeId = (TweenManager._maxActiveLookupId = i - num);
					TweenManager._activeTweens[i - num] = tween;
					TweenManager._activeTweens[i] = null;
				}
			}
			TweenManager._requiresActiveReorganization = false;
			TweenManager._reorganizeFromId = -1;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000444C File Offset: 0x0000264C
		private static void DespawnActiveTweens(List<Tween> tweens)
		{
			for (int i = tweens.Count - 1; i > -1; i--)
			{
				TweenManager.Despawn(tweens[i], true);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000447C File Offset: 0x0000267C
		private static void RemoveActiveTween(Tween t)
		{
			int activeId = t.activeId;
			t.activeId = -1;
			TweenManager._requiresActiveReorganization = true;
			if (TweenManager._reorganizeFromId == -1 || TweenManager._reorganizeFromId > activeId)
			{
				TweenManager._reorganizeFromId = activeId;
			}
			TweenManager._activeTweens[activeId] = null;
			if (t.updateType == UpdateType.Normal)
			{
				if (TweenManager.totActiveDefaultTweens > 0)
				{
					TweenManager.totActiveDefaultTweens--;
					TweenManager.hasActiveDefaultTweens = (TweenManager.totActiveDefaultTweens > 0);
				}
				else
				{
					Debugger.LogRemoveActiveTweenError("totActiveDefaultTweens");
				}
			}
			else if (t.updateType == UpdateType.Fixed)
			{
				if (TweenManager.totActiveFixedTweens > 0)
				{
					TweenManager.totActiveFixedTweens--;
					TweenManager.hasActiveFixedTweens = (TweenManager.totActiveFixedTweens > 0);
				}
				else
				{
					Debugger.LogRemoveActiveTweenError("totActiveFixedTweens");
				}
			}
			else if (TweenManager.totActiveLateTweens > 0)
			{
				TweenManager.totActiveLateTweens--;
				TweenManager.hasActiveLateTweens = (TweenManager.totActiveLateTweens > 0);
			}
			else
			{
				Debugger.LogRemoveActiveTweenError("totActiveLateTweens");
			}
			TweenManager.totActiveTweens--;
			TweenManager.hasActiveTweens = (TweenManager.totActiveTweens > 0);
			if (t.tweenType == TweenType.Tweener)
			{
				TweenManager.totActiveTweeners--;
			}
			else
			{
				TweenManager.totActiveSequences--;
			}
			if (TweenManager.totActiveTweens < 0)
			{
				TweenManager.totActiveTweens = 0;
				Debugger.LogRemoveActiveTweenError("totActiveTweens");
			}
			if (TweenManager.totActiveTweeners < 0)
			{
				TweenManager.totActiveTweeners = 0;
				Debugger.LogRemoveActiveTweenError("totActiveTweeners");
			}
			if (TweenManager.totActiveSequences < 0)
			{
				TweenManager.totActiveSequences = 0;
				Debugger.LogRemoveActiveTweenError("totActiveSequences");
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000045DC File Offset: 0x000027DC
		private static void IncreaseCapacities(TweenManager.CapacityIncreaseMode increaseMode)
		{
			int num = 0;
			int num2 = Mathf.Max((int)((float)TweenManager.maxTweeners * 1.5f), 200);
			int num3 = Mathf.Max((int)((float)TweenManager.maxSequences * 1.5f), 50);
			if (increaseMode != TweenManager.CapacityIncreaseMode.TweenersOnly)
			{
				if (increaseMode != TweenManager.CapacityIncreaseMode.SequencesOnly)
				{
					num += num2;
					TweenManager.maxTweeners += num2;
					TweenManager.maxSequences += num3;
					Array.Resize<Tween>(ref TweenManager._pooledTweeners, TweenManager.maxTweeners);
				}
				else
				{
					num += num3;
					TweenManager.maxSequences += num3;
				}
			}
			else
			{
				num += num2;
				TweenManager.maxTweeners += num2;
				Array.Resize<Tween>(ref TweenManager._pooledTweeners, TweenManager.maxTweeners);
			}
			TweenManager.maxActive = TweenManager.maxTweeners + TweenManager.maxSequences;
			Array.Resize<Tween>(ref TweenManager._activeTweens, TweenManager.maxActive);
			if (num > 0)
			{
				TweenManager._KillList.Capacity += num;
			}
		}

		// Token: 0x04000070 RID: 112
		private const int _DefaultMaxTweeners = 200;

		// Token: 0x04000071 RID: 113
		private const int _DefaultMaxSequences = 50;

		// Token: 0x04000072 RID: 114
		private const string _MaxTweensReached = "Max Tweens reached: capacity has automatically been increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup";

		// Token: 0x04000073 RID: 115
		internal static int maxActive = 250;

		// Token: 0x04000074 RID: 116
		internal static int maxTweeners = 200;

		// Token: 0x04000075 RID: 117
		internal static int maxSequences = 50;

		// Token: 0x04000076 RID: 118
		internal static bool hasActiveTweens;

		// Token: 0x04000077 RID: 119
		internal static bool hasActiveDefaultTweens;

		// Token: 0x04000078 RID: 120
		internal static bool hasActiveLateTweens;

		// Token: 0x04000079 RID: 121
		internal static bool hasActiveFixedTweens;

		// Token: 0x0400007A RID: 122
		internal static int totActiveTweens;

		// Token: 0x0400007B RID: 123
		internal static int totActiveDefaultTweens;

		// Token: 0x0400007C RID: 124
		internal static int totActiveLateTweens;

		// Token: 0x0400007D RID: 125
		internal static int totActiveFixedTweens;

		// Token: 0x0400007E RID: 126
		internal static int totActiveTweeners;

		// Token: 0x0400007F RID: 127
		internal static int totActiveSequences;

		// Token: 0x04000080 RID: 128
		internal static int totPooledTweeners;

		// Token: 0x04000081 RID: 129
		internal static int totPooledSequences;

		// Token: 0x04000082 RID: 130
		internal static int totTweeners;

		// Token: 0x04000083 RID: 131
		internal static int totSequences;

		// Token: 0x04000084 RID: 132
		internal static bool isUpdateLoop;

		// Token: 0x04000085 RID: 133
		internal static Tween[] _activeTweens = new Tween[250];

		// Token: 0x04000086 RID: 134
		private static Tween[] _pooledTweeners = new Tween[200];

		// Token: 0x04000087 RID: 135
		private static readonly Stack<Tween> _PooledSequences = new Stack<Tween>();

		// Token: 0x04000088 RID: 136
		private static readonly List<Tween> _KillList = new List<Tween>(250);

		// Token: 0x04000089 RID: 137
		private static int _maxActiveLookupId = -1;

		// Token: 0x0400008A RID: 138
		private static bool _requiresActiveReorganization;

		// Token: 0x0400008B RID: 139
		private static int _reorganizeFromId = -1;

		// Token: 0x0400008C RID: 140
		private static int _minPooledTweenerId = -1;

		// Token: 0x0400008D RID: 141
		private static int _maxPooledTweenerId = -1;

		// Token: 0x0400008E RID: 142
		private static bool _despawnAllCalledFromUpdateLoopCallback;

		// Token: 0x02000020 RID: 32
		internal enum CapacityIncreaseMode
		{
			// Token: 0x04000090 RID: 144
			TweenersAndSequences,
			// Token: 0x04000091 RID: 145
			TweenersOnly,
			// Token: 0x04000092 RID: 146
			SequencesOnly
		}
	}
}
