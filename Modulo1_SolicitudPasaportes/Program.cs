using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Windows;
using System.Text.RegularExpressions;
using System.Threading;

#nullable enable

namespace Modulo1_SolicitudPasaportes
{
    class Program
    {
        //ATRIBUTOS USER
        static private byte intentoLogin = 3; //Números de intento para iniciar sección
        static private string? dateUser; //Usuario que inicia sección correctamente

        //ATRIBUTOS SOLICITUD
        static private string? LetterData;
        static private long NumberData = 10000;

        //INSTANCIAS
        static Persona solicitante = new();
        static Solicitudes solicitud = new();
        
        //LISTAS
        static readonly private List<string> Extensiones = new();
        static readonly private List<string> Correos = new();


        //Inicializacion del arreglo multidimensional
        //Pagina de referencia latindevelopers.com/articulo/arreglos-en-csharp/
        static readonly string[] Tipos_Solicitudes = 
            { "Renovación de Pasaporte por Vencimiento\t",
              "Renovación de Pasaporte por Agotamiento Para Adultos\t",
              "Renovación de Pasaporte por Deterioro Para Adultos\t",
              "Renovación de Pasaporte por Pérdida (Primera Vez)\t",
              "Renovación de Pasaporte por Pérdida (Segunda Vez)\t",
              "Renovación de Pasaporte por Pérdida (Tercera Vez)\t" };


        //MENUS EPICOS
        //Indexador: Almacena el index de la opcion seleccionada en el arreglo opciones
        static private int indexador = 0;

        static readonly string[] Tipos_Pais = { "Estados Unidos\t", "Republica Dom\t", "Mexico\t", "Colombia\t" };
        static private int indexPais = 0;

        static readonly string[] Tipos_Estados = { "Casado\t", "Soltero\t", "Union Libre\t" };
        static private int indexestado = 0;

        static readonly string[] Tipos_sexo = { "Masculino\t", "Femenino\t", "No Binario\t" };
        static private int indexsexo = 0;

        static void Main(string[] args)
        {

            //ELEMENTOS DE LISTAS
            Extensiones.Add("829");
            Extensiones.Add("809");
            Extensiones.Add("849");

            Correos.Add("@gmail");
            Correos.Add("@Outlook");
            Correos.Add("@hotmail");


            Dictionary<UsuarioAutorizado, string> listAdmins = new(); //Creación de diccionario
            listAdmins.Add(new UsuarioAutorizado("Darius20M"), "Darius20"); //Credenciales del priemer User.
            listAdmins.Add(new UsuarioAutorizado("JeffersonJ03"), "Qwe#741085250320165");
            listAdmins.Add(new UsuarioAutorizado("User2"), "1234");
            listAdmins.Add(new UsuarioAutorizado("user3"), "1234");
            bool mainbucle = true;
            while (mainbucle)//Bucle principal del software
            {
                if (LoginAdmin(listAdmins))//Inicio de seeción del User Admin
                {
                    bool CierreSesión = false;
                    while (!CierreSesión)
                    {
                        Console.WriteLine($"\n\t\t\t\tBienvenido seas {dateUser}");
                        //Console.ReadLine(); //Aquí iniciaría el resto del Software, luego de iniciar sesión

                        Console.Write("\n\n\t\t\t\tIngresando al menu de opciones...");
                        LoadBar(false);
                        Menu();
                        Console.Clear();
                        Console.Write("\n\n\t\t\t\tGenerando número de solicitud...");
                        GenerarSolicitud();
                        Console.WriteLine("\n\n\t\t\t\t   ¿Quieres confirmar estos datos?\n");
                        Console.WriteLine("\n\n\t\t\t\t   Presione Y para confirmar o N para cancelar...");
                        bool subool = false;
                        do
                        {
                            ConsoleKeyInfo UserOpcData = Console.ReadKey(true);
                            if (UserOpcData.Key == ConsoleKey.Y)
                            {
                                Console.WriteLine("\nDatos confirmados. Presione una tecla para continuar.");
                                Console.ReadLine(); //¿Que procede después de aquí?.
                            }
                            else if (UserOpcData.Key == ConsoleKey.N)
                            {
                                Console.Clear();
                                Console.Write("\n\n\t\t\t\tCancelando...");
                                LoadBar(true);
                                Console.Clear();
                            }
                            else
                                continue;
                        }
                        while (subool);

                    }
                }
                else//Esto es lo que pasaría si las credenciales de inicio de sección son incorrectas
                {
                    if (intentoLogin == 0)//Estos es si supera el número de intentos de inicio de sesión,
                    {
                        Console.WriteLine("\t\t\t\tSoftware Pasaportes v1.0\n");
                        Console.WriteLine("\t\t\t\tInicio de Sesión Admin\n\n");
                        Console.WriteLine("\t\t\t\tNúmero de intentos excedido.");
                        Console.Write("\t\t\t\tSoftware bloqueado durante: ");
                        for (int i = 61; i > 0; i--)
                        {
                            Task.Delay(1000).Wait();
                            Console.Write($"{i} segundos.");
                            Console.SetCursorPosition(60, Console.CursorTop);
                        }
                        intentoLogin += 3;
                        Console.Clear();
                        continue;
                    }
                    else//Si aún le queda intentos, continuará ejecutando la setencia: while (mainbucle)
                        continue;
                }
            }
        }

