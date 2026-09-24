using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200037D RID: 893
	public class enter_copy_scene
	{
		// Token: 0x0200037E RID: 894
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B06 RID: 6918 RVA: 0x000935E8 File Offset: 0x000917E8
			public request() : base(enter_copy_scene.request.max_field_count)
			{
			}

			// Token: 0x06001B07 RID: 6919 RVA: 0x000935F8 File Offset: 0x000917F8
			public request(byte[] buffer) : base(enter_copy_scene.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006AD RID: 1709
			// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00093614 File Offset: 0x00091814
			// (set) Token: 0x06001B0A RID: 6922 RVA: 0x0009361C File Offset: 0x0009181C
			public string mapInfoId
			{
				get
				{
					return this._mapInfoId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._mapInfoId = value;
				}
			}

			// Token: 0x170006AE RID: 1710
			// (get) Token: 0x06001B0B RID: 6923 RVA: 0x00093634 File Offset: 0x00091834
			public bool HasMapInfoId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170006AF RID: 1711
			// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00093644 File Offset: 0x00091844
			// (set) Token: 0x06001B0D RID: 6925 RVA: 0x0009364C File Offset: 0x0009184C
			public bool reaminItem
			{
				get
				{
					return this._reaminItem;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._reaminItem = value;
				}
			}

			// Token: 0x170006B0 RID: 1712
			// (get) Token: 0x06001B0E RID: 6926 RVA: 0x00093664 File Offset: 0x00091864
			public bool HasReaminItem
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001B0F RID: 6927 RVA: 0x00093674 File Offset: 0x00091874
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							this.deserialize.read_unknow_data();
						}
						else
						{
							this.reaminItem = this.deserialize.read_boolean();
						}
					}
					else
					{
						this.mapInfoId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06001B10 RID: 6928 RVA: 0x000936EC File Offset: 0x000918EC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.mapInfoId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_boolean(this.reaminItem, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x040019F7 RID: 6647
			private static int max_field_count = 2;

			// Token: 0x040019F8 RID: 6648
			private string _mapInfoId;

			// Token: 0x040019F9 RID: 6649
			private bool _reaminItem;
		}
	}
}
