using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000414 RID: 1044
	public class mail_update
	{
		// Token: 0x02000415 RID: 1045
		public class request : SprotoTypeBase
		{
			// Token: 0x06002050 RID: 8272 RVA: 0x0009E3B8 File Offset: 0x0009C5B8
			public request() : base(mail_update.request.max_field_count)
			{
			}

			// Token: 0x06002051 RID: 8273 RVA: 0x0009E3C8 File Offset: 0x0009C5C8
			public request(byte[] buffer) : base(mail_update.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170008DF RID: 2271
			// (get) Token: 0x06002053 RID: 8275 RVA: 0x0009E3E8 File Offset: 0x0009C5E8
			// (set) Token: 0x06002054 RID: 8276 RVA: 0x0009E3F0 File Offset: 0x0009C5F0
			public long mailId
			{
				get
				{
					return this._mailId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._mailId = value;
				}
			}

			// Token: 0x170008E0 RID: 2272
			// (get) Token: 0x06002055 RID: 8277 RVA: 0x0009E408 File Offset: 0x0009C608
			public bool HasMailId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170008E1 RID: 2273
			// (get) Token: 0x06002056 RID: 8278 RVA: 0x0009E418 File Offset: 0x0009C618
			// (set) Token: 0x06002057 RID: 8279 RVA: 0x0009E420 File Offset: 0x0009C620
			public long sendertype
			{
				get
				{
					return this._sendertype;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._sendertype = value;
				}
			}

			// Token: 0x170008E2 RID: 2274
			// (get) Token: 0x06002058 RID: 8280 RVA: 0x0009E438 File Offset: 0x0009C638
			public bool HasSendertype
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170008E3 RID: 2275
			// (get) Token: 0x06002059 RID: 8281 RVA: 0x0009E448 File Offset: 0x0009C648
			// (set) Token: 0x0600205A RID: 8282 RVA: 0x0009E450 File Offset: 0x0009C650
			public string title
			{
				get
				{
					return this._title;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._title = value;
				}
			}

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x0600205B RID: 8283 RVA: 0x0009E468 File Offset: 0x0009C668
			public bool HasTitle
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x0600205C RID: 8284 RVA: 0x0009E478 File Offset: 0x0009C678
			// (set) Token: 0x0600205D RID: 8285 RVA: 0x0009E480 File Offset: 0x0009C680
			public long senderTime
			{
				get
				{
					return this._senderTime;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._senderTime = value;
				}
			}

			// Token: 0x170008E6 RID: 2278
			// (get) Token: 0x0600205E RID: 8286 RVA: 0x0009E498 File Offset: 0x0009C698
			public bool HasSenderTime
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x170008E7 RID: 2279
			// (get) Token: 0x0600205F RID: 8287 RVA: 0x0009E4A8 File Offset: 0x0009C6A8
			// (set) Token: 0x06002060 RID: 8288 RVA: 0x0009E4B0 File Offset: 0x0009C6B0
			public long receiveId
			{
				get
				{
					return this._receiveId;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._receiveId = value;
				}
			}

			// Token: 0x170008E8 RID: 2280
			// (get) Token: 0x06002061 RID: 8289 RVA: 0x0009E4C8 File Offset: 0x0009C6C8
			public bool HasReceiveId
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x170008E9 RID: 2281
			// (get) Token: 0x06002062 RID: 8290 RVA: 0x0009E4D8 File Offset: 0x0009C6D8
			// (set) Token: 0x06002063 RID: 8291 RVA: 0x0009E4E0 File Offset: 0x0009C6E0
			public long readTime
			{
				get
				{
					return this._readTime;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._readTime = value;
				}
			}

			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x06002064 RID: 8292 RVA: 0x0009E4F8 File Offset: 0x0009C6F8
			public bool HasReadTime
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x170008EB RID: 2283
			// (get) Token: 0x06002065 RID: 8293 RVA: 0x0009E508 File Offset: 0x0009C708
			// (set) Token: 0x06002066 RID: 8294 RVA: 0x0009E510 File Offset: 0x0009C710
			public string context
			{
				get
				{
					return this._context;
				}
				set
				{
					this.has_field.set_field(6, true);
					this._context = value;
				}
			}

			// Token: 0x170008EC RID: 2284
			// (get) Token: 0x06002067 RID: 8295 RVA: 0x0009E528 File Offset: 0x0009C728
			public bool HasContext
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x170008ED RID: 2285
			// (get) Token: 0x06002068 RID: 8296 RVA: 0x0009E538 File Offset: 0x0009C738
			// (set) Token: 0x06002069 RID: 8297 RVA: 0x0009E540 File Offset: 0x0009C740
			public long mailState
			{
				get
				{
					return this._mailState;
				}
				set
				{
					this.has_field.set_field(7, true);
					this._mailState = value;
				}
			}

			// Token: 0x170008EE RID: 2286
			// (get) Token: 0x0600206A RID: 8298 RVA: 0x0009E558 File Offset: 0x0009C758
			public bool HasMailState
			{
				get
				{
					return this.has_field.has_field(7);
				}
			}

			// Token: 0x170008EF RID: 2287
			// (get) Token: 0x0600206B RID: 8299 RVA: 0x0009E568 File Offset: 0x0009C768
			// (set) Token: 0x0600206C RID: 8300 RVA: 0x0009E570 File Offset: 0x0009C770
			public long sortTime
			{
				get
				{
					return this._sortTime;
				}
				set
				{
					this.has_field.set_field(8, true);
					this._sortTime = value;
				}
			}

			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x0600206D RID: 8301 RVA: 0x0009E588 File Offset: 0x0009C788
			public bool HasSortTime
			{
				get
				{
					return this.has_field.has_field(8);
				}
			}

			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x0600206E RID: 8302 RVA: 0x0009E598 File Offset: 0x0009C798
			// (set) Token: 0x0600206F RID: 8303 RVA: 0x0009E5A0 File Offset: 0x0009C7A0
			public Dictionary<string, item> items
			{
				get
				{
					return this._items;
				}
				set
				{
					this.has_field.set_field(9, true);
					this._items = value;
				}
			}

			// Token: 0x170008F2 RID: 2290
			// (get) Token: 0x06002070 RID: 8304 RVA: 0x0009E5B8 File Offset: 0x0009C7B8
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(9);
				}
			}

			// Token: 0x170008F3 RID: 2291
			// (get) Token: 0x06002071 RID: 8305 RVA: 0x0009E5C8 File Offset: 0x0009C7C8
			// (set) Token: 0x06002072 RID: 8306 RVA: 0x0009E5D0 File Offset: 0x0009C7D0
			public long expireday
			{
				get
				{
					return this._expireday;
				}
				set
				{
					this.has_field.set_field(10, true);
					this._expireday = value;
				}
			}

			// Token: 0x170008F4 RID: 2292
			// (get) Token: 0x06002073 RID: 8307 RVA: 0x0009E5E8 File Offset: 0x0009C7E8
			public bool HasExpireday
			{
				get
				{
					return this.has_field.has_field(10);
				}
			}

			// Token: 0x06002074 RID: 8308 RVA: 0x0009E5F8 File Offset: 0x0009C7F8
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.mailId = this.deserialize.read_integer();
						continue;
					case 1:
						this.sendertype = this.deserialize.read_integer();
						continue;
					case 3:
						this.title = this.deserialize.read_string();
						continue;
					case 4:
						this.senderTime = this.deserialize.read_integer();
						continue;
					case 5:
						this.receiveId = this.deserialize.read_integer();
						continue;
					case 6:
						this.readTime = this.deserialize.read_integer();
						continue;
					case 7:
						this.context = this.deserialize.read_string();
						continue;
					case 8:
						this.mailState = this.deserialize.read_integer();
						continue;
					case 9:
						this.sortTime = this.deserialize.read_integer();
						continue;
					case 10:
						this.items = this.deserialize.read_map<string, item>((item v) => v.id);
						continue;
					case 11:
						this.expireday = this.deserialize.read_integer();
						continue;
					}
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002075 RID: 8309 RVA: 0x0009E77C File Offset: 0x0009C97C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.mailId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.sendertype, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.title, 3);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.senderTime, 4);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.receiveId, 5);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.readTime, 6);
				}
				if (this.has_field.has_field(6))
				{
					this.serialize.write_string(this.context, 7);
				}
				if (this.has_field.has_field(7))
				{
					this.serialize.write_integer(this.mailState, 8);
				}
				if (this.has_field.has_field(8))
				{
					this.serialize.write_integer(this.sortTime, 9);
				}
				if (this.has_field.has_field(9))
				{
					this.serialize.write_obj<string, item>(this.items, 10);
				}
				if (this.has_field.has_field(10))
				{
					this.serialize.write_integer(this.expireday, 11);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B6D RID: 7021
			private static int max_field_count = 12;

			// Token: 0x04001B6E RID: 7022
			private long _mailId;

			// Token: 0x04001B6F RID: 7023
			private long _sendertype;

			// Token: 0x04001B70 RID: 7024
			private string _title;

			// Token: 0x04001B71 RID: 7025
			private long _senderTime;

			// Token: 0x04001B72 RID: 7026
			private long _receiveId;

			// Token: 0x04001B73 RID: 7027
			private long _readTime;

			// Token: 0x04001B74 RID: 7028
			private string _context;

			// Token: 0x04001B75 RID: 7029
			private long _mailState;

			// Token: 0x04001B76 RID: 7030
			private long _sortTime;

			// Token: 0x04001B77 RID: 7031
			private Dictionary<string, item> _items;

			// Token: 0x04001B78 RID: 7032
			private long _expireday;
		}
	}
}
