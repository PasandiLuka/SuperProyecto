using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Dapper;

namespace SuperProyecto.Services.Service
{
    public class UsuarioService
    {
            readonly IRepoUsuario _repoUsuario;
    public UsuarioService(IRepoUsuario repoUsuario)
    {
        _repoUsuario = repoUsuario;
    }

    public Usuario? DetalleUsuario(int id) => _repoUsuario.DetalleUsuario(id);

    public void AltaUsuario(UsuarioDto usuarioDto)
    {
        var usuario = ConvertirDtoClase(usuarioDto);
        _repoUsuario.AltaUsuario(usuario);
    }
  
    public void UpdateUsuario(UsuarioDto usuarioDto, int id)
    {
        var usuario = ConvertirDtoClase(usuarioDto);
            _repoUsuario.ActualizarRol(id, usuario.rol);
    }


    static Usuario ConvertirDtoClase(UsuarioDto usuarioDto)
    {
        return new Usuario
        {
            email = usuarioDto.email,
            passwordHash = usuarioDto.password,
            rol = usuarioDto.Rol
        };
    }
    }
}