using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Domain.Models.Requests
{
    public class GetCityRequest
    {
        public int Id { get; }

        protected GetCityRequest(int id)
        {
            Id = id;
        }

        public static GetCityRequest Create(int id)
        { 
            if (id <= 0)
            {
                throw new InvalidOperationException("Id cannot be less than zero");
            }

            return new GetCityRequest(id);
        }
    }
}
