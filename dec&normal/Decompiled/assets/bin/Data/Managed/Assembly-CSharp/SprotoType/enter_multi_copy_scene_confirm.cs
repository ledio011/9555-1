using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200038B RID: 907
	public class enter_multi_copy_scene_confirm
	{
		// Token: 0x0200038C RID: 908
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B4B RID: 6987 RVA: 0x00093DF0 File Offset: 0x00091FF0
			public request() : base(enter_multi_copy_scene_confirm.request.max_field_count)
			{
			}

			// Token: 0x06001B4C RID: 6988 RVA: 0x00093E00 File Offset: 0x00092000
			public request(byte[] buffer) : base(enter_multi_copy_scene_confirm.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006BF RID: 1727
			// (get) Token: 0x06001B4E RID: 6990 RVA: 0x00093E1C File Offset: 0x0009201C
			// (set) Token: 0x06001B4F RID: 6991 RVA: 0x00093E24 File Offset: 0x00092024
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x170006C0 RID: 1728
			// (get) Token: 0x06001B50 RID: 6992 RVA: 0x00093E3C File Offset: 0x0009203C
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170006C1 RID: 1729
			// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00093E4C File Offset: 0x0009204C
			// (set) Token: 0x06001B52 RID: 6994 RVA: 0x00093E54 File Offset: 0x00092054
			public long type1
			{
				get
				{
					return this._type1;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._type1 = value;
				}
			}

			// Token: 0x170006C2 RID: 1730
			// (get) Token: 0x06001B53 RID: 6995 RVA: 0x00093E6C File Offset: 0x0009206C
			public bool HasType1
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170006C3 RID: 1731
			// (get) Token: 0x06001B54 RID: 6996 RVA: 0x00093E7C File Offset: 0x0009207C
			// (set) Token: 0x06001B55 RID: 6997 RVA: 0x00093E84 File Offset: 0x00092084
			public bool reaminItem
			{
				get
				{
					return this._reaminItem;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._reaminItem = value;
				}
			}

			// Token: 0x170006C4 RID: 1732
			// (get) Token: 0x06001B56 RID: 6998 RVA: 0x00093E9C File Offset: 0x0009209C
			public bool HasReaminItem
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06001B57 RID: 6999 RVA: 0x00093EAC File Offset: 0x000920AC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.id = this.deserialize.read_string();
						break;
					case 1:
						this.type1 = this.deserialize.read_integer();
						break;
					case 2:
						this.reaminItem = this.deserialize.read_boolean();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001B58 RID: 7000 RVA: 0x00093F40 File Offset: 0x00092140
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.type1, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_boolean(this.reaminItem, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A07 RID: 6663
			private static int max_field_count = 3;

			// Token: 0x04001A08 RID: 6664
			private string _id;

			// Token: 0x04001A09 RID: 6665
			private long _type1;

			// Token: 0x04001A0A RID: 6666
			private bool _reaminItem;
		}
	}
}
