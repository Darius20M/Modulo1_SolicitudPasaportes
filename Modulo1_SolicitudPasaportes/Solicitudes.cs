using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace Modulo1_SolicitudPasaportes
{
    struct Solicitudes
    {
        string? N_Solicitud;
        string? Tipo_Solicitud;
        string? Nombres_Solicitante;
        string? Apellidos_Solicitante;
        int edad;
        string? Pais_Origen;
        string? Estado_Civil;
        string? Sexo;

        //Getters
        public string? Gn_Solicitud { get => N_Solicitud; set => N_Solicitud = value; }
        public string? GTipo_Solicitud { get => Tipo_Solicitud; set => Tipo_Solicitud = value; }
        public string? GNombres_Solicitante { get => Nombres_Solicitante; set => Nombres_Solicitante = value; }
        public string? GApellidos_Solicitante { get => Apellidos_Solicitante; set => Apellidos_Solicitante = value; }
        public int Gedad { get => edad; set => edad = value; }
        public string? GPais_Origen { get => Pais_Origen; set => Pais_Origen = value; }
        public string? GEstado_Civil { get => Estado_Civil; set => Estado_Civil = value; }
        public string? GSexo { get => Sexo; set => Sexo = value; }
    }
}
