--drop database PhanPhoiVeSo
create database PhanPhoiVeSo
go
use PhanPhoiVeSo

create table LoaiVeSo
(
	LoaiVeSoId int IDENTITY(1,1) PRIMARY KEY,
	TenTinh nvarchar(120) not null
)

create table DaiLy
(
	DaiLyId int IDENTITY(1,1) PRIMARY KEY,
	TenDaiLy nvarchar(120) not null
)

create table PhieuDangKy
(
	PhieuDangKyId int IDENTITY(1,1) PRIMARY KEY,
	DaiLyId int FOREIGN KEY REFERENCES DaiLy(DaiLyId) not null,
	NgayDangKy date not null,
	SLDangKy decimal not null
)

create table PhieuPhatHanh
(
	DaiLyId int FOREIGN KEY REFERENCES DaiLy(DaiLyId) not null,
	LoaiVeSoId int FOREIGN KEY REFERENCES LoaiVeSo(LoaiVeSoId) not null,
	NgayPhat date not null,
	SLPhat decimal not null,
	SLBanDuoc decimal
)