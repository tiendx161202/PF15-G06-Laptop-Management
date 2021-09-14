DROP DATABASE IF EXISTS LaptopM;
CREATE DATABASE	IF NOT EXISTS LaptopM;
USE LaptopM;

create user if not exists 'vtca'@'localhost' identified by 'vtcacademy';
grant all on LaptopM to 'vtca'@'localhost';
ALTER USER 'vtca'@'localhost' IDENTIFIED BY 'vtcacademy';

CREATE TABLE STAFFS
(
	StaffID int not null AUTO_INCREMENT,
    UserName VARCHAR(30),
    Password VARCHAR(100),
    Name VARCHAR(50),
    Phone VARCHAR(50),
    Email VARCHAR(50),
    Role INT NOT NULL DEFAULT 1,
    PRIMARY KEY(STAFFID)
);

CREATE TABLE Customers
(
	Customerid INT NOT NULL AUTO_INCREMENT,
    Name VARCHAR(1000),
    Phone VARCHAR(10),
    Address VARCHAR(200),
    PRIMARY KEY(Customerid)
);

CREATE TABLE Invoices
(
	InvoiceNo INT NOT NULL AUTO_INCREMENT,
    SaleId INT,
    AccountantId INT DEFAULT 0,
    CustomerId INT,
    Datetime DATETIME,
    Status INT,
    PRIMARY KEY(InvoiceNo)
);

CREATE TABLE Laptops
(
	LaptopId INT NOT NULL AUTO_INCREMENT,
    BrandId int,
    Name VARCHAR(500),
    Price DECIMAL,
    CPU VARCHAR(500),
    RAM VARCHAR(500),
    HardDisk VARCHAR(500),
    Monitor VARCHAR(500),
    GraphicsCard VARCHAR(500),
    Jack VARCHAR(500),
    Os VARCHAR(200),
    Battery VARCHAR(200),
    Weight VARCHAR(30),
    WarrantyPeriod VARCHAR(30),
    Stock INT,
    PRIMARY KEY(LaptopId)
);

CREATE TABLE Brands
(
	BrandId INT NOT NULL AUTO_INCREMENT,
    BrandName VARCHAR(50),
    PRIMARY KEY (BrandId)
);

CREATE TABLE InvoiceDetails
(
	InvoiceNo INT NOT NULL,
    LaptopId INT NOT NULL,
    Quanity INT NOT NULL DEFAULT 1,
    Price INT,
    PRIMARY KEY(InvoiceNo, LaptopID)
);

-- FK for Invoice Details
ALTER TABLE InvoiceDetails ADD CONSTRAINT fk_01 FOREIGN KEY (InvoiceNo) REFERENCES invoices(InvoiceNo);
ALTER TABLE InvoiceDetails ADD CONSTRAINT fk_02 FOREIGN KEY (LaptopId) REFERENCES Laptops(LaptopId);

-- FK for Laptops
ALTER TABLE LAPTOPS ADD CONSTRAINT l_fk_01 FOREIGN KEY (brandId) REFERENCES Brands(BrandId);

-- Create Procedure (ADD CUSTOMER)
delimiter $$
create procedure sp_createCustomer(IN name varchar(100), IN Phone VARCHAR(10), IN Address varchar(200), OUT Customerid int)
begin
	insert into Customers(name, Phone, Address) values (Name, Phone, Address); 
    select max(customerId) into customerId from Customers;
    
--    SELECT Customerid FROM Customers WHERE Customerid = max(Customerid);
end $$
delimiter ;

-- call sp_createCustomer('no name','any thing' ,'any where', @cusId);
-- select @cusId;
-- select *from Customers;

-- Insert data to customer
INSERT INTO Customers (customerId, Name, Phone, Address) VALUES (1, "Lê Huy Giang", "0981111111", "Thanh Hoa");
INSERT INTO Customers (Name, Phone, Address) VALUES ("Lê Huy Hoàng", "098222222", "Thanh Hoá");
INSERT INTO Customers (Name, Phone, Address) VALUES ("Lê Huy Khang", "098333333", "Thanh Hoá"),
("Đặng Xuân Tiến", "0987777777", "Cà Mau");

-- Insert data to Staffs
INSERT INTO Staffs (StaffId, UserName, Password, Name, Phone, Email, Role)
	VALUES (1, "Giang1", "pGiang123", "Lê Huy Giang", "0981111111", "lhGiang@gmail.com", 1);

