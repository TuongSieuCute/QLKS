create database QLKS

use QLKS

create table TaiKhoan
(
    TenTk varchar(20) primary key,
	MatKhau varchar(20),
)

insert into TaiKhoan
VALUES
('ql', '1'),
('nv', '2')

create table KhachHang
(
	IdKh varchar(10) primary key,
	TenKh nvarchar(50),
	NgaySinh date,
	GioiTinh nvarchar(5),
	DiaChi nvarchar(50),
	QuocGia nvarchar(50),
	NgayThue date,
)
insert into KhachHang
VALUES
('KH01', N'Nguyễn Văn Lâm', '2002-11-19', N'Nam', N'Hà Nội', N'Việt Nam', '2022-11-19'),
('KH02', N'Trần Thị Kim', '2002-11-19', N'Nữ', N'Cà Mau', N'Việt Nam', '2022-11-19'),
('KH03', N'Huỳnh Hữu Châu', '2002-11-19', N'Nam', N'Đồng Nai', N'Việt Nam', '2022-11-19')

create table Phong
(
	IdP varchar(10) primary key,
	TenP varchar(10),
	LoaiP nvarchar(20),
	KieuP nvarchar(20),
	DonGia int,
	TinhTrang nvarchar(20),
)

insert into Phong
VALUES
('P101', '101', N'Thường', N'Giường đơn', '300000', N'Còn trống'),
('P102', '102', N'Thường', N'Giường đơn', '300000', N'Đã thuê'),
('P103', '103', N'Thường', N'Giường đơn', '300000', N'Còn trống'),
('P104', '104', N'Thường', N'Giường đơn', '300000', N'Đã thuê'),
('P105', '105', N'Thường', N'Giường đơn', '300000', N'Còn trống'),
('P106', '106', N'Thường', N'Giường đơn', '300000', N'Đã thuê'),
('P107', '107', N'Thường', N'Giường đơn', '300000', N'Còn trống'),

('P201', '201', N'Thường', N'Giường đôi', '400000', N'Còn trống'),
('P202', '202', N'Thường', N'Giường đôi', '400000', N'Đã thuê'),
('P203', '203', N'Thường', N'Giường đôi', '400000', N'Còn trống'),
('P204', '204', N'Thường', N'Giường đôi', '400000', N'Đã thuê'),
('P205', '205', N'Thường', N'Giường đôi', '400000', N'Còn trống'),
('P206', '206', N'Thường', N'Giường đôi', '400000', N'Đã thuê'),

('P301', '301', N'Sang', N'Giường đơn', '700000', N'Còn trống'),
('P302', '302', N'Sang', N'Giường đơn', '700000', N'Đã thuê'),
('P303', '303', N'Sang', N'Giường đơn', '700000', N'Còn trống'),
('P304', '304', N'Sang', N'Giường đơn', '700000', N'Đã thuê'),

('P401', '401', N'VIP', N'Giường đôi', '1000000', N'Còn trống'),
('P402', '402', N'VIP', N'Giường đôi', '1000000', N'Còn trống'),
('P403', '403', N'VIP', N'Giường đôi', '1000000', N'Còn trống')

create table DatPhong
(
	IdDp varchar(10) primary key,
	IdKh varchar(10),
	IdP varchar(10),
	NgayDen date,
	NgayDi date,
	
	FOREIGN KEY(IdKh) REFERENCES KhachHang(IdKh),
	FOREIGN KEY(IdP) REFERENCES Phong(IdP),
)
insert into DatPhong
VALUES
('DP01', 'KH01', 'P101', '2022-12-15', '2022-12-15'),
('DP02', 'KH02', 'P102', '2022-12-15', '2022-12-15'),
('DP03', 'KH03', 'P103', '2022-12-15', '2022-12-15')