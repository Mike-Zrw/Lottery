--�û�
CREATE TABLE [BUser](
    [USE_ID] [int] IDENTITY(1,1)  primary key,
    [USE_NAME] [nvarchar](50) not null,--�ʺ�
    [USE_PASSWORD] [nvarchar](50)  null ,
    [USE_ACTIVITY] [bit] not null,		--�Ƿ����	
    USE_UGP_ID [int] not null
)
--�û������
Create Table BUserGroup(
	UGP_ID int IDENTITY(1,1) primary key,
	UGP_NAME varchar(20) not null, --ǰ̨�û� ��̨�û�
)
--ǰ̨�û���
Create Table BDeskUser(
	DUE_ID int IDENTITY(1,1) primary key,
	DUE_USE_ID int not null,
    DUE_REGISTTIME datetime not null,  --ע��ʱ��
    DUE_REALNAME varchar(10),--��ʵ����
    DUE_PHONE [nvarchar](20),  
    DUE_USERDSPNAME [nvarchar](50) ,--�ǳ�
    DUE_SEX int,
    DUE_EMAIL [nvarchar](100),
    DUE_SUT_ID [int] not null
)
--����Ա
Create Table BMasters(
	MST_ID int IDENTITY(1,1) primary key,
	MST_USE_ID int not null
)
--�û����ͱ�
CREATE TABLE [BUserType](
    SUT_ID [int] IDENTITY(1,1) primary key,
    SUT_DESCRIPTION [nvarchar](20) not null--������ͨ�û� ��Ա
)

--��ť��
CREATE TABLE [SPermission](
    PMS_ID int identity,
    PMS_DESCRIPTION [nvarchar](50) not null, --Ȩ������
    PMS_IMGURL [nvarchar](50), --��ťͼƬ
    PMS_FUNC [nvarchar](50), --��ť����
)
--��ɫ��
CREATE TABLE [SRoles](
    ROL_ID [int] IDENTITY primary key,
    ROL_DESCRIPTION [nvarchar](200) not null--��ɫ����
)

--�������ͱ�
Create Table SDealType(
	DAT_ID int identity(1,1) primary key,
	DAT_Name nvarchar(20) not null,--�����������ƣ����磺֧������
	DAT_ACTIVITY int not null--�Ƿ�����
) 
--�˵���
CREATE TABLE [SMenu](
    MNU_ID [int] IDENTITY(1,1) primary key,
    MNU_TEXTCN [nvarchar](100)  not null,--�˵���������ʾ
    MNU_TEXTEN [nvarchar](200)  NULL,--�˵���Ӣ������
    MNU_PARENTID [int] NULL,--���ڵ�
    MNU_SORT [int] NULL,--ͬһ�����ڵ����������
    MNU_URL [nvarchar](200) ,--�˵���ַ
    MNU_IMGURL [nvarchar](50) NULL--�˵�ͼƬ����
)
--�˵��Ͱ�ť�Ĺ�����
CREATE TABLE [SMenuPermission](
	MPM_ID [int] IDENTITY(1,1) primary key,
    MPM_MNU_ID int  NOT NULL,--�˵�ID
    MPM_PMS_ID int NOT NULL,--Ȩ��ID
)

--����Ա�ͽ�ɫ�Ĺ�����
CREATE TABLE [SMasterRoles](
	MTR_ID [int] IDENTITY(1,1) primary key,
    MTR_MST_USE_ID [int] NOT NULL,--�û�ID
    MTR_ROL_ID [int] not null ,--Ȩ��ID
) 
--��ɫ�Ͱ�ť�Ĺ�����
CREATE TABLE [SRolePermission](
	RPM_ID [int] IDENTITY(1,1) primary key,
    RPM_ROL_ID int  NOT NULL,--��ɫID
    RPM_PMS_ID int NOT NULL,--Ȩ��ID
)
--��ɫ�Ͳ˵��Ĺ�����
CREATE TABLE [SRoleMenu](
	RMU_ID [int] IDENTITY(1,1) primary key,
    RMU_ROL_ID int  NOT NULL,--��ɫID
    RMU_MNU_ID int NOT NULL,--�˵�ID
)