INSERT INTO Staffs (UserName, Password, Name, Phone, Email, Role)
	VALUES  ("Tiendx1", "pTien321", "Đặng Xuân Tiến", "0981222222", "dxTien@gmail.com", 2),
			("minhAnh1", "pMAnh445", "Phạm Minh Anh", "0987666543", "mAnh@gmail.com", 1),
            ("lkHuyen1", "pkHuyen12341", "Lữ Khánh Huyền", "0987554512", "lkHuyen@gmail.com", 2);

-- Insert data to Brands
INSERT INTO Brands (BrandId, BrandName) VALUES (1, "ASUS");
INSERT INTO Brands (BrandName) VALUES ("ACER"), ("DELL"), ("MSI"), ("LENOVO"), ("HP"), ("LG"), ("MacBook");

-- Insert data to Laptops
INSERT INTO Laptops (LaptopId, BrandId, Name, price, cpu, ram, harddisk, monitor, graphicscard, jack, os, battery, weight, warrantyperiod, stock)
	VALUES (1, 1, "Asus TUF Gaming FX506LH (HN002T)", 
    21490000, "i5 10300H 2.5GHz", 
    "8 GB, DDR4 2 khe (1 khe 8GB + 1 khe rời) 2933 MHz", 
    "SSD 512 GB NVMe PCIe, Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng",
    "15.6\"Full HD (1920 x 1080) 144Hz",
    "Card rời, GTX 1650 4GB",
    "Jack tai nghe 3.5 mm2 x USB 3.2, HDMI, LAN (RJ45), USB 2.0, USB Type-C",
    "Windows 10 Home SL",
    "3-cell Li-ion, 48 Wh",
    "Nặng 2.3 kg",
    "12 tháng",
    10
    );

INSERT INTO Laptops (BrandId, Name, price, cpu, ram, harddisk, monitor, graphicscard, jack, os, battery, weight, warrantyperiod, stock)
	VALUES (3, "Dell Gaming G3 15 (P89F002BWH) ", 
    31990000, "i7 10750H 2.6GHz", 
    "16 GB, DDR4 2 khe (1 khe 8GB + 1 khe 8GB) 2933 MHz", 
    "SSD 512 GB NVMe PCIe, Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng",
    "15.6\"Full HD (1920 x 1080), 120Hz",
    "Card rời, GTX 1660Ti 6GB",
    "Jack tai nghe 3.5 mm, 1 x USB 3.2HDMILAN (RJ45), Thunderbolt 32 x USB 2.0, Mini DisplayPort",
    "Windows 10 Home SL",
    "4-cell Li-ion, 68 Wh",
    "Nặng 2.58 kg",
    "12 tháng",
    15
    ),
