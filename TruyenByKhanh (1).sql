create database TruyenCuaKhanh
use TruyenCuaKhanh
create table QTV
(	IDadmin nvarchar(50) primary key not null,
	Pass nvarchar(50),
	TenAD nvarchar(255))
create table TacGia
(	MaTG int  primary key not null,
	TenTG nvarchar(255))

create table TheLoai
(	MaTL int  primary key not null,
	TenTL nvarchar(255) not null)

create table NguoiDung
(	ID int IDENTITY(1,1) primary key not null,
	TenDangNhap nvarchar(255) not null, 
	MatKhau nvarchar(20),
	HoTenND nvarchar(255),
	DiaChi nvarchar(255), 
	Email nvarchar (255), 
	DT		varchar(11) unique
	check (DT like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' or
		   DT like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' or
		   DT like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' or
		   DT like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
		   or DT is null))
create table TruyenYeuThich
(	MaYT int IDENTITY  primary key not null, 
	TenYT nvarchar(255),
	ID int
	foreign key (ID) references NguoiDung(ID))
	create table Truyen
(	MaTruyen int  primary key not null,
	TenTruyen nvarchar(255) not null,
	Mota nvarchar(max),
	Hinh varchar(50),
	NoiDung nvarchar(max),
	NgayXB date,
	SoChuong int,
	MaTG int,
	foreign key (MaTG) references TacGia(MaTG)
	on update cascade
	on delete cascade,
	MaTL int
	foreign key (MaTL) references TheLoai(MaTL)
	on update cascade
	on delete cascade,

	MaYT int
	foreign key (MaYT) references TruyenYeuThich(MaYT)
	on update cascade
	on delete cascade)
create table BinhLuan
(	MaComt int IDENTITY(1,1) primary key not null,
	NoiDung text,
	NgayDang date check(NgayDang>=getdate()),
	DanhGia float)
create table ChitietBinhluan
(	IDcomt  int
	foreign key (IDcomt) references BinhLuan(MaComt)
	on update cascade
	on delete set null,
	ID int
	foreign key (ID) references NguoiDung(ID),
	MaTruyen int
	foreign key (MaTruyen) references Truyen(MaTruyen))

	insert into Truyen(MaTruyen,TenTruyen,Mota,Hinh,NoiDung,NgayXB,SoChuong) values ('523',N'Ông nội và cháu',N'Truyện ngắn','/Content/Images/hinh1.jpg',N'Ông nội và người cháu đích tôn 3 tuổi đang ngồi chơi trò bán hàng.

- Cháu: Đây tôi đưa bác 5.000 đồng, nhưng với một điều kiện.
- Ông: Điều kiện gì cũng được.
- Cháu: Thật không?
- Ông: Thật. Bác cứ nói đi.
- Cháu: Bác phải về dạy lại con bác đi nhé, con bác hay đánh tôi lắm đấy.','12/02/2001','1')

insert into Truyen(MaTruyen,TenTruyen,Mota,Hinh,NoiDung,NgayXB,SoChuong) values ('131',N'Con gà trống',N'Truyện ngắn','/Content/Images/sv2.jpg',N'Một nông dân mua được một chú gà trống rất hăng. Gà ta đạp hết gà mái rồi tới vịt mái khiến chúng đẻ liên tục, nông dân nọ rất hả hê. Một bữa, ông ta đi làm đồng về thấy gà trống đang nằm trên bãi cỏ sau nhà, bên trên là mấy con quạ đang chờ ăn xác.

Người nông dân thương xót than thở:

- Ôi chú gà tội nghiệp! Sao mày vội bỏ tao mà đi sớm vậy?

Gà trống mở hé mắt nói:

- Lạy bố, bố đi chỗ khác dùm con kẻo lũ quạ mái bay hết mất bây giờ!','12/02/2001','1')

insert into Truyen(MaTruyen,TenTruyen,Mota,Hinh,NoiDung,NgayXB,SoChuong) values ('999',N'Tea, DNT',N'Truyện ngắn','/Content/Images/hinh3.jpg',N'Nữ thư ký nói với sếp:

- Sếp phải tăng lương cho tôi. Tôi còn phải nuôi mấy đứa con.

- Cô có mấy đứa?

- Hai đứa của tôi và một đứa của mẹ chồng tôi.','12/02/2001','1')


insert into Truyen(MaTruyen,TenTruyen,Mota,Hinh,NoiDung,NgayXB,SoChuong) values ('888',N'Rượu Chua',N'Truyện ngắn','/Content/Images/hinh1.jpg',N'Chủ nhà kia đãi khách rượu. Vừa nhấp môi, ai cũng nhắm mắt kêu "chua quá."

- Một ông khách nói: Tôi có cách là cho rượu mất chua.

- Chủ nhà liền hỏi: Làm thế nào thì hết chua được?

- Ông khách bày: Kiếm một tờ giấy, bưng miệng hũ lại, rồi úp sấp xuống.

- Lấy ngải cứu đốt đít hũ bảy mồi, cứ để thế cho đến rạng ngày mai là hết chua ngay.

- Chủ nhà nói: Thế thì rượu chảy hết còn gì?

- Ông khách nói: Rượu chua để làm quái gì mà còn tiếc!','12/02/2001','1')

insert into Truyen(MaTruyen,TenTruyen,Mota,Hinh,NoiDung,NgayXB,SoChuong) values ('139',N'TIẾNG VANG',N'Truyện ngắn','/Content/Images/hinh3.jpg',N'Hai cha con nọ đang đi men theo triền núi. Bỗng người con trai nhỏ trượt chân và ngã, em la lớn lên “Ối chao!”. Em lấy làm ngạc nhiên khi thấy ở miền núi xa có tiếng ai nhái lại “Ối chao!”.
Em tò mò la lên “Ngươi là ai?” thì em nhận lại tiếng nhái lại “Ngươi là ai?” Tức giận quá, em quát lên: “Quân đốn mạt!” thì em lại nghe tiếng nhái lại: “Quân đốn mạt!”. 
Em nhìn người cha và hỏi “Thế là thế nào hở cha?”
Người cha mỉm cười và nói: 
“Này con hãy xem đây”.
 Nói rồi, ông nói lớn lên: “Anh hay quá!” thì nghe tiếng trả lời “Anh hay quá!”. Rồi ông lại la lên: “Anh tuyệt vời quá!”  thì cũng nghe tiếng trả lời “Anh tuyệt vời quá! ”
Người con ngạc nhiên nhưng cũng vẫn chưa hiểu. Người cha ôn tồn giảng : “Đó là tiếng vang con ạ! Khi có những khoảng trống rộng rãi như ta có trước mặt đây thì các tiếng động lớn hay tiếng nói lớn nó sẽ dội lại như vậy.
Con nói những lời tức giận thì nó sẽ dội lại những lời tức giận, cha nói những lời đẹp đẽ thì nó sẽ dội lại những lời đẹp đẽ!
Ở đời cũng vậy! đời là sự dội lại của các hành động của ta. Khi tâm ta có lòng từ bi thì chúng ta sẽ nhận lại sự yêu thương. Khi ta hành động những điều xấu thì nỗi bất hạnh sẽ lại xảy đến cho chúng ta.”','12/02/2001','1')


insert into QTV(IDadmin,Pass,TenAD) values (N'quockhanh',N'quockhanh',N'Quốc Khánh')