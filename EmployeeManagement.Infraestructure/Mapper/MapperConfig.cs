using EmployeeManagement.Entity;
using EmployeeManagement.Entity.Dto.Request;
using EmployeeManagement.Entity.Dto.Response;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infraestructure.Mapper
{
    public  class MapperConfig
    {
        /// <summary>
        /// Mapper Configuration
        /// </summary>
        /// <returns></returns>
        public static TypeAdapterConfig ConfigureMapper()
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<EmployeeRequestDto, Employee>()
                .Map(dst => dst.Id, org => org.Id)
                .Map(dst => dst.FirstName, org => org.FirstName)
                .Map(dst => dst.LastName, org => org.LastName)
                .Map(dst => dst.Position, org => org.Position)
                .Map(dst => dst.Phone, org => org.Phone)
                .Map(dst => dst.Address, org => org.Address)
                .Map(dst => dst.Email, org => org.Email)
                .IgnoreNonMapped(true);


            return config;
        }
    }
}
