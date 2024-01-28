using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Empleado> empleados = new List<Empleado>();
    const string nombreArchivo = "empleados.txt";

    static void MostrarMenu()
    {
        Console.WriteLine("*********************************");
        Console.WriteLine("   SISTEMA DE CONTROL DE EMPLEADOS  ");
        Console.WriteLine("*********************************");
        Console.WriteLine("1. Registrar nuevo empleado");
        Console.WriteLine("2. Mostrar empleados por departamento");
        Console.WriteLine("3. Mostrar todos los empleados");
        Console.WriteLine("4. Guardar empleados en archivo");
        Console.WriteLine("5. Cargar empleados desde archivo");
        Console.WriteLine("6. Salir");
        Console.WriteLine("*********************************");
        Console.Write("Seleccione una opción (1-6): ");
    }
    static void RegistrarEmpleado()
    {
        Console.Clear();
        Console.WriteLine("*********************************");
        Console.WriteLine("   REGISTRAR NUEVO EMPLEADO   ");
        Console.WriteLine("*********************************");

        Console.Write("\nIngrese el nombre del empleado: ");
        string nombre = Console.ReadLine();

        Console.Write("Ingrese el salario del empleado: ");
        double salario;
        while (!double.TryParse(Console.ReadLine(), out salario) || salario < 0)
        {
            Console.WriteLine("Salario no válido. Inténtelo de nuevo.");
            Console.Write("Ingrese el salario del empleado: ");
        }

        // Mostrar la lista de departamentos disponibles
        List<string> departamentos = ObtenerDepartamentos();
        MostrarListaNumerada(departamentos);

        Console.Write("\nSeleccione el departamento (1-6): ");
        if (int.TryParse(Console.ReadLine(), out int opcion) && opcion >= 1 && opcion <= departamentos.Count)
        {
            string departamento = departamentos[opcion - 1];

            empleados.Add(new Empleado(nombre, salario, departamento));
            Console.WriteLine("Empleado registrado con éxito.");
        }
        else
        {
            Console.WriteLine("Opción no válida. El empleado no ha sido registrado.");
        }

        Console.WriteLine("*********************************");
        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    static void MostrarEmpleadosPorDepartamento()
    {
        Console.Clear();
        Console.WriteLine("*********************************");
        Console.WriteLine("     MOSTRAR EMPLEADOS POR DEPARTAMENTO     ");
        Console.WriteLine("*********************************");

        // Mostrar la lista de departamentos disponibles
        List<string> departamentos = ObtenerDepartamentos();
        MostrarListaNumerada(departamentos);

        Console.Write("\nSeleccione un departamento (1-6): ");

        if (int.TryParse(Console.ReadLine(), out int opcion) && opcion >= 1 && opcion <= departamentos.Count)
        {
            string departamentoSeleccionado = departamentos[opcion - 1];

            Console.WriteLine($"\nEmpleados en el departamento de {departamentoSeleccionado}:");
            bool encontrados = false;

            foreach (var empleado in empleados)
            {
                if (empleado.Departamento.Equals(departamentoSeleccionado, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Nombre: {empleado.Nombre}, Salario: {empleado.Salario}");
                    encontrados = true;
                }
            }

            if (!encontrados)
            {
                Console.WriteLine($"No se encontraron empleados en el departamento de {departamentoSeleccionado}.");
            }
        }
        else
        {
            Console.WriteLine("Opción no válida. Inténtelo de nuevo.");
        }

        Console.WriteLine("*********************************");
        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }

    static void MostrarListaNumerada(List<string> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }
    }

    static List<string> ObtenerDepartamentos()
    {
        // Puedes modificar esta lista según tus necesidades
        return new List<string>
    {
        "Recursos Humanos",
        "Tecnología de la Información",
        "Contabilidad",
        "Mantenimiento",
        "Prevención Industrial",
        "Gestión de Calidad"
    };
    }
    

    static void MostrarTodosLosEmpleados()
    {
        Console.WriteLine("\nTodos los empleados:");
        foreach (var empleado in empleados)
        {
            Console.WriteLine($"Nombre: {empleado.Nombre}, Salario: {empleado.Salario}, Departamento: {empleado.Departamento}");
        }
    }

    static void GuardarEmpleadosEnArchivo()
    {
        using (StreamWriter writer = new StreamWriter(nombreArchivo))
        {
            foreach (var empleado in empleados)
            {
                writer.WriteLine($"{empleado.Nombre},{empleado.Salario},{empleado.Departamento}");
            }
        }
        Console.WriteLine("Empleados guardados en el archivo.");
    }

    static void CargarEmpleadosDesdeArchivo()
    {
        if (File.Exists(nombreArchivo))
        {
            empleados.Clear(); // Limpiar la lista antes de cargar nuevos datos
            using (StreamReader reader = new StreamReader(nombreArchivo))
            {
                while (!reader.EndOfStream)
                {
                    string[] campos = reader.ReadLine().Split(',');
                    if (campos.Length == 3)
                    {
                        string nombre = campos[0];
                        double salario = double.Parse(campos[1]);
                        string departamento = campos[2];

                        empleados.Add(new Empleado(nombre, salario, departamento));
                    }
                }
            }
            Console.WriteLine("Empleados cargados desde el archivo.");
        }
        else
        {
            Console.WriteLine("El archivo de empleados no existe.");
        }
    }

    static void Main()
    {
        CargarEmpleadosDesdeArchivo();

        while (true)
        {
            MostrarMenu();
            Console.Write("Seleccione una opción (1-6): ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    RegistrarEmpleado();
                    break;
                case "2":
                    MostrarEmpleadosPorDepartamento();
                    break;
                case "3":
                    MostrarTodosLosEmpleados();
                    break;
                case "4":
                    GuardarEmpleadosEnArchivo();
                    break;
                case "5":
                    CargarEmpleadosDesdeArchivo();
                    break;
                case "6":
                    Console.WriteLine("Saliendo del programa. ¡Hasta luego!");
                    return;
                default:
                    Console.WriteLine("Opción no válida. Inténtelo de nuevo.");
                    break;
            }
        }
    }
}

class Empleado
{
    public string Nombre { get; set; }
    public double Salario { get; set; }
    public string Departamento { get; set; }

    public Empleado(string nombre, double salario, string departamento)
    {
        Nombre = nombre;
        Salario = salario;
        Departamento = departamento;
    }
}