        static private bool LoginAdmin(Dictionary<UsuarioAutorizado, string> redateCollection)//Método para inciar sección el usuario Admin. Recibe como parametro un dato de tipo Dictionary.
        {
            Console.WriteLine("\t\t\t\tSoftware Pasaportes v1.0\n");
            Console.WriteLine("\t\t\t\tInicio de Sesión Admin\n\n");
            Console.Write("\t\t\t\tUsuario: ");
            dateUser = Console.ReadLine();
            Console.Write("\n\t\t\t\tContraseña: ");
            string? password = Console.ReadLine();
            Console.Clear();
            Console.Write("\n\n\n\t\t\t\tEspere por favor.");
            for (int i = 0; i < 15; i++)
            {
                Task.Delay(110).Wait();
                Console.Write(".");
            }
            if(ValidationLogin(redateCollection, dateUser, password))//Aquí se invoca un método que validará que las credenciales introducidas se encuentren en el diccionario Listadmins
            {
                Console.Clear();
                Console.Write("\n\n\n\t\t\t\tSesión Iniciada Correctamente!");
                Task.Delay(3000).Wait();
                Console.Clear();
                return true;
            }
            else
            {
                Console.Clear();
                intentoLogin--;
                Console.WriteLine("\n\n\n\t\t\t\tUsuario o contraseña incorrecto");
                Console.WriteLine($"\t\t\t\tNúmero de intentos disponibles: {intentoLogin}");
                Task.Delay(2500).Wait();
                Console.Clear();
                return false;
            }
        }

        static private bool ValidationLogin(Dictionary<UsuarioAutorizado, string> dataCollection,  string? datauser_aValidar, string? datapass_aValidar)
        {
            foreach (var searchdata in dataCollection)//Bucle que recorre todos los elementos del diccionario.
            {
                int opc = String.Compare(datauser_aValidar, searchdata.Key.GnameUser, false);//Se almacenan los resultados obtenidos de la comparación
                int opc1 = String.Compare(datapass_aValidar, searchdata.Value, false);
                if (opc == 0 && opc1 == 0)//Si coinciden, retorna True
                    return true;
                else
                    continue;//De lo contrario seguirá buscando hasta terminar de reccorer el diccionario completamente.
            }
            return false;//Retornará false, si nunca encuentra coincidencia entre los elementos del diccionario y los introducidos por el usuario.
        }

