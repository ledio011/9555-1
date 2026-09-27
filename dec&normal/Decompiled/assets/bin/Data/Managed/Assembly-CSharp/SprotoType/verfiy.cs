using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000661 RID: 1633
	public class verfiy
	{
		// Token: 0x02000662 RID: 1634
		public class request : SprotoTypeBase
		{
			// Token: 0x06002F1D RID: 12061 RVA: 0x000BB318 File Offset: 0x000B9518
			public request() : base(verfiy.request.max_field_count)
			{
			}

			// Token: 0x06002F1E RID: 12062 RVA: 0x000BB328 File Offset: 0x000B9528
			public request(byte[] buffer) : base(verfiy.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DDD RID: 3549
			// (get) Token: 0x06002F20 RID: 12064 RVA: 0x000BB344 File Offset: 0x000B9544
			// (set) Token: 0x06002F21 RID: 12065 RVA: 0x000BB34C File Offset: 0x000B954C
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

			// Token: 0x17000DDE RID: 3550
			// (get) Token: 0x06002F22 RID: 12066 RVA: 0x000BB364 File Offset: 0x000B9564
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000DDF RID: 3551
			// (get) Token: 0x06002F23 RID: 12067 RVA: 0x000BB374 File Offset: 0x000B9574
			// (set) Token: 0x06002F24 RID: 12068 RVA: 0x000BB37C File Offset: 0x000B957C
			public string key
			{
				get
				{
					return this._key;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._key = value;
				}
			}

			// Token: 0x17000DE0 RID: 3552
			// (get) Token: 0x06002F25 RID: 12069 RVA: 0x000BB394 File Offset: 0x000B9594
			public bool HasKey
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000DE1 RID: 3553
			// (get) Token: 0x06002F26 RID: 12070 RVA: 0x000BB3A4 File Offset: 0x000B95A4
			// (set) Token: 0x06002F27 RID: 12071 RVA: 0x000BB3AC File Offset: 0x000B95AC
			public string versionCode
			{
				get
				{
					return this._versionCode;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._versionCode = value;
				}
			}

			// Token: 0x17000DE2 RID: 3554
			// (get) Token: 0x06002F28 RID: 12072 RVA: 0x000BB3C4 File Offset: 0x000B95C4
			public bool HasVersionCode
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002F29 RID: 12073 RVA: 0x000BB3D4 File Offset: 0x000B95D4
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
						this.key = this.deserialize.read_string();
						break;
					case 2:
						this.versionCode = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002F2A RID: 12074 RVA: 0x000BB468 File Offset: 0x000B9668
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.key, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.versionCode, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F53 RID: 8019
			private static int max_field_count = 3;

			// Token: 0x04001F54 RID: 8020
			private string _id;

			// Token: 0x04001F55 RID: 8021
			private string _key;

			// Token: 0x04001F56 RID: 8022
			private string _versionCode;
		}

		// Token: 0x02000663 RID: 1635
		public class response : SprotoTypeBase
		{
			// Token: 0x06002F2B RID: 12075 RVA: 0x000BB4F8 File Offset: 0x000B96F8
			public response() : base(verfiy.response.max_field_count)
			{
			}

			// Token: 0x06002F2C RID: 12076 RVA: 0x000BB508 File Offset: 0x000B9708
			public response(byte[] buffer) : base(verfiy.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DE3 RID: 3555
			// (get) Token: 0x06002F2E RID: 12078 RVA: 0x000BB528 File Offset: 0x000B9728
			// (set) Token: 0x06002F2F RID: 12079 RVA: 0x000BB530 File Offset: 0x000B9730
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._state = value;
				}
			}

			// Token: 0x17000DE4 RID: 3556
			// (get) Token: 0x06002F30 RID: 12080 RVA: 0x000BB548 File Offset: 0x000B9748
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000DE5 RID: 3557
			// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000BB558 File Offset: 0x000B9758
			// (set) Token: 0x06002F32 RID: 12082 RVA: 0x000BB560 File Offset: 0x000B9760
			public long session
			{
				get
				{
					return this._session;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._session = value;
				}
			}

			// Token: 0x17000DE6 RID: 3558
			// (get) Token: 0x06002F33 RID: 12083 RVA: 0x000BB578 File Offset: 0x000B9778
			public bool HasSession
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000DE7 RID: 3559
			// (get) Token: 0x06002F34 RID: 12084 RVA: 0x000BB588 File Offset: 0x000B9788
			// (set) Token: 0x06002F35 RID: 12085 RVA: 0x000BB590 File Offset: 0x000B9790
			public List<game_server> game_server
			{
				get
				{
					return this._game_server;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._game_server = value;
				}
			}

			// Token: 0x17000DE8 RID: 3560
			// (get) Token: 0x06002F36 RID: 12086 RVA: 0x000BB5A8 File Offset: 0x000B97A8
			public bool HasGame_server
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000DE9 RID: 3561
			// (get) Token: 0x06002F37 RID: 12087 RVA: 0x000BB5B8 File Offset: 0x000B97B8
			// (set) Token: 0x06002F38 RID: 12088 RVA: 0x000BB5C0 File Offset: 0x000B97C0
			public string user_server
			{
				get
				{
					return this._user_server;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._user_server = value;
				}
			}

			// Token: 0x17000DEA RID: 3562
			// (get) Token: 0x06002F39 RID: 12089 RVA: 0x000BB5D8 File Offset: 0x000B97D8
			public bool HasUser_server
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000DEB RID: 3563
			// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000BB5E8 File Offset: 0x000B97E8
			// (set) Token: 0x06002F3B RID: 12091 RVA: 0x000BB5F0 File Offset: 0x000B97F0
			public long facebook_bind
			{
				get
				{
					return this._facebook_bind;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._facebook_bind = value;
				}
			}

			// Token: 0x17000DEC RID: 3564
			// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000BB608 File Offset: 0x000B9808
			public bool HasFacebook_bind
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000DED RID: 3565
			// (get) Token: 0x06002F3D RID: 12093 RVA: 0x000BB618 File Offset: 0x000B9818
			// (set) Token: 0x06002F3E RID: 12094 RVA: 0x000BB620 File Offset: 0x000B9820
			public string versionCode
			{
				get
				{
					return this._versionCode;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._versionCode = value;
				}
			}

			// Token: 0x17000DEE RID: 3566
			// (get) Token: 0x06002F3F RID: 12095 RVA: 0x000BB638 File Offset: 0x000B9838
			public bool HasVersionCode
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x17000DEF RID: 3567
			// (get) Token: 0x06002F40 RID: 12096 RVA: 0x000BB648 File Offset: 0x000B9848
			// (set) Token: 0x06002F41 RID: 12097 RVA: 0x000BB650 File Offset: 0x000B9850
			public string dataVersionCode
			{
				get
				{
					return this._dataVersionCode;
				}
				set
				{
					this.has_field.set_field(6, true);
					this._dataVersionCode = value;
				}
			}

			// Token: 0x17000DF0 RID: 3568
			// (get) Token: 0x06002F42 RID: 12098 RVA: 0x000BB668 File Offset: 0x000B9868
			public bool HasDataVersionCode
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x17000DF1 RID: 3569
			// (get) Token: 0x06002F43 RID: 12099 RVA: 0x000BB678 File Offset: 0x000B9878
			// (set) Token: 0x06002F44 RID: 12100 RVA: 0x000BB680 File Offset: 0x000B9880
			public long downloadFlag
			{
				get
				{
					return this._downloadFlag;
				}
				set
				{
					this.has_field.set_field(7, true);
					this._downloadFlag = value;
				}
			}

			// Token: 0x17000DF2 RID: 3570
			// (get) Token: 0x06002F45 RID: 12101 RVA: 0x000BB698 File Offset: 0x000B9898
			public bool HasDownloadFlag
			{
				get
				{
					return this.has_field.has_field(7);
				}
			}

			// Token: 0x17000DF3 RID: 3571
			// (get) Token: 0x06002F46 RID: 12102 RVA: 0x000BB6A8 File Offset: 0x000B98A8
			// (set) Token: 0x06002F47 RID: 12103 RVA: 0x000BB6B0 File Offset: 0x000B98B0
			public string notice
			{
				get
				{
					return this._notice;
				}
				set
				{
					this.has_field.set_field(8, true);
					this._notice = value;
				}
			}

			// Token: 0x17000DF4 RID: 3572
			// (get) Token: 0x06002F48 RID: 12104 RVA: 0x000BB6C8 File Offset: 0x000B98C8
			public bool HasNotice
			{
				get
				{
					return this.has_field.has_field(8);
				}
			}

			// Token: 0x17000DF5 RID: 3573
			// (get) Token: 0x06002F49 RID: 12105 RVA: 0x000BB6D8 File Offset: 0x000B98D8
			// (set) Token: 0x06002F4A RID: 12106 RVA: 0x000BB6E0 File Offset: 0x000B98E0
			public string notice_version
			{
				get
				{
					return this._notice_version;
				}
				set
				{
					this.has_field.set_field(9, true);
					this._notice_version = value;
				}
			}

			// Token: 0x17000DF6 RID: 3574
			// (get) Token: 0x06002F4B RID: 12107 RVA: 0x000BB6F8 File Offset: 0x000B98F8
			public bool HasNotice_version
			{
				get
				{
					return this.has_field.has_field(9);
				}
			}

			// Token: 0x17000DF7 RID: 3575
			// (get) Token: 0x06002F4C RID: 12108 RVA: 0x000BB708 File Offset: 0x000B9908
			// (set) Token: 0x06002F4D RID: 12109 RVA: 0x000BB710 File Offset: 0x000B9910
			public string facebook_bind1
			{
				get
				{
					return this._facebook_bind1;
				}
				set
				{
					this.has_field.set_field(10, true);
					this._facebook_bind1 = value;
				}
			}

			// Token: 0x17000DF8 RID: 3576
			// (get) Token: 0x06002F4E RID: 12110 RVA: 0x000BB728 File Offset: 0x000B9928
			public bool HasFacebook_bind1
			{
				get
				{
					return this.has_field.has_field(10);
				}
			}

			// Token: 0x17000DF9 RID: 3577
			// (get) Token: 0x06002F4F RID: 12111 RVA: 0x000BB738 File Offset: 0x000B9938
			// (set) Token: 0x06002F50 RID: 12112 RVA: 0x000BB740 File Offset: 0x000B9940
			public string google_bind
			{
				get
				{
					return this._google_bind;
				}
				set
				{
					this.has_field.set_field(11, true);
					this._google_bind = value;
				}
			}

			// Token: 0x17000DFA RID: 3578
			// (get) Token: 0x06002F51 RID: 12113 RVA: 0x000BB758 File Offset: 0x000B9958
			public bool HasGoogle_bind
			{
				get
				{
					return this.has_field.has_field(11);
				}
			}

			// Token: 0x06002F52 RID: 12114 RVA: 0x000BB768 File Offset: 0x000B9968
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.state = this.deserialize.read_integer();
						break;
					case 1:
						this.session = this.deserialize.read_integer();
						break;
					case 2:
						this.game_server = this.deserialize.read_obj_list<game_server>();
						break;
					case 3:
						this.user_server = this.deserialize.read_string();
						break;
					case 4:
						this.facebook_bind = this.deserialize.read_integer();
						break;
					case 5:
						this.versionCode = this.deserialize.read_string();
						break;
					case 6:
						this.dataVersionCode = this.deserialize.read_string();
						break;
					case 7:
						this.downloadFlag = this.deserialize.read_integer();
						break;
					case 8:
						this.notice = this.deserialize.read_string();
						break;
					case 9:
						this.notice_version = this.deserialize.read_string();
						break;
					case 10:
						this.facebook_bind1 = this.deserialize.read_string();
						break;
					case 11:
						this.google_bind = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002F53 RID: 12115 RVA: 0x000BB8E4 File Offset: 0x000B9AE4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.session, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_obj<game_server>(this.game_server, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_string(this.user_server, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.facebook_bind, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_string(this.versionCode, 5);
				}
				if (this.has_field.has_field(6))
				{
					this.serialize.write_string(this.dataVersionCode, 6);
				}
				if (this.has_field.has_field(7))
				{
					this.serialize.write_integer(this.downloadFlag, 7);
				}
				if (this.has_field.has_field(8))
				{
					this.serialize.write_string(this.notice, 8);
				}
				if (this.has_field.has_field(9))
				{
					this.serialize.write_string(this.notice_version, 9);
				}
				if (this.has_field.has_field(10))
				{
					this.serialize.write_string(this.facebook_bind1, 10);
				}
				if (this.has_field.has_field(11))
				{
					this.serialize.write_string(this.google_bind, 11);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F57 RID: 8023
			private static int max_field_count = 12;

			// Token: 0x04001F58 RID: 8024
			private long _state;

			// Token: 0x04001F59 RID: 8025
			private long _session;

			// Token: 0x04001F5A RID: 8026
			private List<game_server> _game_server;

			// Token: 0x04001F5B RID: 8027
			private string _user_server;

			// Token: 0x04001F5C RID: 8028
			private long _facebook_bind;

			// Token: 0x04001F5D RID: 8029
			private string _versionCode;

			// Token: 0x04001F5E RID: 8030
			private string _dataVersionCode;

			// Token: 0x04001F5F RID: 8031
			private long _downloadFlag;

			// Token: 0x04001F60 RID: 8032
			private string _notice;

			// Token: 0x04001F61 RID: 8033
			private string _notice_version;

			// Token: 0x04001F62 RID: 8034
			private string _facebook_bind1;

			// Token: 0x04001F63 RID: 8035
			private string _google_bind;
		}
	}
}
