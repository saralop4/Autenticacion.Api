﻿using Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Autenticacion.Web.Api.Aplicacion.Validadores
{
    public class IniciarSesionDtoValidador : AbstractValidator<IniciarSesionDto>
    {
        public IniciarSesionDtoValidador()
        {
            RuleFor(u => u.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .NotNull().WithMessage("El correo no puede ser nulo.")
                .MaximumLength(80).WithMessage("El correo no puede tener más de 80 caracteres.")
                .Must(CorreoValido).WithMessage("El correo debe tener un formato válido. (ejemplo@dominio.com)");

            RuleFor(u => u.Contraseña)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .NotNull().WithMessage("La contraseña no puede ser nula.");
        }

        private bool CorreoValido(string correo)
        {
            // Expresión regular para validar el formato del correo
            var correoValido = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, correoValido);
        }
    }
}