        private static void Menu()
        {
            while (true)
            {
                //Metodo Header Menu
                HeaderMenu();
                Console.Write("\t\t\t\t       *  Bienvenido a la Dirección General De Pasaporte  *\n\n");
                Console.Write("\t\t\t\t   -----------------------------------------------------------\n\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\t\t\t\t                Seleccione la Acción a Realizar:\n\n\n");
                Console.ResetColor();
                //Imprime las opciones disponibles Recorriendo todos los indices del arreglo Opciones
                //La propiedad Length indica la cantidad de indices que tiene el arreglo
                //for (inicializador; condición; iterador)
                // Referencia docs.microsoft.com/es-es/dotnet/csharp/language-reference/statements/iteration-statements
                for (int i = 0; i < Tipos_Solicitudes.Length; i++)
                {
                    //? para dar formato   
                    Console.Write("\t\t\t\t    ");
                    //Colores
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;

                    // imprime si el iniciador y el indexador tienen el mismo valor
                    //El operador ?, verifica que sea true o false e imprime -> en caso de ser true, y nada en caso de ser false
                    // referencia docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
                    Console.Write(i == indexador ? "->" : "");
                    Console.ResetColor();
                    Console.ResetColor();
                    //Imprime las opciones 
                    Console.WriteLine((" ") + Tipos_Solicitudes[i] + (""));
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\t\t\t\t    F1 - Salir");
                Console.ResetColor();
                Console.ResetColor();


                //Console.WriteLine(menuSelect);
                //Console.WriteLine(Opciones.Length);

                var TeclaPresionada = Console.ReadKey();

                //Incrementa en caso en caso de presionar flecha abajo y el indexador ser diferente de 5 (que es el maximo indice)
                //Si el indexador es igual a 5, no incrementa debido a que alcanzo el limite indices que tiene el arreglo Opciones
                if (TeclaPresionada.Key == ConsoleKey.DownArrow && indexador != Tipos_Solicitudes.Length - 1)
                    indexador++;
                //Decrecrementa en caso en caso de presionar flecha Arriba y el indexador ser Mayor o igual que 1 ()
                //Si el indexador es menor que 1 no decrementa debido a que alcanzo el minimo de indices que tiene el arreglo Opciones
                else if (TeclaPresionada.Key == ConsoleKey.UpArrow && indexador >= 1)
                    indexador--;
                //Sale al precionar f1
                else if (TeclaPresionada.Key == ConsoleKey.F1)
                {
                    Console.WriteLine("\n\n\t\t\t\t Presiona cualquier tecla para salir:");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else if (TeclaPresionada.Key == ConsoleKey.Enter)
                {
                    //Switch que funciona segun el index de la variable 
                    switch (indexador)
                    {
                        //Case contiene el metodo segun el tipo de solicitud
                        case 0:
                            Vencimiento();
                            break;
                        case 1:
                            Agotamiento();
                            break;
                        case 2:
                            Deterioro();
                            break;
                        case 3:
                            Perdida1();
                            break;
                        case 4:
                            Perdida2();
                            break;
                        case 5:
                            Perdida3();
                            break;
                    }
                    break;
                }
            }
        }

            //Metodo Vencimiento
        private static void Vencimiento()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t Renovación de Pasaporte por Vencimiento.\n");
            Console.Write("\t\t\t\t Presiona Cualquier Tecla Para Continuar Con El Proceso:");
            Console.ReadLine();
            LetterData = "AVO";
            Captura();
        }

        //Metodo Agotamiento
        private static void Agotamiento()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Renovación de Pasaporte por Agotamiento\n");
            Console.Write("\t\t\t\t Presiona Cualquier Tecla Para Continuar Con El Proceso:");
            Console.ReadLine();
            LetterData = "BAT";
            Captura();
        }

        //Metodo Deterioro
        private static void Deterioro()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Renovación de Pasaporte por Deterioro\n");
            Console.Write("\t\t\t\t Presiona Cualquier Tecla Para Continuar Con El Proceso:");
            Console.ReadLine();
            LetterData = "CDR";
            Captura();
        }

