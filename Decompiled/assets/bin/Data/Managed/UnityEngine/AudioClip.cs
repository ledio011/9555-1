using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	public sealed class AudioClip : Object
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600024D RID: 589 RVA: 0x00007134 File Offset: 0x00005334
		// (remove) Token: 0x0600024E RID: 590 RVA: 0x00007150 File Offset: 0x00005350
		private event AudioClip.PCMReaderCallback m_PCMReaderCallback;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600024F RID: 591 RVA: 0x0000716C File Offset: 0x0000536C
		// (remove) Token: 0x06000250 RID: 592 RVA: 0x00007188 File Offset: 0x00005388
		private event AudioClip.PCMSetPositionCallback m_PCMSetPositionCallback;

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000251 RID: 593
		public extern float length { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000252 RID: 594
		public extern int samples { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000253 RID: 595
		public extern int channels { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000254 RID: 596
		public extern int frequency { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000255 RID: 597
		public extern bool isReadyToPlay { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000256 RID: 598
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void GetData(float[] data, int offsetSamples);

		// Token: 0x06000257 RID: 599
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetData(float[] data, int offsetSamples);

		// Token: 0x06000258 RID: 600 RVA: 0x000071A4 File Offset: 0x000053A4
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, _3D, stream, null, null);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000071C4 File Offset: 0x000053C4
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, _3D, stream, pcmreadercallback, null);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000071E4 File Offset: 0x000053E4
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			if (name == null)
			{
				throw new NullReferenceException();
			}
			if (lengthSamples <= 0)
			{
				throw new ArgumentException("Length of created clip must be larger than 0");
			}
			if (channels <= 0)
			{
				throw new ArgumentException("Number of channels in created clip must be greater than 0");
			}
			if (frequency <= 0)
			{
				throw new ArgumentException("Frequency in created clip must be greater than 0");
			}
			AudioClip audioClip = AudioClip.Construct_Internal();
			if (pcmreadercallback != null)
			{
				AudioClip audioClip2 = audioClip;
				audioClip2.m_PCMReaderCallback = (AudioClip.PCMReaderCallback)Delegate.Combine(audioClip2.m_PCMReaderCallback, pcmreadercallback);
			}
			if (pcmsetpositioncallback != null)
			{
				AudioClip audioClip3 = audioClip;
				audioClip3.m_PCMSetPositionCallback = (AudioClip.PCMSetPositionCallback)Delegate.Combine(audioClip3.m_PCMSetPositionCallback, pcmsetpositioncallback);
			}
			audioClip.Init_Internal(name, lengthSamples, channels, frequency, _3D, stream);
			return audioClip;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007288 File Offset: 0x00005488
		private void InvokePCMReaderCallback_Internal(float[] data)
		{
			if (this.m_PCMReaderCallback != null)
			{
				this.m_PCMReaderCallback(data);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000072A4 File Offset: 0x000054A4
		private void InvokePCMSetPositionCallback_Internal(int position)
		{
			if (this.m_PCMSetPositionCallback != null)
			{
				this.m_PCMSetPositionCallback(position);
			}
		}

		// Token: 0x0600025D RID: 605
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern AudioClip Construct_Internal();

		// Token: 0x0600025E RID: 606
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init_Internal(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream);

		// Token: 0x02000022 RID: 34
		// (Invoke) Token: 0x06000260 RID: 608
		public delegate void PCMReaderCallback(float[] data);

		// Token: 0x02000023 RID: 35
		// (Invoke) Token: 0x06000264 RID: 612
		public delegate void PCMSetPositionCallback(int position);
	}
}
