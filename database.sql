USE [MyWebsite]
GO
/****** Object:  Table [dbo].[tbl_support]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_support](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[yahoo] [varchar](255) NULL,
	[skype] [varchar](255) NULL,
	[phone] [varchar](13) NULL,
	[image] [varchar](255) NULL,
 CONSTRAINT [PK_tbl_support] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_size]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_size](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[status] [tinyint] NULL,
	[date_added] [datetime] NULL,
	[last_modified] [datetime] NULL,
 CONSTRAINT [PK_tbl_size] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_shop_config]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_shop_config](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[address] [nvarchar](255) NULL,
	[hotline] [nvarchar](255) NULL,
	[fax] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[link_facebook] [nvarchar](255) NULL,
	[title] [nvarchar](255) NULL,
	[description] [nvarchar](255) NULL,
	[keyword] [varchar](255) NULL,
 CONSTRAINT [PK_tbl_shop_config] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_producer]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_producer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[detail_short] [ntext] NULL,
	[detail] [ntext] NULL,
	[subject] [varchar](255) NULL,
	[image] [varchar](255) NULL,
	[status] [tinyint] NULL,
	[hot] [tinyint] NULL,
	[sort] [int] NULL,
	[date_added] [datetime] NULL,
	[last_modified] [datetime] NULL,
 CONSTRAINT [PK_tbl_producer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_module]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_module](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name_visible] [nvarchar](255) NULL,
	[name_partial] [varchar](255) NULL,
	[type] [int] NULL,
	[date_added] [datetime] NULL,
	[last_modified] [datetime] NULL,
 CONSTRAINT [PK_tbl_module] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_member]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_member](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[password] [varchar](255) NULL,
	[phone] [varchar](255) NULL,
	[email] [varchar](255) NULL,
	[address] [varchar](255) NULL,
	[image] [varchar](255) NULL,
	[birthday] [datetime] NULL,
	[gender] [tinyint] NULL,
	[status] [tinyint] NULL,
	[date_added] [datetime] NULL,
	[last_modified] [datetime] NULL,
 CONSTRAINT [PK_tbl_member] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_news_category]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_news_category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[parent] [int] NULL,
	[detail_short] [ntext] NULL,
	[subject] [varchar](255) NULL,
	[image] [varchar](255) NULL,
	[link] [varchar](255) NULL,
	[status] [tinyint] NULL,
	[hot] [tinyint] NULL,
	[sort] [int] NULL,
	[date_added] [datetime] NULL,
	[last_modified] [datetime] NULL,
	[title] [nvarchar](255) NULL,
	[description] [nvarchar](255) NULL,
	[keyword] [varchar](255) NULL,
 CONSTRAINT [PK_tbl_news_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_images]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_images](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[type] [int] NULL,
	[detail_short] [ntext] NULL,
	[image] [varchar](255) NULL,
	[link] [varchar](255) NULL,
	[status] [tinyint] NULL,
	[sort] [int] NULL,
	[date_added] [datetime] NULL,
	[last_modified] [datetime] NULL,
 CONSTRAINT [PK_tbl_images] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_content]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_content](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name_code] [nvarchar](255) NULL,
	[detail] [ntext] NULL,
 CONSTRAINT [PK_tbl_content] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_color]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_color](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[image] [varchar](255) NULL,
	[color_code] [varchar](255) NULL,
	[status] [tinyint] NULL,
	[date_added] [datetime] NULL,
	[last_modified] [datetime] NULL,
 CONSTRAINT [PK_tbl_color] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_admin]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_admin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[username] [varchar](255) NULL,
	[password] [varchar](255) NULL,
	[email] [varchar](255) NULL,
	[level_admin] [int] NULL,
	[date_added] [datetime] NULL,
	[last_modified] [datetime] NULL,
 CONSTRAINT [PK_tbl_admin] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_item_category]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_item_category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[parent] [int] NULL,
	[detail_short] [ntext] NULL,
	[subject] [varchar](255) NULL,
	[image] [varchar](255) NULL,
	[link] [varchar](255) NULL,
	[status] [tinyint] NULL,
	[hot] [tinyint] NULL,
	[sort] [int] NULL,
	[date_added] [datetime] NULL,
	[last_modified] [datetime] NULL,
	[title] [nvarchar](255) NULL,
	[description] [nvarchar](255) NULL,
	[keyword] [varchar](255) NULL,
 CONSTRAINT [PK__tbl_item__3213E83F09DE7BCC] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_item]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_item](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[parent] [int] NULL,
	[detail_short] [ntext] NULL,
	[detail] [ntext] NULL,
	[subject] [varchar](255) NULL,
	[online_payment] [ntext] NULL,
	[price] [bigint] NULL,
	[promotion_price] [bigint] NULL,
	[image] [varchar](255) NULL,
	[link] [varchar](255) NULL,
	[status] [tinyint] NULL,
	[hot] [tinyint] NULL,
	[sort] [int] NULL,
	[view_amount] [int] NULL,
	[buy_amount] [int] NULL,
	[date_added] [datetime] NULL,
	[last_modified] [datetime] NULL,
	[title] [nvarchar](255) NULL,
	[description] [nvarchar](255) NULL,
	[keyword] [varchar](255) NULL,
 CONSTRAINT [PK__tbl_item__3213E83F060DEAE8] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_order]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_customer] [int] NULL,
	[name_customer] [varchar](255) NULL,
	[email_customer] [varchar](255) NULL,
	[address_customer] [varchar](255) NULL,
	[phone_customer] [varchar](255) NULL,
	[name_receiver] [varchar](255) NULL,
	[email_receiver] [varchar](255) NULL,
	[address_receiver] [varchar](255) NULL,
	[phone_receiver] [varchar](255) NULL,
	[total_amount] [bigint] NULL,
	[note] [ntext] NULL,
	[status] [tinyint] NULL,
	[curency] [tinyint] NULL,
	[date_added] [datetime] NULL,
	[last_modified] [datetime] NULL,
 CONSTRAINT [PK_tbl_order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_news]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_news](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[parent] [int] NULL,
	[detail_short] [ntext] NULL,
	[detail] [ntext] NULL,
	[subject] [varchar](255) NULL,
	[image] [varchar](255) NULL,
	[link] [varchar](255) NULL,
	[status] [tinyint] NULL,
	[hot] [tinyint] NULL,
	[sort] [int] NULL,
	[view_amount] [int] NULL,
	[date_added] [datetime] NULL,
	[last_modified] [datetime] NULL,
	[title] [nvarchar](255) NULL,
	[description] [nvarchar](255) NULL,
	[keyword] [varchar](255) NULL,
 CONSTRAINT [PK_tbl_news] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_order_detail]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_order_detail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_order] [int] NULL,
	[id_product] [int] NULL,
	[quantity] [int] NULL,
	[price] [bigint] NULL,
 CONSTRAINT [PK_tbl_order_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_item_size]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_item_size](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_product] [int] NULL,
	[id_size] [int] NULL,
 CONSTRAINT [PK_tbl_item_size] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_item_color]    Script Date: 08/10/2016 02:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_item_color](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_product] [int] NULL,
	[id_color] [int] NULL,
 CONSTRAINT [PK_tbl_item_color] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_tbl_item_tbl_item_category]    Script Date: 08/10/2016 02:32:39 ******/
