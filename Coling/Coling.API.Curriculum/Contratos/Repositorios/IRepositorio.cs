﻿using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorios
{
    public interface IRepositorio
    {
        Task<IAsyncEnumerable<TableEntity>> ListarTodos<T>() where T : ITableEntity , new(); 
        Task<bool> Insertar<T>(T tmodelo) where T : ITableEntity ;
        Task<bool> Actualizar<T>(T tmodelo) where T : ITableEntity;
        Task<bool> Eliminar<T>(T tmodelo) where T : ITableEntity;
        Task<TableEntity> ListarUno<T>(T tmodelo) where T : ITableEntity;
    }
}
