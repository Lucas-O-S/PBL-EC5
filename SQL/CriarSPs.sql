use [Termo_Light]
go

---SPs Genericas


create or alter procedure sp_delete_generic(
	@id int,
	@tabela varchar(max)

)
as
begin
	declare @sql varchar (max)
	set @sql = ' delete ' + @tabela + ' where id = ' + cast(@id as varchar(max))
	exec (@sql)
end
go

--------------------------------------------------------------

create or alter procedure sp_consulta_generic(
	@id int,
	@tabela varchar(max)
	)
as
begin
	declare @sql varchar(max)
	set @sql = 'select * from ' + @tabela + ' where id = ' + CAST(@id as varchar(max))
	exec (@sql)
end
go
----------------------------------------------

create or alter procedure sp_listagem_generic(
	@tabela varchar(max),
	 @ordem varchar(max) 
	 )
as
begin
	declare @sql varchar(max)
	set @sql = 'select * from ' + @tabela + ' order by ' + @ordem
	exec (@sql)
end
go




---SPs de Empresa

-----------------------------------------------------
create or alter procedure sp_insert_empresa(
	@nome varchar(500),
	@logo varbinary(max),
	@sede varchar(500)
) 
as

begin
	insert into Empresa (nome, logo, sede) values (@nome, @logo, @sede);

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

---------------------------------------------------------------------------

create or alter procedure sp_busca_id_empresa(
	@nome varchar(500)

)
as
begin
	select id from Empresa where nome = @nome
end
go




---SPs de Sensor

-------------------------------------------------------------------------
create or alter procedure sp_insert_sensor(
	@descricao varchar(500),
	@fk_empresa_id int
)
AS
BEGIN

	insert into Sensor(descricao,fk_empresa_id) values (@descricao,@fk_empresa_id)
END
GO



------------------------------------------------------------------------
create or alter procedure sp_update_sensor(
	@id int,
	@descricao varchar(500),
	@fk_empresa_id int
)
as
begin
	update Sensor set descricao = @descricao, fk_empresa_id =@fk_empresa_id where id = @id

end
go



-----SPs de Acesso

-----------------------------------------------------

create or alter procedure sp_insert_acesso(
	@senha varchar(500),
	@Nome_Usuario varchar(500),
	@fk_empresa_id int
)
as
begin

	insert into Acesso(Nome_Usuario,senha,fk_empresa_id) values (@Nome_Usuario,@senha, @fk_empresa_id)
end
go
-----------------------------------------------------------

----Devolve todos os acessos da empresa para fazer o login
create or alter procedure sp_login_acesso(
	@id_empresa int

)
as
begin

	select * 
	from Acesso as a 
	inner join Empresa as e on e.id = a.fk_empresa_id 
	where e.id = @id_empresa


end
go

---------------------------------------------------------------

create or alter procedure sp_update_acesso(
	@id int,
	@senha varchar(500),
	@idEmpresa int
)
as
begin
	update acesso set senha = @senha where id = @id and @idEmpresa = fk_empresa_id
end
go


---------------------------------------------------------

-----SPs Funcionario

create or alter procedure sp_insert_funcionario(
	@nome varchar(500),
	@cargo varchar(500),
	@foto varbinary(max)
)
as
begin


	insert into Funcionario (nome,Cargo,Foto) values (@nome,@cargo,@foto)
end
go

-----------------------------------------------------------------------------

create or alter procedure sp_update_funcionario(
	@id int,
	@nome varchar(500),
	@cargo varchar(500),
	@foto varbinary(max)
)
as
begin

	update Funcionario set nome = @nome, cargo = @cargo, Foto = @foto where id = @id 
end
go

----------------------------------------------------------------------

-----SPs manutenção

create or alter procedure sp_insert_manutencao
(
	@data_hora datetime,
	@fk_sensor_id int,
	@fk_funcionario_id int,
	@estado varchar(100)
	
)
as
begin
	insert into Manutencao(data_hora,fk_sensor_id,fk_funcionario_id,estado) 
	values (@data_hora,@fk_sensor_id, @fk_funcionario_id, @estado)
end
go

--------------------------------------------------------------------------------
create or alter procedure sp_update_manutencao(
	@data_hora datetime,
	@fk_sensor_id int,
	@fk_funcionario_id int,
	@estado varchar(100)
)
as
begin
	update manutencao set data_hora = @data_hora, fk_funcionario_id = @fk_funcionario_id, fk_sensor_id= @fk_sensor_id, estado = @estado


end
go

-----------------------------------------------------------------------------------------------

create or alter procedure sp_consulta_manutencao(
	@id int

)
as
begin
	select * from Manutencao where @id = id
end
go


-------------------------------------------------
