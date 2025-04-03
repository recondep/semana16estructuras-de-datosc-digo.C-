using System;
using System.Collections.Generic;

public class Vuelo
{
    public string Origen { get; set; }
    public string Destino { get; set; }
    public decimal Precio { get; set; }

    public Vuelo(string origen, string destino, decimal precio)
    {
        Origen = origen;
        Destino = destino;
        Precio = precio;
    }
}

public class ArbolVuelos
{
    private Vuelo raiz;

    // Método para insertar un vuelo en el árbol
    public void Insertar(string origen, string destino, decimal precio)
    {
        raiz = InsertarRecursivo(raiz, new Vuelo(origen, destino, precio));
    }

    private Vuelo InsertarRecursivo(Vuelo nodo, Vuelo vuelo)
    {
        if (nodo == null)
        {
            return vuelo;
        }

        if (vuelo.Precio < nodo.Precio)
        {
            nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, vuelo);
        }
        else
        {
            nodo.Derecha = InsertarRecursivo(nodo.Derecha, vuelo);
        }

        return nodo;
    }

    // Método para buscar vuelos baratos
    public List<Vuelo> BuscarVuelosBaratos(decimal maxPrecio)
    {
        List<Vuelo> resultados = new List<Vuelo>();
        BuscarRecursivo(raiz, maxPrecio, resultados);
        return resultados;
    }

    private void BuscarRecursivo(Vuelo nodo, decimal maxPrecio, List<Vuelo> resultados)
    {
        if (nodo != null)
        {
            if (nodo.Precio <= maxPrecio)
            {
                resultados.Add(nodo);
            }
            BuscarRecursivo(nodo.Izquierda, maxPrecio, resultados);
            BuscarRecursivo(nodo.Derecha, maxPrecio, resultados);
        }
    }
}

public class Grafo
{
    private Dictionary<string, List<string>> adjacencias;

    public Grafo()
    {
        adjacencias = new Dictionary<string, List<string>>();
    }

    // Método para agregar un aeropuerto
    public void AgregarVertice(string aeropuerto)
    {
        if (!adjacencias.ContainsKey(aeropuerto))
        {
            adjacencias[aeropuerto] = new List<string>();
        }
    }

    // Método para agregar una conexión entre aeropuertos
    public void AgregarArista(string origen, string destino)
    {
        AgregarVertice(origen);
        AgregarVertice(destino);
        adjacencias[origen].Add(destino);
    }

    // Método para obtener conexiones de un aeropuerto
    public List<string> ObtenerConexiones(string aeropuerto)
    {
        return adjacencias.ContainsKey(aeropuerto) ? adjacencias[aeropuerto] : new List<string>();
    }
}

class Program
{
    static void Main(string[] args)
    {
        ArbolVuelos arbolVuelos = new ArbolVuelos();
        Grafo grafo = new Grafo();

        // Ejemplo de inserción de vuelos
        arbolVuelos.Insertar("Lima", "Bogotá", 200);
        arbolVuelos.Insertar("Lima", "Santiago", 150);
        arbolVuelos.Insertar("Lima", "Buenos Aires", 300);
        grafo.AgregarArista("Lima", "Bogotá");
        grafo.AgregarArista("Lima", "Santiago");
        grafo.AgregarArista("Santiago", "Buenos Aires");

        // Búsqueda de vuelos baratos
        decimal maxPrecio = ObtenerPrecioMaximo();

        var vuelosBaratos = arbolVuelos.BuscarVuelosBaratos(maxPrecio);
        MostrarVuelos(vuelosBaratos);

        // Consulta de conexiones
        string aeropuerto = ObtenerAeropuerto();
        var conexiones = grafo.ObtenerConexiones(aeropuerto);
        MostrarConexiones(conexiones, aeropuerto);
    }

    // Método para obtener el precio máximo del usuario
    static decimal ObtenerPrecioMaximo()
    {
        decimal maxPrecio;
        Console.WriteLine("Ingrese el precio máximo para buscar vuelos:");
        while (!decimal.TryParse(Console.ReadLine(), out maxPrecio) || maxPrecio < 0)
        {
            Console.WriteLine("Por favor, ingrese un valor numérico válido para el precio máximo (debe ser mayor o igual a 0):");
        }
        return maxPrecio;
    }

    // Método para mostrar los vuelos encontrados
    static void MostrarVuelos(List<Vuelo> vuelos)
    {
        if (vuelos.Count > 0)
        {
            Console.WriteLine("Vuelos encontrados:");
            foreach (var vuelo in vuelos)
            {
                Console.WriteLine($"Origen: {vuelo.Origen}, Destino: {vuelo.Destino}, Precio: {vuelo.Precio}");
            }
        }
        else
        {
            Console.WriteLine("No se encontraron vuelos dentro del rango de precio.");
        }
    }

    // Método para obtener el aeropuerto del usuario
    static string ObtenerAeropuerto()
    {
        Console.WriteLine("Ingrese el aeropuerto para ver sus conexiones:");
        return Console.ReadLine();
    }

    // Método para mostrar las conexiones encontradas
    static void MostrarConexiones(List<string> conexiones, string aeropuerto)
    {
        if (conexiones.Count > 0)
        {
            Console.WriteLine($"Conexiones desde {aeropuerto}:");
            foreach (var conexion in conexiones)
            {
                Console.WriteLine(conexion);
            }
        }
        else
        {
            Console.WriteLine("No se encontraron conexiones para el aeropuerto ingresado.");
        }
    }
}
  


  
   
   
