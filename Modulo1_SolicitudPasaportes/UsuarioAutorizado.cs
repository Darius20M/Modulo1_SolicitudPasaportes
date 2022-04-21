using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulo1_SolicitudPasaportes
{
    struct UsuarioAutorizado
    {
        //Campos de clase / atributos
        readonly string nameUser;

        //Getters / Propiedades
        public string GnameUser { get => nameUser; }

        //Constructor
        public UsuarioAutorizado(string admin)
        {
            nameUser = admin;
        }
    }
}
