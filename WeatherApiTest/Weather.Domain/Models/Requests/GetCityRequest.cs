using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Domain.Models.Requests
{
    public class GetCityRequest
    {
        public int Id { get; }
        public string Name { get; }

        protected GetCityRequest(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static GetCityRequest Create(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException($"Name cannot be empty ({id}, {name})");
            }

            if (id <= 0)
            {
                throw new InvalidOperationException("Id cannot be less than zero");
            }

            return new GetCityRequest(id, name);
        }
    }
}
