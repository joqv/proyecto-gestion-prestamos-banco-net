-- SP Listar Clientes
create or alter proc ListarClientes
as
	select * from Clientes
go
--

-- SP Listar Transacciones
create or alter proc ListarTransacciones
as
	select * from Clientes
go
--

-- SP RegistrarCliente
create or alter proc RegistrarCliente(
	@nombre varchar(100),
	@apellido varchar(100),
	@numeroDocumento varchar(15),
	@telefono varchar(20),
	@email varchar(30)
)
as
	insert into Clientes(Nombre, Apellido, NumeroDocumento, Telefono, Email)
	values
	(@nombre, @apellido, @numeroDocumento, @telefono, @email)

	select @@IDENTITY
go
--

-- SP ObtenerClientePorID
create or alter proc ObtenerClientePorID(
	@id int
)
as
	select * from Clientes c where c.IdCliente = @id
go
---


-- SP Listar CuentasBancarias
create or alter proc ListarCuentasBancarias
as
	select * from CuentasBancarias
go
--

-- SP CrearCuentaBancaria
create or alter proc CrearCuentaBancaria(
	@idCliente int,
	@idTipoCuenta int,
	@idMoneda int,
	@saldoInicial decimal(18, 4)
)
as
begin
	
	set nocount on;
	declare @nuevoNumeroCuenta varchar(50);
	declare @numeroRandom int;

	begin transaction;

		begin try

			set @numeroRandom = FLOOR(RAND() * 900000) + 100000;
			set @nuevoNumeroCuenta = CAST(@idCliente as varchar(10)) + '-' + CAST(@numeroRandom as varchar(6)) + '-' + FORMAT(GETDATE(), 'yyyMMdd');

			insert into CuentasBancarias(IdCliente, IdTipoCuenta, IdMoneda, NumeroCuenta, Saldo)
			values
			(@idCliente, @idTipoCuenta, @idMoneda, @nuevoNumeroCuenta, @saldoInicial)

			commit transaction;

			select @@IDENTITY

		end try
		begin catch
			if @@TRANCOUNT > 0
				rollback transaction;
			throw
		end catch

end
go
--


-- SP ObtenerCuentaBancariaPorID
create or alter proc ObtenerCuentaBancariaPorID(
	@id int
)
as
	select * from CuentasBancarias c where c.IdCuenta = @id
go
---


-- SP ObtenerCuentasBancariasPorCliente
create or alter proc ObtenerCuentasBancariasPorCliente(
	@idCliente int
)
as
	select * from CuentasBancarias cb where cb.IdCliente = @idCliente
go
--


-- SP ListarMonedas
create or alter proc ListarMonedas
as
	select * from Monedas
go
--

-- SP ListarTiposCuenta
create or alter proc ListarTiposCuenta
as
	select * from TipoCuenta
go
--


exec ObtenerClientePorID 3
go

exec ObtenerCuentasBancariasPorCliente 5
go


-- SP ObtenerPrestamoPorID
create or alter proc ObtenerPrestamoPorId(
	@id int
)
as
	select * from Prestamos p where p.IdPrestamo = @id
go
---



-- SP Listar Prestamos
create or alter proc ListarPrestamos
as
	select * from Prestamos
go
--


-- SP ObtenerPrestamosPorCliente
create or alter proc ObtenerPrestamosPorCliente(
	@idCliente int
)
as
	select * from Prestamos cp where cp.IdCliente = @idCliente
go
--


ObtenerPrestamosPorCliente 1
go



