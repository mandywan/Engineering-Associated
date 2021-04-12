/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     2/26/2021 10:44:04 PM                        */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CompanyOfficeGroup') and o.name = 'FK_COMPANYO_REFERENCE_OFFICE')
alter table CompanyOfficeGroup
   drop constraint FK_COMPANYO_REFERENCE_OFFICE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Employee') and o.name = 'FK_EMPLOYEE_REFERENCE_OFFICE')
alter table Employee
   drop constraint FK_EMPLOYEE_REFERENCE_OFFICE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Employee') and o.name = 'FK_EMPLOYEE_REFERENCE_SUPERVISOR_EMPLOYEE')
alter table Employee
   drop constraint FK_EMPLOYEE_REFERENCE_SUPERVISOR_EMPLOYEE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Employee') and o.name = 'FK_EMPLOYEE_REFERENCE_COMPANY')
alter table Employee
   drop constraint FK_EMPLOYEE_REFERENCE_COMPANY
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Employee') and o.name = 'FK_EMPLOYEE_REFERENCE_COMPANYO')
alter table Employee
   drop constraint FK_EMPLOYEE_REFERENCE_COMPANYO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Employee') and o.name = 'FK_EMPLOYEE_REFERENCE_LOCATION')
alter table Employee
   drop constraint FK_EMPLOYEE_REFERENCE_LOCATION
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EmployeeSkills') and o.name = 'FK_EMPLOYEE_REFERENCE_SKILL')
alter table EmployeeSkills
   drop constraint FK_EMPLOYEE_REFERENCE_SKILL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EmployeeSkills') and o.name = 'FK_EMPLOYEE_REFERENCE_EMPLOYEE')
alter table EmployeeSkills
   drop constraint FK_EMPLOYEE_REFERENCE_EMPLOYEE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Office') and o.name = 'FK_OFFICE_REFERENCE_COMPANY')
alter table Office
   drop constraint FK_OFFICE_REFERENCE_COMPANY
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Skill') and o.name = 'FK_SKILL_REFERENCE_CATEGORY')
alter table Skill
   drop constraint FK_SKILL_REFERENCE_CATEGORY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Admin')
            and   type = 'U')
   drop table Admin
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Category')
            and   type = 'U')
   drop table Category
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Company')
            and   type = 'U')
   drop table Company
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CompanyOfficeGroup')
            and   type = 'U')
   drop table CompanyOfficeGroup
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Employee')
            and   type = 'U')
   drop table Employee
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EmployeeSkills')
            and   type = 'U')
   drop table EmployeeSkills
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Location')
            and   type = 'U')
   drop table Location
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Office')
            and   type = 'U')
   drop table Office
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Skill')
            and   type = 'U')
   drop table Skill
go

/*==============================================================*/
/* Table: Admin                                                 */
/*==============================================================*/
create table Admin (
   AdminID              int                  identity,
   Username             varchar(30)          null,
   Password             varchar(Max)         null,
   constraint PK_ADMIN primary key (AdminID)
)
go

/*==============================================================*/
/* Table: Category                                              */
/*==============================================================*/
create table Category (
   SkillCategoryId      VARCHAR(32)          not null,
   Label                VARCHAR(100)         not null,
   SortValue            VARCHAR(10)          not null,
   constraint pkSkillCategory primary key (SkillCategoryId)
)
go

/*==============================================================*/
/* Table: Company                                               */
/*==============================================================*/
create table Company (
   CompanyCode          VARCHAR(2)           not null,
   Label                VARCHAR(20)          not null,
   ManagerEmployeeNumber int                  not null,
   constraint pkLocationCompany primary key (CompanyCode)
)
go

/*==============================================================*/
/* Table: CompanyOfficeGroup                                    */
/*==============================================================*/
create table CompanyOfficeGroup (
   CompanyCode          VARCHAR(2)           not null,
   OfficeCode           VARCHAR(2)           not null,
   GroupCode            VARCHAR(2)           not null,
   Label                VARCHAR(20)          not null,
   ManagerEmployeeNumber int                  not null,
   constraint pkLocationGroup primary key (CompanyCode, OfficeCode, GroupCode)
)
go

