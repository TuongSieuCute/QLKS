create database QLKS

use QLKS 

create table TaiKhoan
(
    TenTk varchar(20) primary key,
	MatKhau varchar(20),
)

insert into TaiKhoan
values
('nv01', 1),
('nv02', 1),
('nv03', 1)

create table KhachHang
(
	IdKh varchar(10) primary key,
	TenKh nvarchar(50),
	NgaySinh date,
	GioiTinh nvarchar(5),
	Sdt varchar(15),
	DiaChi nvarchar(50),
	QuocGia nvarchar(50),
	IdP varchar(10) unique,
	NgayDen date,
	NgayDi date,

	FOREIGN KEY(IdP) REFERENCES Phong(IdP),
)
insert into KhachHang
values
('KH01', N'Nguyễn Văn Lâm', '2002-11-19', N'Nam','0393267354', N'Hà Nội', N'Việt Nam', '101', '2023-01-01', '2023-01-02'),
('KH02', N'Trần Thị Kim', '2002-11-19', N'Nữ','0482739206', N'Cà Mau', N'Việt Nam', '102', '2023-01-01', '2023-01-02'),
('KH03', N'Huỳnh Hữu Châu', '2002-11-19', N'Nam','0461934762', N'Đồng Nai', N'Việt Nam', '103', '2023-01-01', '2023-01-02')

create table Phong
(
	IdP varchar(10) primary key,
	LoaiP nvarchar(20),
	KieuP nvarchar(20),
	DonGia float,
	TinhTrang nvarchar(20),
)

insert into Phong
values
('101', N'Thường', N'Giường đơn', 300000, N'Còn trống'),
('102', N'Thường', N'Giường đơn', 300000, N'Còn trống'),
('103', N'Thường', N'Giường đơn', 300000, N'Còn trống'),

('201', N'Sang', N'Giường đôi', 400000, N'Còn trống'),
('202', N'Sang', N'Giường đôi', 400000, N'Còn trống'),
('203', N'Sang', N'Giường đôi', 400000, N'Còn trống'),

('301', N'VIP', N'Giường đơn', 700000, N'Còn trống'),
('302', N'VIP', N'Giường đơn', 700000, N'Còn trống'),
('303', N'VIP', N'Giường đơn', 700000, N'Còn trống')

create table DichVu
(
	IdDv varchar(10) primary key,
	TenDv nvarchar(50),
	DonGia float,
)
insert into DichVu
values
('DV01', N'Giặt ủi quần áo', 100000),
('DV02', N'Xe đưa đón sân bay', 200000),
('DV03', N'Nhà hàng', 300000),
('DV04', N'Hội họp, văn phòng', 300000),
('DV05', N'Quầy bar', 1000000),
('DV06', N'Spa', 300000)

create table SuDungDichVu
(
	IdP varchar(10),
	IdDv varchar(10),
	SoLuong int,
	Tien float,

	FOREIGN KEY(IdP) REFERENCES Phong(IdP),
	FOREIGN KEY(IdDv) REFERENCES DichVu(IdDv),
	primary key(IdP, IdDv),
)

create table HoaDon
(
	IdHd varchar(10) primary key,
	IdKh varchar(10),
	IdP varchar(10),
	NgayThanhToan date,
	PhuThu float,
	TienCanThanhToan float,

	FOREIGN KEY(IdKh) REFERENCES KhachHang(IdKh),
	FOREIGN KEY(IdP) REFERENCES Phong(IdP),
)