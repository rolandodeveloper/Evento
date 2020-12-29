using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EventoRegistro
{
    class clsCliente
    {
        public clsCliente(int argCodigo,string argNombres,string argApePaterno,string argApeMaterno,char argSexo,string argEstadoCivil,
            char argTipo,DateTime argFechaNacimiento,short argPuntaje,byte argCantidadHijos,bool argRecomendado)
        {
            codigo = argCodigo;nombres = argNombres;apellidoPaterno = argApePaterno;apellidoMaterno = argApeMaterno;
            sexo = argSexo;estadoCivil = argEstadoCivil;tipo = argTipo;fechaNacimiento = argFechaNacimiento;
            puntaje = argPuntaje;cantidadHijos = argCantidadHijos;esRecomendado = argRecomendado;
        }

        private int _codigo;

        public int codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        private string _nombres;

        public string nombres
        {
            get { return _nombres; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(null,"El nombre del cliente no puede quedar vacío.");
                }
                else if (value.Trim().Length<3)
                {
                    throw new ArgumentOutOfRangeException(null,"El nombre del cliente debe tener al menos 3 caracteres.");
                }
                else if (value.Trim().Length>50)
                {
                    throw new ArgumentOutOfRangeException(null,"El nombre del cliente debe tener 50 caracteres como máximo.");
                }
                _nombres =  CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value);
            }
        }

        private string _apellidoPaterno;

        public string apellidoPaterno
        {
            get { return _apellidoPaterno; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(null, "El apellido paterno no puede quedar vacío.");
                }
                else if (value.Trim().Length < 3)
                {
                    throw new ArgumentOutOfRangeException(null, "El apellido paterno debe tener al menos 3 caracteres.");
                }
                else if (value.Trim().Length > 40)
                {
                    throw new ArgumentOutOfRangeException(null, "El apellido paterno debe tener 40 caracteres como máximo.");
                }                
                _apellidoPaterno = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value);
            }
        }

        private string _apellidoMaterno;

        public string apellidoMaterno
        {
            get { return _apellidoMaterno; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(null, "El apellido materno no puede quedar vacío.");
                }
                else if (value.Trim().Length < 3)
                {
                    throw new ArgumentOutOfRangeException(null, "El apellido materno debe tener al menos 3 caracteres.");
                }
                else if (value.Trim().Length > 40)
                {
                    throw new ArgumentOutOfRangeException(null, "El apellido materno debe tener 40 caracteres como máximo.");
                }
                _apellidoMaterno = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value);
            }
        }

        private char _sexo;

        public char sexo
        {
            get { return _sexo; }
            set 
            {
                if (char.IsWhiteSpace(value))
                {
                    throw new ArgumentNullException(null, "El sexo no puede quedar vacío.");
                }
                else if (value!='M' && value != 'F')
                {
                    throw new ArgumentOutOfRangeException(null, "El sexo debe ser Masculino o Femenino.");
                }
                _sexo = value; 
            }
        }

        private string _estadoCivil;

        public string estadoCivil
        {
            get { return _estadoCivil; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(null, "El estado civil no puede quedar vacío.");
                }
                else if (value.Trim().ToUpper() !="SOLTERO" && value.Trim().ToUpper() != "CASADO" && value.Trim().ToUpper() != "CONVIVIENTE")
                {
                    throw new ArgumentOutOfRangeException(null,"El estado civil debe ser soltero, casado o conviviente.");
                }
                _estadoCivil = value.Trim().ToUpper().Substring(0, 3);
            }
        }

        private char _tipo;

        public char tipo
        {
            get { return _tipo; }
            set 
            {
                if (char.IsWhiteSpace(value))
                {
                    throw new ArgumentNullException(null, "El tipo de cliente no puede quedar vacío.");
                }
                else if (value != 'O' && value != 'P' && value != 'S')
                {
                    throw new ArgumentOutOfRangeException(null, "El tipo de cliente debe ser oro, plata o estándar.");
                }
                _tipo = value; 
            }
        }

        private string _correoElectronico;

        public string correoElectronico
        {
            get { return _correoElectronico; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    _correoElectronico = null;                    
                }
                else if (value.Trim().Length < 5)
                {
                    throw new ArgumentOutOfRangeException(null, "El correo electrónico debe tener al menos 5 caracteres.");
                }
                else if (value.Trim().Length > 70)
                {
                    throw new ArgumentOutOfRangeException(null, "El correo electrónico debe tener 70 caracteres como máximo.");
                }
                _correoElectronico = value.Trim().ToLower(); 
            }
        }

        private DateTime _fechaNacimiento;

        public DateTime fechaNacimiento
        {
            get { return _fechaNacimiento; }
            set 
            {                
                if (value> new DateTime(System.DateTime.Now.AddYears(-25).Year,
                                            System.DateTime.Now.Month,
                                            System.DateTime.Now.Day))
                {
                    throw new ArgumentOutOfRangeException(null, "La fecha de nacimiento es inválida. \n" + "Solo admiten clientes con 25 años cumplidos al 30 de septiembre.");
                }                
                _fechaNacimiento = value; 
            }
        }

        private short _puntaje;

        public short puntaje
        {
            get { return _puntaje; }
            set 
            {
                if (value<50)
                {
                    throw new ArgumentOutOfRangeException(null,"El puntaje mínimo es 50 puntos.");
                }
                _puntaje = value; 
            }
        }

        private byte _cantidadHijos;

        public byte cantidadHijos
        {
            get { return _cantidadHijos; }
            set { _cantidadHijos = value; }
        }

        private bool _esRecomendado;

        public bool esRecomendado
        {
            get { return _esRecomendado; }
            set { _esRecomendado = value; }
        }

        public void Insertar()
        {            
            MySqlConnection cn = new MySqlConnection("Server=tuServidor;Port=1234;Database=EventoClientes;Uid=root;Pwd=passw;");
            MySqlCommand cmd = new MySqlCommand("usp_Cliente_Insertar", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("parNombres",nombres);
            cmd.Parameters.AddWithValue("parApellidoPaterno",apellidoPaterno);
            cmd.Parameters.AddWithValue("parApellidoMaterno",apellidoMaterno);
            cmd.Parameters.AddWithValue("parSexo",sexo);
            cmd.Parameters.AddWithValue("parEstadoCivil",estadoCivil);
            cmd.Parameters.AddWithValue("parTipo",tipo);
            if (string.IsNullOrEmpty(correoElectronico))
                cmd.Parameters.AddWithValue("parCorreo", DBNull.Value);            
            else
                cmd.Parameters.AddWithValue("parCorreo", correoElectronico);
            cmd.Parameters.AddWithValue("parFechaNacimiento",fechaNacimiento);
            cmd.Parameters.AddWithValue("parPuntaje",puntaje);
            cmd.Parameters.AddWithValue("parCantidadHijos",cantidadHijos);
            cmd.Parameters.AddWithValue("parEsRecomendado",esRecomendado);            
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();cn.Dispose();                 
        }
    }
}
