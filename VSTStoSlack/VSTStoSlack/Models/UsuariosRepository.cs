using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSTStoSlack.Models
{
    public static class UsuariosRepository
    {
        public static List<Usuario> GetUsuarios()
        {
            var usuarios = new List<Usuario>();

            usuarios.Add(new Usuario()
            {
                NomeVSO = "Arya Stark(mailto:aryastark@hotmail.com)",
                NomeSlack = "arya"
            });
            usuarios.Add(new Usuario()
            {
                NomeVSO = "Robb Stark(mailto:robbstark@hotmail.com)",
                NomeSlack = "thekingofthenorth"
            });
            usuarios.Add(new Usuario()
            {
                NomeVSO = "Reinaldo Camargo(mailto:camargo.reinaldo@hotmail.com)",
                NomeSlack = "reinaldo"
            });

            return usuarios;
        }

    }
}