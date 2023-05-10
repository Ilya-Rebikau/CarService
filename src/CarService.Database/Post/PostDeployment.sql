---AspNetRoles
insert into dbo.AspNetRoles
values 
('420ecee9-cf46-4824-9dd5-84cabe7748aa', 'admin', 'ADMIN', '463fddb3-0509-498d-bec2-2762dd825cbb'),
('55b4665d-f989-47d9-ae4d-5bc66150603c', 'manager', 'MANAGER', 'c3b4b8a8-db6f-4113-936f-e710885dc0e9'),
('91f86e9a-9464-4686-aac3-a7e3788298e1', 'user', 'USER', 'ba426d36-4cff-48a0-b726-b734128c418d')

---AspNetUsers
insert into dbo.AspNetUsers
values 
('72357c52-99fd-43c1-898e-9ff3bb7aa6e9', 'admin@mail.ru', 'ADMIN@MAIL.RU', NULL, NULL, 
'admin@mail.ru', 'ADMIN@MAIL.RU', 0, 
'AQAAAAEAACcQAAAAEFBeM9W0syVC7yg0Zasu09oh71rBfBpIgPWMdSk6EG4qhdaQagrDRr0gRCKqf8JvlA==', 
'YRHJBWNGAE6ZJTK4GMGZPBOBAIPY2JBV', '4a32730f-15d0-48a6-bc96-fec26c83f698', NULL, 0, 0, NULL, 1, 0, NULL)

---AspNetUserRoles
insert into dbo.AspNetUserRoles
values 
('72357c52-99fd-43c1-898e-9ff3bb7aa6e9', '420ecee9-cf46-4824-9dd5-84cabe7748aa')

---CarBrands
insert into dbo.CarBrands
values
('BMW', NULL), ('Mersedes', NULL), ('Audi', NULL)

---CarTypes
insert into dbo.CarTypes
values
(N'Легковой автомобиль'), (N'Грузовой автомобиль'), (N'Мотоцикл')