ALTER TABLE [dbo].[tbl_item]  WITH CHECK ADD  CONSTRAINT [FK_tbl_item_tbl_item_category] FOREIGN KEY([parent])
REFERENCES [dbo].[tbl_item_category] ([id])
GO
ALTER TABLE [dbo].[tbl_item] CHECK CONSTRAINT [FK_tbl_item_tbl_item_category]
GO
/****** Object:  ForeignKey [FK_tbl_item_color_tbl_color]    Script Date: 08/10/2016 02:32:39 ******/
ALTER TABLE [dbo].[tbl_item_color]  WITH CHECK ADD  CONSTRAINT [FK_tbl_item_color_tbl_color] FOREIGN KEY([id_color])
REFERENCES [dbo].[tbl_color] ([id])
GO
ALTER TABLE [dbo].[tbl_item_color] CHECK CONSTRAINT [FK_tbl_item_color_tbl_color]
GO
/****** Object:  ForeignKey [FK_tbl_item_color_tbl_item]    Script Date: 08/10/2016 02:32:39 ******/
ALTER TABLE [dbo].[tbl_item_color]  WITH CHECK ADD  CONSTRAINT [FK_tbl_item_color_tbl_item] FOREIGN KEY([id_product])
REFERENCES [dbo].[tbl_item] ([id])
GO
ALTER TABLE [dbo].[tbl_item_color] CHECK CONSTRAINT [FK_tbl_item_color_tbl_item]
GO
/****** Object:  ForeignKey [FK_tbl_item_size_tbl_item]    Script Date: 08/10/2016 02:32:39 ******/
ALTER TABLE [dbo].[tbl_item_size]  WITH CHECK ADD  CONSTRAINT [FK_tbl_item_size_tbl_item] FOREIGN KEY([id_product])
REFERENCES [dbo].[tbl_item] ([id])
GO
ALTER TABLE [dbo].[tbl_item_size] CHECK CONSTRAINT [FK_tbl_item_size_tbl_item]
GO
/****** Object:  ForeignKey [FK_tbl_item_size_tbl_size]    Script Date: 08/10/2016 02:32:39 ******/
ALTER TABLE [dbo].[tbl_item_size]  WITH CHECK ADD  CONSTRAINT [FK_tbl_item_size_tbl_size] FOREIGN KEY([id_size])
REFERENCES [dbo].[tbl_size] ([id])
GO
ALTER TABLE [dbo].[tbl_item_size] CHECK CONSTRAINT [FK_tbl_item_size_tbl_size]
GO
/****** Object:  ForeignKey [FK_tbl_news_tbl_news_category]    Script Date: 08/10/2016 02:32:39 ******/
ALTER TABLE [dbo].[tbl_news]  WITH CHECK ADD  CONSTRAINT [FK_tbl_news_tbl_news_category] FOREIGN KEY([parent])
REFERENCES [dbo].[tbl_news_category] ([id])
GO
ALTER TABLE [dbo].[tbl_news] CHECK CONSTRAINT [FK_tbl_news_tbl_news_category]
GO
/****** Object:  ForeignKey [FK_tbl_order_tbl_member]    Script Date: 08/10/2016 02:32:39 ******/
ALTER TABLE [dbo].[tbl_order]  WITH CHECK ADD  CONSTRAINT [FK_tbl_order_tbl_member] FOREIGN KEY([id_customer])
REFERENCES [dbo].[tbl_member] ([id])
GO
ALTER TABLE [dbo].[tbl_order] CHECK CONSTRAINT [FK_tbl_order_tbl_member]
GO
/****** Object:  ForeignKey [FK_tbl_order_detail_tbl_item]    Script Date: 08/10/2016 02:32:39 ******/
ALTER TABLE [dbo].[tbl_order_detail]  WITH CHECK ADD  CONSTRAINT [FK_tbl_order_detail_tbl_item] FOREIGN KEY([id_product])
REFERENCES [dbo].[tbl_item] ([id])
GO
ALTER TABLE [dbo].[tbl_order_detail] CHECK CONSTRAINT [FK_tbl_order_detail_tbl_item]
GO
/****** Object:  ForeignKey [FK_tbl_order_detail_tbl_order]    Script Date: 08/10/2016 02:32:39 ******/
ALTER TABLE [dbo].[tbl_order_detail]  WITH CHECK ADD  CONSTRAINT [FK_tbl_order_detail_tbl_order] FOREIGN KEY([id_order])
REFERENCES [dbo].[tbl_order] ([id])
GO
ALTER TABLE [dbo].[tbl_order_detail] CHECK CONSTRAINT [FK_tbl_order_detail_tbl_order]
GO
