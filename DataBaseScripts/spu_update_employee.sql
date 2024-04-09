
create procedure spu_update_employee(
@i_employee_id varchar(50),
@i_first_name varchar(50),
@i_last_name varchar(50),
@i_email varchar(50),
@i_position varchar(50),
@i_phone varchar(50),
@i_address varchar(50)
) as begin

	declare @w_total_row_count int

	update emp
	set
	emp.FirstName = ISNULL(@i_first_name, emp.FirstName),
	emp.LastName = ISNULL(@i_last_name, emp.LastName),
	emp.Email = ISNULL(@i_email,emp.Email),
	emp.Position = ISNULL(@i_position, emp.Position),
	emp.Phone = ISNULL(@i_phone, emp.Phone),
	emp.Address = ISNULL(Address, emp.Address),
	emp.ModifiedDate = GETDATE()
	from employee AS emp
	where id = @i_employee_id

	SELECT @w_total_row_count = @@ROWCOUNT;

end