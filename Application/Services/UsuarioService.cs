using Application.Helpers;
using Domain.Entidades;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Add(IFormFile file)
        {
           var excel = new ExcelHelper<Usuarios>(file);
            var usuarios = excel.GetValues();
            await _repository.Add(usuarios);
        }
    }
}
