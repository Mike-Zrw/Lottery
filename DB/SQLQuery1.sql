--用户
CREATE TABLE [BUser](
    [USE_ID] [int] IDENTITY(1,1)  primary key,
    [USE_NAME] [nvarchar](50) not null,--帐号
    [USE_PASSWORD] [nvarchar](50)  null ,
    [USE_ACTIVITY] [bit] not null,		--是否可用	
    USE_UGP_ID [int] not null
)
--用户分组表
Create Table BUserGroup(
	UGP_ID int IDENTITY(1,1) primary key,
	UGP_NAME varchar(20) not null, --前台用户 后台用户
)
--前台用户表
Create Table BDeskUser(
	DUE_ID int IDENTITY(1,1) primary key,
	DUE_USE_ID int not null,
    DUE_REGISTTIME datetime not null,  --注册时间
    DUE_REALNAME varchar(10),--真实姓名
    DUE_PHONE [nvarchar](20),  
    DUE_USERDSPNAME [nvarchar](50) ,--昵称
    DUE_SEX int,
    DUE_EMAIL [nvarchar](100),
    DUE_SUT_ID [int] not null
)
--管理员
Create Table BMasters(
	MST_ID int IDENTITY(1,1) primary key,
	MST_USE_ID int not null
)
--用户类型表：
CREATE TABLE [BUserType](
    SUT_ID [int] IDENTITY(1,1) primary key,
    SUT_DESCRIPTION [nvarchar](20) not null--例：普通用户 会员
)

--按钮表：
CREATE TABLE [SPermission](
    PMS_ID int identity,
    PMS_DESCRIPTION [nvarchar](50) not null, --权限名称
    PMS_IMGURL [nvarchar](50), --按钮图片
    PMS_FUNC [nvarchar](50), --按钮函数
)
--角色表：
CREATE TABLE [SRoles](
    ROL_ID [int] IDENTITY primary key,
    ROL_DESCRIPTION [nvarchar](200) not null--角色名称
)

--交易类型表
Create Table SDealType(
	DAT_ID int identity(1,1) primary key,
	DAT_Name nvarchar(20) not null,--交易类型名称（例如：支付宝）
	DAT_ACTIVITY int not null--是否启用
) 
--菜单表
CREATE TABLE [SMenu](
    MNU_ID [int] IDENTITY(1,1) primary key,
    MNU_TEXTCN [nvarchar](100)  not null,--菜单的中文显示
    MNU_TEXTEN [nvarchar](200)  NULL,--菜单的英文名称
    MNU_PARENTID [int] NULL,--父节点
    MNU_SORT [int] NULL,--同一个父节点下面的排序
    MNU_URL [nvarchar](200) ,--菜单地址
    MNU_IMGURL [nvarchar](50) NULL--菜单图片链接
)
--菜单和按钮的关联表
CREATE TABLE [SMenuPermission](
	MPM_ID [int] IDENTITY(1,1) primary key,
    MPM_MNU_ID int  NOT NULL,--菜单ID
    MPM_PMS_ID int NOT NULL,--权限ID
)

--管理员和角色的关联表：
CREATE TABLE [SMasterRoles](
	MTR_ID [int] IDENTITY(1,1) primary key,
    MTR_MST_USE_ID [int] NOT NULL,--用户ID
    MTR_ROL_ID [int] not null ,--权限ID
) 
--角色和按钮的关联表
CREATE TABLE [SRolePermission](
	RPM_ID [int] IDENTITY(1,1) primary key,
    RPM_ROL_ID int  NOT NULL,--角色ID
    RPM_PMS_ID int NOT NULL,--权限ID
)
--角色和菜单的关联表
CREATE TABLE [SRoleMenu](
	RMU_ID [int] IDENTITY(1,1) primary key,
    RMU_ROL_ID int  NOT NULL,--角色ID
    RMU_MNU_ID int NOT NULL,--菜单ID
)

