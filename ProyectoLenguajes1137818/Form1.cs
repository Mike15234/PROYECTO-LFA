using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoLenguajes1137818
{
    public partial class Proyecto : Form
    {
        string EXPCOMP = "";//AQUI SE ALMACENARÁ LA EXPRESION REGULAR EN BASE A LOS TOKENS
        int linea = 1;  //Linea donde se encuentra el analizador
        Dictionary<int, String> Tokens;//ALMACENARÁ LAS EXPRESIONES POSIBLES
        string First = "";
        string Last = "";
        string Follow = "";
        DFA dFA = new DFA();

        public static List<char> oper = new List<char>();//LISTA DE OPERADORES
        
        public static List<char> TOKENSL = new List<char>();//LISTA DE TOKENS

        public Proyecto()
        {
            InitializeComponent();
            

        }
        int errores = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "rich text box(*.txt) | *.txt";
            openFileDialog1.FileName = "";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.InitialDirectory = "Escritorio";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] Direccion = openFileDialog1.FileName.Split('\\');
                string[] Nombre = Direccion[Direccion.Length - 1].Split('.');
                label1.Text = Nombre[0];
                richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            linea = 1;
            label1.Text = "";
            richTextBox1.Text = "";
            data.Rows.Clear();
            dataF.Rows.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string cadena = richTextBox1.Text;
            AnalizadorLexico(cadena);

        }


        private void AnalizadorLexico(string cadena)
        {
            List<int> IDS = new List<int>();//AQUI SE ALMACENARÁN LOS IDs YA UTILIZADOS
            List<int> digitoss = new List<int>();//LISTA QUE SE UTILIZARÁ PARA VALIDAR LOS DIGITOS DE LOS TOKENS
            Tokens = new Dictionary<int, string>();
            for (int i = 0; i < 10; i++)
            {
                digitoss.Add(i);
            }

            linea = 1;
            string expresion = "";//Expresion a enviar al arbol
            string id = "";//Exprecion no...
            int abiertos = 0;//Parentesis o llaves abiertas...
            int cerrados = 0;//Parentesis o llaves cerradas...
            int estadoinicial = 0;  //Es el estado 0 de los automatas
            int actual = 0;   //Estado del automata en el que se encuentra
            string concatenar;  //Une las palabras para analizarlas
            string nueva="";  //Se envia para analizar
            ExpRegular.LlenarOp(oper);
            //Arbol.FillInDictionaryHierarchy(oper);
            ExpRegular.GenerarL(oper, TOKENSL, EXPCOMP);//var treeTOKENS = Arbol.GenerarArbol(TOKENSL, oper, EXPCOMP);

            for (estadoinicial = 0; estadoinicial < cadena.Length; estadoinicial++)
            {
               
                concatenar = cadena[estadoinicial].ToString();

                switch (actual)
                {
                    case 0:
                        First = "";
                        Last = "";
                        Follow = "";
                        switch (concatenar)
                        {
                            case " ":
                                if (nueva == " ")
                                {
                                    actual = 0;
                                }
                                else if (nueva == "TOKEN")
                                {
                                    nueva += " ";
                                    actual = 50;
                                }
                                else
                                {
                                    actual = 1000;
                                    estadoinicial--;
                                    nueva = "";
                                }
                                break;

                            case "\r":
                            case "\t":
                            case "\b":
                            case "\f":
                            case "-":
                            case "\"":
                            case "_":
                            case "=":
                            case "*":
                            case "+":
                            case "":
                                break;

                            case ".":
                                if (cadena[estadoinicial - 1].ToString() == "." || cadena[estadoinicial+1].ToString()==".")
                                {
                                }
                                else
                                {
                                    ErrorEncontrado("Falta un punto", linea);
                                }
                                break;
                            case "\n":
                                linea++;
                                break;
                            case "'":
                                if (nueva!="")
                                {
                                    actual = 1000;
                                    estadoinicial--; 
                                }
                                break;

                            case "{":
                            case "}":
                                break;

                            case "(":
                                abiertos++;
                                break;
                            case ")":
                                cerrados++;
                                break;

                            case "S":
                                actual = 1;
                                nueva += concatenar;
                                First = concatenar;
                                break;
                            case "L":
                                actual = 2;
                                nueva += concatenar;
                                First = concatenar;
                                break;
                            case "T":
                                actual = 3;
                                nueva += concatenar;
                                First = concatenar;
                                break;
                            case "D":
                                actual = 4;
                                nueva += concatenar;
                                First = concatenar;
                                break;
                            case "C":
                                actual = 5;
                                nueva += concatenar;
                                First = concatenar;
                                break;

                            case "A":
                                actual = 7;
                                nueva += concatenar;
                                First = concatenar;
                                break;

                            case "P":
                                actual = 8;
                                First = concatenar;
                                nueva += concatenar;

                                break;

                            case "R":
                                actual = 9;
                                First = concatenar;
                                nueva += concatenar;
                                break;
                            case "I":
                                actual = 10;
                                nueva += concatenar;
                                First = concatenar;
                                break;

                            case "V":
                                actual = 13;
                                nueva += concatenar;
                                First = concatenar;
                                break;
                            case "E":
                                actual = 14;
                                nueva+=concatenar;
                                First = concatenar;
                                break;
                            default://Por si encuentra algo nuevo como digitos o nombres no establecidos....
                                if (cadena[estadoinicial + 1].ToString() != " ")
                                {
                                    nueva += concatenar;
                                }
                                else
                                {
                                    nueva += concatenar;
                                    actual = 1000;
                                    estadoinicial--;
                                }
                                break;
                        }
                        break;



                    case 1:   //SETS
                        if (concatenar=="E")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar+",";
                            }
                            
                        }
                        else if (concatenar=="T")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "S")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                            actual = 1000;
                            estadoinicial--;
                            Last = concatenar;
                        }
                        else if (concatenar != " ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                                Last = concatenar;
                            }
                            else
                            {
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                                Last = concatenar;
                            }

                        }
                        break;

                    case 2: ///LETRA
                        if (concatenar == "E")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "T")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar == "R")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar=="A")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                            actual = 1000;
                            estadoinicial--;
                            Last = concatenar;
                        }
                        else if (concatenar != " ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                nueva += concatenar;
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                                Last = concatenar;
                            }

                        }
                        break;
                    case 3: //TOKEN o TOKENS
                        if (concatenar == "O")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "Y")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                            actual = 12;
                        }
                        else if (concatenar == "K")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "E")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "N")
                        {
                            if (cadena[estadoinicial + 1].ToString()!=" ")
                            {
                                nueva += concatenar;
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva +=concatenar;
                                actual = 1001;
                                estadoinicial--;
                            }
                            
                        }
                        else if (concatenar=="S")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            nueva += concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar != " ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                            }

                        }
                        break;

                    case 4: //DIGITO
                        if (concatenar == "I")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "G")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar=="T")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar=="O")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            nueva += concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar == "'")
                        {
                            actual = 1000;
                            estadoinicial--;
                        }

                        else if (concatenar!=" ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                            }

                        }

                        break;
                    case 5: //CHARSET o CHR(..)

                        if (concatenar == "H")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar=="A")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "R")
                        {
                            if (cadena[estadoinicial+1].ToString()=="(")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                actual = 6;
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            
                        }
                        else if (concatenar == "S")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "E")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "T")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            nueva += concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar == "O")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                            actual = 11;
                        }
                        else if (concatenar != " ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                            }

                        }
                        break;

                    case 6://Completacion del CHR(..)
                        if (concatenar == "(")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                            abiertos++;
                        }
                        else if (concatenar==")")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            nueva += concatenar;
                            actual = 1000;
                            estadoinicial--;
                            cerrados++;
                        }

                        else if (concatenar != " ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                            }

                        }
                        break;

                    case 7://ACTIONS
                        if (concatenar == "C")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar=="T")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar=="I")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "O")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "N")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar=="S")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            nueva += concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar=="'")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar != " ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                            }

                        }
                        break;
                    case 8://Program
                        if (concatenar=="R")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "O")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "G")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "A")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar == "M")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            nueva += concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar != " ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                            }

                        }
                        break;

                    case 9: //Reservadas    
                        if (concatenar == "E")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (concatenar=="S")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar=="R")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if(concatenar=="V")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar=="A")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar=="D")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar=="'")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar == "(")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                            abiertos++;
                        }
                        else if (concatenar == ")")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            cerrados++;
                            nueva += concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        break;
                    case 10://INCLUDE
                        if (concatenar=="N")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar == "C")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar == "L")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar == "U")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar == "D")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar == "E")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            nueva += concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar == "'")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar != " ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                            }

                        }
                        break;
                    case 11://CONST
                        if (concatenar=="O")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar == "N")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar == "S")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar == "T")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            actual = 1000;
                            estadoinicial--;
                            nueva += concatenar;
                        }
                        else if (concatenar != " ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                            }

                        }
                        break;

                    case 12://TYPE
                        if (concatenar=="P")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar=="E")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            nueva += concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar != " ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                            }

                        }
                        break;

                    case 13://VAR
                        if (concatenar == "A")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar == "R")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            nueva += concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar == "'")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = concatenar;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar != " ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                            }

                        }
                        break;


                    case 14://ERROR
                        if (concatenar=="O")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            nueva += concatenar;
                        }
                        else if (concatenar=="R")
                        {
                            if (cadena[estadoinicial + 1].ToString() != "")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                            }
                        }
                        else if (concatenar=="=")
                        {
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                            Last = Follow;
                            actual = 1000;
                            estadoinicial--;
                        }
                        else if (concatenar != " ")
                        {
                            if (cadena[estadoinicial + 1].ToString() != " ")
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                nueva += concatenar;
                            }
                            else
                            {
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                                Last = concatenar;
                                nueva += concatenar;
                                actual = 1000;
                                estadoinicial--;
                            }

                        }
                        break;


                    case 50:
                        if (concatenar == " ")
                        {
                            //NADA
                        }
                        else if (concatenar == "=")
                        {
                            actual = 150;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (digitoss.Contains(Int16.Parse(concatenar)))
                        {
                            id += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        else if (cadena[estadoinicial+1].ToString() !=" ")
                        {
                            nueva += concatenar;
                            if (!Follow.Contains(concatenar))
                            {
                                Follow += concatenar + ",";
                            }
                        }
                        break;


                    case 150:
                        if (concatenar == "'")
                        {
                            if ((cadena[estadoinicial-1].ToString()=="'")&& cadena[estadoinicial + 1].ToString() == "'")
                            {
                                expresion += concatenar;
                                First=concatenar;
                                actual = 175;
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                            }
                            else if (expresion!="")
                            {
                                First=expresion;
                                Enviar(expresion, id);
                                nueva = "";
                                expresion = "";
                                id = "";
                                actual = 175;
                                if (!Follow.Contains(concatenar))
                                {
                                    Follow += concatenar + ",";
                                }
                            }
                        }
                        else if (concatenar=="D")
                        {
                            First=concatenar;
                            expresion += concatenar;
                            actual = 175;
                        }
                        else if (concatenar == "L")
                        {
                            First=concatenar;
                            expresion += concatenar;
                            actual = 175;
                        }
                        else if(concatenar!=" ")
                        {
                            First=concatenar;
                            expresion += concatenar;
                            actual = 175;
                        }
                        break;

                    case 175:
                        if (concatenar != " ")
                        {
                            if (Follow.Contains(concatenar))
                            {
                                expresion += concatenar;
                            }
                            else
                            {
                                Follow += concatenar + ",";
                                expresion += concatenar;
                            }
                            
                        }
                        if ((cadena[estadoinicial - 1].ToString() == "'") && cadena[estadoinicial + 1].ToString() == "'")
                        {
                            if (Follow.Contains(concatenar))
                            {
                                expresion += concatenar;
                            }
                            else
                            {
                                Follow += concatenar + ",";
                                expresion += concatenar;
                            }
                        }
                        if (concatenar=="\n")
                        {
                            string []expp = Follow.Split(',');
                            for (int i = 0; i < expp.Length; i++)
                            {
                                if(expp[i]!=" "&&expp[i]!="\n" && expp[i] != "" && expp[i] != "\t" && expp[i] != "*" && expp[i] != "'" && expp[i] != First)
                                {
                                    Last = expp[i];
                                }
                            }
                            if (First=="\"")
                            {
                                Last = "\"";
                            }
                            else if (First == "<")
                            {
                                Last = ">,DIGITO";
                            }
                            else if (First == ">")
                            {
                                Last = "DIGITO";
                            }
                            else if (First == "L")
                            {
                                Last = "A";
                            }
                            else if (First == "+" || First == "-" )
                            {
                                Last = "DIGITO";
                            }
                            else if (First == "=")
                            {
                                Last = "DIGITO,LETRA,<,>,=,R,D,V,\",\'";
                            }
                            
                            else if(Last == ")")
                            {
                                First = "(";
                            }

                            if (First=="E")
                            {
                                Last = "O";
                            }
                            if (First == "*")
                            {
                                Enviar(expresion, id);
                                nueva = "";
                                actual = 0;
                                estadoinicial--;
                                First = "";
                                Last = "";
                                Follow = "";
                                expresion = "";
                                id = "";

                            }
                            else
                            {
                                Agregar(First, Follow, Last);
                                Enviar(expresion, id);
                                nueva = "";
                                actual = 0;
                                estadoinicial--;
                                First = "";
                                Last = "";
                                Follow = "";
                                expresion = "";
                                id = "";
                            }
                        }
                        break;
                    
                    case 1000://ACEPTACION

                        Validos(nueva, linea);
                        actual = 0;
                        nueva = "";
                        if (First!=""&&First!=" "&&First!="\n"&&First!="\t"&&First!="\r")
                        {
                            if (First=="E")
                            {
                                Last = "O";
                            }

                            Agregar(First, Follow, Last);
                        }
                        break;

                    case 1001://ESPECIAL PARA TOKENS

                        Validos(nueva, linea);
                        actual = 0;
                        break;
                }
                
            }
            if (abiertos!=cerrados)
            {
                ErrorEncontrado("No se cerró una llave o parentesis, revise su codigo", 0);
            }


        }

        private void Enviar(string Exp, string Id)
        {
            if (Tokens.ContainsKey(Int16.Parse(Id)))
            {
                ErrorEncontrado("El id ya se encuentra registrado", linea);
            }
            else
            {
                Tokens.Add(Int16.Parse(Id), Exp);
                EXPCOMP += Exp;
            }

        }

        private void Validos(string cadena,int linea)
        {
            List<String> Validos = new List<string>();
            Validos.Add("SETS");
            Validos.Add("DIGITO");
            Validos.Add("LETRA");
            Validos.Add("PROGRAM");
            Validos.Add("INCLUDE");
            Validos.Add("CONST");
            Validos.Add("CHARSET");
            Validos.Add("TOKEN");
            Validos.Add("TOKENS");
            Validos.Add("RESERVADAS()");
            Validos.Add("TYPE");
            Validos.Add("VAR");
            Validos.Add("ERROR");
            string chr = "CHR(";
            for (int i = 0; i <256; i++)
            {
                chr += i.ToString()+")";
                Validos.Add(chr);
                chr = "CHR(";
            }
            
            Validos.Add("<");
            Validos.Add(">");
            Validos.Add("|");
            Validos.Add("ACTIONS");
            Validos.Add("");

            for (int i = 0; i < 100; i++)
            {
                Validos.Add(i.ToString());
            }  //Digitos

            char letra = 'A';
            string n = "";
            while (letra <= 'Z')
            {
                Validos.Add(letra.ToString());
                n=letra.ToString().ToLower();
                Validos.Add(n);
                letra++;

            }  //Letras mayusculas

            if (!Validos.Contains(cadena))
            {
                ErrorEncontrado(cadena, linea);
                errores++;
            }
        }

        private void Agregar(string Fir, string Fol, string Las)
        {
            int nuevito = dataF.Rows.Add();
            dataF.Rows[nuevito].Cells["Fi"].Value = Fir;
            dataF.Rows[nuevito].Cells["La"].Value = Las;
            dataF.Rows[nuevito].Cells["Fo"].Value = Fol;

        }

        private void ErrorEncontrado(string cadena, int linea)
        {
            int indice = data.Rows.Add();
            data.Rows[indice].Cells["Cadena"].Value = cadena;
            data.Rows[indice].Cells["Linea"].Value = linea;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            dFA.Show();
        }

        private void Proyecto_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dFA.Close();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
