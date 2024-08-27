using Domain.Entidades;
using Domain.Interfaces.Repositories;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UsuarioRepository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioDBContext _dbContext;
        public UsuarioRepository(UsuarioDBContext usuarioDBContext)
        {
            _dbContext = usuarioDBContext;
        }

        //public async Task Add(List<Usuarios> usuarios)
        //{
        //    await _dbContext.BulkInsertAsync(usuarios);
        //}

        public async Task Add(List<Usuarios> usuarios)
        {
            foreach (var usuario in usuarios)
            {
                 _dbContext.Add(usuario);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
