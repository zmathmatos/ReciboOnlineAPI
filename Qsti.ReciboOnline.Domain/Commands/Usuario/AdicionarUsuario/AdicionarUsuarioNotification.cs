﻿using MediatR;

namespace Qsti.ReciboOnline.Domain.Commands.Usuario.AdicionarUsuario
{
    public class AdicionarUsuarioNotification : INotification
    {
        public AdicionarUsuarioNotification(Entities.Usuario usuario)
        {
            Usuario = usuario;
        }

        public Entities.Usuario Usuario { get; set; }
    }
}
