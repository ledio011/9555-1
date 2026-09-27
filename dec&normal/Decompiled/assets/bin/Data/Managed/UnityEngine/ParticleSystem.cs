using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000CA RID: 202
	public sealed class ParticleSystem : Component
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600082E RID: 2094
		public extern bool isPlaying { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600082F RID: 2095
		// (set) Token: 0x06000830 RID: 2096
		public extern bool enableEmission { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001BE RID: 446
		// (set) Token: 0x06000831 RID: 2097
		public extern float emissionRate { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000832 RID: 2098
		// (set) Token: 0x06000833 RID: 2099
		public extern float startSpeed { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000834 RID: 2100
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Internal_Play();

		// Token: 0x06000835 RID: 2101
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Internal_Stop();

		// Token: 0x06000836 RID: 2102
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Internal_Clear();

		// Token: 0x06000837 RID: 2103 RVA: 0x0001312C File Offset: 0x0001132C
		[ExcludeFromDocs]
		public void Play()
		{
			bool withChildren = true;
			this.Play(withChildren);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00013144 File Offset: 0x00011344
		public void Play([DefaultValue("true")] bool withChildren)
		{
			if (withChildren)
			{
				ParticleSystem[] particleSystems = ParticleSystem.GetParticleSystems(this);
				foreach (ParticleSystem particleSystem in particleSystems)
				{
					particleSystem.Internal_Play();
				}
			}
			else
			{
				this.Internal_Play();
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001318C File Offset: 0x0001138C
		[ExcludeFromDocs]
		public void Stop()
		{
			bool withChildren = true;
			this.Stop(withChildren);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000131A4 File Offset: 0x000113A4
		public void Stop([DefaultValue("true")] bool withChildren)
		{
			if (withChildren)
			{
				ParticleSystem[] particleSystems = ParticleSystem.GetParticleSystems(this);
				foreach (ParticleSystem particleSystem in particleSystems)
				{
					particleSystem.Internal_Stop();
				}
			}
			else
			{
				this.Internal_Stop();
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000131EC File Offset: 0x000113EC
		[ExcludeFromDocs]
		public void Clear()
		{
			bool withChildren = true;
			this.Clear(withChildren);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00013204 File Offset: 0x00011404
		public void Clear([DefaultValue("true")] bool withChildren)
		{
			if (withChildren)
			{
				ParticleSystem[] particleSystems = ParticleSystem.GetParticleSystems(this);
				foreach (ParticleSystem particleSystem in particleSystems)
				{
					particleSystem.Internal_Clear();
				}
			}
			else
			{
				this.Internal_Clear();
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001324C File Offset: 0x0001144C
		internal static ParticleSystem[] GetParticleSystems(ParticleSystem root)
		{
			if (!root)
			{
				return null;
			}
			List<ParticleSystem> list = new List<ParticleSystem>();
			list.Add(root);
			ParticleSystem.GetDirectParticleSystemChildrenRecursive(root.transform, list);
			return list.ToArray();
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00013288 File Offset: 0x00011488
		private static void GetDirectParticleSystemChildrenRecursive(Transform transform, List<ParticleSystem> particleSystems)
		{
			foreach (object obj in transform)
			{
				Transform transform2 = (Transform)obj;
				ParticleSystem component = transform2.gameObject.GetComponent<ParticleSystem>();
				if (component != null)
				{
					particleSystems.Add(component);
					ParticleSystem.GetDirectParticleSystemChildrenRecursive(transform2, particleSystems);
				}
			}
		}
	}
}
