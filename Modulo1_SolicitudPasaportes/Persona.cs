using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace Modulo1_SolicitudPasaportes
{
    struct Persona
    {
        string? Nombres;
        string? Apellidos;
        DateTime Fecha_Nacimiento;
        string Pais_Nacimiento;
        string? Provincia;
        //string Ciudad;  eliminamos este porque es lo mismo que provincia
        string? Sector;
        int Edad;
        string? Direccion;
        string? Lugar_Nacimiento;
        string Telefono;
        string Estado_civil;
        string Sexo;
        string? Correo;
        string? Cedula;

        public string? GNombres { get => Nombres; set => Nombres = value; }
        public string? GApellidos { get => Apellidos; set => Apellidos = value; }
        public DateTime GFecha_Nacimiento { get => Fecha_Nacimiento; set => Fecha_Nacimiento = value; }
        public string GPais_Nacimiento { get => Pais_Nacimiento; set => Pais_Nacimiento = value; }
        public string? GProvincia { get => Provincia; set => Provincia = value; }
        //public string GPCiudad { get => Ciudad; set => Ciudad = value; } eliminamos este porque es lo mismo que provincia
        public string? GSector { get => Sector; set => Sector = value; }
        public int GEdad { get => Edad; set => Edad = value; }
        public string? GDireccion { get => Direccion; set => Direccion = value; }
        public string? GLugar_Nacimiento { get => Lugar_Nacimiento; set => Lugar_Nacimiento = value; }
        public string GTelefono { get => Telefono; set => Telefono = value; }
        public string GEstado_civil { get => Estado_civil; set => Estado_civil = value; }
        public string GSexo { get => Sexo; set => Sexo = value; }
        public string? GCorreo { get => Correo; set => Correo = value; }
        public string? GCedula { get => Cedula; set => Cedula = value; }
    }
}
