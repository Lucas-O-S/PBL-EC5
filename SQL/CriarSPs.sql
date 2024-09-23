
---SPs de Empresa

-----------------------------------------------------
create or alter procedure sp_insert_empresa(
	@nome varchar(500),
	@logo varbinary(max),
	@sede varchar(500)
) 
as

begin

declare @id int 

	select @id = MAX(id) + 1 from Empresa
	if (@id is null)
		begin
			set @id = 1
		end
	insert into Empresa (id, nome, logo, sede) values (@id, @nome, @logo, @sede);

end
go
------------------------------------------------------------------------------------------------

create or alter  procedure sp_delete_empresa(
	@id int
) as
begin
	delete empresa where id = @id
end
go

-------------------------------------------------------------------------------------------

create or alter procedure sp_update_empresa(
	@id int,
	@nome varchar(500),
	@logo varbinary(max),
	@sede varchar(500)
)
as
begin
	update Empresa set nome = @nome, logo = @logo, sede = @sede
	where id = @id

end
go

---------------------------------------------------------------

create or alter procedure sp_busca_empresa(
	@id int
)
as
begin
	select * from Empresa where id = @id
end
go

----------------------------------------------------------------
create or alter procedure sp_listagem_empresa
as
begin
	select * from Empresa
end
go

