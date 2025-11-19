using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class clsPonente_CN
    {
        private clsPonente_CD objPonente_CD = new clsPonente_CD();

        public int mtdInsertar(clsPonente_CE obj, out string mensaje)
        {
            return objPonente_CD.mtdInsertar(obj, out mensaje);
        }

        public DataTable mtdListar()
        {
            return objPonente_CD.mtdListar();
        }

        public bool mtdActualizar(clsPonente_CE obj, out string mensaje)
        {
            return objPonente_CD.mtdActualizar(obj, out mensaje);
        }

        public bool mtdEliminar(int idPonente, out string mensaje)
        {
            return objPonente_CD.mtdEliminar(idPonente, out mensaje);
        }


    }
}
