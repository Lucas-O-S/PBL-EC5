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

	declare @id int 

	select @id = MAX(id) + 1 from Empresa
	if (@id is null)
		begin
			set @id = 1
		end
	insert into Empresa (id, nome, logo, sede) values (@id, @nome, @logo, @sede);

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
	declare @id int
	select @id = MAX(id) + 1 from Sensor
	if (@id is null)
	begin
		set @id = 1
	end
	insert into Sensor(id,descricao,fk_empresa_id) values (@id,@descricao,@fk_empresa_id)
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
	declare @id int
	select @id = max(id) + 1 from Acesso
	if (@id is null)
	begin
		set @id = 1
	end
	insert into Acesso(id,Nome_Usuario,senha,fk_empresa_id) values (@id,@Nome_Usuario,@senha, @fk_empresa_id)
end
go
-----------------------------------------------------------

create or alter procedure sp_login_acesso(
	@NomeEmpresa varchar(500),
	@Nome_Usuario varchar(500),
	@senha varchar(500)

)
as
begin
	declare @idEmpresa int
	select @idEmpresa = id from Empresa where @NomeEmpresa = nome
	select count(*) as resultado 
	from Acesso as a 
	inner join Empresa as e on e.id = a.fk_empresa_id 
	where e.id = @idEmpresa and a.senha = @senha and a.Nome_Usuario = @Nome_Usuario 


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
	declare @id int
	select @id = max(id) + 1 from Funcionario
	if (@id is null)
	begin
		set @id = 1
	end

	insert into Funcionario (id,nome,Cargo,Foto) values (@id,@nome,@cargo,@foto)
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
	declare @buscaMudanca int

	--Verifica se mudeou alguma primary key
	select @buscaMudanca = COUNT(*) from Manutencao 
	where @data_hora = data_hora and @fk_funcionario_id = fk_funcionario_id and fk_sensor_id = @fk_sensor_id
	
	

	---Se a pessoa mudar somente o estado da manutenção só muda o estado
	if(@buscaMudanca > 1)
	begin
		update Manutencao set estado = @estado where @data_hora = data_hora and @fk_funcionario_id = fk_funcionario_id and fk_sensor_id = @fk_sensor_id
	end

	else 

	--Se a pessoa mudar o funcionario, sensor ou data cria-se um novo registro e deleta antigo
	begin
		delete Manutencao where @data_hora = data_hora and @fk_funcionario_id = fk_funcionario_id and fk_sensor_id = @fk_sensor_id
		exec sp_insert_manutencao @data_hora, @fk_sensor_id, @fk_funcionario_id, @estado
	end


end
go
---------------------------------------------------------------

create or alter procedure sp_delete_manutencao(
	@data_hora datetime,
	@fk_sensor_id int,
	@fk_funcionario_id int,
	@estado varchar(100)

)
as
begin
	delete Manutencao 
	where @data_hora = data_hora and @fk_funcionario_id = fk_funcionario_id and fk_sensor_id = @fk_sensor_id
end
go

-----------------------------------------------------------------------------------------------

create or alter procedure sp_consulta_manutencao(
	@data_hora datetime,
	@fk_sensor_id int,
	@fk_funcionario_id int,
	@estado varchar(100)

)
as
begin
	select * from Manutencao where @data_hora = data_hora and @fk_funcionario_id = fk_funcionario_id and fk_sensor_id = @fk_sensor_id
end
go


-------------------------------------------------

--------SPs temperatura

create or alter procedure sp_insert_temperatura(
	@data_hora datetime,
	@valor int,
	@fk_sensor_id int
)
as
begin
	insert into Temperatura(data_hora,valor,fk_sensor_id)
	values (@data_hora,@valor,@fk_sensor_id)
end
go

------------------------------------------------------------

create or alter procedure sp_update_temperatura(
	@data_hora datetime,
	@valor int,
	@fk_sensor_id int

)
as
begin
	update Temperatura set valor = @valor where @data_hora = data_hora and @fk_sensor_id = fk_sensor_id
end
go

---------------------------------------------------------

create or alter procedure sp_delete_temperatura(
	@data_hora datetime,
	@valor int,
	@fk_sensor_id int

)
as
begin
	delete Temperatura where @data_hora = data_hora and @fk_sensor_id = fk_sensor_id
end
go
--------------------------------------------------------------------------------------------

create or alter procedure sp_consulta_temperatura(
	@data_hora datetime,
	@valor int,
	@fk_sensor_id int

)
as
begin
	select * from Temperatura where @data_hora = data_hora and @fk_sensor_id = fk_sensor_id
end
go

-------------------------------------------------------------------------------