--�û����п���Ϣ��
Create Table BUserBankCard(
	UBC_ID int IDENTITY(1,1)  primary key,
	UBC_USE_ID int not null,
	UBC_BANKNAME varchar(50) not null,
	UBC_CARDNUMBER varchar(20) not null,
)
--�û�����
Create Table BUserMoney(
	USM_ID int identity(1,1) primary key,
	USM_USE_ID int not null,
	USM_MONEY decimal(18,2)not null,
)
--���ϴ���¼��
Create Table PayOnLine(
	POL_ID int IDENTITY(1,1)  primary key,
	POL_CREATETIME datetime not null,             --����ʱ��
	POL_DAT_ID int not null,   --��������
	POL_MONEY decimal(18,2) not null,
	POL_USE_ID int not null,
	POL_STATE varchar(10) not null,   --����״̬
	POL_CONFIRMTIME datetime,--����ȷ��ʱ��
)
--���䶯��¼��
Create Table BChangeMoney(
	CGM_ID int identity(1,1) primary key,
	CGM_USE_ID int  not null,
	CGM_MONEY decimal(18,2) not null,  --���ӻ���ٵĽ��
	CGM_SOURSE varchar(50), --���;��
	CGM_CREATETIME datetime not null,
	CGM_BEFOREMONEY decimal(18,2) not null,  --�䶯ǰ���
	CGM_AFTERMONEY decimal(18,2) not null,  --�䶯����
    CGM_DESC nvarchar(200),  --����������֧������ֵ
)
--ȡ�������
Create Table BDrawMoney(
	DAM_ID  int identity(1,1) primary key,
	DAM_DUE_ID int  not null,  --ȡ���û�id
	DAM_CREATETIME datetime not null,
	DAM_MONEY  decimal(18,2) not null,
	DAM_DESC varchar(200),--����
	DAW_STATE int not null, --0δ���� 1.�����ɹ� 2.����ʧ��
	DAW_MST_ID int not null,--��̨������
	DAW_CONFIRMTIME datetime --����ʱ��
)
--������־��
create Table BOperationLog(
	 OPL_ID  int identity(1,1) primary key,
	 OPL_USE_ID int,
	 OPL_TITLE nvarchar(50), --����
	 OPL_DESC NVARCHAR(200), --��������
	 OPL_CREATETIME datetime  --����ʱ��
)
--��½��־��
CREATE Table BLoginLog(
	LGL_ID int identity(1,1) primary key,
	LGL_USE_ID int ,
	LGL_LOGINTYPE nvarchar(10),--��½��ʽ ��������
	LGL_LOGINETIME datetime, --��¼ʱ��
    LGL_IP [nvarchar](30), --��½IP
)
--վ����
create table BMessage(
	MSG_ID int identity(1,1) primary key,
	MSG_TITLE varchar(80) not null,
	MSG_CONTENT nvarchar(2000) not null,
	MSG_SENDPERSON varchar(20) not null,--������
	MSG_CREATETIME datetime
)
--���ż�¼��
Create table PSendMsg(
	SMG_ID int identity(1,1) primary key,
	SMG_USE_ID int not null, --������ 0Ϊϵͳ
	SMG_MSG_ID int not null
)
--���ż�¼��
Create table PReceiveMsg(
	RMG_ID int identity(1,1) primary key,
	RMG_USE_ID int not null,--������
	RMG_MSG_ID int not null,
	RMG_READSTATE int not null, --�Ķ�״̬
)



-----------------��Ʊ����--------------------------
--ʱʱ�ʿ�����¼��--
create table BSSC(
	SSC_ID int identity(1,1) primary key,
	SSC_NO varchar(20), --�ں�
	SSC_NUMBER varchar(20),--������
	SSC_DATE date not null, --��������
	SSC_WRITEDT datetime, --д�����ڣ�ץȡ���ڣ�
	SSC_STATE int, --�ȶ�״̬  0 δ�ض� 1 �ѱȶ�
)
--ʱʱ���淨��---��һ�Ƕ������ǡ�������
create table RSSC_TYPE(
	RST_ID int identity(1,1) primary key,
	RST_NAME varchar(20) not null,
	RST_PARENT_ID int not null,--��id����������ֱѡ�ĸ�Ϊ����
)
--ʱʱ�ʹ����¼��--
CREATE TABLE BSSC_DUE_BUY(
	SCD_ID int identity(1,1) primary key,
	SCD_RST_ID int not null,
	SCD_DUE_ID int not null,
	SCD_DAT_ID int not null,--֧����ʽ
	SCD_DATE date not null,
	SCD_SCC_NO int not null,
	SCD_NUMBERS nvarchar(200) not null, --�����Ʊ���룬���Զ���Ĺ������ƴ��д��
	SCD_TIMES int not null, --���� Ĭ��1��
	SCD_STATE int not null, --�ȶ�״̬  0δ�ȶԣ�1 ���н� 2δ�н�
)

--ʱʱ���н���¼��--
CREATE TABLE BSSC_SUCCESS_BUY(
	SSB_ID int identity(1,1) primary key,
	SSB_DATE date not null,
	SSB_SCD_ID int not null,
	SSB_MONEY date not null,  --�н����
)