/*==============================================================*/
/* Table: Employee                                              */
/*==============================================================*/
create table Employee (
   isContractor         bit                  not null,
   SupervisorEmployeeNumber int                  null,
   EmployeeNumber       int                  identity,
   CompanyCode          VARCHAR(2)           not null,
   OfficeCode           VARCHAR(2)           not null,
   GroupCode            VARCHAR(2)           not null,
   LocationId           VARCHAR(20)          not null,
   LastName             VARCHAR(50)          null,
   FirstName            VARCHAR(25)          null,
   EmploymentType       VARCHAR(10)          null,
   Title                VARCHAR(50)          null,
   HireDate             DATE                 null,
   TerminationDate      DATE                 null,
   YearsPriorExperience NUMERIC(3,1)         null,
   Email                VARCHAR(50)          null,
   WorkPhone            VARCHAR(24)          null,
   WorkCell             VARCHAR(24)          null,
   PhotoUrl             VARCHAR(255)         constraint pkEmployee null,
   Bio                  varchar(Max)         null,
   ExtraInfo            varchar(Max)         null,
   constraint PK_EMPLOYEE primary key (EmployeeNumber)
)
go

/*==============================================================*/
/* Table: EmployeeSkills                                        */
/*==============================================================*/
create table EmployeeSkills (
   EmployeeNumber       int                  not null,
   SkillId              VARCHAR(32)          not null,
   constraint pkEmployeeSkills primary key (EmployeeNumber, SkillId)
)
go

/*==============================================================*/
/* Table: Location                                              */
/*==============================================================*/
create table Location (
   LocationId           VARCHAR(20)          not null,
   Label                VARCHAR(100)         not null,
   SortValue            VARCHAR(100)         not null,
   constraint pkLocationPhysical primary key (LocationId)
)
go

/*==============================================================*/
/* Table: Office                                                */
/*==============================================================*/
create table Office (
   CompanyCode          VARCHAR(2)           not null,
   OfficeCode           VARCHAR(2)           not null,
   Label                VARCHAR(20)          not null,
   ManagerEmployeeNumber int                  not null,
   constraint pkLocationOffice primary key (CompanyCode, OfficeCode)
)
go

/*==============================================================*/
/* Table: Skill                                                 */
/*==============================================================*/
create table Skill (
   SkillCategoryId      VARCHAR(32)          not null,
   SkillId              VARCHAR(32)          not null,
   Label                VARCHAR(100)         not null,
   SortValue            VARCHAR(10)          not null,
   constraint pkSkill primary key (SkillId)
)
go

alter table CompanyOfficeGroup
   add constraint FK_COMPANYO_REFERENCE_OFFICE foreign key (CompanyCode, OfficeCode)
      references Office (CompanyCode, OfficeCode)
go

alter table Employee
   add constraint FK_EMPLOYEE_REFERENCE_OFFICE foreign key (CompanyCode, OfficeCode)
      references Office (CompanyCode, OfficeCode)
go

alter table Employee
   add constraint FK_EMPLOYEE_REFERENCE_SUPERVISOR_EMPLOYEE foreign key (SupervisorEmployeeNumber)
      references Employee (EmployeeNumber)
go

alter table Employee
   add constraint FK_EMPLOYEE_REFERENCE_COMPANY foreign key (CompanyCode)
      references Company (CompanyCode)
go

alter table Employee
   add constraint FK_EMPLOYEE_REFERENCE_COMPANYO foreign key (CompanyCode, OfficeCode, GroupCode)
      references CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode)
go

alter table Employee
   add constraint FK_EMPLOYEE_REFERENCE_LOCATION foreign key (LocationId)
      references Location (LocationId)
go

alter table EmployeeSkills
   add constraint FK_EMPLOYEE_REFERENCE_SKILL foreign key (SkillId)
      references Skill (SkillId)
go

alter table EmployeeSkills
   add constraint FK_EMPLOYEE_REFERENCE_EMPLOYEE foreign key (EmployeeNumber)
      references Employee (EmployeeNumber)
go

alter table Office
   add constraint FK_OFFICE_REFERENCE_COMPANY foreign key (CompanyCode)
      references Company (CompanyCode)
go

alter table Skill
   add constraint FK_SKILL_REFERENCE_CATEGORY foreign key (SkillCategoryId)
      references Category (SkillCategoryId)
go

