using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SuperProyecto.Core;
using SuperProyecto.Core.Enums;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Dapper
{
    public class RepoUsuario : Repo, IRepoUsuario
    {
        public RepoUsuario(IAdo ado) : base(ado) { }

        private static readonly string _queryDetalleUsuario
            = @"SELECT * FROM Usuario WHERE idUsuario = @idUsuario";
        public Usuario? DetalleUsuario(int idUsuario)
        {
            return _conexion.QueryFirstOrDefault<Usuario>(
                _queryDetalleUsuario,
                new
                {
                    idUsuario = idUsuario
                }
            );
        }

        private static readonly string _queryDetalleUsuarioXEmail
            = @"SELECT * FROM Usuario WHERE email = @email";
        public Usuario? DetalleUsuarioXEmail(string email)
        {
            return _conexion.QueryFirstOrDefault<Usuario>(
                _queryDetalleUsuarioXEmail,
                new
                {
                    email = email
                }
            );
        }

        private static readonly string _queryAltaUsuario
            = @"INSERT INTO Usuario (email, passwordHash, rol) VALUES (@email, @passwordHash, @rol)";
        public void AltaUsuario(Usuario usuario)
        {
            _conexion.Execute(
                _queryAltaUsuario,
                new
                {
                    usuario.email,
                    usuario.passwordHash,
                    usuario.rol
                });
        }

        private static readonly string _queryUpdateRolUsu
            = @"UPDATE Usuario SET rol = @rol WHERE idUsuario = @idUsuario";
        public void ActualizarRol(int idUsuario, ERol rol)
        {
            _conexion.Execute(
                _queryUpdateRolUsu,
                new
                {
                    rol,
                    idUsuario
                });
        }

        private static readonly string _queryUniqueEmail
            = @"SELECT * FROM Usuario WHERE email = @email";
        public bool UniqueEmail(string email)
        {
            var result = _conexion.QueryFirstOrDefault(
                _queryUniqueEmail,
                new
                {
                    email
                }
            );
            return result != null;
        }
    }
}