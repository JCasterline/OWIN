--<UP>
CREATE TABLE [Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Name] [varchar](150) NULL,
	[PasswordHash] [varchar](max) NULL,
	[SecurityStamp] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[EmailConfirmed] [bit] NULL,
	[PhoneNumber] [varchar](11) NULL,
	[PhoneNumberConfirmed] [bit] NULL,
	[TwoFactorEnabled] [bit] NULL,
	[AccessFailedCount] [int] NULL,
	[IsLockedOut] [bit] NULL,
	[LockoutEndDateTimeOffset] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
));

CREATE TABLE [Claims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](255) NOT NULL,
	[Value] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CLaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
));

CREATE TABLE [UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [varchar](255) NOT NULL,
	[ClaimValue] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
));

CREATE TABLE [UserLogins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[LoginProvider] [varchar](255) NULL,
	[ProviderKey] [varchar](255) NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
));
--</UP>

--<DOWN>
DROP TABLE [UserClaims];
DROP TABLE [UserLogins];
DROP TABLE [Claims];
DROP TABLE [Users];
--</DOWN>