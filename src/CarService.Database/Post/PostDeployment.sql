insert into dbo.AspNetRoles
values 
('420ecee9-cf46-4824-9dd5-84cabe7748aa', 'admin', 'ADMIN', '463fddb3-0509-498d-bec2-2762dd825cbb'),
('55b4665d-f989-47d9-ae4d-5bc66150603c', 'manager', 'MANAGER', 'c3b4b8a8-db6f-4113-936f-e710885dc0e9'),
('91f86e9a-9464-4686-aac3-a7e3788298e1', 'user', 'USER', 'ba426d36-4cff-48a0-b726-b734128c418d')

---AspNetUsers
insert into dbo.AspNetUsers
values 
('25ed5e6a-2563-45d8-b9eb-bd77f8c9f83e', 'manager@mail.ru@mail.ru', 'MANAGER@MAIL.RU', NULL, NULL, 
'manager@mail.ru', 'MANAGER@MAIL.RU', 0, 
'AQAAAAEAACcQAAAAEC81ktyJThar7bZwM/bQAMgw0gGjFsgE54f5813sHq38JSpMrUZaJtf+c/ujTUWu3g==', 
'N3EXAEE2JBJ3IBNTHTJGZ5BB6WF4BWYJ', '3966505b-f92f-4dfb-bc35-65b14e4e2280', NULL, 0, 0, NULL, 1, 0),

('72357c52-99fd-43c1-898e-9ff3bb7aa6e9', 'admin@mail.ru', 'ADMIN@MAIL.RU', NULL, NULL, 
'admin@mail.ru', 'ADMIN@MAIL.RU', 0, 
'AQAAAAEAACcQAAAAEFBeM9W0syVC7yg0Zasu09oh71rBfBpIgPWMdSk6EG4qhdaQagrDRr0gRCKqf8JvlA==', 
'YRHJBWNGAE6ZJTK4GMGZPBOBAIPY2JBV', '4a32730f-15d0-48a6-bc96-fec26c83f698', NULL, 0, 0, NULL, 1, 0),

('ffe41a98-cdbc-4799-b6a1-b7d3f9a924f0', 'user@mail.ru', 'USER@MAIL.RU', NULL, NULL, 
'user@mail.ru', 'USER@MAIL.RU', 0, 
'AQAAAAEAACcQAAAAECf+hgn5fJFXwBpoiHI4m4EdUtRyoC1qCDuls1tUzZNqXtiukjBD7zFoPHGieu+g6w==', 
'3R46CL6WQUWSW3SA56L7OUV5HMEV2QKQ', '005a0e67-9393-4f69-84d4-05cc1ecc38d1', NULL, 0, 0, NULL, 1, 0)

---AspNetUserRoles
insert into dbo.AspNetUserRoles
values 
('25ed5e6a-2563-45d8-b9eb-bd77f8c9f83e', '420ecee9-cf46-4824-9dd5-84cabe7748aa'),
('72357c52-99fd-43c1-898e-9ff3bb7aa6e9', '420ecee9-cf46-4824-9dd5-84cabe7748aa'),
('ffe41a98-cdbc-4799-b6a1-b7d3f9a924f0', '91f86e9a-9464-4686-aac3-a7e3788298e1')