﻿namespace Huy.Clothing.Application.Interfaces.Repositories;

public interface IRepository<T> :IBaseReaderRepository<T>,IBaseWriterRepository<T>,IBaseRepo<T> where T : class
{
}