-- SP Crear Prestamos
create or alter proc AgregarPrestamo(
	@idCliente int,
	@idMoneda int,
	@montoPrincipal decimal(18, 4),
	@tasaInteres decimal(5, 4),
	@plazoMeses int
)
as
begin
	set nocount on;

	declare @idPrestamo int;
	declare @idCuentaDestino int;
	declare @fechaInicio date = GETDATE();
	declare @fechaFinEstimada date = DATEADD(MONTH, @plazoMeses, @fechaInicio);

	declare @montoTotalCuota decimal(18, 4);
	declare @tasaMensual decimal(18, 10);
	declare @saldoPendiente decimal(18, 4);
	declare @interes decimal(18, 4);
	declare @capital decimal(18, 4);
	declare @cuotaMes int = 1;

	begin transaction;
		begin try

			select top 1 @idCuentaDestino = cb.IdCuenta
			from CuentasBancarias cb
			where cb.IdCliente = @idCliente and cb.IdMoneda = @idMoneda

			if @idCuentaDestino is null
			begin
				raiserror('El cliente no tiene una cuenta bancaria.', 16, 1);
			end

			insert into Prestamos(IdCliente, IdMoneda, MontoPrincipal, TasaInteres, PlazoMeses, FechaInicio, FechaFinEstimada, SaldoPendiente, EstadoPrestamo)
			values
			(@idCliente, @idMoneda, @montoPrincipal, @tasaInteres, @plazoMeses, @fechaInicio, @fechaFinEstimada, @montoPrincipal, 'ACTIVO');

			set @idPrestamo = SCOPE_IDENTITY();
			set @saldoPendiente = @montoPrincipal;

			set @tasaMensual = @tasaInteres / 12

			set @montoTotalCuota = (@montoPrincipal * @tasaMensual) / (1 - POWER(1 + @tasaMensual, -@plazoMeses));

			if @tasaInteres = 0
			begin
				set @montoPrincipal = @montoPrincipal / @plazoMeses
			end


			while @cuotaMes <= @plazoMeses
			begin
				set @interes = @saldoPendiente * @tasaMensual;

				set @capital = @montoTotalCuota - @interes;

				set @saldoPendiente = @saldoPendiente - @capital;

				insert into CuotasPrestamo(IdPrestamo, NumeroCuota, FechaVencimiento, MontoTotalCuota, MontoCapital, MontoInteres, SaldoPendiente, MontoPagado, EstadoCuota)
				values
				(@idPrestamo, @cuotaMes, DATEADD(MONTH, @cuotaMes, @fechaInicio), @montoTotalCuota, @capital, @interes, @saldoPendiente, 0.00, 'PENDIENTE');

				set @cuotaMes = @cuotaMes + 1
			end


			update CuentasBancarias
			set Saldo = Saldo + @montoPrincipal where IdCuenta = @idCuentaDestino

			insert into Transacciones (IdCuentaOrigen, IdCuentaDestino, TipoTransaccion, Monto, IdMoneda, Descripcion)
			values
			(null, @idCuentaDestino, 'DEPOSITO_PRESTAMO', @montoPrincipal, @idMoneda, 'Depósito de préstamo. Nro: ' + cast(@idPrestamo as varchar));


			commit transaction;
			select @idPrestamo
		end try

		begin catch
			rollback transaction;
			throw;
		end catch
end;
go
--




