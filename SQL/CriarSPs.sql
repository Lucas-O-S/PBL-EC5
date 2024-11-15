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

--------------------------------------------------------------------------

create or alter procedure sp_verificar_sensor(
	@descricao varchar(max)
)
as
begin
	select COUNT(descricao) as cont from sensor where descricao = @descricao
end
go

------------------------------------------------------------------------------

create or alter procedure sp_avancado_sensor(
	@descricao varchar(max),
	@empresa varchar(max),
	@tipo bit

)
as
begin
	declare @likeDescricao varchar(max)
	set @likeDescricao = '%' + @descricao + '%'
	declare @likeEmpresa varchar(max)
	set @likeEmpresa = '%' + @empresa + '%'

	select s.id,descricao, fk_empresa_id 
	from sensor as s
	inner join empresa as e
	on e.id = s.fk_empresa_id
	where s.descricao like @likeDescricao and
	( (@tipo = 0 and e.nome like @likeDescricao) or (@tipo = 1 and e.nome = @empresa))
				
end
go


-----SPs de Acesso

-----------------------------------------------------

create or alter procedure sp_insert_acesso(
	@senha varchar(500),
	@Nome_Usuario varchar(500),
	@login_Usuario varchar(500),
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
	@Nome_Usuario varchar(500),
	@login_Usuario varchar(500),
	@senha varchar(500),
	@idEmpresa int
	)
as
begin
	update acesso set Nome_Usuario = @Nome_Usuario, fk_empresa_id = @idEmpresa,  senha = @senha, @login_Usuario = @login_Usuario where id = @id
end
go


---------------------------------------------------------

-----SPs Funcionario

create or alter procedure sp_insert_funcionario(
	@nome varchar(500),
	@cargo varchar(500),
	@foto varbinary(max),
	@dataContratacao datetime
)
as
begin


	insert into Funcionario (nome,Cargo,Foto,dataContratacao) values (@nome,@cargo,@foto,@dataContratacao)
end
go

-----------------------------------------------------------------------------

create or alter procedure sp_update_funcionario(
	@id int,
	@nome varchar(500),
	@cargo varchar(500),
	@foto varbinary(max),
	@dataContratacao datetime

)
as
begin

	update Funcionario set nome = @nome, cargo = @cargo, Foto = @foto, dataContratacao = @dataContratacao where id = @id 
end
go

----------------------------------------------------------------------

-----SPs manuten��o

create or alter procedure sp_insert_manutencao
(
	@data_hora datetime,
	@fk_sensor_id int,
	@fk_funcionario_id int,
	@estado int	
)
as
begin
	insert into Manutencao(data_hora,fk_sensor_id,fk_funcionario_id,estado) 
	values (@data_hora,@fk_sensor_id, @fk_funcionario_id, @estado)
end
go

--------------------------------------------------------------------------------
create or alter procedure sp_update_manutencao(
	@id int,
	@data_hora datetime,
	@fk_sensor_id int,
	@fk_funcionario_id int,
	@estado int
)
as
begin
	update manutencao set data_hora = @data_hora, fk_funcionario_id = @fk_funcionario_id, fk_sensor_id= @fk_sensor_id, estado = @estado where id = @id


end
go

-----------------------------------------------------------------------------------------------

create or alter procedure sp_avancado_manutencao(
	@data_hora_inicial datetime,
	@data_hora_final datetime,
	@funcionario varchar(max),
	@empresa varchar(max),
	@sensor varchar(max),
	@estado int
)
as
begin
	set @funcionario = '%' + @funcionario + '%'
	set @empresa = '%' + @empresa + '%'
	set @sensor = '%' + @sensor + '%'

	select m.id, m.fk_funcionario_id, m.fk_sensor_id, m.data_hora, m.estado
	from manutencao as m
	inner join sensor as s on s.id = m.fk_sensor_id
	inner join funcionario as f on f.id = m.fk_funcionario_id
	inner join empresa as e on e.id = s.fk_empresa_id
	where (m.data_hora between @data_hora_inicial and @data_hora_final)
	and e.nome like @empresa and s.descricao like @sensor and f.nome like @funcionario
	and ((estado = @estado and @estado !=4) or (@estado=4))
end
go
---------------------------------------------------------------------------
EXEC sp_avancado_manutencao
    @data_hora_inicial = '2024-01-01 00:00:00',  -- Defina a data/hora inicial no formato correto
    @data_hora_final = '2024-12-31 23:59:59',    -- Defina a data/hora final no formato correto
    @funcionario = '',                            -- Deixe vazio para buscar todos os funcion�rios
    @empresa = '',                                -- Deixe vazio para buscar todas as empresas
    @sensor = '',                                 -- Deixe vazio para buscar todos os sensores
    @estado = 4;                                  -- Defina o estado, 4 significa todos os estados
