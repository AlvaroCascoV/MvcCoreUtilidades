using System.Security.Cryptography;
using System.Text;

namespace MvcCoreUtilidades.Helpers
{
    public class HelperCryptography
    {
        //CREAMOS LOS METODOS DE TIPO STATIC
        //SIMPLEMENTE DEVOLVEMOS UN TEXTO CIFRADO SIMPLE
        public static string EncriptarTextoBasico(string contenido)
        {
            //EL CIFRADO SE REALIZA A NIVEL DE BYTES
            //DEBEMOS CONVERTIR EL TEXTO DE ENTRADA A BYTES[]
            byte[] entrada;
            //DESPUES DE CIFRAR LOS BYTES, NOS DARA UNA SALIDA DE BYTES[]
            byte[] salida;
            //NECESITAMOS UNA CLASE PARA CONVERTIR DE BYTE[] A STRING Y VICEVERSA
            UnicodeEncoding encoding = new UnicodeEncoding();
            //NECESITAMOS UN OBJETO PARA CIFRAR EL CONTENIDO
            SHA1 managed = SHA1.Create();
            //CONVERTIMMOS EL TEXTO DE ENTRADA A BYTES[]
            entrada = encoding.GetBytes(contenido);
            //LOS OBJETOS DE CIFRADO TIENEN UN METODO LLAMADO
            //ComputeHash() QUE RECIBEN UN ARRAY DE BYTES, REALIZAN
            //ACCIONES INTERNAS Y DEVUELVEN EN ARRAY CON SU CONTENIDO
            salida = managed.ComputeHash(entrada);
            //CONVERTIMOS LOS BYTES A TEXTO
            string resultado = encoding.GetString(salida);
            return resultado;
        }
    }
}
