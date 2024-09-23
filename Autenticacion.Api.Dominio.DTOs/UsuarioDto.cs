﻿namespace Autenticacion.Api.Dominio.DTOs
{
    public class UsuarioDto
    {
        public long IdUsuario { get; set; }

        public long IdRol { get; set; }

        public long IdPersona { get; set; }

        public string Correo { get; set; } = null!;

        public string Contraseña { get; set; } = null!;

        public string? UsuarioQueActualiza { get; set; }

        public string IpDeRegistro { get; set; } = null!;

        public DateOnly? FechaDeActualizado { get; set; }

        public TimeOnly? HoraDeActualizado { get; set; }

        public string? IpDeActualizado { get; set; }

        // public virtual Persona IdPersonaNavigation { get; set; } = null!;

        //    public virtual Role IdRolNavigation { get; set; } = null!;

    }
}