(2, "Acer Nitro 5 Gaming AN515 57 74NU (NH.QD9SV.001.)",
    29990000, "i7 11800H 2.30 GHz",
    "8 GB, DDR4 2 khe (1 khe 8GB + 1 khe rời) 3200 MHz", 
    "SSD 512 GB NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB), Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB), Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB)",
    "15.6\"Full HD (1920 x 1080) 144Hz",
    "Card rời, RTX 3050Ti 4GB",
    "Jack tai nghe 3.5 mm, 3 x USB 3.2, HDMILAN (RJ45), USB Type-C",
    "Windows 10 Home SL",
    "4-cell Li-ion, 57 Wh",
    "Nặng 2.2 kg",
    "12 tháng",
    12
),
(2, "Acer Aspire 7 Gaming A715 41G R150 R7",
19340000,"Ryzen 7 3750H 2.30 GHz",
"8 GB, DDR4 (2 khe) 2400 MHz",
"SSD 512 GB NVMe PCIe",
"15.6\"Full HD (1920 x 1080)",
"Card rời, GTX 1650Ti 4GB",
"Jack tai nghe 3.5 mm, 2 x USB 3.2, HDMI, LAN (RJ45), USB 2.0, USB Type-C",
"Windows 10 Home SL",
"4-cell Li-ion, 48 Wh",
"Nặng 2.1 Kg",
"24 tháng",
5
),
(2,"Acer Predator Helios PH315 54 78W5",
"30590000","i7 11800H 2.30 GHz",
"8 GB, DDR4 2 khe (1 khe 8GB + 1 khe rời) 3200 MHz",
"SSD 512 GB NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB,)Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB),Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB)",
"15.6\"Full HD (1920 x 1080) 144Hz",
"Card rời, RTX 3050Ti 4GB",
"Jack tai nghe 3.5 mm, 3 x USB 3.2, HDMI, LAN (RJ45), Mini DisplayPort, USB Type-C",
"Windows 10 Home SL",
"4-cell Li-ion, 57 Wh",
"Nặng 2.3 kg",
"12 tháng",
5
),
(2,"Acer Aspire A514 54 53T8",
"17990000","i5 1135G7 2.4GHz",
"8 GB, DDR4 (On board 4GB +1 khe 4GB) 2666 MHz",
"Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB),SSD 1 TB M.2 PCIe",
"14\"Full HD (1920 x 1080)",
"Card tích hợp, Intel Iris Xe",
"3 x USB 3.2, HDMI, Jack tai nghe 3.5 mm, LAN (RJ45), USB Type-C",
"Windows 10 Home SL",
"Li-ion, 48 Wh",
"Nặng 1.46 kg",
"24 tháng",
4
),
(1,"Asus TUF Gaming FX516PM",
"26690000","i7 11370H 3.3GHz",
"16 GB, DDR4 2 khe (8GB onboard+ 1 khe 8GB) 3200 MHz",
"SSD 512 GB NVMe PCIe,Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng",
"15.6\"Full HD (1920 x 1080) 144Hz",
"Card rời, RTX 3060 6GB",
"Jack tai nghe 3.5 mm, Thunderbolt 4 USB-C, 3 x USB 3.2, LAN (RJ45), HDMI 2.0",
"Windows 10 Home SL",
"4-cell Li-ion, 76 Wh",
"Nặng 2.3 kg",
"24 tháng",
5
),
(1,"Asus VivoBook A415EA",
17180000,"i5 1135G7 2.4GHz",
"8 GB, DDR4 (On board) 3200 MHz",
"SSD 512 GB NVMe PCIe",
"14\"Full HD (1920 x 1080)",
"Card tích hợp,I ntel Iris Xe",
"2 x USB 2.0,HDMI, Jack tai nghe 3.5 mm, USB 3.1, USB Type-C",
"Windows 10 Home SL",
"3-cell Li-ion, 42 Wh",
"Nặng 1.4 kg",
"24 tháng",
5
),
(3,"Dell Vostro 5410",
20420000,"i5 11300H 3.1GHz",
"8 GB, DDR4 2 khe (1 khe 8GB + 1 khe rời) 3200 MHz",
"SSD 512 GB NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)",
"14\"Full HD (1920 x 1200)",
"Card tích hợp,Intel Iris Xe",
"2 x USB 3.2,HDMI, Jack tai nghe 3.5 mm, LAN (RJ45), USB Type-C (Power Delivery and DisplayPort)",
"Windows 10 Home SL",
"4-cell Li-ion, 54 Wh",
"Nặng 1.44 kg",
"12 tháng",
5
),
(3,"Dell Inspiron 7306A",
29690000,"i7 1165G7 2.8GHz",
"16 GB, DDR4 2 khe (8 GB onboard + 8 GB onboard) 4267 MHz",
"SSD 512 GB NVMe PCIe",
"13.3',4K/UHD (3840 x 2160)",
"Card tích hợp, Intel Iris Xe",
"Jack tai nghe 3.5 mm, Thunderbolt 4 USB-C, 1 x USB 3.2,HDMI",
"Windows 10 Home SL",
"4-cell, 60 Wh",
"Nặng 1.37 kg",
"24 tháng ",
6
),
(3,"Dell Gaming G3 15",
25190000,"i7 10750H 2.6GHz",
"16 GB, DDR4 2 khe (1 khe 8GB + 1 khe 8GB) 2933 MHz",
"SSD 256 GB NVMe PCIe, HDD 1 TB, Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng",
"15.6\"Full HD (1920 x 1080) 120Hz",
"Card rời, GTX 1650Ti 4GB",
"Jack tai nghe 3.5 mm, 1 x USB 3.2, HDMI, LAN (RJ45) 2 x USB 2.0, USB Type-C",
"Windows 10 Home SL",
"3-cell Li-ion, 51 Wh",
"Nặng 2.56 kg",
"24 tháng ",
4
),
(4,"MSI Katana Gaming GF66 11UC",
26990000,"i7,11800H,2.30 GHz",
"8 GB, DDR4 2 khe (1 khe 8GB + 1 khe rời),3200 MHz",
"SSD 512 GB NVMe PCIe,Không hỗ trợ khe cắm SSD M2 mở rộng thêm",
"15.6\",Full HD (1920 x 1080) 144Hz",
"Card rời, RTX 3050 4GB",
"Jack tai nghe 3.5 mm, 2 x USB 3.2, HDMI, LAN (RJ45), USB 2.0,USB Type-C",
"Windows 10 Home SL",
"3-cell Li-ion, 53 Wh",
"Nặng 2.1 kg",
"24 tháng ",
4
),
(4,"MSI Gaming GE66 Raider 11UH",
70190000,"i7 11800H 2.30 GHz",
"32 GB, DDR4 2 khe (1 khe 16GB + 1 khe 16GB) 3200 MHz",
"SSD 2 TB NVMe PCIe, Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 2TB)",
"15.6\",QHD (2560 x 1440) 240Hz",
"Card rời, RTX 3080 16GB",
"Jack tai nghe 3.5 mm, Thunderbolt 4 USB-C, 3 x USB 3.2, HDMI, LAN (RJ45), Mini DisplayPort, USB Type-C",
"Windows 10 Home SL",
"4-cell, 99.9 Wh",
"Nặng 2.38 kg",
"24 tháng ",
3
),
(4,"MSI Gaming GF65 10UE",
29240000,"i7 10750H 2.6GHz",
"16 GB, DDR4 2 khe (1 khe 8GB + 1 khe 8GB) 3200 MHz",
"SSD 512 GB NVMe PCIe, Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng",
"15.6\",Full HD (1920 x 1080) 144Hz",
"Card rời, RTX 3060 Max-Q 6GB",
"Jack tai nghe 3.5 mm,2 x USB 3.2, HDMI, LAN (RJ45), 2 x USB Type-C",
"Windows 10 Home SL",
"3-cell Li-ion, 51 Wh",
"Nặng 1.86 kg",
"24 tháng ",
4
),
(5,"Lenovo IdeaPad Slim 5 15ITL05",
16640000,"i5 1135G7 2.4GHz",
"8 GB, DDR4 (On board) 3200 MHz",
"SSD 512 GB NVMe PCIe",
"15.6\", Full HD (1920 x 1080)",
"Card tích hợp, Intel Iris Xe",
"2 x USB 3.2, HDMI, Jack tai nghe 3.5 mm, USB Type-C (Power Delivery and DisplayPort)",
"Windows 10 Home SL",
"3-cell Li-ion, 45 Wh",
"Nặng 1.66 kg",
"12 tháng ",
5
),
(5,"Lenovo Ideapad 5 15ITL05",
18890000,"i5 1135G7 2.4GHz",
"8 GB, DDR4 (On board) 3200 MHz",
"Hỗ trợ khe cắm HDD SATA, SSD 512 GB NVMe PCIe",
"15.6\",Full HD (1920 x 1080)",
"Card rời, MX450 2GB",
"2 x USB 3.2, HDMI, Jack tai nghe 3.5 mm, USB Type-C (Power Delivery and DisplayPort)",
"Windows 10 Home SL",
"57 Wh",
"Nặng 1.66 kg",
"24 tháng ",
5
),
(5,"Lenovo Ideapad Gaming 3 15IMH05",
24290000,"i7 10750H 2.6GHz",
"8 GB, DDR4 (2 khe) 2933 MHz",
"SSD 512 GB NVMe PCIe, Hỗ trợ khe cắm HDD SATA",
"15.6\",Full HD (1920 x 1080)120Hz",
"Card rời, GTX 1650Ti 4GB",
"Jack tai nghe 3.5 mm, 2 x USB 3.2, HDMI, LAN (RJ45), USB Type-C",
"Windows 10 Home SL",
"45 Wh",
"Nặng 2.2 kg",
"12 tháng ",
4
),
(6,"HP 240 G8",
16190000,"i5 1135G7 2.4GHz",
"8 GB, DDR4 2 khe (1 khe 8GB + 1 khe rời) 3200 MHz",
"SSD 512 GB NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB), Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB)",
"14\",Full HD (1920 x 1080)",
"Card tích hợp,Intel Iris Xe",
"Jack tai nghe 3.5 mm, HDMI, LAN (RJ45), 2 x USB 3.1, USB Type-C",
"Windows 10 Home SL",
"3-cell Li-ion, 41 Wh",
"Nặng 1.426 kg",
"12 tháng ",
5
),
(6,"HP Pavilion 15 eg0505TU",
17090000,"i5 1135G7 2.4GHz",
"8 GB, DDR4 2 khe (1 khe 4GB + 1 khe 4GB) 3200 MHz",
"SSD 512 GB NVMe PCIe",
"15.6\",Full HD (1920 x 1080)",
"Card tích hợp, Intel Iris Xe",
"2 x USB 3.1, HDMI, USB Type-C, Jack tai nghe 3.5 mm",
"Windows 10 Home SL",
"3-cell Li-ion, 41 Wh",
"Nặng 1.677 kg",
"24 tháng ",
5
),
(6,"HP Omen 15 ek0078TX ",
51740000,"i7 10750H 2.6GHz",
"16 GB, DDR4 2 khe (1 khe 8GB + 1 khe 8GB) 2933 MHz",
"SSD 1 TB M.2 PCIe, Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng",
"15.6\",Full HD (1920 x 1080),300Hz",
"Card rời, RTX 2070 Max-Q 8GB",
"Jack tai nghe 3.5 mm, HDMI, LAN (RJ45), Thunderbolt 3, Mini DisplayPort, 3x SuperSpeed USB A",
"Windows 10 Home SL",
"6-cell Li-ion, 70.9 Wh",
"Nặng 2.36 kg",
"24 tháng ",
3
),
(7,"LG Gram 17 2021 ",
54890000,"i7 1165G7 2.8GHz",
"16 GB, LPDDR4X (On board) 4266 MHz",
"Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng, SSD 1 TB M.2 PCIe",
"17\",WQXGA (2560 x 1600)",
"Card tích hợp, Intel Iris Xe",
"2 x Thunderbolt 4 USB-C,2 x USB 3.2, HDMI, Jack tai nghe 3.5 mm",
"Windows 10 Home Standard",
"2-cell Li-ion, 80 Wh",
"Nặng 1.35 kg",
"24 tháng ",
5
),
(7,"LG Gram 16 2021 ",
50890000,"i7 1165G7 2.8GHz",
"16 GB, LPDDR4X (On board) 4266 MHz",
"Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng, SSD 512 GB NVMe PCIe",
"16\",WQXGA (2560 x 1600)",
"Card tích hợp,Intel Iris Xe",
"2 x Thunderbolt 4 USB-C,2 x USB 3.2, HDMI, Jack tai nghe 3.5 mm",
"Windows 10 Home Standard",
"2-cell Li-ion, 80 Wh",
"Nặng 1.19 kg",
"24 tháng ",
4
),

