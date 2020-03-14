using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLenguajes1137818
{
    class ExpRegular
    {
        public static void LlenarOp(List<char> op)
        {
            op.Add('*');
            op.Add('+');
            op.Add('|');
            op.Add('?');
            op.Add('(');
            op.Add(')');
            op.Add('.');
        }
        public static void GenerarL(List<char> op, List<char> kind, string regEx)
        {
            for (int i = 0; i < regEx.Length; i++)
            {
                if (!op.Contains(regEx[i]) && regEx[i] != '\\' && !kind.Contains(regEx[i]))
                {
                    kind.Add(regEx[i]);
                }
                if (i - 1 > 0)
                {
                    if (regEx[i - 1] == '\\' && !kind.Contains(regEx[i]))
                    {
                        kind.Add(regEx[i]);
                    }
                }
            }
        }
    }
}
