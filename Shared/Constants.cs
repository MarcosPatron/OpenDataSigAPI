using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Constants
    {
        public class TiposErrores
        {
            public const string NO_EXISTE_USUARIO = "No existe el usuario en BBDD";
            public const string NO_EXISTE_HILO = "No existe la conversación en BBDD";
            public const string MENSAJE_CLIENTE_VACIO = "El mensaje del cliente está vacío";
            public const string USUARIO_CLIENTE_VACIO = "El usuario del cliente está vacío";
            public const string CONVERSACION_ESTADO_ERRONEO = "La conversación se encuentra en un estado erróneo.";
        }

        //OPERACIONES DE BASE DE DATOS
        public class Operaciones
        {
            public const string UPDATE = "Update";
            public const string SOFT_DELETE = "Soft Delete";
            public const string REACTIVATE = "Reactivate";
        }

        public class TiposLogs
        {
            public const string FATAL = "FATAL";
            public const string ERROR = "ERROR";
            public const string INFO = "INFO";
            public const string WARNING = "WARN";
        }
    }
}
