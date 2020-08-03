using System;
using DevOff_Desafio_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevOff_Desafio_2.Controllers
{
    public class ApiController : ControllerBase
    {
        /*
            
            ٩(^‿^)۶ ٩(^‿^)۶ ٩(^‿^)۶ ٩(^‿^)۶ ٩(^‿^)۶ ٩(^‿^)۶

            ahora a mejorar un poco la cosa

            listo :D

            no es lo mejor, pero safa ^ ^

        */

        [HttpPost("cifrar")]
        public IActionResult Cifrar([FromBody] CifradoRequest model)
        {
            // crea la matriz que calcula el mensaje
            string[,] m = CrearMatriz(model.Mensaje, model.Vueltas, false);

            // crea el retorno
            var retorno = new CifradoResponse
            {
                Mensaje = ConvertirMatrizMensaje(m, true)
            };

            return Ok(retorno);
        }


        [HttpPost("descifrar")]
        public IActionResult Descifrar([FromBody] CifradoRequest model)
        {
            // crea la matriz que calcula el mensaje
            string[,] m = CrearMatriz(model.Mensaje, model.Vueltas, true);

            // crea el retorno
            var retorno = new CifradoResponse
            {
                Mensaje = ConvertirMatrizMensaje(m, false).Trim()
            };

            return Ok(retorno);
        }

        private string ConvertirMatrizMensaje(string[,] m, bool invertir)
        {
            string nuevoMensaje = "";


            if (!invertir)
            {
                int filas = m.GetLength(0);
                int columnas = m.GetLength(1);
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        nuevoMensaje += m[i, j];
                    }
                }

            }
            else
            {
                int filas = m.GetLength(0);
                int columnas = m.GetLength(1);
                for (int i = 0; i < columnas; i++)
                {
                    for (int j = 0; j < filas; j++)
                    {
                        nuevoMensaje += m[j, i];
                    }
                }
            }

            return nuevoMensaje;
        }

        private string[,] CrearMatriz(string mensaje, int vueltas, bool invertir)
        {
            // columnas = cantidad de vueltas 
            int columnas = vueltas;

            // calcula las filas en base a la cantidad de letras del mensaje
            int filas = Convert.ToInt32(Math.Ceiling((double)mensaje.Length / (double)columnas));

            // crea la matriz en base a estos numeros
            string[,] m = new string[filas, columnas];


            if (!invertir)
            {
                // recorre la matriz para completar con el mensaje
                int aux = 0; // el indice de recorrido del mensaje;
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        if (aux < mensaje.Length)
                        {
                            m[i, j] = mensaje.Substring(aux, 1);
                            aux++;
                        }
                        else
                        {
                            m[i, j] = " ";
                        }
                    }
                }
            }
            else
            {
                int aux = 0; // el indice de recorrido del mensaje;
                for (int i = 0; i < columnas; i++)
                {
                    for (int j = 0; j < filas; j++)
                    {
                        if (aux < mensaje.Length)
                        {
                            m[j, i] = mensaje.Substring(aux, 1);
                            aux++;
                        }
                        else
                        {
                            m[j, i] = " ";
                        }
                    }
                }
            }

            return m;
        }
    }
}