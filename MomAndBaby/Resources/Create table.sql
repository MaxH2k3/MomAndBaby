CREATE TABLE [Role] (
	[id] int PRIMARY KEY IDENTITY(1, 1),
	[name] varchar(50)
)

CREATE TABLE [Status] (
	[id] int primary key identity(1, 1),
	[name] varchar(50)
)

CREATE TABLE [Category] (
    [id] int primary key identity(1, 1),
    [name] varchar(255)
)

CREATE TABLE [User] (
[id] uniqueidentifier PRIMARY KEY,
[username] NVARCHAR(255) UNIQUE NOT NULL,
[password] VARBINARY(MAX) ,
[passwordSalt] VARBINARY(MAX),
[email] VARCHAR(255) UNIQUE NOT NULL,
[full_name] NVARCHAR(255),
[phone_number] VARCHAR(20),
[address] NVARCHAR(MAX),
[role_id] int,
[created_at] DATETIME DEFAULT (GETDATE()),
[updated_at] DATETIME DEFAULT (GETDATE()),
[status] VARCHAR(50),
FOREIGN KEY ([role_id]) REFERENCES role
)

 CREATE TABLE [UserValidation] (
	[id] int primary key identity(1, 1),
	[user_id] uniqueidentifier,
	[otp] VARCHAR(6),
	[created_at] DATETIME DEFAULT (GETDATE()),
	[expired_at] DATETIME DEFAULT (GETDATE()),
	FOREIGN KEY ([user_id]) REFERENCES [User] ([id])
 )

CREATE TABLE [Product] (
[id] uniqueidentifier PRIMARY KEY,
[category_id] int references Category([id]),
[name] NVARCHAR(255) NOT NULL,
[description] NVARCHAR(MAX),
[unit_price] DECIMAL(10,2) NOT NULL,
[purchase_price] DECIMAL(10, 2) NOT NULL,
[stock] INT NOT NULL,
[image] VARCHAR(max),
[original] VARCHAR(255),
[company] VARCHAR(255),
[created_at] DATETIME DEFAULT (GETDATE()),
[updated_at] DATETIME DEFAULT (GETDATE()),
[status] varchar(20)
)

CREATE TABLE [Order] (
[id] INT PRIMARY KEY IDENTITY(1, 1),
[user_id] uniqueidentifier,
[order_date] DATETIME DEFAULT (GETDATE()),
[total_amount] DECIMAL(10,2) NOT NULL,
[payment_method] VARCHAR(255),
[status_id] int,
[shipping_address] NVARCHAR(MAX),
[voucher_id] int,
FOREIGN KEY ([user_id]) REFERENCES [User] ([id]),
FOREIGN KEY ([status_id]) REFERENCES [Status] ([id])
)

CREATE TABLE [Order_Detail] (
[id] INT PRIMARY KEY IDENTITY(1, 1),
[order_id] int,
[product_id] uniqueidentifier,
[quantity] INT NOT NULL,
[price] DECIMAL(10,2) NOT NULL,
FOREIGN KEY ([order_id]) REFERENCES [Order] ([id]) ON DELETE CASCADE,
FOREIGN KEY ([product_id]) REFERENCES [Product] ([id])
)

CREATE TABLE [Review] (
[id] INT PRIMARY KEY IDENTITY(1, 1),
[user_id] uniqueidentifier,
[product_id] uniqueidentifier,
[rating] INT NOT NULL,
[comment] NVARCHAR(MAX),
[created_at] DATETIME DEFAULT (GETDATE()),
[Status] bit,
FOREIGN KEY ([user_id]) REFERENCES [User] ([id]),
FOREIGN KEY ([product_id]) REFERENCES [Product] ([id])
)

CREATE TABLE [Article] (
[id] INT PRIMARY KEY IDENTITY(1, 1),
[title] NVARCHAR(255) NOT NULL,
[content] NVARCHAR(MAX),
[author_id] uniqueidentifier,
[created_at] DATETIME DEFAULT (GETDATE()),
FOREIGN KEY ([author_id]) REFERENCES [User] ([id])
)

CREATE TABLE [Voucher] (
[id] INT PRIMARY KEY IDENTITY(1, 1),
[code] VARCHAR(20) UNIQUE NOT NULL,
[discount] DECIMAL(5,2) NOT NULL,
[start_date] DATE NOT NULL,
[end_date] DATE NOT NULL,
[created_by] uniqueidentifier,
[amount] int,
FOREIGN KEY ([created_by]) REFERENCES [User] ([id])
)

CREATE TABLE [Message] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [sender_id] uniqueidentifier references [User](id),
  [receiver_id] uniqueidentifier,-- user này đang nhắn vào group nào
  [is_system] bit,
  [content] varchar(max),
  [created_at] datetime
)

CREATE TABLE [Order_Tracking] (
[id] int primary key identity(1, 1),
[order_id] int,
[order_confirmation] datetime,
[package] datetime,
[delivery] datetime,
[complete] datetime,
FOREIGN KEY ([order_id]) REFERENCES [Order] ([id])
)

CREATE TABLE [Gift] (
[id] int PRIMARY KEY IDENTITY(1, 1),
[name] varchar(255),
[stock] int,
[exchange_point] int,
)