--用户银行卡信息表
Create Table BUserBankCard(
	UBC_ID int IDENTITY(1,1)  primary key,
	UBC_USE_ID int not null,
	UBC_BANKNAME varchar(50) not null,
	UBC_CARDNUMBER varchar(20) not null,
)
--用户余额表
Create Table BUserMoney(
	USM_ID int identity(1,1) primary key,
	USM_USE_ID int not null,
	USM_MONEY decimal(18,2)not null,
)
--线上存款记录表
Create Table PayOnLine(
	POL_ID int IDENTITY(1,1)  primary key,
	POL_CREATETIME datetime not null,             --创建时间
	POL_DAT_ID int not null,   --交易类型
	POL_MONEY decimal(18,2) not null,
	POL_USE_ID int not null,
	POL_STATE varchar(10) not null,   --订单状态
	POL_CONFIRMTIME datetime,--订单确认时间
)
--金额变动记录表
Create Table BChangeMoney(
	CGM_ID int identity(1,1) primary key,
	CGM_USE_ID int  not null,
	CGM_MONEY decimal(18,2) not null,  --增加或减少的金额
	CGM_SOURSE varchar(50), --存款途径
	CGM_CREATETIME datetime not null,
	CGM_BEFOREMONEY decimal(18,2) not null,  --变动前金额
	CGM_AFTERMONEY decimal(18,2) not null,  --变动后金额
    CGM_DESC nvarchar(200),  --描述，例：支付宝充值
)
--取款申请表
Create Table BDrawMoney(
	DAM_ID  int identity(1,1) primary key,
	DAM_DUE_ID int  not null,  --取款用户id
	DAM_CREATETIME datetime not null,
	DAM_MONEY  decimal(18,2) not null,
	DAM_DESC varchar(200),--描述
	DAW_STATE int not null, --0未审批 1.审批成功 2.审批失败
	DAW_MST_ID int not null,--后台审批人
	DAW_CONFIRMTIME datetime --审批时间
)
--操作日志表
create Table BOperationLog(
	 OPL_ID  int identity(1,1) primary key,
	 OPL_USE_ID int,
	 OPL_TITLE nvarchar(50), --标题
	 OPL_DESC NVARCHAR(200), --详情描述
	 OPL_CREATETIME datetime  --操作时间
)
--登陆日志表
CREATE Table BLoginLog(
	LGL_ID int identity(1,1) primary key,
	LGL_USE_ID int ,
	LGL_LOGINTYPE nvarchar(10),--登陆方式 例：电脑
	LGL_LOGINETIME datetime, --登录时间
    LGL_IP [nvarchar](30), --登陆IP
)
--站内信
create table BMessage(
	MSG_ID int identity(1,1) primary key,
	MSG_TITLE varchar(80) not null,
	MSG_CONTENT nvarchar(2000) not null,
	MSG_SENDPERSON varchar(20) not null,--发送者
	MSG_CREATETIME datetime
)
--发信记录表
Create table PSendMsg(
	SMG_ID int identity(1,1) primary key,
	SMG_USE_ID int not null, --发信人 0为系统
	SMG_MSG_ID int not null
)
--收信记录表
Create table PReceiveMsg(
	RMG_ID int identity(1,1) primary key,
	RMG_USE_ID int not null,--收信人
	RMG_MSG_ID int not null,
	RMG_READSTATE int not null, --阅读状态
)



-----------------彩票号码--------------------------
--时时彩开奖记录表--
create table BSSC(
	SSC_ID int identity(1,1) primary key,
	SSC_NO varchar(20), --期号
	SSC_NUMBER varchar(20),--开奖号
	SSC_DATE date not null, --开奖日期
	SSC_WRITEDT datetime, --写入日期（抓取日期）
	SSC_STATE int, --比对状态  0 未必对 1 已比对
)
--时时彩玩法表---（一星二星三星。。。）
create table RSSC_TYPE(
	RST_ID int identity(1,1) primary key,
	RST_NAME varchar(20) not null,
	RST_PARENT_ID int not null,--父id，例如三星直选的父为三星
)
--时时彩购买记录表--
CREATE TABLE BSSC_DUE_BUY(
	SCD_ID int identity(1,1) primary key,
	SCD_RST_ID int not null,
	SCD_DUE_ID int not null,
	SCD_DAT_ID int not null,--支付方式
	SCD_DATE date not null,
	SCD_SCC_NO int not null,
	SCD_NUMBERS nvarchar(200) not null, --购买彩票号码，以自定义的规则进行拼接写入
	SCD_TIMES int not null, --倍数 默认1倍
	SCD_STATE int not null, --比对状态  0未比对，1 已中奖 2未中奖
)

--时时彩中奖纪录表--
CREATE TABLE BSSC_SUCCESS_BUY(
	SSB_ID int identity(1,1) primary key,
	SSB_DATE date not null,
	SSB_SCD_ID int not null,
	SSB_MONEY date not null,  --中奖金额
)