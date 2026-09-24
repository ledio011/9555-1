using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x0200000B RID: 11
	[DesignerCategory("Component")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class Component : MarshalByRefObject, IComponent, IDisposable
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000024D4 File Offset: 0x000006D4
		public Component()
		{
			this.event_handlers = null;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000024F0 File Offset: 0x000006F0
		protected EventHandlerList Events
		{
			get
			{
				if (this.event_handlers == null)
				{
					this.event_handlers = new EventHandlerList();
				}
				return this.event_handlers;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002510 File Offset: 0x00000710
		~Component()
		{
			this.Dispose(false);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002540 File Offset: 0x00000740
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002550 File Offset: 0x00000750
		protected virtual void Dispose(bool release_all)
		{
			if (release_all)
			{
				if (this.mySite != null && this.mySite.Container != null)
				{
					this.mySite.Container.Remove(this);
				}
				EventHandler eventHandler = (EventHandler)this.Events[this.disposedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025B8 File Offset: 0x000007B8
		public override string ToString()
		{
			if (this.mySite == null)
			{
				return base.GetType().ToString();
			}
			return string.Format("{0} [{1}]", this.mySite.Name, base.GetType().ToString());
		}

		// Token: 0x0400001D RID: 29
		private EventHandlerList event_handlers;

		// Token: 0x0400001E RID: 30
		private ISite mySite;

		// Token: 0x0400001F RID: 31
		private object disposedEvent = new object();
	}
}
