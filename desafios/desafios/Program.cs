/*
 * Created by SharpDevelop.
 * User: josel
 * Date: 18/04/2026
 * Time: 15:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace TallerIngenieria
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("===Desafio 1===");
			string entradaUsuario = "Andrea_Arismendi;123";
			EjecutarDesafio1(entradaUsuario);
			
			Console.WriteLine("\n===Desafio 2===");
			
			ClonarImagen();
			
			Console.WriteLine("\n===Desafio 3===");
			string rutaCarpeta = AppDomain.CurrentDomain.BaseDirectory;
			LimpiarArchivosPesados(rutaCarpeta);
			
			Console.WriteLine("\n-------------------------------------------");
			Console.WriteLine("Presiona cualquier tecla para salir...");
			Console.ReadKey();
		}

		public static void EjecutarDesafio1(string entrada)
		{
			char[] separadores = new char[] { ';' };
			string[] partes = entrada.Split(separadores);
			
			if (partes.Length >= 2)
			{
				string clave = partes[1];
				if (clave.Contains("123"))
				{
					using (StreamWriter sw = new StreamWriter("seguridad.txt", true))
					{
						sw.WriteLine("Clave Débil detectada: " + DateTime.Now.ToString());
					}
					Console.WriteLine("- Alerta: Clave debil guardada en seguridad.txt");
				}
			}
		}

		public static void ClonarImagen()
		{
			string origen = "AVATAR.jpg";
			string destino = "respaldo.jpg";

			if (File.Exists(origen))
			{
				try
				{
					using (FileStream fsOrigen = new FileStream(origen, FileMode.Open, FileAccess.Read))
					using (FileStream fsDestino = new FileStream(destino, FileMode.Create, FileAccess.Write))
					{
						byte[] buffer = new byte[1024];
						int bytesLeidos;

						while ((bytesLeidos = fsOrigen.Read(buffer, 0, buffer.Length)) > 0)
						{
							fsDestino.Write(buffer, 0, bytesLeidos);
						}
					}
					Console.WriteLine("- Imagen clonada exitosamente como 'respaldo.jpg'");
				}
				catch (Exception ex)
				{
					Console.WriteLine("- Error al clonar: " + ex.Message);
				}
			}
			else
			{
				Console.WriteLine("- Aviso: No se encontro 'avatar.jpg' para clonar.");
			}
		}

		public static void LimpiarArchivosPesados(string ruta)
		{
			if (Directory.Exists(ruta))
			{
				string[] archivos = Directory.GetFiles(ruta);

				foreach (string archivo in archivos)
				{
					FileInfo info = new FileInfo(archivo);

					if (info.Length > 5120)
					{
						if (info.Extension != ".exe" && info.Name != "seguridad.txt")
						{
							try
							{
								string nombreFichero = info.Name;
								info.Delete();
								Console.WriteLine("- Borrado por pesado (>5KB): " + nombreFichero);
							}
							catch (Exception ex)
							{
								Console.WriteLine("-No se pudo borrar " + info.Name + ": " + ex.Message);
							}
						}
					}
				}
			}
		}
	}
}