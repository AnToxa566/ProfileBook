﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProfileBook.Model;

namespace ProfileBook.Services.Repository
{
    public interface IRepository
    {
        Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new();

        Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new();

        Task<int> DeletAsync<T>(T entity) where T : IEntityBase, new();

        Task<List<T>> GetAllAsync<T>() where T : IEntityBase, new();
    }
}