        //Metodo Perdida1
        private static void Perdida1()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Renovación de Pasaporte por Perdida (Primera Vez)\n");
            Console.Write("\t\t\t\tPresiona Cualquier Tecla Para Continuar Con El Proceso:");
            Console.ReadLine();
            LetterData = "DPF";
            Captura();
        }

        //Metodo Perdida2
        private static void Perdida2()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Renovación de Pasaporte por Perdida (Segunda Vez)\n");
            Console.Write("\t\t\t\t Presiona Cualquier Tecla Para Continuar Con El Proceso:");
            Console.ReadLine();
            LetterData = "EPS";
            Captura();
        }

        //Metodo Perdida3
        private static void Perdida3()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Renovación de Pasaporte por Perdida (Tercera Vez)\n");
            Console.Write("\t\t\t\t Presiona Cualquier Tecla Para Continuar Con El Proceso:");
            Console.ReadLine();
            LetterData = "FPT";
            Captura();
        }

        private static void Captura()
        {
            
            bool Nom = true;
            while (Nom)
            {
                Console.Clear();
                HeaderFormularioLoading(false);        
                Console.Write("\n\t\t\t\t   Ingrese sus nombres: ");
                solicitante.GNombres = Console.ReadLine();


                if (!IsLetters(solicitante.GNombres))//el pasa por parametro el nombre y va al metro y si tiene numero devuelve true y sigue intentado
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\t\t\t\t   Error");
                    Console.ResetColor();
                    Console.Write("\t\t\t\t, el nombre no tiene un formato válido.");
                    Console.WriteLine("\n\\n\t\t\t\t   Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Nom = true;
                }
                else if(string.IsNullOrEmpty(solicitante.GNombres)) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\t\t\t\t   Error");
                    Console.ResetColor();
                    Console.Write("\t\t\t\t, No puede dejar esta casilla vacía");
                }
                else { Nom = false; } //si no tiene numero false y sale
            }

            while (!Nom)
            {
                Console.Write("\n\t\t\t\t   Ingrese sus apellidos: ");
                
                //Console.Write("\t\t\t\tIngrese sus apellidos: ");
                solicitante.GApellidos = Console.ReadLine();

                if (!IsLetters(solicitante.GApellidos)) Nom = false; //el pasa por parametro el nombre y va al metodo y si tiene numero devuelve false y sigue intentado
                else if (string.IsNullOrEmpty(solicitante.GApellidos))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\t\t\t\t   Error"); Console.ResetColor();
                    Console.Write("\t\t\t\t, No puede dejar esta casilla vacía");
                }
                else Nom = true; //si no tiene numero true y sale
            }

            //
            //fecha
            bool fec = true;
            while (fec)
            {
                try
                {
                    #pragma warning disable CS8604 // Posible argumento de referencia nulo
                    Console.Write("\n\t\t\t\t   Ingresa la fecha en el siguiente formato YYYY/MM/DD: ");
                    solicitante.GFecha_Nacimiento = DateTime.Parse(Console.ReadLine());
                  
                    fec = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\n\t\t\t\t   Fecha incorrecta intente nuevamente:");
                }

            }
            //
            //==================================================
            bool BoolPais = true;
            while (BoolPais)
            {
                Console.CursorVisible = false;
                Console.Clear();
                HeaderFormularioLoading(true);
                Console.WriteLine("\n\t\t\t\t  Seleccione Su Pais de Nacimiento:\n");
                for (int i = 0; i < Tipos_Pais.Length; i++)
                {
                    Console.Write("\t\t\t\t    ");

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(i == indexPais ? "->" : "");
                    Console.ResetColor();
                    Console.ResetColor();
                    Console.WriteLine((" ") + Tipos_Pais[i] + (""));
                }
                Console.ResetColor();

                var TeclaPais = Console.ReadKey();
                if (TeclaPais.Key == ConsoleKey.DownArrow && indexPais != Tipos_Pais.Length - 1)
                    indexPais++;
                else if (TeclaPais.Key == ConsoleKey.UpArrow && indexPais >= 1)
                    indexPais--;
                else if (TeclaPais.Key == ConsoleKey.Enter)
                {
                    solicitante.GPais_Nacimiento = Tipos_Pais[indexPais]; //almacena el pais segun el indice que se le dio enter
                    BoolPais = false;
                }
            }
            //==================================================
            Console.Write("");
            while (Nom)
            {
                Console.Write("\n\t\t\t\t  Ingrese la ciudad en la que vive: "); //captura de provincia
                solicitante.GProvincia = Console.ReadLine();
                if (!IsLetters(solicitante.GProvincia)) Nom = true; //el pasa por parametro el nombre y va al metro y si tiene numero devuelve true y sigue intentado
                else Nom = false; //si no tiene numero false y sale
            }
            
            while (!Nom)
            {
                Console.Write("\n\t\t\t\t  Ingrese su Sector: "); //caputra de sector
                solicitante.GSector = Console.ReadLine();
                if (!IsLetters(solicitante.GSector)) Nom = false; //el pasa por parametro el nombre y va al metro y si tiene numero devuelve true y sigue intentado
                else Nom = true; //si no tiene numero false y sale
            }

            //
             Edad();// llamada del metodo edad
            //
            

            while (Nom)
            {
                    Console.Write("\n\t\t\t\t  Ingrese Su direccion: ");
                    solicitante.GDireccion = Console.ReadLine();

                if (solicitante.GDireccion?.Length < 7) { 
                    Console.Write("\n\t\t\t\t  Error, La Dirección es muy corta."); Nom = true; }
                else Nom = false;
                
                        
            }

            //
            //Lugar de nacimiento
            //
            while (!Nom)
            {
                Console.Write("\n\t\t\t\t  Ingrese su ciudad de origen: ");
                solicitante.GLugar_Nacimiento = Console.ReadLine();

                if (solicitante.GLugar_Nacimiento?.Length < 7){
                    Console.Write("\n\t\t\t\t  Error, La ciudad es muy corta."); Nom = false;}
                else if (string.IsNullOrEmpty(solicitante.GNombres)) { 
                Console.WriteLine("\n\t\t\t\t  Error, no puede dejar esta info. vacía."); Nom = false;}
                else Nom = true;
            }

            //--------------------------------------------------
            //
            //telefono

            bool Btel = true;
            long validacion;
            while (Btel == true)
            {
                Console.Write("\n\t\t\t\t  Digite su número de telefono: ");

                try
                {
                    validacion = long.Parse(Console.ReadLine());
                    solicitante.GTelefono = validacion.ToString();

                    //Si comienza con extension inexistente entra al if (lista de extensiones arriba)
                    if (!Extensiones.Contains(solicitante.GTelefono.Substring(0, 3))) 
                        Console.WriteLine("\n\t\t\t\t  Escribió una extension inexistente");
                    else if (solicitante.GTelefono.Length != 10) //si contiene una cantidad diferente a 10 caracteres
                        Console.WriteLine("\n\t\t\t\t  Solo 10 caracteres");
                    else //si esta todo bien. Y da formato.
                    {
                        solicitante.GTelefono = string.Format("{0:###-###-####}", validacion);
                        Console.WriteLine("" + solicitante.GTelefono);
                        Btel = false;
                    }
                }
                catch (Exception)//Validación si escribe letras
                {
                    Console.WriteLine("\n\t\t\t\t  Solo números");
                }
            }

            //
            //==================================================
            bool BoolEstado = true;
            while (BoolEstado)
            {
                Console.CursorVisible = false;
                Console.Clear();
                HeaderFormularioLoading(true);
                Console.WriteLine("\n\t\t\t\t  Seleccione su Estado Civil\t");
                for (int i = 0; i < Tipos_Estados.Length; i++)
                {
                    Console.Write("\t\t\t\t    ");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(i == indexestado ? "->" : "");
                    Console.ResetColor();
                    Console.ResetColor();
                    Console.WriteLine((" ") + Tipos_Estados[i] + "");
                }
                //  Console.Write("\n\t\t\t\t F1 - Salir");
                Console.ResetColor();
                var TeclaEstado = Console.ReadKey();

                if (TeclaEstado.Key == ConsoleKey.DownArrow && indexestado != Tipos_Estados.Length - 1)
                    indexestado++;
                else if (TeclaEstado.Key == ConsoleKey.UpArrow && indexestado >= 1)
                    indexestado--;
                else if (TeclaEstado.Key == ConsoleKey.Enter)
                {
                    solicitante.GEstado_civil = Tipos_Estados[indexPais]; //almacena el pais segun el indice que se le dio enter
                    BoolEstado = false;
                }

            }

            //==================================================
            bool Boolsexo = true; //Seleccion de Sexo
            while (Boolsexo)
            {
                Console.CursorVisible = false;
                Console.Clear();
                HeaderFormularioLoading(true);
                Console.WriteLine("\n\t\t\t\t  Seleccione su Sexo\t");
                for (int i = 0; i < Tipos_sexo.Length; i++)
                {
                    Console.Write("\t\t\t\t    ");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;


                    Console.Write(i == indexsexo ? "->" : "");
                    Console.ResetColor();
                    Console.ResetColor();
                    Console.WriteLine((" ") + Tipos_sexo[i] + "");
                }
                //  Console.Write("\n\t\t\t\t F1 - Salir");
                Console.ResetColor();
                var TeclaSexo = Console.ReadKey();

                if (TeclaSexo.Key == ConsoleKey.DownArrow && indexsexo != Tipos_sexo.Length - 1)
                    indexsexo++;
                else if (TeclaSexo.Key == ConsoleKey.UpArrow && indexsexo >= 1)
                    indexsexo--;
                else if (TeclaSexo.Key == ConsoleKey.Enter)
                {
                    solicitante.GSexo = Tipos_sexo[indexsexo]; //almacena el sexo segun el indice que se le dio enter
                    Boolsexo = false;
                }
            }

            //CORREO ELECTRONICO
            //Using text regular expressions
            int repetidor = 1;
            while (repetidor == 1)
            {
                Console.Clear();
                HeaderFormularioLoading(false);
                Console.Write("\n\t\t\t\t  Escriba Su Correo Electrónico: ");
                solicitante.GCorreo = Console.ReadLine();

                if (!Regex.IsMatch(solicitante.GCorreo, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                Console.WriteLine("\n\t\t\t\t  Error, formato incorrecto."+"\n Escriba un correo con el siguiente formato: "+"Ejemplo:   aaaaa@sss.com ");

                /*else if(!Correos.Contains(solicitante.GCorreo))
                    Console.WriteLine("No contiene @gmail, bien escrito");*/ //Esta setencia no funciona. Favor Evaluar
                else
                    repetidor = 0;
            }

            bool Cbucle = true;
            long cedula;
            while (Cbucle == true)
            {
                Console.Write("\n\t\t\t\t  Digite Su cédula o Acta de nacimiento: ");
                try
                {
                    cedula = long.Parse(Console.ReadLine()); //Validación no tenga números
                    solicitante.GCedula = cedula.ToString();

                    if (solicitante.GCedula.Length != 11) //Validación sean 11 caracteres
                        Console.WriteLine("\n\t\t\t\t  Error: la cédula debe contener 11 carácteres.\n");
                    else //Si todo salio bien, entra aquí.
                    {
                        solicitante.GCedula = string.Format("{0:###-#######-#}", cedula);
                        //Console.ReadKey();
                        Cbucle = false;
                    }
                    #pragma warning restore CS8604 // Posible argumento de referencia nulo/ activacion de la Advertencia
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n\t\t\t\t  Error: La cédula solo contiene números.\n");
                }
            }
        }

   
        //Metodo que permite identificar si los caracteres son letras o no
        static bool IsLetters(string? sCaracteres)
        {
            #pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            foreach (char ch in sCaracteres)
            #pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            {
                if (!Char.IsLetter(ch) && ch != 32)
                    return false;
            }
            return true;
        }

        static void Edad() //metodo edad
        {
            DateTime FechaActual = DateTime.Today;
            int edad = FechaActual.Year - solicitante.GFecha_Nacimiento.Year;
            if (FechaActual.Month < solicitante.GFecha_Nacimiento.Month)
                --edad;

            solicitante.GEdad = edad;
        }

        static void LoadBar(bool ColorGreen) //Barra de carga con opción de aplicar o no el color verde
        {
            if (ColorGreen)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Task.Delay(2500).Wait();
                Console.Write("\n\n\t\t\t\t░░░░░░");
                Task.Delay(500).Wait();
                Console.Write("░░░░░░░░░░░░░░░");
                Task.Delay(500).Wait();
                Console.Write("░░░░░░░░░░░░░░░░░░░░░");
                Task.Delay(500).Wait();
                Console.Write("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
                Task.Delay(500).Wait();
                Console.ResetColor();
            }
            else
            {
                Task.Delay(2500).Wait();
                Console.Write("\n\n\t\t\t\t░░░░░░");
                Task.Delay(500).Wait();
                Console.Write("░░░░░░░░░░░░░░░");
                Task.Delay(500).Wait();
                Console.Write("░░░░░░░░░░░░░░░░░░░░░");
                Task.Delay(500).Wait();
                Console.Write("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
                Task.Delay(500).Wait();
            }
        }

        static void HeaderMenu() //Header menu tras inicio de sesion
        {
            //Limpiar la consola
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("");
            Console.WriteLine(@"
                                   ╔═════════════════════════════════════════════════════════╗
                                   ║                     ╔╦╗   ╔═╗   ╔═╗                     ║
                                   ║                      ║║ * ║ ╦ * ╠═╝                     ║
                                   ║                     ═╩╝   ╚═╝   ╩                       ║
                                   ╚═════════════════════════════════════════════════════════╝
                                   --------------DIRECCIÓN GENERAL DE PASAPORTE---------------
                ");
        }

        static void HeaderFormularioLoading(bool ConLoad) //Header Con Loading
        {
            if (ConLoad == false) {
                //Entra si es false
                //Limpiar la consola
                Console.Clear();
                HeaderMenu();
                LoadingRotativo();
                HeaderMenu();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\t\t\t\t           Por Favor Complete Los Siguientes Datos:\n\n\n");
                Console.ResetColor();
            }
            else
            {
                //Entra si es true
                //Limpiar la consola
                Console.Clear();
                HeaderMenu();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\t\t\t\t           Por Favor Complete Los Siguientes Datos:\n\n\n");
                Console.ResetColor();
            }
        }


        static void LoadingRotativo() //Barra de carga con opción de aplicar o no el color verde
        {
            var contador = 0;
            for (int i = 0; i < 28; i++)
            {
                //Esconder el cursor
                Console.CursorVisible = false;
                switch (contador % 4)
                {
                    case 0:
                        Console.Clear();
                        HeaderMenu();
                        Console.Write("\t\t\t\t                       Cargando Formulario.\n\n\n");
                        Console.Write("\t\t\t\t                                //"); break;
                    case 1:
                        Console.Clear();
                        HeaderMenu();
                        Console.Write("\t\t\t\t                       Cargando Formulario..\n\n\n");
                        Console.Write("\t\t\t\t                                -"); break;
                    case 2:
                        Console.Clear();
                        HeaderMenu();
                        Console.Write("\t\t\t\t                       Cargando Formulario.\n\n\n");
                        Console.Write("\t\t\t\t                                \\"); break;
                    case 3:
                        Console.Clear();
                        HeaderMenu();
                        Console.Write("\t\t\t\t                       Cargando Formulario.\n\n\n");
                        Console.Write("\t\t\t\t                                |"); break;
                }
                //Posicion del cursor
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                contador++;
                //Using System.Threading
                Thread.Sleep(40);
                Console.Clear();
            }
        }



        static private void GenerarSolicitud()
        {
            solicitud.Gn_Solicitud = LetterData + NumberData + 1;
            LoadBar(true);
            solicitud.GTipo_Solicitud = Tipos_Solicitudes[indexador];
            Console.Clear();
            HeaderMenu();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\t\t\t\t                  *  Datos de Su Solicitud  *\n");
            Console.ResetColor();
            Console.Write("\n\t\t\t\t   -----------------------------------------------------------\n\n");
            Task.Delay(512).Wait();
            Console.Write("\n\t\t\t\t   Número de solicitud: ");
            Task.Delay(250).Wait();
            Console.Write($"{solicitud.Gn_Solicitud}\n");
            Task.Delay(512).Wait(); 
            Console.Write("\n\t\t\t\t   Tipo de Solicitud: ");
            Task.Delay(250).Wait();
            Console.Write($"{solicitud.GTipo_Solicitud}\n");
            Task.Delay(512).Wait();
            Console.Write("\n\t\t\t\t   Nombre completo del solicitante: ");
            solicitud.GNombres_Solicitante = solicitante.GNombres;
            solicitud.GApellidos_Solicitante = solicitante.GApellidos;
            Task.Delay(250).Wait();
            Console.Write($"{solicitud.GNombres_Solicitante}" + " " + $"{solicitud.GApellidos_Solicitante}\n");
            Task.Delay(512).Wait();
            Console.Write("\n\t\t\t\t   Edad del solicitante: ");
            solicitud.Gedad = solicitante.GEdad;
            Task.Delay(250).Wait();
            Console.Write($"{solicitud.Gedad}\n");
            Task.Delay(512).Wait();
            Console.Write("\n\t\t\t\t   Pais de nacimiento: ");
            solicitud.GPais_Origen = solicitante.GPais_Nacimiento;
            Task.Delay(250).Wait();
            Console.Write($"{solicitud.GPais_Origen}\n");
            Task.Delay(512).Wait();
            Console.Write("\n\t\t\t\t   Estado Civil: ");
            solicitud.GEstado_Civil = solicitante.GEstado_civil;
            Task.Delay(250).Wait();
            Console.Write($"{solicitud.GEstado_Civil}\n");
            Task.Delay(512).Wait();
            Console.Write("\n\t\t\t\t   Sexo del Solicitante: ");
            solicitud.GSexo = solicitante.GSexo;
            Task.Delay(250).Wait();
            Console.Write($"{solicitud.GSexo}\n\n");
            Console.Write("\n\t\t\t\t   -----------------------------------------------------------\n\n");
        }
    }
}  