(8,"Apple MacBook Pro M1 2020 (Z11C)",
40490000,"Apple M1",
"16 GB",
"SSD 512 GB",
"13.3\", Retina (2560 x 1600)",
"Card tích hợp, 8 nhân GPU",
"2 x Thunderbolt 3 (USB-C),J ack tai nghe 3.5 mm",
"Mac OS",
"Khoảng 10 tiếng",
"Nặng 1.4 kg",
"24 tháng ",
4
),
(8,"Apple MacBook Air M1 2020 (MGND3SA/A) ",
26090000,"Apple M1",
"8 GB",
"SSD 256 GB",
"13.3\", Retina (2560 x 1600)",
"Card tích hợp, 7 nhân GPU",
"2 x Thunderbolt 3 (USB-C), Jack tai nghe 3.5 mm",
"Mac OS",
"Khoảng 10 tiếng",
"Nặng 1.29 kg",
"12 tháng ",
4
),
(8,"Apple MacBook Pro M1 2020 (MYDC2SA/A) ",
35990000,"Apple M1",
"8 GB",
"SSD 512 GB",
"13.3\", Retina (2560 x 1600)",
"Card tích hợp, 8 nhân GPU",
"2 x Thunderbolt 3 (USB-C), Jack tai nghe 3.5 mm",
"Mac OS",
"Khoảng 10 tiếng",
"Nặng 1.4 kg",
"12 tháng ",
5
);

