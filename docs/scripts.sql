-- select * from aspnetusers;
-- SELECT * from Toggles
-- SELECT * from ToggleUsers


-- Create isButtonBlue with TRUE as default value
 insert into Toggles (IsDeleted, CreatedDate, UpdatedDate, [Version], Name, DefaultValue) values (0, getdate(), getdate(), 1, 'isButtonBlue', 1);

-- Override isButtonBlue with FALSE for application 1
declare @app1 UNIQUEIDENTIFIER;
select @app1 = Id from aspnetusers where UserName = 'application1';
insert into ToggleUsers (IsDeleted, CreatedDate, UpdatedDate, ToggleId, UserId, ToggleValue) values (0, GETDATE(), getdate(), 1, @app1, 0)

-- Create isButtonGreen with TRUE for application 1
insert into Toggles (IsDeleted, CreatedDate, UpdatedDate, [Version], Name, DefaultValue) values (0, getdate(), getdate(), 1, 'isButtonGreen', 3);
declare @app1 UNIQUEIDENTIFIER;
select @app1 = Id from aspnetusers where UserName = 'application1';
insert into ToggleUsers (IsDeleted, CreatedDate, UpdatedDate, ToggleId, UserId, ToggleValue) values (0, GETDATE(), getdate(), 2, @app1, 1)

-- Create isButtonRed with TRUE for all applications except application 1
insert into Toggles (IsDeleted, CreatedDate, UpdatedDate, [Version], Name, DefaultValue) values (0, getdate(), getdate(), 1, 'isButtonRed', 1);
declare @app1 UNIQUEIDENTIFIER;
select @app1 = Id from aspnetusers where UserName = 'application1';
insert into ToggleUsers (IsDeleted, CreatedDate, UpdatedDate, ToggleId, UserId, ToggleValue) values (0, GETDATE(), getdate(), 3, @app1, 2)


-- DOMAIN
-- 0 False,
-- 1 True,
-- 2 Excluded,
-- 3 Exclusive
