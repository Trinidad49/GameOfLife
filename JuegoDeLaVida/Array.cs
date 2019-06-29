using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeLaVida
{
    class Array
    {
        private int[,,] array;
        private int activo;
        private int copia;

        //Constructor genera array 3 dim todo 0 100x100x2. 
        public Array() {
            this.activo = 0;
            this.copia = 1;
            this.array = new int[100, 100, 2];
        }

        //POSIBLE CONSTRUCTOR CON TAMAÑO CUSTOM DEL ARRAY¿?

        //Mira el numero de vecinos de una celula
        private int Vecinos(int x, int y) {
            int vecinos = 0;
            //Comprobar que no se sale del array
            bool menosX, menosY, masX, masY;
            if (x - 1 >= 0)
            {
                menosX = true;
            }
            else {
                menosX = false;
            }
            if (y - 1 >= 0)
            {
                menosY = true;
            }
            else
            {
                menosY = false;
            }
            if (x + 1 < array.GetLength(0))
            {
                masX = true;
            }
            else {
                masX = false;
            }
            if (y + 1 < array.GetLength(1))
            {
                masY = true;
            }
            else
            {
                masY = false;
            }

            //Sumar 1 a vecinos si el vecino esta vivo
            if ((menosX == true) && (menosY == true)) {
                if (array[x - 1, y - 1, activo] == 1)
                {
                    vecinos += 1;
                }
            }
            if (menosY == true)
            {
                if (array[x, y - 1, activo] == 1)
                {
                    vecinos += 1;
                }
            }
            if ((masX == true) && (menosY == true))
            {
                if (array[x + 1, y - 1, activo] == 1)
                {
                    vecinos += 1;
                }
            }
            if (menosX == true)
            {
                if (array[x - 1, y, activo] == 1)
                {
                    vecinos += 1;
                }
            }
            if (masX == true)
            {
                if (array[x + 1, y , activo] == 1)
                {
                    vecinos += 1;
                }
            }
            if ((menosX == true) && (masY == true))
            {
                if (array[x - 1, y + 1, activo] == 1)
                {
                    vecinos += 1;
                }
            }
            if (masY == true)
            {
                if (array[x, y + 1, activo] == 1)
                {
                    vecinos += 1;
                }
            }
            if ((masX == true) && (masY == true))
            {
                if (array[x + 1, y + 1, activo] == 1)
                {
                    vecinos += 1;
                }
            }

            return vecinos;
        }

        //Ciclo para comprobar si cada celula muere/vive etc
        public void Ciclo() {
            for (int i = 0; i < array.GetLength(0); i++) {
                for (int j = 0; j < array.GetLength(1); j++) {
                    int vecinitos = Vecinos(i, j);
                    if ((array[i, j, activo] == 1) && (vecinitos == 2 || vecinitos == 3))
                    {
                        array[i, j, copia] = 1;
                    }
                    else if (array[i, j, activo] == 1)
                    {
                        array[i, j, copia] = 0;
                    }
                    else if ((array[i, j, activo] == 0) && vecinitos == 3)
                    {
                        array[i, j, copia] = 1;
                    }
                    else {
                        array[i, j, copia] = 0;
                    }
                }
            }

            if (activo == 0)
            {
                activo = 1;
                copia = 0;
            }
            else {
                activo = 0;
                copia = 1;
            }
        }

        //Cambiar el estado en una celda
        public void CambioEstado(int x, int y) {

            if (array[x, y, activo] == 0)
            {
                array[x, y, activo] = 1;
            }
            else
            {
                array[x, y, activo] = 0;
            }
        }

        //Devuelve el valor de activo
        public int Activo() {
            return activo;
        }

        //GetLength
        public int GetLength(int i) {
            return array.GetLength(i);
        }

        //Devuelve el valor de la casilla
        public int Valor(int x, int y) {
            return array[x, y, activo];
        }

    }
}
