using Azure.Data.Tables;
using Azure.Data.Tables.Sas;
using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Coling.API.Curriculum.Implementacion.Repositorios
{
    public class Repositorio : IRepositorio
    {
        private string cadenaConexion;
        public Repositorio(string cadena)
        {
            this.cadenaConexion = cadena;
        }

        public async Task<bool> Actualizar<T>(T tmodelo) where T : ITableEntity
        {
            var tabla = new TableClient(cadenaConexion, tmodelo.GetType().Name);
            await tabla.CreateIfNotExistsAsync();
            tmodelo.PartitionKey = tmodelo.GetType().Name + "es";

            var entidad = await tabla.GetEntityAsync<TableEntity>(tmodelo.PartitionKey, tmodelo.RowKey);
            if (entidad.Value != null)
            {
                tmodelo.ETag = entidad.Value.ETag;
                await tabla.UpdateEntityAsync(tmodelo, tmodelo.ETag);
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Eliminar<T>(T tmodelo) where T : ITableEntity
        {
            var tabla = new TableClient(cadenaConexion, tmodelo.GetType().Name);
            tabla.CreateIfNotExistsAsync();
            tmodelo.PartitionKey = tmodelo.GetType().Name + "es";
            await tabla.DeleteEntityAsync(tmodelo.PartitionKey, tmodelo.RowKey);
            return true;
        }

        public async Task<bool> Insertar<T>(T tmodelo) where T : ITableEntity
        {
            try
            {
                var tabla = new TableClient(cadenaConexion, tmodelo.GetType().Name);
                tabla.CreateIfNotExistsAsync();
                tmodelo.PartitionKey = tmodelo.GetType().Name + "es";
                tmodelo.RowKey = Guid.NewGuid().ToString();
                await tabla.UpsertEntityAsync(tmodelo);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IAsyncEnumerable<TableEntity>> ListarTodos<T>() where T : ITableEntity, new()
        {
            var tabla = new TableClient(cadenaConexion, typeof(T).Name);
            tabla.CreateIfNotExistsAsync();
            var pageResponse = tabla.QueryAsync<TableEntity>(filter: "", maxPerPage: 10);
            return pageResponse;
        }

        public async Task<TableEntity> ListarUno<T>(T tmodelo) where T : ITableEntity
        {
            var tabla = new TableClient(cadenaConexion, typeof(T).Name);
            tabla.CreateIfNotExists();
            tmodelo.PartitionKey = tmodelo.GetType().Name + "es";
            var exist = await tabla.GetEntityIfExistsAsync<TableEntity>(tmodelo.PartitionKey, tmodelo.RowKey);
            if (exist.HasValue)
            {
                var data = await tabla.GetEntityAsync<TableEntity>(tmodelo.PartitionKey, tmodelo.RowKey);
                return data;
            }
            return null;
        }

    }
}