-- Insert data to Invoices
INSERT INTO Invoices (invoiceNo, saleId, customerid, datetime, status)
	VALUES (1, 2, 1, Now(), 1);

-- Insert data to Invoice Details
INSERT INTO InvoiceDetails (invoiceno, laptopid, quanity, price)
	VALUES (1, 2, 1, 31990000);
INSERT INTO InvoiceDetails (invoiceno, laptopid, quanity, price)
	VALUES (1, 3, 1, 29990000);

-- Show Data
-- SELECT * FROM brands;
-- SELECT * FROM Laptops;
-- SELECT * FROM Customers;
-- SELECT * FROM staffs;
-- SELECT * FROM Invoices;
-- SELECT * FROM Invoicedetails;

UPDATE staffs SET UserName = "Giang1111" WHERE staffid = 1;
UPDATE staffs SET UserName = "Tien2222" WHERE staffid = 2;
UPDATE staffs SET UserName = "Manhlt23" WHERE staffid = 3;

UPDATE staffs SET Password = "61198ded08f2e09fa7e65321472efe47" WHERE staffid = 1; -- pass: Giang123@
UPDATE staffs SET Password = "e426514be6bd76dedaf40277d9d016d5" WHERE staffid = 2; -- pass: Tien123@
UPDATE staffs SET Password = "aeb25905a41a1dd79ba133d7260cb116" WHERE staffid = 3; -- pass: MAnh345^


-- SELECT * FROM Laptops INNER JOIN Brands ON laptops.BrandId = brands.BrandId WHERE Laptops.laptopId = 3;
