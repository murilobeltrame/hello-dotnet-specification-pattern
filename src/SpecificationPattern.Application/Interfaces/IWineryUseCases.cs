﻿using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Application.Interfaces
{
    public interface IWineryUseCases : IBaseCRUDUseCases<Winery, CreateWineryRequest, FetchWineriesRequest> { }

    public class FetchWineriesRequest : IBaseFilter
    {
        public uint? Page { get; set; }

        public ushort? Size { get; set; }

        public string? Sort { get; set; }

        public string? Name { get; set; }
    }

    public class CreateWineryRequest
    {
        public string Name { get; set; }
    }
}