-- SP Pagar Cuota Prestamo
create or alter proc PagarCuotaPrestamo(
	@idCuota int,
	@idCuentaOrigen int,
	@montoAPagar decimal(18, 4)
)
as
begin
	set nocount on;
	
	declare @idPrestamo int;
	declare @montoTotalCuota decimal(18, 4);
	declare @montoPagadoActualCuota decimal(18, 4);
	declare @montoCapitalCuota decimal(18, 4);
	declare @estadoCuotaActual varchar(50);
	declare @idMonedaCuota int;

	declare @idClientePrestamo int;
	declare @saldoPendientePrestamo decimal(18, 4);
	declare @estadoPrestamoActual varchar(50);

	declare @idClienteCuenta int;
	declare @saldoCuentaOrigen decimal(18, 4);
	declare @idMonedaCuentaOrigen int;

	declare @montoPendienteCuota decimal(18, 4);
	declare @montoRealPagado decimal(18, 4);

	begin transaction;
		begin try
			
			-- CuotaPrestamo

			select 
				@idPrestamo = cp.IdPrestamo,
				@montoTotalCuota = cp.MontoTotalCuota,
				@montoPagadoActualCuota = cp.MontoPagado,
				@montoCapitalCuota = cp.MontoCapital,
				@estadoCuotaActual = cp.EstadoCuota,
				@idMonedaCuota = p.IdMoneda
			from CuotasPrestamo cp
			inner join Prestamos p on (cp.IdPrestamo = p.IdPrestamo)
			where cp.IdCuota = @idCuota

			if @idPrestamo is null
			begin
				raiserror('La cuota no existe.', 16, 1);
			end

			if @estadoCuotaActual = 'PAGADA'
			begin
				raiserror('La cuota ya ha sido pagada.', 16, 1);
			end

			if @montoAPagar <= 0
			begin
				raiserror('El monto debe ser mayor a cero.', 16, 1);
			end

			-- Prestamo y Cuenta

			select
				@idClientePrestamo = p.IdCliente,
				@saldoPendientePrestamo = p.SaldoPendiente,
				@estadoPrestamoActual = p.EstadoPrestamo
			from Prestamos p
			where p.IdPrestamo = @idPrestamo


			select
				@idClienteCuenta = cb.IdCliente,
				@saldoCuentaOrigen = cb.Saldo,
				@idMonedaCuentaOrigen = cb.IdMoneda
			from CuentasBancarias cb
			where cb.IdCuenta = @idCuentaOrigen

			if @idClienteCuenta is null
			begin
				raiserror('La cuenta bancaria no existe', 16, 1);
			end

			if @idClienteCuenta <> @idClientePrestamo
			begin
				raiserror('La cuenta no tiene relación con el préstamo', 16, 1);
			end

			if @idMonedaCuentaOrigen <> @idMonedaCuota
			begin
				raiserror('Las monedas no coiciden.', 16 ,1);
			end

			set @montoPendienteCuota = @montoTotalCuota - @montoPagadoActualCuota;
			set @montoRealPagado = IIF(@montoAPagar > @montoPendienteCuota, @montoPendienteCuota, @montoAPagar);

			if @saldoCuentaOrigen < @montoRealPagado
			begin
				raiserror('Saldo insuficiente en la cuenta de origen.', 16 , 1);
			end


			update CuentasBancarias
			set Saldo = Saldo - @montoRealPagado
			where IdCuenta = @idCuentaOrigen

			update CuotasPrestamo
			set
				MontoPagado = MontoPagado + @montoRealPagado,
				EstadoCuota = 
					case
						when (MontoPagado + @montoRealPagado) >= MontoTotalCuota then 'PAGADA'
						else 'PENDIENTE'
					end
			where IdCuota = @idCuota


			if (select cp.EstadoCuota from CuotasPrestamo cp where IdCuota = @idCuota) = 'PAGADA'
			begin
				update Prestamos
				set SaldoPendiente = SaldoPendiente - @montoCapitalCuota
				where IdPrestamo = @idPrestamo

				if (select p.SaldoPendiente from Prestamos p where p.IdPrestamo = @idPrestamo) <= 0.1000
				begin
					update Prestamos
					set EstadoPrestamo = 'PAGADO'
					where IdPrestamo = @idPrestamo
				end
			end

			
			insert into Transacciones (IdCuentaOrigen, IdCuentaDestino, TipoTransaccion, Monto, IdMoneda, Descripcion)
			values
			(@idCuentaOrigen, null, 'PAGO_PRESTAMO', @montoRealPagado, @idMonedaCuota, 'Pago de cuota nro: ' + CAST( (select NumeroCuota from CuotasPrestamo where IdCuota = @idCuota) as varchar) + ' del préstamo nro ' + CAST(@idPrestamo as varchar) );


			commit transaction;

			select @idCuota
		end try

		begin catch
			if @@TRANCOUNT > 0
				rollback transaction;
			throw;
		end catch

end;
go
--


-- SP Listar CuotasPrestamo
create or alter proc ListarCuotasPrestamo
as
	select * from CuotasPrestamo
go
--


-- SP ObtenerCuotaPorID
create or alter proc ObtenerCuotaPorId(
	@id int
)
as
	select * from CuotasPrestamo cp where cp.IdCuota = @id
go
---


-- SP ObtenerCuotasPorPrestamo
create or alter proc ObtenerCuotasPorPrestamo(
	@idPrestamo int
)
as
	select * from CuotasPrestamo cp where cp.IdPrestamo = @idPrestamo
go
---


-- prueba, no ejecutar
-- idcliente, idMoneda, montoPrincipal, tasaInteres, plazoMeses
exec AgregarPrestamo 3, 3, 100000, 0.12, 24
--
